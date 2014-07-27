using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataObjects.OutputData;
using SAMAPILibrary.CalculationWrappers.Executables;
using SAMAPILibrary.FinancialModels;

namespace SAMAPILibrary.DataObjects.FinancialModels
{
    public class AnnualOutputParams : IDataParamSetter
    {
        /// <summary>
        /// Analysis period in years
        /// </summary>
        public readonly int analysis_years;

        /// <summary>
        /// Annual availability (as percent, 100 = 100%). Length = 12 or single value.
        /// </summary>
        public readonly float[] energy_availability;

        /// <summary>
        /// Annual degradation rate (as percent, 100 = 100%). Length = 12 or sing
        /// </summary>
        public readonly float[] energy_degradation;

        /// <summary>
        /// First year hourly curtailment for each month (0-1). Dimensions should be 24/row, 12 rows.
        /// </summary>
        public readonly float[,] energy_curtailment;

        /// <summary>
        /// True/false: output the lifetime hourly output
        /// </summary>
        public readonly int system_use_lifetime_output = 0;

        /// <summary>
        /// The hourly irradiance in year 1
        /// </summary>
        public readonly float[] energy_net_hourly;

        public AnnualOutputParams(IAnnualOutputInputs input)
        {
            energy_net_hourly = input.getHourlyElectricityProuction();

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
