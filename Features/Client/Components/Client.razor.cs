using BlazorStateSample.Common;
using BlazorStateSample.Features.Base.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorStateSample.Features.Client.Components
{
    public partial class ClientBase : BaseComponent
    {
        [Parameter]
        public int ClientId { get; set; }
        public LoadingStatus Status => ClientState.Status;
        public ClientDto Client => ClientState.SelectedClient;

        protected override void OnInitialized()
        {
            _ = Mediator.Send(new ClientState.SearchClientAction(ClientId));
        }

        public void OnValidSubmit()
        {
            _ = Mediator.Send(new ClientState.UpdateClientAction(Client.Id, Client.LastName, Client.Name));
        }
    }
}
