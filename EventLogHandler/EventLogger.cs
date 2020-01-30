using System;
using System.IO;
using System.Web.Configuration;
using System.Configuration;

namespace EventLogHandler
{
    /// <summary>
    /// This class is designed to write events(Error/Warning/Information) into log file
    /// Consuming application should specify AppSettings["ErrorLogsPath"] in its config file
    /// else error log file will be create in root folder of consuming application
    /// </summary>
    public class EventLogger
    {
        string FileName = DateTime.Now.ToString("ddMMyyyy") + ".txt";
        string ErrorLogsPath = WebConfigurationManager.AppSettings["ErrorLogsPath"];

        /// <summary>
        /// This method will write Exception event in log file
        /// </summary>
        /// <param name="ExceptionDetails"></param>
        /// <param name="ex"></param>
        public void LogException(string ExceptionDetails, Exception ex)
        {
            try
            {
                //It is advised to have ErrorLog folder already created and path to be mention in config file of consuming application
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(this.ErrorLogsPath));

                //Append error log
                FileStream FS = new FileStream(System.Web.HttpContext.Current.Server.MapPath(ErrorLogsPath + "\\" + FileName), FileMode.Append, FileAccess.Write);
                StreamWriter SW = new StreamWriter(FS);
                SW.Write(Environment.NewLine + "========== Exception ==========" + Environment.NewLine + ExceptionDetails + Environment.NewLine 
                    + " :Exception Message:: " + ex.Message + Environment.NewLine + " :StackTrace:: " + ex.StackTrace);
                SW.Close();
            }
            catch (Exception exp)
            {
                System.Web.HttpContext.Current.Response.Write("Error while saving LogException");
            }
        }

        /// <summary>
        /// This method will write Error event in log file
        /// </summary>
        /// <param name="ErrorLog"></param>
        public void LogError(string ErrorLog)
        {
            try
            {
                //It is advised to have ErrorLog folder already created and path to be mention in config file of consuming application
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(this.ErrorLogsPath));

                //Append error log
                FileStream FS = new FileStream(System.Web.HttpContext.Current.Server.MapPath(this.ErrorLogsPath + "\\" + this.FileName), FileMode.Append, FileAccess.Write);
                StreamWriter SW = new StreamWriter(FS);
                SW.Write(Environment.NewLine + "========== ERROR ==========" + Environment.NewLine + ErrorLog + Environment.NewLine);
                SW.Close();
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("Error while saving LogError");
            }
        }

        /// <summary>
        /// This method will write Warning event in log file
        /// </summary>
        /// <param name="WarningLog"></param>
        public void LogWarning(string WarningLog)
        {
            try
            {
                //It is advised to have ErrorLog folder already created and path to be mention in config file of consuming application
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(this.ErrorLogsPath));

                //Append error log
                FileStream FS = new FileStream(System.Web.HttpContext.Current.Server.MapPath(ErrorLogsPath + "\\" + FileName), FileMode.Append, FileAccess.Write);
                StreamWriter SW = new StreamWriter(FS);
                SW.Write(Environment.NewLine + "========== Warning ==========" + Environment.NewLine + WarningLog + Environment.NewLine);
                SW.Close();
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("Error while saving LogWarning");
            }
        }

        /// <summary>
        /// This method will write Information if any in log file
        /// </summary>
        /// <param name="InfoLog"></param>
        public void LogInformation(string InfoLog)
        {
            try
            {
                //It is advised to have ErrorLog folder already created and path to be mention in config file of consuming application
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(this.ErrorLogsPath));

                //Append error log
                FileStream FS = new FileStream(System.Web.HttpContext.Current.Server.MapPath(ErrorLogsPath + "\\" + FileName), FileMode.Append, FileAccess.Write);
                StreamWriter SW = new StreamWriter(FS);
                SW.Write(Environment.NewLine + "========== Information ==========" + Environment.NewLine + InfoLog + Environment.NewLine);
                SW.Close();
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("Error while saving LogInformation");
            }
        }
    }
}
