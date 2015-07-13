using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace WorktileSDK
{
    /// <summary>
    /// 授权认证返回类型
    /// </summary>
    public enum ResponseType
    {
        /// <summary>
        /// Code
        /// </summary>
        Code,
        /// <summary>
        /// Access Token
        /// </summary>
        Token
    }

    /// <summary>
    /// 回调返回类型
    /// </summary>
    public enum DisplayType
    {
        Mobile,
        Web
    }

    internal enum GrantType
    {
        AuthorizationCode,
        Password,
        RefreshToken
    }

    internal enum RequestMethod
    {
        Get,
        Post,
        Delete,
        Put
    }
   
    /// <summary>
    /// Token验证返回值
    /// </summary>
    public enum TokenResult
    {
        /// <summary>
        /// 正常
        /// </summary>
        Success,
        /// <summary>
        /// Token已过期
        /// </summary>
        TokenExpired,
        /// <summary>
        /// Token已被占用
        /// </summary>
        TokenUsed,
        /// <summary>
        /// Token已被回收
        /// </summary>
        TokenRevoked,
        /// <summary>
        /// Token被拒绝
        /// </summary>
        TokenRejected,
        /// <summary>
        /// 其他问题
        /// </summary>
        Other
    }

    internal class WorktileParameterComparer : IComparer<WorktileParameter>
    {

        public int Compare(WorktileParameter x, WorktileParameter y)
        {
            return StringComparer.CurrentCulture.Compare(x.Name, y.Name);
        }
    }

    /// <summary>
    /// 微博工具类
    /// </summary>
    public static class Utility
    {
        internal static string BuildQueryString(Dictionary<string, string> parameters)
        {
            List<string> pairs = new List<string>();
            foreach (KeyValuePair<string, string> item in parameters)
            {
                if (string.IsNullOrEmpty(item.Value))
                    continue;

                pairs.Add(string.Format("{0}={1}", Uri.EscapeDataString(item.Key), Uri.EscapeDataString(item.Value)));
            }

            return string.Join("&", pairs.ToArray());
        }

        internal static string BuildQueryString(params WorktileParameter[] parameters)
        {
            List<string> pairs = new List<string>();
            foreach (var item in parameters)
            {
                if (item.IsBinaryData)
                    continue;

                var value = string.Format("{0}", item.Value);
                if (!string.IsNullOrEmpty(value))
                {
                    pairs.Add(string.Format("{0}={1}", Uri.EscapeDataString(item.Name), Uri.EscapeDataString(value)));
                }
            }

            return string.Join("&", pairs.ToArray());
        }

        internal static string GetBoundary()
        {
            string pattern = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder boundaryBuilder = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                var index = rnd.Next(pattern.Length);
                boundaryBuilder.Append(pattern[index]);
            }
            return boundaryBuilder.ToString();
        }
        /// <summary>
        /// 创建Post Body
        /// </summary>
        /// <param name="boundary"></param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        internal static byte[] BuildPostData(string boundary, params WorktileParameter[] parameters)
        {
            List<WorktileParameter> pairs = new List<WorktileParameter>(parameters);
            pairs.Sort(new WorktileParameterComparer());
            MemoryStream buff = new MemoryStream();

            byte[] headerBuff = Encoding.ASCII.GetBytes(string.Format("\r\n--{0}\r\n", boundary));
            byte[] footerBuff = Encoding.ASCII.GetBytes(string.Format("\r\n--{0}--", boundary));


            StringBuilder contentBuilder = new StringBuilder();

            foreach (var p in pairs)
            {
                if (!p.IsBinaryData)
                {
                    var value = string.Format("{0}", p.Value);
                    if (string.IsNullOrEmpty(value))
                    {
                        continue;
                    }


                    buff.Write(headerBuff, 0, headerBuff.Length);
                    byte[] dispositonBuff = Encoding.UTF8.GetBytes(string.Format("content-disposition: form-data; name=\"{0}\"\r\n\r\n{1}", p.Name, p.Value.ToString()));
                    buff.Write(dispositonBuff, 0, dispositonBuff.Length);

                }
                else
                {
                    buff.Write(headerBuff, 0, headerBuff.Length);
                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: \"image/unknow\"\r\nContent-Transfer-Encoding: binary\r\n\r\n";
                    byte[] fileBuff = System.Text.Encoding.UTF8.GetBytes(string.Format(headerTemplate, p.Name, string.Format("upload{0}", BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0))));
                    buff.Write(fileBuff, 0, fileBuff.Length);
                    byte[] file = (byte[])p.Value;
                    buff.Write(file, 0, file.Length);

                }
            }

            buff.Write(footerBuff, 0, footerBuff.Length);
            buff.Position = 0;

            byte[] contentBuff = new byte[buff.Length];
            buff.Read(contentBuff, 0, contentBuff.Length);
            buff.Close();
            buff.Dispose();
            return contentBuff;


        }



    }
}
