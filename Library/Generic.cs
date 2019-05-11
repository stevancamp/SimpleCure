using System.Collections.Generic;

namespace Library
{
    public class Generic<T> : ResponseBase
    {
        public List<T> GenericClassList { get; set; }
        public T GenericClass { get; set; }
    }
}
