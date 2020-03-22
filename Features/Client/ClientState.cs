using BlazorState;
using BlazorStateSample.Common;

namespace BlazorStateSample.Features.Client
{
    public partial class ClientState : State<ClientState>
    {
        public LoadingStatus Status { get; private set; }
        public int SelectedClientId { get; private set; }
        public ClientDto? SelectedClient { get; private set; }

        public override void Initialize()
        {
            Status = LoadingStatus.Idle;
        }
    }
}
