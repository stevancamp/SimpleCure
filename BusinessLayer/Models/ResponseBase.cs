﻿using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class ResponseBase
    {
        public string ResponseMessage { get; set; }
        public bool ResponseSuccess { get; set; }
        public int ResponseInt { get; set; }
        public string ResponseString { get; set; }
        public List<string> ResponseListString { get; set; }
        public List<int> ResponseListInt { get; set; }
        public ResponseTypes responseTypes { get; set; }
    }
}