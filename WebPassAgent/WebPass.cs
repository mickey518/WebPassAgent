using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPassAgent
{
    public class WebPass
    {
        public WebPass()
        {
            DisaplyPwd = "***";
        }
        public Guid Id { get; set; }
        public string Host { get; set; }
        public string Uri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisaplyPwd { get; set; }
    }
}
