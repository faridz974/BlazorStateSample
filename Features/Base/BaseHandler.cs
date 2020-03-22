using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorState;
using BlazorStateSample.Features.Application;
using BlazorStateSample.Features.Client;

namespace BlazorStateSample.Features.Base
{
    public abstract class BaseHandler<TAction> : ActionHandler<TAction> where TAction : IAction
    {
        protected ApplicationState ApplicationState => Store.GetState<ApplicationState>();
        protected ClientState ClientState => Store.GetState<ClientState>();

        protected BaseHandler(IStore aStore) : base(aStore)
        {
        }
    }
}
