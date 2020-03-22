using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorStateSample.Common.Validation
{
    public class FluentValidator<TValidator> : ComponentBase where TValidator : IValidator, new()
    {
        private readonly static char[] Separators = new[] { '.', '[' };
        private TValidator _validator;

        /// <summary>
        /// Enable access to the ASP.NET Core Service Provider / DI.
        /// </summary>
        [Inject]
        private IServiceProvider ServiceProvider { get; set; }

        [CascadingParameter]
        private EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            var scope = ServiceProvider.CreateScope();

            var validatorType = typeof(TValidator);
            _validator = (TValidator)scope.ServiceProvider.GetService(validatorType);
            var messages = new ValidationMessageStore(EditContext);

            // Revalidate when any field changes, or if the entire form requests validation
            // (e.g., on submit)

            EditContext.OnFieldChanged += (sender, eventArgs)
                => ValidateModel((EditContext)sender, messages);

            EditContext.OnValidationRequested += (sender, eventArgs)
                => ValidateModel((EditContext)sender, messages);
        }

        private void ValidateModel(EditContext editContext, ValidationMessageStore messages)
        {
            var validationResult = _validator.Validate(editContext.Model);
            messages.Clear();
            foreach (var error in validationResult.Errors)
            {
                var fieldIdentifier = ToFieldIdentifier(editContext, error.PropertyName);
                messages.Add(fieldIdentifier, error.ErrorMessage);
            }
            editContext.NotifyValidationStateChanged();
        }

        private static FieldIdentifier ToFieldIdentifier(EditContext editContext, string propertyPath)
        {
            // This method parses property paths like 'SomeProp.MyCollection[123].ChildProp'
            // and returns a FieldIdentifier which is an (instance, propName) pair. For example,
            // it would return the pair (SomeProp.MyCollection[123], "ChildProp"). It traverses
            // as far into the propertyPath as it can go until it finds any null instance.

            var obj = editContext.Model;

            while (true)
            {
                var nextTokenEnd = propertyPath.IndexOfAny(Separators);
                if (nextTokenEnd < 0)
                {
                    return new FieldIdentifier(obj, propertyPath);
                }

                var nextToken = propertyPath.Substring(0, nextTokenEnd);
                propertyPath = propertyPath.Substring(nextTokenEnd + 1);

                object newObj;
                if (nextToken.EndsWith("]"))
                {
                    // It's an indexer
                    // This code assumes C# conventions (one indexer named Item with one param)
                    nextToken = nextToken.Substring(0, nextToken.Length - 1);
                    var prop = obj.GetType().GetProperty("Item");
                    if (prop is null)
                    {
                        throw new InvalidOperationException($"Could not find indexer named Item with one param");
                    }

                    var indexerType = prop.GetIndexParameters()[0].ParameterType;
                    var indexerValue = Convert.ChangeType(nextToken, indexerType);
                    newObj = prop.GetValue(obj, new object[] { indexerValue });
                }
                else
                {
                    // It's a regular property
                    var prop = obj.GetType().GetProperty(nextToken);
                    if (prop is null)
                    {
                        throw new InvalidOperationException($"Could not find property named {nextToken} on object of type {obj.GetType().FullName}.");
                    }
                    newObj = prop.GetValue(obj);
                }

                if (newObj is null)
                {
                    // This is as far as we can go
                    return new FieldIdentifier(obj, nextToken);
                }
                if (nextToken.EndsWith("]"))
                {
                    // It's an indexer
                    // This code assumes C# conventions (one indexer named Item with one param)
                    nextToken = nextToken.Substring(0, nextToken.Length - 1);
                    var prop = obj.GetType().GetProperty("Item");
                    var indexerType = prop.GetIndexParameters()[0].ParameterType;
                    var indexerValue = Convert.ChangeType(nextToken, indexerType);
                    newObj = prop.GetValue(obj, new object[] { indexerValue });
                }
                else
                {
                    // It's a regular property
                    var prop = obj.GetType().GetProperty(nextToken);
                    if (prop == null)
                    {
                        throw new InvalidOperationException($"Could not find property named {nextToken} on object of type {obj.GetType().FullName}.");
                    }
                    newObj = prop.GetValue(obj);
                }

                if (newObj == null)
                {
                    // This is as far as we can go
                    return new FieldIdentifier(obj, nextToken);
                }

                obj = newObj;
            }
        }
    }
}
