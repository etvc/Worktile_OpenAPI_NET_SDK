using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorktileSDK.Entity;

namespace WorktileSDK.API
{
    public class EntryAPI : BaseAPI
    {
        public EntryAPI(Client client)
            : base(client)
        {

        }

        /// <summary>
        /// 获取项目的任务组列表 
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public IEnumerable<Entry> GetEntries(string pid)
        {
            string result = Client.HttpGetRequest("/entries", new WorktileParameter("pid", pid));
            return JsonConvert.DeserializeObject<List<Entry>>(result);
        }

        /// <summary>
        /// 创建任务组
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <param name="name">任务组名称</param>
        /// <returns></returns>
        public EntryOfAdd AddProjectEntry(string pid, string name)
        {
            string result = Client.HttpPostRequest("/entry",
                new WorktileParameter("pid", pid),
                new WorktileParameter("name", name));
            return JsonConvert.DeserializeObject<EntryOfAdd>(result);
        }

        /// <summary>
        /// 任务组重命名 
        /// </summary>
        /// <param name="entry_id">任务组id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="name">任务组名称</param>
        /// <returns></returns>
        public bool UpdateProjectEntryName(string entry_id, string pid, string name)
        {
            string result = Client.HttpPutRequest("/entries/:entry_id",
                new WorktileParameter("pid", pid),
                new WorktileParameter("entry_id", entry_id),
                new WorktileParameter("name", name));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 删除任务组
        /// </summary>
        /// <param name="entry_id">任务组id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool RemoveProjectEntry(string entry_id, string pid)
        {
            string result = Client.HttpDeleteRequest("/entries/:entry_id",
                new WorktileParameter("pid", pid),
               new WorktileParameter("entry_id", entry_id));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 关注任务组
        /// </summary>
        /// <param name="entry_id">任务组id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool WatchProjectEntry(string entry_id, string pid)
        {
            string result = Client.HttpPostRequest("/entries/:entry_id/watcher",
                new WorktileParameter("pid", pid),
               new WorktileParameter("entry_id", entry_id));
            return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 取消关注任务组
        /// </summary>
        /// <param name="entry_id">任务组id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool CancelWatchProjectEntry(string entry_id, string pid)
        {
            string result = Client.HttpDeleteRequest("/entries/:entry_id/watcher",
                new WorktileParameter("pid", pid),
               new WorktileParameter("entry_id", entry_id));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }
    }
}
