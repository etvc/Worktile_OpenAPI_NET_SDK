using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK.Entity
{
    public class Event
    {
        public string event_id { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public string location { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public int recurrence { get; set; }
        public IEnumerable<member> attendees { get; set; }
        public project project { get; set; }
    }
}
