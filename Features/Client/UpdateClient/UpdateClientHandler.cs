using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using BlazorStateSample.Features.Base;
using MediatR;

namespace BlazorStateSample.Features.Client
{
    public partial class ClientState
    {
        public class UpdateClientHandler : BaseHandler<UpdateClientAction>
        {
            public UpdateClientHandler(IStore aStore) : base(aStore)
            {
            }

            public override Task<Unit> Handle(UpdateClientAction aAction, CancellationToken aCancellationToken)
            {
                // In real world scenario, calling an api
                Console.WriteLine($"Updated client {aAction.ClientId} {aAction.Name} {aAction.LastName}");
                return Task.FromResult(Unit.Value);
            }
        }
    }
}
