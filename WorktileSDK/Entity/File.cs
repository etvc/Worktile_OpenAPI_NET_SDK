using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK.Entity
{
    public class File
    {
        public string fid { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string pid { get; set; }
        public string size { get; set; }
        public string path { get; set; }
        public string folder_id { get; set; }
        public string folder_name { get; set; }
        public int type { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public IEnumerable<watcher> watchers { get; set; }
        public member owner { get; set; }
    }

    public class Image
    {
        public string fid { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string pid { get; set; }
        public string size { get; set; }
        public string path { get; set; }
        public string folder_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
