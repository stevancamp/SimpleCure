using System.Collections.Generic;

namespace Library
{
    public class ResponseBase
    {
        public string ResponseMessage { get; set; }
        public bool ResponseSuccess { get; set; }
        public int ResponseInt { get; set; }
        public string ResponseString { get; set; }
        public List<string> ResponseListString { get; set; }
        public List<int> ResponseListInt { get; set; }
    }
}
