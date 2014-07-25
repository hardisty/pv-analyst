using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataObjects.OutputData;

namespace SAMAPILibrary.DataObjects.FinancialModels
{
    public class DegradationParams : IDataParamSetter
    {
        /// <summary>
        /// Analysis period in years
        /// </summary>
        public int analysis_years;

        /// <summary>
        /// Annual availability (as percent, 100 = 100%). Length = 12 or single value.
        /// </summary>
        public float[] energy_availability;

        /// <summary>
        /// Annual degradation rate (as percent, 100 = 100%). Length = 12 or sing
        /// </summary>
        public float[] energy_degradation;

        /// <summary>
        /// First year hourly curtailment for each month (0-1). Dimensions should be 24/row, 12 rows.
        /// </summary>
        public float[,] energy_curtailment;

        /// <summary>
        /// True/false: output the lifetime hourly output
        /// </summary>
        public int system_use_lifetime_output;

        /// <summary>
        /// The hourly irradiance in year 1
        /// </summary>
        public float[] energy_net_hourly;

        public DegradationParams(SystemModelOutput smo)
        {
            //Defaults
            analysis_years = 25;
            energy_availability = new float[] { 100f };
            energy_degradation = new float[] { 0.5f };
            energy_curtailment = new float[,]
                  { { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },  //Jan
                    { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
                    { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
                    { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
                    { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
                    { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
                    { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
                    { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
                    { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
                    { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
                    { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
                    { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f } };//Dec
            system_use_lifetime_output = 0;
            energy_net_hourly = smo.ac_hourly;
        }

        public void setDataParameters(SAMAPI.Data data)
        {
            data.SetNumber("analysis_years", analysis_years);
            data.SetArray("energy_availability", energy_availability);
            data.SetArray("energy_degradation", energy_degradation);
            data.SetMatrix("energy_curtailment", energy_curtailment);
            data.SetNumber("system_use_lifetime_output", system_use_lifetime_output);
            data.SetArray("energy_net_hourly", energy_net_hourly);
        }
    }
}
