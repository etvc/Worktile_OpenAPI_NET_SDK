using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK.Entity
{
    public class Post
    {
        public string post_id { get; set; }
        public string pid { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public string content { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public member owner { get; set; }
        public IEnumerable<watcher> watchers { get; set; }
    }
}
