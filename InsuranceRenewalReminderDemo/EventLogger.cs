using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;

namespace InsuranceRenewalReminderDemo
{
    public static class EventLogger
    {
        public static void LogError(string ErrorLog)
        {
            try
            {
                //Create customised Output file name
                string FileName = DateTime.Now.ToString("ddMMyyyy") + ".txt";
                string ErrorLogsPath = WebConfigurationManager.AppSettings["ErrorLogsPath"];

                //Append error log
                FileStream FS = new FileStream(System.Web.HttpContext.Current.Server.MapPath(ErrorLogsPath + "\\" + FileName), FileMode.Append, FileAccess.Write);
                StreamWriter SW = new StreamWriter(FS);
                SW.Write(Environment.NewLine  + "========== ERROR ==========" + Environment.NewLine + ErrorLog + Environment.NewLine);
                SW.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public static void LogWarning(string WarningLog)
        {
            try
            {
                //Create customised Output file name
                string FileName = DateTime.Now.ToString("ddMMyyyy") + ".txt";
                string ErrorLogsPath = WebConfigurationManager.AppSettings["ErrorLogsPath"];

                //Append error log
                FileStream FS = new FileStream(System.Web.HttpContext.Current.Server.MapPath(ErrorLogsPath + "\\" + FileName), FileMode.Append, FileAccess.Write);
                StreamWriter SW = new StreamWriter(FS);
                SW.Write(Environment.NewLine + "========== Warning ==========" + Environment.NewLine + WarningLog + Environment.NewLine);
                SW.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public static void LogInformation(string InfoLog)
        {
            try
            {
                //Create customised Output file name
                string FileName = DateTime.Now.ToString("ddMMyyyy") + ".txt";
                string ErrorLogsPath = WebConfigurationManager.AppSettings["ErrorLogsPath"];

                //Append error log
                FileStream FS = new FileStream(System.Web.HttpContext.Current.Server.MapPath(ErrorLogsPath + "\\" + FileName), FileMode.Append, FileAccess.Write);
                StreamWriter SW = new StreamWriter(FS);
                SW.Write(Environment.NewLine + "========== Information ==========" + Environment.NewLine + InfoLog + Environment.NewLine);
                SW.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}