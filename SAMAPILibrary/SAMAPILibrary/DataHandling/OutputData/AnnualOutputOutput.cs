using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.OutputData
{
    public class AnnualOutputOutput
    {
        private readonly Data data;

        /// <summary>
        /// Number of years for which the analysis was performed
        /// </summary>
        public int analysis_years { 
            get { 
                return (int) data.GetNumber("analysis_years"); 
            } 
        }
      
        /// <summary>
        /// Hourly net energy delivered to the grid for year 1, in Watts
        /// </summary>
        public float[] net_hourly
        {
            get
            {
                return data.GetArray("hourly_e_net_delivered");
            }
        }

        /// <summary>
        /// Year-by-year energy delivered to the grid, in Watts
        /// </summary>
        public float[] net_annual
        {
            get
            {
                return data.GetArray("annual_e_net_delivered");
            }
        }

        /// <summary>
        /// Energy degredation for each year, as percent
        /// </summary>
        public float[] energy_degradation
        {
            get
            {
                return data.GetArray("energy_degradation");
            }
        }

        /// <summary>
        /// Energy availability for each year, as percent
        /// </summary>
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
