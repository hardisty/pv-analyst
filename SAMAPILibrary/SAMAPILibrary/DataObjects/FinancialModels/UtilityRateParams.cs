using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataObjects.OutputData;
using SAMAPILibrary.CalculationWrappers.Executables;
using SAMAPILibrary.FinancialModels;

namespace SAMAPILibrary.DataObjects.FinancialModels
{
    public class UtilityRateParams : IDataParamSetter
    {

        /// <summary>
        /// The hourly net energy at the grid with the system (positive out?).
        /// </summary>
        public readonly float[] e_with_system;

        /// <summary>
        /// The hourly net energy at the grid without the system (i.e. the load)
        /// </summary>
        public readonly float[] e_without_system;

        /// <summary>
        /// The annual rate of load escalation (as percent, 100 = 100%). Length = 12 or single value.
        /// </summary>
        public readonly float[] load_escalation;

        /// <summary>
        /// The annual rate of electricity cost escalation (as percent, 100 = 100%) . Length = 12 or single value.
        /// </summary>
        public readonly float[] rate_escalation;

        /// <summary>
        /// Whether the sell rate should be forced to match the buy rate ("Enforce Net Metering"). Boolean 1/0 - T/F
        /// </summary>
        public readonly int ur_sell_eq_buy;

        /// <summary>
        /// A monthly fixed charge in dollars.
        /// </summary>
        public readonly float ur_monthly_fixed_charge;

        /// <summary>
        /// A flat buy rate for electricity in $/kWh.
        /// </summary>
        public readonly float ur_flat_buy_rate;

        /// <summary>
        /// A flat sell rate for electricity, in $/kWh. Ignored if "ur_sell_eq_buy" is true.
        /// </summary>
        public readonly float ur_flat_sell_rate;

        /// Never use these (?) 
        /// TODO decide on this
        public readonly int ur_tou_enable = 0;
        public readonly int ur_ec_enable = 0; //utilityrate2 only
        public readonly int ur_dc_enable = 0;
        public readonly int ur_tr_enable = 0;

        // Carryover
        public readonly int analysis_years;
        public readonly float[] system_degradation;
        public readonly float[] system_availability;

        private UtilityRateParams()
        {
            //Defaults
            e_without_system = new float[] { 0f };
            load_escalation = new float[] { 2.5f };
            rate_escalation = new float[] { 2.5f };
            ur_sell_eq_buy = 1;
            ur_monthly_fixed_charge = 0f;
            ur_flat_buy_rate = 0.12f;
            ur_flat_sell_rate = 0f;
        }

        public UtilityRateParams(IAnnualOutputInputs inputs):this()
        {
            e_with_system = inputs.getHourlyElectricityProuction();
            analysis_years = 25;
            system_degradation = new float[] {0.5f};
            system_availability = new float[] {100f};
        }

        public UtilityRateParams(IUtilityRateInputs inputs):this()
        {
            e_with_system = inputs.getHourlyElectricityProuction();
            analysis_years = inputs.getAnalysisYears();
            system_degradation = inputs.getSystemDegradation();
            system_availability = inputs.getSystemAvailability();
        }

        public void setDataParameters(SAMAPI.Data data)
        {
            data.SetNumber("analysis_years", analysis_years);
            data.SetArray("system_degradation", system_degradation);
            data.SetArray("system_availability", system_availability);

            data.SetArray("e_with_system", e_with_system);
            //data.SetArray("e_without_system", e_without_system);
            data.SetArray("load_escalation", load_escalation);
            data.SetArray("rate_escalation", rate_escalation);
            data.SetNumber("ur_sell_eq_buy", ur_sell_eq_buy);
            data.SetNumber("ur_monthly_fixed_charge", ur_monthly_fixed_charge);
            data.SetNumber("ur_flat_buy_rate", ur_flat_buy_rate);
            data.SetNumber("ur_flat_sell_rate", ur_flat_sell_rate);

            // Don't use these?
            // TODO Decide on this
            data.SetNumber("ur_tou_enable", 0f);
            data.SetNumber("ur_ec_enable", 0f);
            data.SetNumber("ur_dc_enable", 0f);
            data.SetNumber("ur_tr_enable", 0f);
        }
    }
}
