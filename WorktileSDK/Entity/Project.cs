using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK.Entity
{
    public class Project
    {
        public string pid { get; set; }
        public string name { get; set; }
        public string team_id { get; set; }
        public string desc { get; set; }
        public int archived { get; set; }
        public string pic { get; set; }
        public string bg { get; set; }
        public int visibility { get; set; }
        public int is_star { get; set; }
        public double pos { get; set; }
        public int member_count { get; set; }
        public int curr_role { get; set; }
        public int permission { get; set; }
    }
}
