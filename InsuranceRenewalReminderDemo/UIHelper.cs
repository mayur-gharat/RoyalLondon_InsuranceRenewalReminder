using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using InsuranceRenewalCalculator;

namespace InsuranceRenewalReminder
{
    /// <summary>
    /// This is Helper / Utility class for methods of UI Project
    /// </summary>
    public class UIHelper
    {
        public List<InputField> GetInputFields(string FilePath)
        {
            List<InputField> InputFields = null;

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
                                Console.WriteLine("Invalid PayoutAmount, Skip this row of ID = " + IpField.ID);
                                Counter++;
                                continue;
                            }
                            if (double.TryParse(Values[6], out IpField.AnnualPremium) == false)
                            {
                                Console.WriteLine("Invalid AnnualPremium, Skip this row of ID = " + IpField.ID);
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
                    //Show messge File not found
                    Console.WriteLine("Input file not found");
                }
            }  
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return InputFields;

        }

        public ResponseBase CreateOutputFiles(List<InputField> InputFields)
        {
            ResponseBase Response = new ResponseBase();
            try
            {
                bool FileCreated = false;
                //Negative use case checking
                if (InputFields == null || InputFields.Count == 0)
                {
                    Console.WriteLine("No Inputs received");
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

                        FileStream FS = new FileStream(System.Web.HttpContext.Current.Server.MapPath(OutputFilePath + "\\" + FileName), FileMode.Create, FileAccess.Write);
                        StreamWriter SW = new StreamWriter(FS);
                        SW.Write(FinalContent.ToString());
                        SW.Close();
                        FileCreated = true;
                    }
                    else
                    {
                        Response.ReturnCode = 1;
                        Response.ReturnMessage = Response.ReturnMessage + "<br/>" + "Record for " + FileName  + " already Present, No updates made.";
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
                Console.WriteLine(ex.InnerException);
                Response.ReturnCode = -1;
                Response.ReturnMessage = Response.ReturnMessage + Environment.NewLine + "Error!!!";
                return Response;
            }

            return Response;

        }

        public string FillTemplateData(string Templatecontent, InputField InputData)
        {
            //Take template content and fill all data in it.
            //Any change in Template InputFiled will require change in Property Class of InputFeild and mapping in This mathod.

            string FinalContent = Templatecontent;
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
                Console.WriteLine(ex.InnerException);
            }

            return FinalContent;

        }

        public static string ReadTemplateFile()
        {
            string TemplateContent = string.Empty;
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
                Console.WriteLine(ex.InnerException);
            }

            return TemplateContent;

        }
    }
}
