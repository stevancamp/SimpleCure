using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class Generic<T> : ResponseBase
    {
        public List<T> GenericClassList { get; set; } = new List<T>();
        public T GenericClass { get; set; }            
    }
}
