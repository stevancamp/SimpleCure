using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCure.Models
{
    public class ResponeBase
    {
        public string ResponseMessage { get; set; }
        public bool ResponseSuccess { get; set; }
        public int ResponseInt { get; set; }
        public string ResponseString { get; set; }
        public List<string> ResponseListString { get; set; }
        public List<int> ResponseListInt { get; set; }
    }
}