using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorktileSDK.Entity;

namespace WorktileSDK.API
{
    public class TeamAPI : BaseAPI
    {

        public TeamAPI(Client client)
            : base(client)
        {
        }

        /// <summary>
        /// 获取用户所在的团队
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Team> GetTeams()
        {
            string result = Client.HttpGetRequest("/teams");
            return JsonConvert.DeserializeObject<List<Team>>(result);
        }

        /// <summary>
        /// 获取团队信息
        /// </summary>
        /// <param name="team_id">团队team_id</param>
        /// <returns></returns>
        public Team GetTeamInfo(string team_id)
        {
            string result = Client.HttpGetRequest("/teams/:team_id", new WorktileParameter("team_id", team_id));
            return JsonConvert.DeserializeObject<Team>(result);
        }

        /// <summary>
        /// 获取团队成员
        /// </summary>
        /// <param name="team_id">团队team_id</param>
        /// <returns></returns>
        public IEnumerable<Member> GetTeamMembers(string team_id)
        {
            string result = Client.HttpGetRequest("/teams/:team_id/members", new WorktileParameter("team_id", team_id));
            return JsonConvert.DeserializeObject<List<Member>>(result);
        }


        /// <summary>
        /// 获取团队所有项目
        /// </summary>
        /// <param name="team_id">团队team_id</param>
        /// <returns></returns>
        public IEnumerable<ProjectAPI> GetTeamProjects(string team_id)
        {
            string result = Client.HttpGetRequest("/teams/:team_id/projects", new WorktileParameter("team_id", team_id));
            return JsonConvert.DeserializeObject<List<ProjectAPI>>(result);
        }
    }
}
