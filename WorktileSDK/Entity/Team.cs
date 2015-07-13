using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK.Entity
{
    public class Team
    {
        public string team_id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string pic { get; set; }
        public string desc { get; set; }
        public DateTime created_at { get; set; }
        public int visibility { get; set; }
        public created_by created_by { get; set; }
    }

    public class created_by
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public string avatar { get; set; }
        public string desc { get; set; }
        public int status { get; set; }
        public int online { get; set; }
    }

    public class Member
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public string avatar { get; set; }
        public int online { get; set; }
        public int status { get; set; }
        public string email { get; set; }
        public string desc { get; set; }
        public int role { get; set; }
        public DateTime join_date { get; set; }
    }

    public class MemberOfAdd
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public string avatar { get; set; }
        public string desc { get; set; }
        public int status { get; set; }
        public int online { get; set; }
        public int role { get; set; }
    }
}
