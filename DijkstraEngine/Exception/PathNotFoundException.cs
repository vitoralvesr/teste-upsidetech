using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraEngine.Exception
{
    [Serializable]
    public class PathNotFoundException : System.Exception
    {
        public PathNotFoundException() { }
        public PathNotFoundException(string message) : base(message) { }
        public PathNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected PathNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
