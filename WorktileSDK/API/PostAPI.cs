using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorktileSDK.Entity;

namespace WorktileSDK.API
{
    public class PostAPI : BaseAPI
    {
        public PostAPI(Client client)
            : base(client)
        {

        }

        /// <summary>
        /// 获取话题列表
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public IEnumerable<Post> GetProjectPosts(string pid)
        {
            string result = Client.HttpGetRequest("/posts",new WorktileParameter("pid",pid));
            return JsonConvert.DeserializeObject<List<Post>>(result);
        }

        /// <summary>
        /// 发起话题
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <param name="name">话题名称</param>
        /// <param name="content">话题内容</param>
        /// <returns></returns>
        public Post CreatePost(string pid, string name, string content)
        {
            string result = Client.HttpPostRequest("/post", new WorktileParameter("name", name),
                new WorktileParameter("pid",pid),
                new WorktileParameter("content", content));
            return JsonConvert.DeserializeObject<Post>(result);
        }

        /// <summary>
        /// 获取话题详情 
        /// </summary>
        /// <param name="post_id">话题id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public Post PostDetail(string post_id, string pid)
        {
            string result = Client.HttpGetRequest("/posts/:post_id",
                new WorktileParameter("pid",pid),
                new WorktileParameter("post_id", post_id));
            return JsonConvert.DeserializeObject<Post>(result);
        }

        /// <summary>
        /// 修改话题
        /// </summary>
        /// <param name="post_id">话题id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="name">话题名称</param>
        /// <param name="content">话题内容</param>
        /// <returns></returns>
        public bool UpdatePost(string post_id, string pid, string name, string content)
        {
            string result = Client.HttpPutRequest("/posts/:post_id",
                new WorktileParameter("pid",pid),
                new WorktileParameter("post_id", post_id),
                new WorktileParameter("name", name),
                new WorktileParameter("content", content));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 删除话题
        /// </summary>
        /// <param name="post_id">话题id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool DeletePost(string post_id, string pid)
        {
            string result = Client.HttpDeleteRequest("/posts/:post_id",
                new WorktileParameter("pid",pid),
                new WorktileParameter("post_id", post_id));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 添加关注话题 
        /// </summary>
        /// <param name="post_id">话题id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="uids">项目成员uid的集合</param>
        /// <returns></returns>
        public bool AddPostWatchers(string post_id, string pid, string[] uids)
        {
            string result = Client.HttpPostRequest("/posts/:post_id/watcher",
                new WorktileParameter("pid",pid),
              new WorktileParameter("post_id", post_id),
              new WorktileParameter("uids", Utility.Array2String(uids)));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 取消关注话题 
        /// </summary>
        /// <param name="post_id">话题id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="uid">项目成员uid</param>
        /// <returns></returns>
        public bool RemovePostWatcher(string post_id, string pid, string[] uid)
        {
            string result = Client.HttpDeleteRequest("/posts/:post_id/watchers/:uid",
                new WorktileParameter("pid",pid),
              new WorktileParameter("post_id", post_id),
              new WorktileParameter("uid", uid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="tid">话题post_id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="message">评论内容</param>
        /// <param name="fids">文件fid集合</param>
        /// <returns></returns>
        public Comment AddComments(string post_id, string pid, string message, string[] fids)
        {
            string result = Client.HttpPostRequest("/posts/:post_id/comment",
                new WorktileParameter("pid",pid),
                new WorktileParameter("post_id", post_id),
                new WorktileParameter("message", message),
                new WorktileParameter("fids", Utility.Array2String(fids)));
            return JsonConvert.DeserializeObject<Comment>(result);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="tid">话题post_id</param>
        /// <param name="cid">评论的cid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool DeleteComment(string post_id, string cid, string pid)
        {
            string result = Client.HttpDeleteRequest("/posts/:post_id/comments/:cid"
                ,new WorktileParameter("pid",pid),
             new WorktileParameter("post_id", post_id),
             new WorktileParameter("cid", cid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }
    }
}
