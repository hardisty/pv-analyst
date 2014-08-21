using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.OutputData;
using SAMAPILibrary.DataHandling.Parameters;

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
        public float[] getAnnualNetEnergy()
        {
            return UtilityOutput.energy_net;
        }
        public float[] getAnnualValueOfEnergyProduced()
        {
            return UtilityOutput.getAnnualValueOfNetEnergy();
        }
        public float[] getElecCostWithoutSystem()
        {
            return UtilityOutput.getElectricityCostWithoutSystem();
        }
        public float[] getElecCostWithSystem()
        {
            return UtilityOutput.getElectricityCostWithSystem();
        }
        public float getNameplateCapacity()
        {
            return SystemOutput.sys_dc_rating;
        }
        public float getInverterACCapacity()
        {
            return SystemOutput.inv_ac_rating;
        }
        public float getSystemCost()
        {
            return CostOutput.total_costs;
        }

        public float getCostPerWatt()
        {
            return CostOutput.cost_per_watt_dc;
        }

        public float[] getOperatingExpense()
        {
            return LoanOutput.cf_operating_expenses;
        }
        public float[] getTotalDebtPayment()
        {
            return LoanOutput.cf_debt_payment_total;
        }
    }
    
}

