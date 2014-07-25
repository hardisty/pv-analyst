using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.FinancialModels
{
    public interface IUtilityRateInputs: IAnnualOutputInputs
    {
        int getAnalysisYears();
        float[] getSystemAvailability();
        float[] getSystemDegradation();
    }
}
