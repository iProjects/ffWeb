using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ffWeb.Framework.ExceptionTypes
{
   public class UserInterfaceException : System.Exception, ISerializable
   {
      public UserInterfaceException()
         : base()
      { 
         // Add implementation (if required)
      }

      public UserInterfaceException(string message)
         : base(message)
      { 
         // Add implemenation (if required)
      }

      public UserInterfaceException(string message, System.Exception inner)
         : base(message, inner)
      { 
         // Add implementation
      }

      protected UserInterfaceException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      { 
         //Add implemenation
      }
   }
}
