using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktileSDK
{
    public class Error
    {
        public string error_code
        {
            get;
            set;
        }

        public string request
        {
            get;
            set;
        }

        public string error_message
        {
            get;
            set;
        }
    }
}
