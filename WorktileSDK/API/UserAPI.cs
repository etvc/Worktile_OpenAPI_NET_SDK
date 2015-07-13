using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorktileSDK.Entity;

namespace WorktileSDK.API
{
    public class UserAPI : BaseAPI
    {

        public UserAPI(Client client)
            : base(client)
        {

        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public User GetUserProfile()
        {
            string result = Client.HttpGetRequest("/user/profile"); ;
            return JsonConvert.DeserializeObject<User>(result);
        }
    }
}
