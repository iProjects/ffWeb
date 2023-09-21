using System; 
using System.Collections; 
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO; 
using System.Linq;
using System.Management;
using System.Management.Instrumentation;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading; 
using System.Xml;  
using Microsoft.Win32;

namespace ffWeb.UI.MVC
{
    public static class Utils
    {
        public static string NextSeries(string from)
        {
            //string from is a string of the form STRNUM, STR_NUM,etc
            //common this is that is has a number ending
            return AddToStringId(from, 1);
        }
        public static string AddToStringId(string id, int summand)
        {
            // set the begin-pointer of for the number to the end of the original id
            var intPos = id.Length;
            // go back from end of id to the begin while a char is a number
            for (int i = id.Length - 1; i >= 0; i--)
            {
                var charTmp = id.Substring(i, 1).ToCharArray()[0];
                if (char.IsNumber(charTmp))
                {
                    // set the position one element back
                    intPos--;
                }
                else
                {
                    // we found a char and so we can break up
                    break;
                }
            }

            var numberString = string.Empty;
            if (intPos < id.Length)
            {
                // the for-loop has found at least one numeric char at the end
                numberString = id.Substring(intPos, id.Length - intPos);
            }

            if (numberString.Length == 0)
            {
                // no number was found at the and so we simply add the summand as string
                id += summand.ToString();
            }
            else
            {
                // cut off the id-string up to the last char before the number at the end
                id = id.Substring(0, id.Length - numberString.Length);
                // add the Increment-operation-result to the end of the id-string and replace
                // the value which stood there before
                try
                {
                    id += (int.Parse(numberString) + summand).ToString();
                }
                catch (OverflowException ex)
                {
                    //too large, reset
                    string msg = ex.Message;
                    id += summand.ToString();
                }
            }
            // return the result
            return id;
        }


        #region Resources
        /// <summary>
        /// Takes the full name of a resource and loads it in to a stream.
        /// </summary>
        /// <param name="resourceName">Assuming an embedded resource is a file
        /// called info.png and is located in a folder called Resources, it
        /// will be compiled in to the assembly with this fully qualified
        /// name: Full.Assembly.Name.Resources.info.png. That is the string
        /// that you should pass to this method.</param>
        /// <returns></returns>
        public static Stream GetEmbeddedResourceStream(string resourceName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }

        /// <summary>
        /// Get the list of all emdedded resources in the assembly.
        /// </summary>
        /// <returns>An array of fully qualified resource names</returns>
        public static string[] GetEmbeddedResourceNames()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceNames();
        }

        public static string AssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }


        #endregion

        #region  "Helpers"
        public static string ConvertFirstLetterToUpper(string s)
        {
            try
            {
                System.Globalization.CultureInfo c = new System.Globalization.CultureInfo("en-us", false);
                System.Globalization.TextInfo t = c.TextInfo;

                return t.ToTitleCase(s);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }
        public static bool SendEmail(string template)
        {
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add("calvinr451@gmail.com");
                message.Subject = "soft books school administrator follow up [ " + DateTime.Now.ToString() + " ]";
                message.From = new System.Net.Mail.MailAddress("kevinmk30@gmail.com");
                message.Body = template;
                message.ReplyToList.Add("kevinmk30@gmail.com");
                message.Bcc.Add(
"brianmatin@gmail.com,brianmatin6@gmail.com,brianmatin8@gmail.com, brnkevin65@gmail.com,bkevin719@gmail.com,bkevin812@gmail.com,calvinr451@gmail.com, kevinmatin4@gmail.com, kevinmatin5@gmail.com, kevinmatin6@gmail.com, kevinmk30@gmail.com, nikevnitam@gmail.com, matinbrian5@gmail.com,matinbrian6@gmail.com");

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.UseDefaultCredentials = false;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = "kevinmk30@gmail.com";
                NetworkCred.Password = "kevinbrian";
                smtp.Credentials = NetworkCred;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Timeout = 5000000;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        public static string ReadLogFile()
        {
            try
            {
                string content = null;
                string inputPath = ("Logs/error.txt");
                if (File.Exists(inputPath))
                {
                    using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                        {
                            content = reader.ReadToEnd();
                        }
                    }
                }
                return content;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }
        public static bool IsConnectedToInternet()
        {
            try
            {
                WebClient client = new WebClient();
                if (client.DownloadString("http://www.google.com/") != null)
                {

                }
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }
        public static string GetFormattedIpAddresses()
        {
            try
            {
                string formattedipaddresses = null;
                int adrresscounter = 0;
                IPAddress[] ipAddresses = Dns.GetHostAddresses(FQDN.GetFQDN()).Reverse().ToArray();
                foreach (var ip in ipAddresses)
                {
                    adrresscounter++;
                    formattedipaddresses += adrresscounter + " [ " + ip.ToString() + " ],  ";
                }
                return formattedipaddresses;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }
        public static string GetMACAddress()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String sMacAddress = string.Empty;
                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == String.Empty)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }
                return sMacAddress;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }
        public static bool LogEventViewer(Exception ex)
        {
            try
            {
                int log_eventid = DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Minute;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("ERROR OCCOURED AT : " + DateTime.Now.ToString());
                sb.AppendLine("SOURCE : " + ex.Source);
                sb.AppendLine("MESSAGE : " + ex.Message);
                if (ex.InnerException != null)
                {
                    sb.AppendLine("INNER EXCEPTION : " + ex.InnerException.Message);
                }
                if (ex.StackTrace != null)
                {
                    sb.AppendLine("STACK TRACE : " + ex.StackTrace);
                }
                sb.AppendLine("WHOLE EXCEPTION : " + ex.ToString());
                string msg = sb.ToString();

                String eventsourceName = "Soft Books School";
                String logName = "SB School";
                if (!EventLog.SourceExists(eventsourceName))
                {
                    EventLog.CreateEventSource(eventsourceName, logName);
                }
                EventLog myLog = new EventLog();
                myLog.Source = eventsourceName;
                myLog.MachineName = Environment.MachineName;
                myLog.WriteEntry(msg, EventLogEntryType.Information, log_eventid);

                return true;
            }
            catch (Exception exc)
            {
                string msg = exc.Message;
                return false;
            }
        }

    }

    public static class FQDN
    {
        public static string GetFQDN()
        {
            string domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            string hostName = System.Net.Dns.GetHostName();
            string fqdn = "";
            if (!hostName.Contains(domainName))
                fqdn = hostName + "." + domainName;
            else
                fqdn = hostName;
            return fqdn;
        }
    }

    public static class CheckInternet
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        //Creating a function that uses the API function...
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }
    }
        #endregion  "Helpers"

}