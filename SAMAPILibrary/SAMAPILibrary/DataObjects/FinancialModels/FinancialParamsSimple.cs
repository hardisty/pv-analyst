using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataObjects.FinancialModels
{


    public class FinancialParamsSimple
    {
        /// <summary>
        /// Duration of mortgage
        /// </summary>
        public readonly int loan_term;

        /// <summary>
        /// Mortgage rate
        /// </summary>
        public readonly float loan_rate;

        /// <summary>
        /// Percent of system cost that loan is taken for
        /// </summary>
        public readonly float loan_debt;

        public FinancialParamsSimple(int loanTerm, float loanRate, float loanPctDebt)
        {
            loan_term = loanTerm;
            loan_rate = loanRate;
            loan_debt = loanPctDebt;
        }

    }
}
