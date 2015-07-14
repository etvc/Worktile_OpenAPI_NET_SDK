using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK.Entity
{
    public class Entry
    {
        public string entry_id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool watched { get; set; }
    }

    public class EntryOfAdd
    {
        public string entry_id { get; set; }
        public string name { get; set; }
        public string pid { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
