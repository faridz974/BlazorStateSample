using BlazorState;
using BlazorStateSample.Common;
using BlazorStateSample.Features.Base;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorStateSample.Features.Client
{
    public partial class ClientState
    {
        public class SearchClientHandler : BaseHandler<SearchClientAction>
        {
            private readonly IMediator _mediator;

            public SearchClientHandler(IStore aStore, IMediator mediator) : base(aStore)
            {
                _mediator = mediator;
            }

            public override Task<Unit> Handle(SearchClientAction aAction, CancellationToken aCancellationToken)
            {
                ClientState.Status = LoadingStatus.Loading;
                ClientState.SelectedClientId = aAction.ClientId;
                _ = _mediator.Send(new FetchClientAction(), aCancellationToken);
                return Task.FromResult(Unit.Value);
            }
        }
    }
}
