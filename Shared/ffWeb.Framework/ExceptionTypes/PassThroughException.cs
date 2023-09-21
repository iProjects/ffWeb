using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ffWeb.Framework.ExceptionTypes
{
   public class PassThroughException : BaseException, ISerializable
   { 
      public PassThroughException()
         : base()
      { 
         // Add implementation (if required)
      }

      public PassThroughException(string message)
         : base(message)
      { 
         // Add implemenation (if required)
      }

      public PassThroughException(string message, System.Exception inner)
         : base(message, inner)
      { 
         // Add implementation
      }

      protected PassThroughException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      { 
         //Add implemenation
      }
   }
}
