using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.ParameterTypes.OutputData;
using SAMAPILibrary.DataObjects.OutputData;
using SAMAPILibrary.DataHandling;
using SAMAPILibrary.DataObjects;
using SAMAPILibrary.DataHandling.ParameterTypes;

namespace SAMAPILibrary.CalculationWrappers
{
    public class PVSystemModel
    {
        //InputParams IP;

        GISData gisData;
        ArrayParameterListBuilder arrayBuilder;
        UtilityRateParameterBuilder utilityBuilder;
        CashLoanParameterBuilder cashBuilder;
        SizeAndCostParameterBuilder sizeBuilder;

        SystemModelOutput SystemOutput;
        SizeAndCostParameterList CostOutput;
        UtilityRateOutput UtilityOutput;
        CashLoanOutput LoanOutput;

        public PVSystemModel(GISData gis,ArrayParameterListBuilder apl,UtilityRateParameterBuilder upl, CashLoanParameterBuilder clp, SizeAndCostParameterBuilder scb)
        {
            gisData = gis;
            arrayBuilder = apl;
            utilityBuilder = upl;
            cashBuilder = clp;
            sizeBuilder = scb;
            
        }

        public void run()
        {
            arrayBuilder.initialize(gisData);
            SystemOutput = arrayBuilder.build().runModule();

            utilityBuilder.initialize(SystemOutput);
            UtilityOutput = utilityBuilder.build().runModule();

            sizeBuilder.initialize(SystemOutput);
            CostOutput = sizeBuilder.build();
            
            cashBuilder.initialize(CostOutput, UtilityOutput);
            LoanOutput = cashBuilder.build().runModule();
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

