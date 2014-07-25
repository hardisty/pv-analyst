using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.FinancialModels
{
    public interface ICashLoanInputs
    {
        int getAnalysisYears();
        float[] getAnnualValueOfNetEnergy();
        float[] getAnnualNetEnergy();
    }
}
