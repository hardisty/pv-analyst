using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataObjects.OutputData;
using SAMAPILibrary.DataObjects.FinancialModels;
using SAMAPILibrary.DataObjects;
using SAMAPILibrary.CalculationWrappers.Executables;

namespace SAMAPILibrary.CalculationWrappers
{
    public class ModelPVSystem
    {
        InputParams IP;

        SystemModelOutput SystemOutput;
        SizeAndCostParams CostOutput;
        UtilityRateOutput UtilityOutput;
        CashLoanOutput LoanOutput;

        public ModelPVSystem(InputParams input)
        {
            IP = input;
            this.run();
        }

        private void run()
        {
            SystemOutput = pvsam1.run(IP.getGISData(), IP.getArrayParams());
            CostOutput = new SizeAndCostParams(SystemOutput);
            UtilityRateParams up = new UtilityRateParams(SystemOutput);
            UtilityOutput = utilityrate.run(up);
            CashLoanParams clp = new CashLoanParams(IP.getFinancialParams(),UtilityOutput,CostOutput);
            LoanOutput = cashloan.run(clp);
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
    }
}
