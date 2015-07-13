using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK.API
{
    public class BaseAPI
    {
        protected Client Client
        {
            get;
            private set;
        }

        public BaseAPI(Client client)
        {
            this.Client = client;
        }
    }
}
