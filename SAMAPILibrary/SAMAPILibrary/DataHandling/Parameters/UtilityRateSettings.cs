using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.OutputData;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.Parameters
{
    public class UtilityRateSettings: IRunnableSettings
    {
        //Specified
        [ISettings.Param("e_with_system", "array")]
        public float[] e_with_system = new float[8760];

        [ISettings.Param("load_escalation", "array")]
        public float[] load_escalation = new float[] { 0 };

        [ISettings.Param("rate_escalation", "array")]
        public float[] rate_escalation = new float[] { 0.5f + 2.5f };

        [ISettings.Param("ur_sell_eq_buy", "float")]
        public float ur_sell_eq_buy = 1;

        [ISettings.Param("ur_monthly_fixed_charge", "float")]
        public float ur_monthly_fixed_charge = 0;

        [ISettings.Param("ur_flat_buy_rate", "float")]
        public float ur_flat_buy_rate = 0.12f;

        [ISettings.Param("ur_flat_sell_rate", "float")]
        public float ur_flat_sell_rate = 0.12f;

        [ISettings.Param("self_shading_enabled", "float")]
        public float self_shading_enabled = 0;

        [ISettings.Param("ur_tou_enable", "float")]
        public float ur_tou_enable = 0;

        [ISettings.Param("ur_ec_enable", "float")]
        public float ur_ec_enable = 0;

        [ISettings.Param("ur_dc_enable", "float")]
        public float ur_dc_enable = 0;

        [ISettings.Param("ur_tr_enable", "float")]
        public float ur_tr_enable = 0;

        [ISettings.Param("analysis_years", "float")]
        public float analysis_years = 25;

        [ISettings.Param("system_degradation", "array")]
        public float[] system_degradation = new float[] {0.5f};

        [ISettings.Param("system_availability", "array")]
        public float[] system_availability = new float[] {100f};


        private UtilityRateSettings() { }

        public static UtilityRateSettings getDefault()
        {
            return new UtilityRateSettings();
        }

        public void setValuesFromPriorOutput(PVSAMV1Output s)
        {
            e_with_system = s.getHourlyElectricityProuction();
        }

        public override string getModuleName()
        {
            return "utilityrate";
        }

        public override Output getOutputClass(Data data)
        {
            return new UtilityRateOutput(data);
        }
    }
}
