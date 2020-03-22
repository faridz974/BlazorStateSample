using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorState;

namespace BlazorStateSample.Features.Client
{
    public partial class ClientState
    {
        public class UpdateClientAction : IAction
        {
            public UpdateClientAction(int clientId, string lastName, string name)
            {
                ClientId = clientId;
                LastName = lastName;
                Name = name;
            }
            public int ClientId { get;  }
            public string LastName { get;  }
            public string Name { get;  }
        }
    }
}
