using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using InsuranceRenewalCalculator;
using EventLogHandler;

namespace InsuranceRenewalReminder
{
    /// <summary>
    /// This is Helper / Utility class for methods of UI Project
    /// </summary>
    public class UIHelper
    {
        /// <summary>
        /// This method is useful in getting / reading input CSV file
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public List<InputField> GetInputFields(string FilePath)
        {
            #region Declaration

            const string MethodName = "InsuranceRenewalReminder::UIHelper::GetInputFields::  ";
            List<InputField> InputFields = null;
            EventLogger EventLog = new EventLogger();

            #endregion

            try
            {
                //Set counter on how many files to be generated at max.
                int Counter = 0;
                int MaxInputCount = Convert.ToInt32(WebConfigurationManager.AppSettings["MaxInputFields"]);
                if (!(MaxInputCount > 0)) //Fallback for Counter if missing or invalid in Setting
                {
                    MaxInputCount = 10;
                }

                //Read Input CSV file
                if (File.Exists(FilePath))
                {
                    InputFields = new List<InputField>();

                    using (var Reader = new StreamReader(FilePath))
                    {
                        while ((!Reader.EndOfStream) && Counter < MaxInputCount)
                        {
                            var line = Reader.ReadLine();
                            var Values = line.Split(',');
                            InputField IpField = new InputField();
                            if(Int32.TryParse(Values[0], out IpField.ID) == false)
                            {
                                Console.WriteLine("Invalid ID, Skip this row of ID = " + Values[0]);
                                Counter++;
                                continue;
                            }
                            IpField.Title = Values[1];
                            IpField.FirstName = Values[2];
                            IpField.Surname = Values[3];
                            IpField.ProductName = Values[4];
                            if(double.TryParse(Values[5], out IpField.PayoutAmount) == false)
                            {
                                EventLog.LogWarning(MethodName + "Invalid PayoutAmount, Skip this row of ID = " + IpField.ID);
                                Counter++;
                                continue;
                            }
                            if (double.TryParse(Values[6], out IpField.AnnualPremium) == false)
                            {
                                EventLog.LogWarning(MethodName + "Invalid AnnualPremium, Skip this row of ID = " + IpField.ID);
                                Counter++;
                                continue;
                            }

                            InputFields.Add(IpField);
                            Counter++;
                        }
                    }
                }
                else
                {
                    //Log error Show messge File not found
                    EventLog.LogError(MethodName + "Input file not found");

                }
            }  
            catch(Exception ex)
            {
                //Log error 
                EventLog.LogError(MethodName + ex.Message);
            }

            return InputFields;

        }

        /// <summary>
        /// This method is is main mathod for creating output files
        /// </summary>
        /// <param name="InputFields"></param>
        /// <returns></returns>
        public ResponseBase CreateOutputFiles(List<InputField> InputFields)
        {
            #region Declaration

            const string MethodName = "InsuranceRenewalReminder::UIHelper::CreateOutputFiles::  ";
            ResponseBase Response = new ResponseBase();
            EventLogger EventLog = new EventLogger();

            #endregion

            try
            {
                bool FileCreated = false;
                //Negative use case checking
                if (InputFields == null || InputFields.Count == 0)
                {
                    //Console.WriteLine("No Inputs received");
                    Response.ReturnCode = -1;
                    Response.ReturnMessage = Response.ReturnMessage + Environment.NewLine + "No Inputs received";
                    return Response;
                }
                //Get input template
                string TemplateContent = ReadTemplateFile();

                //Create output files for each input
                foreach(InputField InputData in InputFields)
                {
                    //Create customised Output file name
                    string FileName = InputData.ID + "-" + InputData.FirstName + "_" + InputData.Surname + ".txt";

                    string OutputFilePath = WebConfigurationManager.AppSettings["OutputFilePath"];

                    //Check if Output file not present previously
                    if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(OutputFilePath + "\\" + FileName)))
                    {
                        string FinalContent = FillTemplateData(TemplateContent.ToString(), InputData);
                        //string FinalContent = FormatTemplateData(TemplateContent.ToString(), InputData); // Alternate method with compromised readability.

                        FileStream FS = new FileStream(System.Web.HttpContext.Current.Server.MapPath(OutputFilePath + "\\" + FileName), FileMode.Create, FileAccess.Write);
                        StreamWriter SW = new StreamWriter(FS);
                        SW.Write(FinalContent.ToString());
                        SW.Close();
                        FileCreated = true;
                    }
                    else
                    {
                        //Update response and log warning
                        Response.ReturnCode = 1;
                        Response.ReturnMessage = Response.ReturnMessage + "<br/>" + "Record for " + FileName  + " already Present, No updates made.";

                        EventLog.LogWarning(MethodName + "Record for " + FileName + " already Present, No updates made.");
                    }

                }

                //return success if atleast one fine created.
                if(FileCreated)
                {
                    Response.ReturnCode = 0;
                }
            }
            catch (Exception ex)
            {
                //Log error & Return response
                EventLog.LogError(MethodName + ex.Message);

                Response.ReturnCode = -1;
                Response.ReturnMessage = Response.ReturnMessage + Environment.NewLine + "Error while generating output file!!!";
                return Response;
            }

            return Response;

        }

        /// <summary>
        /// Take template content and fill all data in it.
        /// Any change in Template InputFiled will require change in Property Class of InputFeild and mapping in This mathod.
        /// </summary>
        /// <param name="Templatecontent"></param>
        /// <param name="InputData"></param>
        /// <returns></returns>
        public string FillTemplateData(string Templatecontent, InputField InputData)
        {
            const string MethodName = "InsuranceRenewalReminder::UIHelper::FillTemplateData::  ";
            string FinalContent = Templatecontent;
            EventLogger EventLog = new EventLogger();

            try
            {
                if(!string.IsNullOrWhiteSpace(Templatecontent) && InputData != null)
                {
                    FinalContent = FinalContent.Replace("#CurrentDate#", DateTime.Now.ToString("dd/MM/yyyy"));
                    FinalContent = FinalContent.Replace("#Title#", InputData.Title);
                    FinalContent = FinalContent.Replace("#FirstName#", InputData.FirstName);
                    FinalContent = FinalContent.Replace("#Surname#", InputData.Surname);
                    FinalContent = FinalContent.Replace("#ProductName#", InputData.ProductName);
                    FinalContent = FinalContent.Replace("#PayoutAmount#", Convert.ToString(InputData.PayoutAmount));
                    FinalContent = FinalContent.Replace("#AnnualPremium#", Convert.ToString(InputData.AnnualPremium));
                    FinalContent = FinalContent.Replace("#CreditCharge#", Convert.ToString(InputData.CreditCharge));
                    FinalContent = FinalContent.Replace("#TotalAnnualPremium#", Convert.ToString(InputData.TotalAnnualPremium));
                    FinalContent = FinalContent.Replace("#InitialMonthlyPayment#", Convert.ToString(InputData.InitialMonthlyPayment));
                    FinalContent = FinalContent.Replace("#OtherMonthlyPayment#", Convert.ToString(InputData.OtherMonthlyPayment));
                }
            }
            catch (Exception ex)
            {
                //Log error
                EventLog.LogError(MethodName + ex.Message);
            }

            return FinalContent;

        }

        /// <summary>
        /// Aleternate method for FillTemplateData
        /// Take template content and fill all data in it using Formated tempate and String.Format method.
        /// Any change in Template InputFiled will require change in this mathod.
        /// As we using string.Format here hence code reduced or less than FillTemplateData however Code Readability is compomised here 
        /// as well as any change in template input will require responsible work in this method as well.
        /// 
        /// </summary>
        /// <param name="TemplateContent"></param>
        /// <param name="InputData"></param>
        /// <returns></returns>
        public string FormatTemplateData(string TemplateContent, InputField InputData)
        {
            #region Declaration

            const string MethodName = "InsuranceRenewalReminder::UIHelper::FormatTemplateData::  ";
            string FinalContent = TemplateContent;
            EventLogger EventLog = new EventLogger();

            #endregion

            try
            {
                if (!string.IsNullOrWhiteSpace(TemplateContent) && InputData != null)
                {

                    string[] InputArray = new string[] { DateTime.Now.ToString("dd/MM/yyyy"), InputData.Title, InputData.FirstName, InputData.Surname,
                        InputData.Title, InputData.Surname, InputData.ProductName, Convert.ToString(InputData.PayoutAmount),
                        Convert.ToString(InputData.AnnualPremium),Convert.ToString(InputData.CreditCharge),Convert.ToString(InputData.TotalAnnualPremium),
                        Convert.ToString(InputData.InitialMonthlyPayment),Convert.ToString(InputData.OtherMonthlyPayment) };

                    FinalContent = string.Format(FinalContent, InputArray);

                }
            }
            catch (Exception ex)
            {
                //Log error
                EventLog.LogError(MethodName + ex.Message);
            }

            return FinalContent;

        }

        /// <summary>
        /// Utility method to read the template
        /// Kept static as for now template will be common for all object / user.
        /// </summary>
        /// <returns></returns>
        public static string ReadTemplateFile()
        {

            const string MethodName = "InsuranceRenewalReminder::UIHelper::ReadTemplateFile:: ";
            string TemplateContent = string.Empty;
            EventLogger EventLog = new EventLogger();

            try
            {
                //Get input template path
                string TemplateFile = WebConfigurationManager.AppSettings["InsuranceRenewalReminderTemplateFile"];

                //Read and return template file
                using (StreamReader Reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(TemplateFile)))
                {
                    TemplateContent = Reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                //Log error
                EventLog.LogError(MethodName + ex.Message);
            }

            return TemplateContent;

        }
    }
}
