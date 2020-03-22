using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using BlazorStateSample.Common;
using BlazorStateSample.Features.Base;
using MediatR;

namespace BlazorStateSample.Features.Client
{
    public partial class ClientState
    {
        public class FetchClientHandler : BaseHandler<FetchClientAction>
        {
            public FetchClientHandler(IStore aStore) : base(aStore)
            {
            }

            public override async Task<Unit> Handle(FetchClientAction aAction, CancellationToken aCancellationToken)
            {
                try
                {
                    // Fake handler on server
                    await Task.Delay(2000, aCancellationToken);
                    ClientState.SelectedClient = new ClientDto
                    {
                        Id = ClientState.SelectedClientId,
                        LastName = "Bond",
                        Name = "James"
                    };
                    ClientState.Status = LoadingStatus.Loaded;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                    ClientState.Status = LoadingStatus.Error;
                }

                return Unit.Value;
            }
        }
    }
}
