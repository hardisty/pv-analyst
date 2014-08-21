using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.OutputData
{

    public class UtilityRateOutput
    {
        private Data data;

        public int analysis_years { 
            get { 
                return (int) data.GetNumber("analysis_years"); 
            } 
        }

        public float[] energy_value{
            get{
                return data.GetArray("energy_value");
            }
        }
        
        public float[] energy_net
        {
            get
            {
                return data.GetArray("energy_net");
            }
        }

        public float[] elec_cost_without_system
        {
            get
            {
                return data.GetArray("elec_cost_without_system");
            }
        }
        public float[] elec_cost_with_system
        {
            get
            {
                return data.GetArray("elec_cost_with_system");
            }
        }

        public UtilityRateOutput(Data data)
        {
            this.data = data;
        }

        public int getAnalysisYears()
        {
            return analysis_years;
        }

        public float[] getAnnualValueOfNetEnergy()
        {
            return energy_value;
        }

        public float[] getAnnualNetEnergy()
        {
            return energy_net;
        }

        public float[] getElectricityCostWithoutSystem()
        {
            return elec_cost_without_system;
        }
        public float[] getElectricityCostWithSystem()
        {
            return elec_cost_with_system;
        }
    }
}
