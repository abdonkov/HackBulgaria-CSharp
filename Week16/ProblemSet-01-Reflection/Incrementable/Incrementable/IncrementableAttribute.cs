using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incrementable
{
    public class IncrementableAttribute : Attribute
    {
        public IncrementableAttribute()
        {

        }
    }


    [Serializable]
    public class NonIncrementablePropertyTypeException : Exception
    {
        public NonIncrementablePropertyTypeException() : base("Property is not of incrementable type!") { }
        public NonIncrementablePropertyTypeException(string message) : base(message) { }
        public NonIncrementablePropertyTypeException(string message, Exception inner) : base(message, inner) { }
        protected NonIncrementablePropertyTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
