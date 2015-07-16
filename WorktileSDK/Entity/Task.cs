using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK.Entity
{
    public class Task
    {
        public string name { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string entry_id { get; set; }
        public string entry_name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int pos { get; set; }
        public IEnumerable<label> labels { get; set; }
        public IEnumerable<todo> todos { get; set; }
        public badges badges { get; set; }
        public IEnumerable<watcher> watchers { get; set; }
        public IEnumerable<member> members { get; set; }
        public int completed { get; set; }
        public DateTime expire_date { get; set; }
        public string desc { get; set; }
        public project project { get; set; }
    }

    public class label
    {
        public string name { get; set; }
        public string desc { get; set; }
    }

    public class todo
    {
        public string todo_id { get; set; }
        public string name { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("checked")]
        public int check { get; set; }
        public int pos { get; set; }
    }

    public class badges
    {
        public DateTime expire_date { get; set; }
        public int comment_count { get; set; }
        public int todo_checked_count { get; set; }
        public int todo_count { get; set; }
        public int file_count { get; set; }
    }

    public class watcher
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public string avatar { get; set; }
        public string desc { get; set; }
        public int status { get; set; }
        public int online { get; set; }
    }

    public class member
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public string avatar { get; set; }
        public string desc { get; set; }
        public int status { get; set; }
        public int online { get; set; }
    }

    public class project
    {
        public string pid { get; set; }
        public string name { get; set; }
        public string pic { get; set; }
        public string bg { get; set; }
    }

    public class file
    {
        public string fid { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string pid { get; set; }
        public int size { get; set; }
        public string path { get; set; }
        public string folder_id { get; set; }
    }

}
