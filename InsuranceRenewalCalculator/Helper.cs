using System;

namespace InsuranceRenewalCalculator
{
    /// <summary>
    /// This class separated to calculate/formulate premium payble to individual
    /// </summary>
    public static class Helper
    {
        // 5% credit charge
        public static double CalculateCreditCharge(double Premium)
        {
            //CreditCharge percentage can be changed according to business need
            double CreditChargePerc;
            double.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["CreditCharge"], out CreditChargePerc);
            if(CreditChargePerc == 0)
            {
                CreditChargePerc = 5; // Fallback in case of 0 to satisfy current condition.
            }
            return Math.Round((Premium /100) * CreditChargePerc, 2);
        }
        public static double CalculateTotalPremium(double Premium)
        {
            return Premium + CalculateCreditCharge(Premium);
        }
        public static double CalculateAvgMonthlyPremium(double Premium)
        {
            return Math.Round(Premium / 12, 2);
        }

        public static double CalculateOtherMonthlyPayment(double Premium)
        {
            return Math.Floor((CalculateTotalPremium(Premium) / 12) * 100) / 100;
        }

        public static double CalculateInitialMonthlyPayment(double Premium)
        {
            return Math.Round(CalculateTotalPremium(Premium) - ((CalculateOtherMonthlyPayment(Premium) * 11)),2);
        }
    }
}
