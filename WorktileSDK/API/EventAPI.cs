using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorktileSDK.Entity;

namespace WorktileSDK.API
{
    public class EventAPI : BaseAPI
    {
        public EventAPI(Client client)
            : base(client)
        {

        }

        /// <summary>
        /// 获取日程列表
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <param name="start">开始时间的时间戳</param>
        /// <param name="end">结束时间的时间戳</param>
        /// <returns></returns>
        public IEnumerable<Event> GetEvents(string pid, string start, string end)
        {
            string result = Client.HttpGetRequest("/events", new WorktileParameter("pid", pid),
                new WorktileParameter("start", start),
                new WorktileParameter("end", end));
            return JsonConvert.DeserializeObject<List<Event>>(result);
        }

        /// <summary>
        /// 我参与的今日日程 
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public IEnumerable<Event> GetTodayEvents(string pid)
        {
            string result = Client.HttpGetRequest(string.Format("/events/today?pid={0}", pid));
            return JsonConvert.DeserializeObject<List<Event>>(result);
        }

        /// <summary>
        /// 新建日程
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <param name="name">日程名称</param>
        /// <param name="location">位置或地点</param>
        /// <param name="start_date">开始日期</param>
        /// <param name="start_time">开始时间</param>
        /// <param name="end_date">结束日期</param>
        /// <param name="end_time">结束时间</param>
        /// <returns></returns>
        public Event CreateEvent(string pid, string name, string location, string start_date, string start_time, string end_date, string end_time)
        {
            string result = Client.HttpPostRequest(string.Format("/event?pid={0}", pid),
                new WorktileParameter("name", name),
                new WorktileParameter("location", location),
                new WorktileParameter("start_date", start_date),
                new WorktileParameter("start_time", start_time),
                new WorktileParameter("end_date", end_date),
                new WorktileParameter("end_time", end_time));
            return JsonConvert.DeserializeObject<Event>(result);
        }

        /// <summary>
        /// 获取日程详情
        /// </summary>
        /// <param name="event_id">日程id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public Event GetEventDetail(string event_id, string pid)
        {
            string result = Client.HttpGetRequest(string.Format("/events/:event_id?pid={0}", pid),
               new WorktileParameter("event_id", event_id));
            return JsonConvert.DeserializeObject<Event>(result);
        }

        /// <summary>
        /// 修改日程 
        /// </summary>
        /// <param name="event_id">日程id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="name">日程名称</param>
        /// <param name="summary">日程描述</param>
        /// <param name="start_date">开始日期</param>
        /// <param name="start_time">开始时间</param>
        /// <param name="end_date">结束日期</param>
        /// <param name="end_time">结束时间</param>
        /// <returns></returns>
        public bool UpdateEvent(string event_id, string pid, string name, string summary, string
            start_date, string start_time, string end_date, string end_time)
        {
            string result = Client.HttpPutRequest(string.Format("/events/:event_id?pid={0}", pid),
              new WorktileParameter("event_id", event_id),
              new WorktileParameter("name", name),
              new WorktileParameter("summary", summary),
              new WorktileParameter("start_date", start_date),
              new WorktileParameter("start_time", start_time),
              new WorktileParameter("end_date", end_date),
              new WorktileParameter("end_time", end_time));
            return JsonConvert.DeserializeObject<bool>(result);
        }

        /// <summary>
        /// 删除日程
        /// </summary>
        /// <param name="event_id">日程id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public bool DeleteEvent(string event_id, string pid)
        {
            string result = Client.HttpPutRequest(string.Format("/events/:event_id?pid={0}", pid),
             new WorktileParameter("event_id", event_id));
            return JsonConvert.DeserializeObject<bool>(result);
        }

        /// <summary>
        /// 添加参与成员
        /// </summary>
        /// <param name="event_id">日程id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="uid">成员uid</param>
        /// <returns></returns>
        public bool AddAttendee(string event_id, string pid, string uid)
        {
            string result = Client.HttpPutRequest(string.Format("/events/:event_id/attendee?pid={0}", pid),
                new WorktileParameter("event_id", event_id),
                new WorktileParameter("uid", uid));
            return JsonConvert.DeserializeObject<bool>(result);
        }

        /// <summary>
        /// 移除参与成员 
        /// </summary>
        /// <param name="event_id">日程id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="attendee_id">参与日程的成员id</param>
        /// <returns></returns>
        public bool RemoveAttendee(string event_id, string pid, string attendee_id)
        {
            string result = Client.HttpPutRequest(string.Format("/events/:event_id/attendees/:attendee_id?pid={0}", pid),
                new WorktileParameter("event_id", event_id),
                new WorktileParameter("attendee_id", attendee_id));
            return JsonConvert.DeserializeObject<bool>(result);
        }

        /// <summary>
        /// 获取日程的评论列表
        /// </summary>
        /// <param name="event_id">日程id</param>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public IEnumerable<Comment> GetEventComments(string event_id, string pid)
        {
            string result = Client.HttpPutRequest(string.Format("/events/:event_id/comments?pid={0}", pid),
               new WorktileParameter("event_id", event_id));
            return JsonConvert.DeserializeObject<List<Comment>>(result);
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="event_id">日程id</param>
        /// <param name="pid">项目pid</param>
        /// <param name="message">评论内容</param>
        /// <param name="fids">文件fid集合</param>
        /// <returns></returns>
        public Comment AddComments(string event_id, string pid, string message, string[] fids)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (string fid in fids)
            {
                sb.Append("'" + fid + "',");
            }
            string str = sb.ToString().TrimEnd(',') + "]";
            string result = Client.HttpGetRequest(string.Format("/events/:event_id/comment?pid={0}", pid),
                new WorktileParameter("event_id", event_id),
                new WorktileParameter("message", message),
                new WorktileParameter("fids", str));
            return JsonConvert.DeserializeObject<Comment>(result);
        }

        /// <summary>
        /// 删除评论 
        /// </summary>
        /// <param name="event_id">日程tid</param>
        /// <param name="pid">项目pid</param>
        /// <param name="cid">评论的cid</param>
        /// <returns></returns>
        public bool DeleteComment(string event_id, string pid, string cid)
        {
            string result = Client.HttpGetRequest(string.Format("/events/:event_id/comments/:cid?pid={0}", pid),
                new WorktileParameter("event_id", event_id),
                new WorktileParameter("cid", cid));
            return JsonConvert.DeserializeObject<bool>(result);
        }
    }
}
