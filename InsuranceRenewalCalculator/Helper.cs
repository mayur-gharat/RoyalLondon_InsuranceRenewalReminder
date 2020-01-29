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

        /// <summary>
        /// Total premium = Premium + Credit Charge
        /// </summary>
        /// <param name="Premium"></param>
        /// <returns></returns>
        public static double CalculateTotalPremium(double Premium)
        {
            return Premium + CalculateCreditCharge(Premium);
        }

        public static double CalculateAvgMonthlyPremium(double Premium)
        {
            return Math.Round(Premium / 12, 2);
        }

        /// <summary>
        /// Other Monthly payment is payment to be done for rest of 11 months having valid currency in decimal on lowar side
        /// </summary>
        /// <param name="Premium"></param>
        /// <returns></returns>
        public static double CalculateOtherMonthlyPayment(double Premium)
        {
            return Math.Floor((CalculateTotalPremium(Premium) / 12) * 100) / 100;
        }

        /// <summary>
        /// /// Inital Monthly payment is payment to be done for first month having valid currency in decimal on higher side
        /// </summary>
        /// <param name="Premium"></param>
        /// <returns></returns>
        public static double CalculateInitialMonthlyPayment(double Premium)
        {
            return Math.Round(CalculateTotalPremium(Premium) - ((CalculateOtherMonthlyPayment(Premium) * 11)),2);
        }
    }
}
