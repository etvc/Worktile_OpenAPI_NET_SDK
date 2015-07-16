using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK.Entity
{
    public class Comment
    {
        public string cid { get; set; }
        public string message { get; set; }
        public string raw_message { get; set; }
        public int type { get; set; }
        public int format { get; set; }
        public string[] fids { get; set; }
        public member owner { get; set; }
        public IEnumerable<file> files { get; set; }
        public DateTime created_at { get; set; }
    }
}
