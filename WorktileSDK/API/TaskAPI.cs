using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorktileSDK.Entity;

namespace WorktileSDK.API
{
    public class TaskAPI : BaseAPI
    {
        public TaskAPI(Client client)
            : base(client)
        {

        }

        /// <summary>
        /// 获取项目的任务列表
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <param name="type">任务类型 已封装成枚举</param>
        /// <returns></returns>
        public IEnumerable<Task> GetProjectTasks(string pid, TaskType type)
        {
            string result = Client.HttpGetRequest("/tasks", new WorktileParameter("pid", pid), new WorktileParameter("type", TaskType2String(type)));
            return JsonConvert.DeserializeObject<List<Task>>(result);
        }

        private static string TaskType2String(TaskType type)
        {
            string typestr = "";
            switch (type)
            {
                case TaskType.all:
                    typestr = "all";
                    break;
                case TaskType.completed:
                    typestr = "completed";
                    break;
                case TaskType.expired:
                    typestr = "expired";
                    break;
                case TaskType.uncompleted:
                    typestr = "uncompleted";
                    break;
                default:
                    typestr = "all";
                    break;
            }
            return typestr;
        }

        /// <summary>
        /// 即将过期的任务 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Task> GetTodayTasks()
        {
            string result = Client.HttpGetRequest("/tasks/today");
            return JsonConvert.DeserializeObject<List<Task>>(result);
        }

        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <param name="name">任务名称</param>
        /// <param name="entry_id">任务组entry_id</param>
        /// <returns></returns>
        public Task CreateTask(string pid, string name, string entry_id)
        {
            string result = Client.HttpPostRequest("/task", new WorktileParameter("pid", pid), new WorktileParameter("name", name),
                new WorktileParameter("entry_id", entry_id));
            return JsonConvert.DeserializeObject<Task>(result);
        }

        /// <summary>
        /// 任务详情
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public Task GetTaskDetail(string tid, string pid)
        {
            string result = Client.HttpGetRequest("/tasks/:tid", new WorktileParameter("tid", tid),
                new WorktileParameter("pid", pid));
            return JsonConvert.DeserializeObject<Task>(result);
        }

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="name">任务名称</param>
        /// <param name="desc">任务描述</param>
        /// <returns></returns>
        public bool UpdateTask(string tid, string pid, string name, string desc)
        {
            string result = Client.HttpPutRequest("/tasks/:tid", new WorktileParameter("pid", pid), new WorktileParameter("tid", tid),
                new WorktileParameter("name", name),
                new WorktileParameter("desc", desc));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool DeleteTask(string tid, string pid)
        {
            string result = Client.HttpDeleteRequest("/tasks/:tid", new WorktileParameter("pid", pid), new WorktileParameter("tid", tid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 移动任务 
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="to_pid">移动目标项目的pid</param>
        /// <param name="to_entry_id">移动目标项目的任务组id</param>
        /// <returns></returns>
        public bool MoveTask(string tid, string pid, string to_pid, string to_entry_id)
        {
            string result = Client.HttpPutRequest("/tasks/:tid/move",
                new WorktileParameter("pid", pid),
                new WorktileParameter("tid", tid),
                new WorktileParameter("to_pid", to_pid),
                new WorktileParameter("to_entry_id", to_entry_id));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 设置截止日期 
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="expire">设置截止日期的时间戳</param>
        /// <returns></returns>
        public bool SetTaskExpire(string tid, string pid, int expire)
        {
            string result = Client.HttpPutRequest("/tasks/:tid/expire",
                new WorktileParameter("pid", pid),
               new WorktileParameter("expire", expire));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 分配任务
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="uid">项目成员uid</param>
        /// <returns></returns>
        public bool AssignTask(string tid, string pid, string uid)
        {
            string result = Client.HttpPutRequest("/tasks/:tid/member",
                new WorktileParameter("pid", pid),
               new WorktileParameter("uid", uid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 取消分配任务
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="member_id">项目成员uid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool CancelAssignTask(string tid, string member_id, string pid)
        {
            string result = Client.HttpPutRequest("/tasks/:tid/members/:member_id",
                new WorktileParameter("pid", pid),
               new WorktileParameter("tid", tid),
               new WorktileParameter("member_id", member_id));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 添加关注任务
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="uids">项目成员uid的集合</param>
        /// <returns></returns>
        public bool AddWatcher(string tid, string pid, string[] uids)
        {
            string result = Client.HttpPostRequest("/tasks/:tid/watcher",
                new WorktileParameter("pid", pid),
               new WorktileParameter("tid", tid),
               new WorktileParameter("uids", Utility.Array2String(uids)));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 取消关注任务
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="uid">项目成员uid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool RemoveWatcher(string tid, string uid, string pid)
        {
            string result = Client.HttpPostRequest("/tasks/:tid/watchers/:uid",
                new WorktileParameter("pid", pid),
             new WorktileParameter("tid", tid),
             new WorktileParameter("uid", uid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="label">标签名字</param>
        /// <returns></returns>
        public bool SetLabels(string tid, string pid, string label)
        {
            string result = Client.HttpPutRequest("/tasks/:tid/labels", new WorktileParameter("pid", pid), new WorktileParameter("tid", tid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="label">任务标签名称</param>
        /// <returns></returns>
        public bool DeleteLabels(string tid, string pid, string label)
        {
            string result = Client.HttpDeleteRequest("/tasks/:tid/labels", new WorktileParameter("pid", pid), new WorktileParameter("tid", tid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public bool TaskComplete(string tid, string pid)
        {
            string result = Client.HttpPutRequest("/tasks/:tid/complete", new WorktileParameter("pid", pid), new WorktileParameter("tid", tid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 取消完成任务
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public bool TaskUnComplete(string tid, string pid)
        {
            string result = Client.HttpPutRequest("/tasks/:tid/uncomplete", new WorktileParameter("pid", pid), new WorktileParameter("tid", tid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 添加检查项
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="name">检查项内容</param>
        /// <returns></returns>
        public todo AddTodo(string tid, string pid, string name)
        {
            string result = Client.HttpPostRequest("/tasks/:tid/todo", new WorktileParameter("pid", pid), new WorktileParameter("tid", tid),
                new WorktileParameter("name", name));
            return JsonConvert.DeserializeObject<todo>(result);
        }

        /// <summary>
        /// 修改检查项
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="todo_id">检查项id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="name">检查项内容</param>
        /// <returns></returns>
        public todo UpdateTodo(string tid, string todo_id, string pid, string name)
        {
            string result = Client.HttpPutRequest("/tasks/:tid/todos/:todo_id", new WorktileParameter("pid", pid), new WorktileParameter("tid", tid),
                new WorktileParameter("todo_id", todo_id),
                new WorktileParameter("name", name));
            return JsonConvert.DeserializeObject<todo>(result);
        }

        /// <summary>
        /// 完成检查项 
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="todo_id">检查项id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool TodoChecked(string tid, string todo_id, string pid)
        {
            string result = Client.HttpPutRequest("/tasks/:tid/todos/:todo_id/checked",
                new WorktileParameter("pid", pid),
                new WorktileParameter("tid", tid),
                new WorktileParameter("todo_id", todo_id));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 取消完成检查项
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="todo_id">检查项id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool TodoUnchecked(string tid, string todo_id, string pid)
        {
            string result = Client.HttpPutRequest("/tasks/:tid/todos/:todo_id/unchecked",
                new WorktileParameter("pid", pid),
               new WorktileParameter("tid", tid),
               new WorktileParameter("todo_id", todo_id));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 删除检查项 
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="todo_id">检查项id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool DeleteTodo(string tid, string todo_id, string pid)
        {
            string result = Client.HttpDeleteRequest("/tasks/:tid/todos/:todo_id",
                new WorktileParameter("pid", pid),
             new WorktileParameter("tid", tid),
             new WorktileParameter("todo_id", todo_id));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 获取项目的已归档的任务列表
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <param name="page">当前页，默认只为1</param>
        /// <param name="size">每页获取的任务数</param>
        /// <returns></returns>
        public IEnumerable<Task> GetProjectArchivedTasks(string pid, int page, int size)
        {
            string result = Client.HttpGetRequest("/tasks/archived",
                new WorktileParameter("pid", pid),
                new WorktileParameter("page", page),
                new WorktileParameter("size", size));
            return JsonConvert.DeserializeObject<List<Task>>(result);
        }

        /// <summary>
        /// 归档项目
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <param name="entry_id">任务组id</param>
        /// <returns></returns>
        public bool ArchiveProject(string pid, string entry_id)
        {
            string result = Client.HttpPutRequest("/tasks/archive", new WorktileParameter("pid", pid), new WorktileParameter("entry_id", entry_id));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 归档任务
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool ArchiveTask(string tid, string pid)
        {
            string result = Client.HttpPutRequest("/tasks/:tid/archive", new WorktileParameter("pid", pid), new WorktileParameter("tid", tid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 激活归档任务
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="entry_id">项目中的任务组id</param>
        /// <returns></returns>
        public bool UnarchiveTask(string tid, string pid, string entry_id)
        {
            string result = Client.HttpPutRequest("/tasks/:tid/unarchive", new WorktileParameter("pid", pid), new WorktileParameter("tid", tid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }

        /// <summary>
        /// 获取任务的评论列表
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public IEnumerable<Comment> GetTaskComments(string tid, string pid)
        {
            string result = Client.HttpGetRequest("/tasks/:tid/comments", new WorktileParameter("pid", pid), new WorktileParameter("tid", tid));
            return JsonConvert.DeserializeObject<List<Comment>>(result);
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="message">评论内容</param>
        /// <param name="fids">文件fid集合</param>
        /// <returns></returns>
        public Comment AddComments(string tid, string pid, string message, string[] fids)
        {
            string result = Client.HttpPostRequest("/tasks/:tid/comment",
                new WorktileParameter("pid", pid),
                new WorktileParameter("tid", tid),
                new WorktileParameter("message", message),
                new WorktileParameter("fids", Utility.Array2String(fids)));
            return JsonConvert.DeserializeObject<Comment>(result);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="tid">任务tid</param>
        /// <param name="cid">评论的cid</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool DeleteComment(string tid, string cid, string pid)
        {
            string result = Client.HttpDeleteRequest("/tasks/:tid/comments/:cid",
                new WorktileParameter("pid", pid),
             new WorktileParameter("tid", tid),
             new WorktileParameter("cid", cid));
             return JsonConvert.DeserializeObject<Result>(result).success;
        }
    }
}
