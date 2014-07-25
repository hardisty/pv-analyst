using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;
using SAMAPILibrary.CalculationWrappers;
using SAMAPILibrary.FinancialModels;

namespace SAMAPILibrary.DataObjects.OutputData
{
    public class AnnualOutputOutput: IUtilityRateInputs
    {
        private readonly Data data;

        public int analysis_years { 
            get { 
                return (int) data.GetNumber("analysis_years"); 
            } 
        }
      
        public float[] net_hourly
        {
            get
            {
                return data.GetArray("hourly_e_net_delivered");
            }
        }

        public float[] net_annual
        {
            get
            {
                return data.GetArray("annual_e_net_delivered");
            }
        }

        public float[] energy_degradation
        {
            get
            {
                return data.GetArray("energy_degradation");
            }
        }

        public float[] energy_availability
        {
            get
            {
                return data.GetArray("energy_availability");
            }
        }

        public AnnualOutputOutput(Data data)
        {
            this.data = data;
        }

        public int getAnalysisYears()
        {
            return analysis_years;
        }

        public float[] getSystemAvailability()
        {
            return energy_availability;
        }

        public float[] getSystemDegradation()
        {
            return energy_degradation;
        }

        public float[] getHourlyElectricityProuction()
        {
            return net_hourly;
        }
    }
}
