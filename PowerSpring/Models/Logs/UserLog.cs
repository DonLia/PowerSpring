using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Logs
{
    public class UserLog
    {   
        public int Id { get; set; }
        public string Time { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public string Table { get; set; }
        public string Attribute { get; set; }
        public string Value { get; set; }
    }
}
