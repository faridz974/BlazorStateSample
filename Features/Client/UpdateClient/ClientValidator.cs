using BlazorStateSample.Common;
using FluentValidation;

namespace BlazorStateSample.Features.Client
{
    public class ClientValidator : AbstractValidator<ClientDto>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name mandatory");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName mandatory");

        }
    }
}