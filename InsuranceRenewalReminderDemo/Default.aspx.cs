using System;
using System.Web.Configuration;
using System.Web.UI;
using InsuranceRenewalCalculator;

namespace InsuranceRenewalReminder
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInput_Click(object sender, EventArgs e)
        {
            //Reset notification message
            lblResult.Text = "";
            lblResult.ForeColor = System.Drawing.Color.Black;

            String FilePath = Server.MapPath(WebConfigurationManager.AppSettings["InputFilePath"] + "\\" + flupload.FileName);
            if(flupload.HasFile)
            {
                //Upload input file
                flupload.SaveAs(FilePath);
            }
            else
            {
                lblResult.Text = "Please select appropreate Input file.";
                lblResult.ForeColor = System.Drawing.Color.Red;
            }

            //Generate Files
            System.IO.FileInfo file = new System.IO.FileInfo(FilePath);
            if (file.Exists)
            {
                UIHelper Helper = new UIHelper();
                ResponseBase result = Helper.CreateOutputFiles(Helper.GetInputFields(FilePath));

                //Show notification
                if (result == null)
                {
                    lblResult.Text = "Failed!!";
                    lblResult.ForeColor = System.Drawing.Color.Red;
                }
                else if (result.ReturnCode < 0)
                {
                    lblResult.Text = result.ReturnMessage;
                    lblResult.ForeColor = System.Drawing.Color.Red;
                }
                else if (result.ReturnCode > 0)
                {
                    lblResult.Text = result.ReturnMessage;
                    lblResult.ForeColor = System.Drawing.Color.Orange;
                }
                else
                {
                    lblResult.Text = "Success!!, Files generated at " + Server.MapPath(WebConfigurationManager.AppSettings["OutputFilePath"]);
                    if (!string.IsNullOrWhiteSpace(result.ReturnMessage))
                    {
                        lblResult.Text = lblResult.Text + "<br/>" + result.ReturnMessage;
                    }
                    lblResult.ForeColor = System.Drawing.Color.Green;
                }

                //Delete uploaded file after finish
                file.Delete();
            }
        }
    }
}