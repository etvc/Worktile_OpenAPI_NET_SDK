using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorktileSDK.Entity;

namespace WorktileSDK.API
{
    public class PageAPI : BaseAPI
    {
        public PageAPI(Client client)
            : base(client)
        {

        }

        /// <summary>
        /// 获取文档列表
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public IEnumerable<Page> GetProjectPages(string pid)
        {
            string result = Client.HttpGetRequest("/pages", new WorktileParameter("pid", pid));
            return JsonConvert.DeserializeObject<List<Page>>(result);
        }

        /// <summary>
        /// 新建文档 
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <param name="name">文档名称</param>
        /// <param name="content">文档内容</param>
        /// <param name="parent_id">父文档id</param>
        /// <returns></returns>
        public Page CreatePage(string pid, string name, string content, string parent_id)
        {
            string result = Client.HttpPostRequest("/page", new WorktileParameter("name", name)
            , new WorktileParameter("pid", pid)
                , new WorktileParameter("content", content),
                new WorktileParameter("parent_id", parent_id));
            return JsonConvert.DeserializeObject<Page>(result);
        }

        /// <summary>
        /// 文档详情
        /// </summary>
        /// <param name="page_id">文档id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public Page PageDetail(string page_id, string pid)
        {
            string result = Client.HttpGetRequest("/pages/:page_id", new WorktileParameter("page_id", page_id), new WorktileParameter("pid", pid));
            return JsonConvert.DeserializeObject<Page>(result);
        }

        /// <summary>
        /// 更新文档
        /// </summary>
        /// <param name="page_id">文档id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="name">文档名称</param>
        /// <param name="content">文档内容</param>
        /// <param name="parent_id">父文档id</param>
        /// <returns></returns>
        public bool UpdatePage(string page_id, string pid, string name, string content, string parent_id)
        {
            string result = Client.HttpPutRequest("/pages/:page_id", new WorktileParameter("pid", pid), new WorktileParameter("page_id", page_id),
                new WorktileParameter("name", name),
                new WorktileParameter("content", content),
                new WorktileParameter("parent_id", parent_id));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 删除文档
        /// </summary>
        /// <param name="page_id">文档id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool DeletePage(string page_id, string pid)
        {
            string result = Client.HttpDeleteRequest("/pages/:page_id", new WorktileParameter("page_id", page_id), new WorktileParameter("pid", pid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 添加关注文档
        /// </summary>
        /// <param name="page_id">文档id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="uids">项目成员uid的集合</param>
        /// <returns></returns>
        public bool AddPageWatcher(string page_id, string pid, string[] uids)
        {
            string result = Client.HttpPostRequest("/pages/:page_id/watcher", new WorktileParameter("pid", pid), new WorktileParameter("page_id", page_id),
                new WorktileParameter("uids", Utility.Array2String(uids)));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 取消关注文档 
        /// </summary>
        /// <param name="page_id">文档id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="uid">项目成员uid</param>
        /// <returns></returns>
        public bool RemovePageWatcher(string page_id, string pid, string uid)
        {
            string result = Client.HttpDeleteRequest("/pages/:page_id/watchers/:uid", new WorktileParameter("pid", pid), new WorktileParameter("page_id", page_id),
                new WorktileParameter("uid", uid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 获取文档的评论列表
        /// </summary>
        /// <param name="page_id">文档id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public IEnumerable<Comment> GetPageComments(string page_id, string pid)
        {
            string result = Client.HttpDeleteRequest("/pages/:page_id/comments", new WorktileParameter("pid", pid), new WorktileParameter("page_id", page_id));
            return JsonConvert.DeserializeObject<List<Comment>>(result);
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="page_id">文档id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="message">评论内容</param>
        /// <param name="fids">文件fid集合</param>
        /// <returns></returns>
        public Comment AddComments(string page_id, string pid, string message, string[] fids)
        {
            string result = Client.HttpPostRequest("/pages/:page_id/comment",
                new WorktileParameter("pid", pid),
                new WorktileParameter("page_id", page_id),
                new WorktileParameter("message", message),
                new WorktileParameter("fids", Utility.Array2String(fids)));
            return JsonConvert.DeserializeObject<Comment>(result);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="page_id">文档page_id</param>
        /// <param name="cid">评论的cid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool DeleteComment(string page_id, string cid, string pid)
        {
            string result = Client.HttpDeleteRequest("/pages/:page_id/comments/:cid",
                new WorktileParameter("pid", pid),
             new WorktileParameter("page_id", page_id),
             new WorktileParameter("cid", cid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }
    }

}
