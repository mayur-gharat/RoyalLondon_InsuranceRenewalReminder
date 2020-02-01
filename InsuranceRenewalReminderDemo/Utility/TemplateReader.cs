using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using EventLogHandler;

namespace InsuranceRenewalReminder
{
    /// <summary>
    /// Created Singonton pattern for reading template as it will be common for all users
    /// </summary>
    public sealed class TemplateReader
    {
        TemplateReader()
        { }

        private static readonly object padlock = new object();
        private static TemplateReader instance = null;

        public static TemplateReader GetInstance()
        {
            {
                lock(padlock)
                {
                    if(instance == null)
                    {
                        instance = new TemplateReader();
                    }
                    return instance;
                }
            }
        }

        public string ReadTemplateFile()
        {

            const string MethodName = "InsuranceRenewalReminder::UIHelper::ReadTemplateFile:: ";
            string TemplateContent = string.Empty;
            EventLogger EventLog = new EventLogger();

            try
            {
                //Get input template path
                string TemplateFile = WebConfigurationManager.AppSettings["InsuranceRenewalReminderTemplateFile"];

                //Read and return template file
                using (StreamReader Reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(TemplateFile), System.Text.UTF8Encoding.UTF7))
                {
                    TemplateContent = Reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                //Log error
                EventLog.LogException(MethodName, ex);
            }

            return TemplateContent;

        }
    }
}