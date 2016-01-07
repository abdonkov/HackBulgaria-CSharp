using System;

namespace XmlLibrary
{
    [Serializable]
    public class XmlMarkupBuilderFinalizedException : ApplicationException
    {
        public XmlMarkupBuilderFinalizedException() : this("Used operation on finalized object!") { }
        public XmlMarkupBuilderFinalizedException(string message) : base(message) { }
        public XmlMarkupBuilderFinalizedException(string message, Exception inner) : base(message, inner) { }
        protected XmlMarkupBuilderFinalizedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class XmlMarkupNoRootXmlObjectException : ApplicationException
    {
        public XmlMarkupNoRootXmlObjectException() : this("No root XML object!") { }
        public XmlMarkupNoRootXmlObjectException(string message) : base(message) { }
        public XmlMarkupNoRootXmlObjectException(string message, Exception inner) : base(message, inner) { }
        protected XmlMarkupNoRootXmlObjectException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class XmlMarkupNoOpenedTagException : ApplicationException
    {
        public XmlMarkupNoOpenedTagException() : this("No oppened tag!") { }
        public XmlMarkupNoOpenedTagException(string message) : base(message) { }
        public XmlMarkupNoOpenedTagException(string message, Exception inner) : base(message, inner) { }
        protected XmlMarkupNoOpenedTagException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
