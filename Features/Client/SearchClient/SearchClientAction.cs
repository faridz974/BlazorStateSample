using BlazorState;

namespace BlazorStateSample.Features.Client
{
    public partial class ClientState
    {
        public class SearchClientAction : IAction
        {
            public SearchClientAction(int clientId)
            {
                ClientId = clientId;
            }
            public int ClientId { get; }
        }
    }
}
