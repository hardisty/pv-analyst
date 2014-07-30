using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.ParameterTypes.OutputData;
using SAMAPILibrary.DataObjects.OutputData;
using SAMAPILibrary.DataHandling;
using SAMAPILibrary.DataObjects;

namespace SAMAPILibrary.CalculationWrappers
{
    class PVSystemModel
    {
        //InputParams IP;

        SystemModelOutput SystemOutput;
        //SizeAndCostParams CostOutput;
        UtilityRateOutput UtilityOutput;
        CashLoanOutput LoanOutput;

        public PVSystemModel(GISData gis)
        {
            
            
        }

        private void run()
        {
            
        }

        //TODO add more outputs or decide how they should look
        public float getNetPresentValue()
        {
            return LoanOutput.npv;
        }

        public float getYearOneOutput()
        {
            return SystemOutput.ac_annual;
        }

        public float[] getAnnualValueOfEnergyProduced()
        {
            return UtilityOutput.getAnnualValueOfNetEnergy();
        }
    }
    
}

