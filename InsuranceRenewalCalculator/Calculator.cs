using System;

namespace InsuranceRenewalCalculator
{
    /// <summary>
    /// This class separated to calculate/formulate premium payble to individual
    /// </summary>
    public static class Calculator
    {
        /// <summary>
        /// An additional charge that applies when paying monthly by Direct Debit
        /// 5% of the annual premium, rounded up to a whole number of pound and pence
        /// </summary>
        /// <param name="Premium"></param>
        /// <returns></returns>
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
        /// The total amount payable for the year when paying monthly by Direct Debit
        /// The annual premuim plus the credit charge.
        /// </summary>
        /// <param name="Premium"></param>
        /// <returns></returns>
        public static double CalculateTotalPremium(double Premium)
        {
            return Premium + CalculateCreditCharge(Premium);
        }

        /// <summary>
        /// The average amount payable per month when paying monthly by Direct Debit
        /// The Total Premium (Direct Debit) divided by the number of the months in a year.
        /// </summary>
        /// <param name="Premium"></param>
        /// <returns></returns>
        public static double CalculateAvgMonthlyPremium(double Premium)
        {
            return Math.Round(Premium / 12, 2);
        }

        /// <summary>
        /// The amount of the other 11 monthly payments.
        /// Will equal the Average Monthly Premium if that is a whole number of pound and pence; otherwise will be lower.
        /// </summary>
        /// <param name="Premium"></param>
        /// <returns></returns>
        public static double CalculateOtherMonthlyPayment(double Premium)
        {
            return Math.Floor((CalculateTotalPremium(Premium) / 12) * 100) / 100;
        }

        /// <summary>
        /// The amount of the First monthly payments.
        /// Will equal the Average Monthly Premium if that is a whole number of pound and pence; otherwise will be higher.
        /// </summary>
        /// <param name="Premium"></param>
        /// <returns></returns>
        public static double CalculateInitialMonthlyPayment(double Premium)
        {
            return Math.Round(CalculateTotalPremium(Premium) - ((CalculateOtherMonthlyPayment(Premium) * 11)),2);
        }
    }
}
