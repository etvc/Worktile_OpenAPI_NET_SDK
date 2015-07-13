using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK
{
    public class OAuth
    {
        private const string AUTHORIZE_URL = "https://open.worktile.com/oauth2/authorize";
        private const string ACCESS_TOKEN_URL = "https://api.worktile.com/oauth2/access_token";

        /// <summary>
        /// 获取App Key
        /// </summary>
        public string AppKey
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获取Access Token
        /// </summary>
        public string AccessToken
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获取或设置回调地址
        /// </summary>
        public string CallbackUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Refresh Token 似乎目前没用
        /// </summary>
        public string RefreshToken
        {
            get;
            internal set;
        }

        public OAuth(string appKey, string callbackUrl)
        {
            this.AppKey = appKey;
            this.AccessToken = string.Empty;
            this.CallbackUrl = callbackUrl;
        }

        /// <summary>
        /// 根据返回来的code获取token
        /// </summary>
        /// <param name="code"></param>
        public void GetAccessTokenByCode(string code)
        {
            string result = Request(ACCESS_TOKEN_URL, RequestMethod.Post, new WorktileParameter("client_id", AppKey), new WorktileParameter("code", code));
            var token = JsonConvert.DeserializeObject<AccessToken>(result);
            AccessToken = token.access_token;
        }

        /// <summary>
        /// 获取授权url
        /// </summary>
        /// <param name="state"></param>
        /// <param name="display"></param>
        /// <returns></returns>
        public string GetAuthorizeURL(string state = null, DisplayType display = DisplayType.Web)
        {
            Dictionary<string, string> config = new Dictionary<string, string>()
			{
				{"client_id",AppKey},
				{"redirect_uri",CallbackUrl},
				{"state",state??string.Empty},
				{"display",display.ToString().ToLower()},
			};
            UriBuilder builder = new UriBuilder(AUTHORIZE_URL);
            builder.Query = Utility.BuildQueryString(config);

            return builder.ToString();
        }

        internal string Request(string url, RequestMethod method = RequestMethod.Get, params WorktileParameter[] parameters)
        {
            string rawUrl = string.Empty;
            UriBuilder uri = new UriBuilder(url);
            string result = string.Empty;

            bool multi = false;
            multi = parameters.Count(p => p.IsBinaryData) > 0;

            switch (method)
            {
                case RequestMethod.Get:
                case RequestMethod.Put:
                case RequestMethod.Delete:
                    {
                        uri.Query = Utility.BuildQueryString(parameters);
                    }
                    break;
                case RequestMethod.Post:
                    {
                        if (!multi)
                        {
                            uri.Query = Utility.BuildQueryString(parameters);
                        }
                    }
                    break;
            }

            HttpWebRequest http = WebRequest.Create(uri.Uri) as HttpWebRequest;
            http.ServicePoint.Expect100Continue = false;
            http.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0)";

            if (!string.IsNullOrEmpty(AccessToken))
            {
                http.ContentType = "application/json";
                http.Headers["access_token"] = AccessToken;
            }

            switch (method)
            {
                case RequestMethod.Get:
                    {
                        http.Method = "GET";
                    }
                    break;
                case RequestMethod.Delete:
                    {
                        http.Method = "DELETE";
                    }
                    break;
                case RequestMethod.Post:
                    {
                        http.Method = "POST";

                        if (multi)
                        {
                            string boundary = Utility.GetBoundary();
                            http.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
                            http.AllowWriteStreamBuffering = true;
                            using (Stream request = http.GetRequestStream())
                            {
                                try
                                {
                                    var raw = Utility.BuildPostData(boundary, parameters);
                                    request.Write(raw, 0, raw.Length);
                                }
                                finally
                                {
                                    request.Close();
                                }
                            }
                        }
                        else
                        {
                            http.ContentType = "application/x-www-form-urlencoded";

                            using (StreamWriter request = new StreamWriter(http.GetRequestStream()))
                            {
                                try
                                {
                                    request.Write(Utility.BuildQueryString(parameters));
                                }
                                finally
                                {
                                    request.Close();
                                }
                            }
                        }
                    }
                    break;
                case RequestMethod.Put:
                    {
                        http.Method = "PUT";

                        if (multi)
                        {
                            string boundary = Utility.GetBoundary();
                            http.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
                            http.AllowWriteStreamBuffering = true;
                            using (Stream request = http.GetRequestStream())
                            {
                                try
                                {
                                    var raw = Utility.BuildPostData(boundary, parameters);
                                    request.Write(raw, 0, raw.Length);
                                }
                                finally
                                {
                                    request.Close();
                                }
                            }
                        }
                        else
                        {
                            http.ContentType = "application/x-www-form-urlencoded";

                            using (StreamWriter request = new StreamWriter(http.GetRequestStream()))
                            {
                                try
                                {
                                    request.Write(Utility.BuildQueryString(parameters));
                                }
                                finally
                                {
                                    request.Close();
                                }
                            }
                        }
                    }
                    break;
            }

            try
            {
                using (WebResponse response = http.GetResponse())
                {

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        try
                        {
                            result = reader.ReadToEnd();
                        }
                        catch (WorktileException)
                        {
                            throw;
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }


                    response.Close();
                }
            }
            catch (System.Net.WebException webEx)
            {
                if (webEx.Response != null)
                {
                    using (StreamReader reader = new StreamReader(webEx.Response.GetResponseStream()))
                    {
                        string errorInfo = reader.ReadToEnd();

                        Error error = JsonConvert.DeserializeObject<Error>(errorInfo);

                        reader.Close();

                        throw new WorktileException(error.error_code, error.error_message, error.request);

                    }
                }
                else
                {
                    throw new WorktileException(webEx.Message);
                }

            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
