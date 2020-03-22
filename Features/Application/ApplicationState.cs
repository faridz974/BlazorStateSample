using BlazorState;

namespace BlazorStateSample.Features.Application
{
    public partial class ApplicationState : State<ApplicationState>
    {
        public string Version => GetType().Assembly.GetName().Version.ToString();

        public ApplicationState()
        {
        }

        public override void Initialize()
        {
        }
    }
}
