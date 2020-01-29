using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRenewalCalculator
{
    /// <summary>
    /// This is Type for input field in CSV file as well as field needed in template file
    /// Any change in CSV or template will require revisit to this file
    /// </summary>
    public class InputField
    {
        public int ID;
        public string Title;
        public string FirstName;
        public string Surname;
        public string ProductName;
        public double PayoutAmount;
        public double AnnualPremium;

        public double CreditCharge
        {
            get
            {
                return Helper.CalculateCreditCharge(this.AnnualPremium);
            }
        }
        public double TotalAnnualPremium
        {
            get
            {
                return Helper.CalculateTotalPremium(this.AnnualPremium);
            }
        }
        public double AvgMonthlyPremium
        {
            get
            {
                return Helper.CalculateAvgMonthlyPremium(this.AnnualPremium);
            }
        }
        public double OtherMonthlyPayment
        {
            get
            {
                return Helper.CalculateOtherMonthlyPayment(this.AnnualPremium);
            }
        }

        public double InitialMonthlyPayment
        {
            get
            {
                return Helper.CalculateInitialMonthlyPayment(this.AnnualPremium);
            }
        }

    }
}
