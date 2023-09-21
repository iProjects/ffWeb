using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ffWeb.Framework.ExceptionTypes
{


    [Serializable]
    public class PostingException : BusinessLogicCustomException
    {
        public PostingException()
            : base() { }

        public PostingException(string message)
            : base(message) { }

        public PostingException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public PostingException(string message, Exception innerException)
            : base(message, innerException) { }

        public PostingException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected PostingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }


    [Serializable]
    public class LimitException : PostingException
    {
        public LimitException()
            : base() { }

        public LimitException(string message)
            : base(message) { }

        public LimitException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public LimitException(string message, Exception innerException)
            : base(message, innerException) { }

        public LimitException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected LimitException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class AccountStatusException : PostingException
    {
        public AccountStatusException()
            : base() { }

        public AccountStatusException(string message)
            : base(message) { }

        public AccountStatusException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public AccountStatusException(string message, Exception innerException)
            : base(message, innerException) { }

        public AccountStatusException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected AccountStatusException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
