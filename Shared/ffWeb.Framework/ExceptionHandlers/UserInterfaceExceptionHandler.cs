using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ffWeb.Framework.ExceptionTypes; 

namespace ffWeb.Framework.ExceptionHandlers
{
   public static class UserInterfaceExceptionHandler
   {

      public static bool HandleExcetion(ref System.Exception ex)
      {
         bool rethrow = false;
         try
         {
            if (ex is BaseException)
            {
               // do nothing as Data Access or Business Logic exception has already been logged / handled
            }
            else
            {
               rethrow = ExceptionPolicy.HandleException(ex, "UserInterfacePolicy");
            }
         }
         catch (Exception exp)
         {
            ex = exp;
         }
         return rethrow;
      }
   }
}
