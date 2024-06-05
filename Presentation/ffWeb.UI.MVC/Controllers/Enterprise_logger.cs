using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ffWeb.UI.MVC
{
    public class Enterprise_logger
    {
        protected LogWriter log_writer;

        public Enterprise_logger()
        {
            log_writer = new LogWriterFactory().Create();
            Logger.SetLogWriter(log_writer, false);
        }

        public LogWriter logWriter
        {
            get
            {
                return log_writer;
            }
        }

    }
}