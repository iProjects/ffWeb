using fPeerLending.Business;
using fPeerLending.Entities;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging; 
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters; 
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ffWeb.UI.MVC.Models
{


    [EventSource(Name = "fanikiwa-ffweb")]
    public class BasicErrorLogger : EventSource
    {
        public static readonly BasicErrorLogger Log = new BasicErrorLogger();

        [Event(1, Message = "{0}", Level = EventLevel.Critical)]
        public void Critical(string message)
        {
            if (IsEnabled())
            WriteEvent(1, message);
        }

        [Event(2, Message = "{0}", Level = EventLevel.Error)]
        public void Error(string message)
        {
            if (IsEnabled()) 
                WriteEvent(2, message);
        }

        [Event(3, Message = "{0}", Level = EventLevel.Warning)]
        public void Warning(string message)
        {
            if (IsEnabled())
                WriteEvent(3, message);
        }

    }



    [EventSource(Name = "fanikiwa-ffweb")]
    internal sealed class LogErrorEventSource : EventSource
    {
        [Event(1, Level = EventLevel.Informational, Keywords = Keywords.Loader | Keywords.Critical)]
        public void Load(long ImageBase, string Name)
        { 
            if (IsEnabled()) WriteEvent(1, ImageBase, Name);
        }

        [Event(2, Level = EventLevel.Verbose, Keywords = Keywords.Loader)]
        public void Unload(long ImageBase) 
        {
            if (IsEnabled()) WriteEvent(2, ImageBase);
        }

        public class Keywords
        {
            public const EventKeywords Loader = (EventKeywords)0x0001;
            public const EventKeywords Critical = (EventKeywords)0x0002;
        }
    }


    enum MyColor { Red, Yellow, Blue };

    [EventSource(Name = "fanikiwa-ffweb")]
    public class MyCompanyEventSource : EventSource
    {
        public class Keywords
        {
            public const EventKeywords Page = (EventKeywords)1;
            public const EventKeywords DataBase = (EventKeywords)2;
            public const EventKeywords Diagnostic = (EventKeywords)4;
            public const EventKeywords Perf = (EventKeywords)8;
        }

        public class Tasks
        {
            public const EventTask Page = (EventTask)1;
            public const EventTask DBQuery = (EventTask)2;
        }

        [Event(1, Message = "Application Falure: {0}", Level = EventLevel.Error, Keywords = Keywords.Diagnostic)]
        public void Failure(string message)
        {
            WriteEvent(1, message); 
        }

        [Event(2, Message = "Starting up.", Keywords = Keywords.Perf, Level = EventLevel.Informational)]
        public void Startup() 
        {
            WriteEvent(2); 
        }

        [Event(3, Message = "loading page {1} activityID={0}", Opcode = EventOpcode.Start,
            Task = Tasks.Page, Keywords = Keywords.Page, Level = EventLevel.Informational)]
        public void PageStart(int ID, string url) 
        {
            if (IsEnabled()) WriteEvent(3, ID, url); 
        }

        [Event(4, Opcode = EventOpcode.Stop, Task = Tasks.Page, Keywords = Keywords.Page, Level = EventLevel.Informational)]
        public void PageStop(int ID)
        {
            if (IsEnabled()) WriteEvent(4, ID); 
        }

        [Event(5, Opcode = EventOpcode.Start, Task = Tasks.DBQuery, Keywords = Keywords.DataBase, Level = EventLevel.Informational)]
        public void DBQueryStart(string sqlQuery)
        {
            WriteEvent(5, sqlQuery); 
        }

        [Event(6, Opcode = EventOpcode.Stop, Task = Tasks.DBQuery, Keywords = Keywords.DataBase, Level = EventLevel.Informational)]
        public void DBQueryStop()
        {
            WriteEvent(6); 
        }

        [Event(7, Level = EventLevel.Verbose, Keywords = Keywords.DataBase)]
        public void Mark(int ID)
        {
            if (IsEnabled()) WriteEvent(7, ID); 
        }

        //[Event(8)]
        //public void LogColor(MyColor color) { WriteEvent(8, (int)color); }

        public static MyCompanyEventSource Log = new MyCompanyEventSource();
    }


    [RunInstaller(true)]
    public class MyEventLogInstaller : Installer
    {
        private EventLogInstaller myEventLogInstaller;

        public MyEventLogInstaller()
        {
            // Create an instance of an EventLogInstaller.
            myEventLogInstaller = new EventLogInstaller();

            // Set the source name of the event log.
            myEventLogInstaller.Source = "NewLogSource";

            // Set the event log that the source writes entries to.
            myEventLogInstaller.Log = "MyNewLog";

            // Add myEventLogInstaller to the Installer collection.
            Installers.Add(myEventLogInstaller);
        }

        public static MyEventLogInstaller Log = new MyEventLogInstaller();
    }

}