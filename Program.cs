using BlazorState;
using FluentValidation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorStateSample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddBlazorState(
                (aOptions) =>
                {
                    //aOptions.UseReduxDevToolsBehavior = true;
                    aOptions.Assemblies = new[]
                    {
                        typeof(Program).GetTypeInfo().Assembly

                    };
                });

            await builder.Build().RunAsync();
        }
    }
}
