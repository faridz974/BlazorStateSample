using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorState.Pipeline.ReduxDevTools;
using BlazorStateSample.Features.Application;
using BlazorStateSample.Features.Client;

namespace BlazorStateSample.Features.Base.Components
{
    public class BaseComponent : BlazorStateDevToolsComponent
    {
        internal ApplicationState ApplicationState => GetState<ApplicationState>();
        internal ClientState ClientState => GetState<ClientState>();
    }
}
