using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK
{
    public class Client
    {
        const string BASE_URL = "https://api.worktile.com/v1";

        public APIResource API { get; private set; }

        public OAuth OAuth
        {
            get;
            private set;
        }

        public Client(OAuth oauth)
        {
            this.OAuth = oauth;
            this.API = new APIResource(this);
        }

        public string HttpGetRequest(string command, params WorktileParameter[] pars)
        {
            return HttpRequest(command, RequestMethod.Get, pars);
        }

        public string HttpPostRequest(string command, params WorktileParameter[] pars)
        {
            return HttpRequest(command, RequestMethod.Post, pars);
        }

        public string HttpPutRequest(string command, params WorktileParameter[] pars)
        {
            return HttpRequest(command, RequestMethod.Put, pars);
        }

        public string HttpDeleteRequest(string command, params WorktileParameter[] pars)
        {
            return HttpRequest(command, RequestMethod.Delete, pars);
        }

        private string HttpRequest(string command, RequestMethod method, params WorktileParameter[] pars)
        {
            List<WorktileParameter> normal_params = new List<WorktileParameter>();
            foreach (var par in pars)
            {
                if (command.Contains(par.Name))
                {
                    command = command.Replace(":" + par.Name, par.Value.ToString());
                }
                else
                {
                    normal_params.Add(par);
                }
            }
            return OAuth.Request(string.Format("{0}{1}", BASE_URL, command), method, normal_params.ToArray());
        }
    }
}
