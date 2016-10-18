using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorktileSDK.Entity;

namespace WorktileSDK.API
{
    public class FileAPI : BaseAPI
    {
        public FileAPI(Client client)
            : base(client)
        {

        }

        /// <summary>
        /// 文件/文件夹列表
        /// </summary>
        /// <param name="folder_id">文件夹id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public IEnumerable<File> GetProjectFiles(string folder_id, string pid)
        {
            string result = Client.HttpGetRequest("/files", new WorktileParameter("pid", pid), new WorktileParameter("folder_id", folder_id));
            return JsonConvert.DeserializeObject<List<File>>(result);
        }

        /// <summary>
        /// 项目中所有的图片列表
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public IEnumerable<Image> GetProjectImages(string pid)
        {
            string result = Client.HttpGetRequest("/files/images", new WorktileParameter("pid", pid));
            return JsonConvert.DeserializeObject<List<Image>>(result);
        }

        /// <summary>
        /// 文件/文件夹详情 
        /// </summary>
        /// <param name="fid">文件fid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public File FileDetail(string fid, string pid)
        {
            string result = Client.HttpGetRequest("/files/:fid", new WorktileParameter("pid", pid), new WorktileParameter("fid", fid));
            return JsonConvert.DeserializeObject<File>(result);
        }

        /// <summary>
        /// 修改文件名和描述
        /// </summary>
        /// <param name="fid">文件fid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="name">文件名称</param>
        /// <param name="desc">文件描述</param>
        /// <returns></returns>
        public bool UpdateFile(string fid, string pid, string name, string desc)
        {
            string result = Client.HttpPutRequest("/files/:fid", new WorktileParameter("pid", pid), new WorktileParameter("fid", fid),
                new WorktileParameter("name", name),
                new WorktileParameter("desc", desc));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="tid"></param>
        /// <param name="post_id"></param>
        /// <param name="event_id"></param>
        /// <param name="file"></param>
        /// <param name="type">上传文件关联的类型：project,task,post,event，默认值：project</param>
        /// <param name="folder_id">文件夹id，如果type的值为project，需要传该属性，默认值为空</param>
        /// <returns></returns>
        public File AddFile(string pid, string tid, string post_id, string event_id, byte[] file, string type = "project", string folder_id = "")
        {
            string result = Client.HttpPostRequest("/file"
                , new WorktileParameter("pid", pid)
                , new WorktileParameter("type", type)
                , new WorktileParameter("tid", tid)
             , new WorktileParameter("post_id", post_id)
             , new WorktileParameter("folder_id", folder_id),
             new WorktileParameter("event_id", event_id),
             new WorktileParameter("file", file));
            return JsonConvert.DeserializeObject<File>(result);
        }
        public File AddFile(string pid, string tid, byte[] file)
        {
            return AddFile(pid, tid, "", "", file, "task", "");
        }
        public File AddFile(string pid, byte[] file)
        {
            return AddFile(pid, "", "", "", file, "project", "");
        }
        /// <summary>
        /// 删除文件 
        /// </summary>
        /// <param name="fid">文件fid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool DeleteFile(string fid, string pid)
        {
            string result = Client.HttpDeleteRequest("/files/:fid", new WorktileParameter("pid", pid), new WorktileParameter("fid", fid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 移动文件 
        /// </summary>
        /// <param name="fid">文件fid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="from">文件所在的当前文件夹id</param>
        /// <param name="to">目标文件夹id</param>
        /// <returns></returns>
        public bool MoveFile(string fid, string pid, string from, string to)
        {
            string result = Client.HttpPutRequest("/files/:fid/move", new WorktileParameter("pid", pid), new WorktileParameter("fid", fid),
                new WorktileParameter("from", from),
                new WorktileParameter("to", to));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 添加关注文件
        /// </summary>
        /// <param name="fid">文件fid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="uids">项目成员uid的集合</param>
        /// <returns></returns>
        public bool AddFileWatchers(string fid, string pid, string[] uids)
        {
            string result = Client.HttpPostRequest("/files/:fid/watcher", new WorktileParameter("pid", pid), new WorktileParameter("fid", fid),
                new WorktileParameter("uids", Utility.Array2String(uids)));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 取消关注文件
        /// </summary>
        /// <param name="fid">文件fid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="uid">项目成员uid</param>
        /// <returns></returns>
        public bool RemoveFileWatcher(string fid, string pid, string uid)
        {
            string result = Client.HttpPostRequest("/files/:fid/watchers/:uid", new WorktileParameter("pid", pid), new WorktileParameter("fid", fid),
                  new WorktileParameter("uid", uid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 获取文件的评论列表
        /// </summary>
        /// <param name="fid">文件fid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public IEnumerable<Comment> GetFileComments(string fid, string pid)
        {
            string result = Client.HttpPostRequest("/files/:fid/comments", new WorktileParameter("pid", pid), new WorktileParameter("fid", fid));
            return JsonConvert.DeserializeObject<List<Comment>>(result);
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="fid">文件fid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="message">评论内容</param>
        /// <param name="fids">文件fid集合</param>
        /// <returns></returns>
        public Comment AddComments(string fid, string pid, string message, string[] fids)
        {
            string result = Client.HttpGetRequest("/files/:fid/comment", new WorktileParameter("pid", pid),
                new WorktileParameter("fid", fid),
                new WorktileParameter("message", message),
                new WorktileParameter("fids", Utility.Array2String(fids)));
            return JsonConvert.DeserializeObject<Comment>(result);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="tid">文件fid</param>
        /// <param name="cid">评论的cid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool DeleteComment(string fid, string cid, string pid)
        {
            string result = Client.HttpGetRequest("/tasks/:tid/comments/:cid", new WorktileParameter("pid", pid),
             new WorktileParameter("fid", fid),
             new WorktileParameter("cid", cid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }
    }
}
