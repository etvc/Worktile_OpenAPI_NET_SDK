using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorktileSDK.Entity;

namespace WorktileSDK.API
{
    public class ProjectAPI : BaseAPI
    {
        public ProjectAPI(Client client)
            : base(client)
        {

        }

        /// <summary>
        /// 获取用户所有项目 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Project> GetProjects()
        {
            string result = Client.HttpGetRequest("/projects");
            return JsonConvert.DeserializeObject<List<Project>>(result);
        }

        /// <summary>
        /// 获取项目详情
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public Project GetProjectInfo(string pid)
        {
            string result = Client.HttpGetRequest("/projects/:pid", new WorktileParameter("pid", pid));
            return JsonConvert.DeserializeObject<Project>(result);
        }

        /// <summary>
        /// 获取项目成员
        /// </summary>
        /// <param name="pid">项目pid</param>
        /// <returns></returns>
        public IEnumerable<Member> GetProjectMembers(string pid)
        {
            string result = Client.HttpGetRequest("/projects/:pid/members", new WorktileParameter("pid", pid));
            return JsonConvert.DeserializeObject<List<Member>>(result);
        }

        /// <summary>
        /// 项目添加成员
        /// </summary>
        /// <param name="pid">项目uid</param>
        /// <param name="uid">成员uid</param>
        /// <param name="role">成员角色</param>
        /// <returns>添加成功后，添加的成员信息</returns>
        public MemberOfAdd AddProjectMember(string pid, string uid, int role)
        {
            string result = Client.HttpPostRequest("/projects/:pid/members",
                new WorktileParameter("pid", pid),
                new WorktileParameter("uid", uid),
                new WorktileParameter("role", role));
            return JsonConvert.DeserializeObject<MemberOfAdd>(result);
        }

        /// <summary>
        /// 项目移除成员
        /// </summary>
        /// <param name="pid">项目uid</param>
        /// <param name="uid">成员uid</param>
        /// <returns></returns>
        public bool RemoveProjectMember(string pid, string uid)
        {
            string result = Client.HttpDeleteRequest("/projects/:pid/members",
                new WorktileParameter("pid", pid),
                new WorktileParameter("uid", uid));
            return JsonConvert.DeserializeObject<bool>(result);
        }
    }
}
