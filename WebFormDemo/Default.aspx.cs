using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorktileSDK;

namespace WebFormDemo
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var client = Session["client"] as Client;
            if (client == null)
            {
                string code = Request["code"];
                if (string.IsNullOrEmpty(code))
                {
                    OAuth auth = new OAuth(Properties.Settings.Default.AppKey, Properties.Settings.Default.CallBack);
                    HyperLink1.Text = "请授权";
                    HyperLink1.NavigateUrl = auth.GetAuthorizeURL();

                }
                else
                {
                    OAuth auth = new OAuth(Properties.Settings.Default.AppKey, Properties.Settings.Default.CallBack);
                    string msg;
            
                    if (auth.GetAccessTokenByCode(code, out msg))
                    {
                          client = new Client(auth);
                        Session["client"] = client;
                        HyperLink1.Text = "授权成功";
                        HyperLink1.NavigateUrl = "#";
                    }
                    else
                    {
                        HyperLink1.Text = "授权失败";
                        HyperLink1.NavigateUrl = "#";
                    }
                }

            }
            if (client!=null)
            {
                HyperLink1.Text ="任务数量"+ client.API.ProjectAPI.GetProjects().Count().ToString ();
            }
        }
    }
}