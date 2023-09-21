using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ffWeb.Framework.ExceptionTypes;

namespace ffWeb.Framework.ExceptionHandlers
{
    public static class BusinessLogicExceptionHandler
    {
        public static bool HandleException(ref System.Exception ex)
        {
            bool rethrow = false;
            if ((ex is DataAccessException) || (ex is DataAccessCustomException))
            {
                rethrow = ExceptionPolicy.HandleException(ex, "PassThroughPolicy");
                ex = new PassThroughException(ex.Message);
            }
            else if (ex is BusinessLogicCustomException)
            {
                rethrow = ExceptionPolicy.HandleException(ex, "BusinessLogicCustomPolicy");
            }
            else
            {
                rethrow = ExceptionPolicy.HandleException(ex, "BusinessLogicPolicy");
            }
            if (rethrow)
            {
                throw ex;
            }
            return rethrow;
        }



    }
}