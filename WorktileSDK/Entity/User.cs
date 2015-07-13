using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK.Entity
{
    public class User
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public string email { get; set; }
        public string desc { get; set; }
        public string avatar { get; set; }
        public int status { get; set; }
        public int online { get; set; }
    }
}
