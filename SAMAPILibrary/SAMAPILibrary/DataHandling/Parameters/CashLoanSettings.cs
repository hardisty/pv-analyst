using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.OutputData;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.Parameters
{
    public class CashLoanSettings: IRunnableSettings
    {
        [ISettings.Param("federal_tax_rate", "float")]
        public float federal_tax_rate = 28;
        [ISettings.Param("state_tax_rate", "float")]
        public float state_tax_rate = 4;
        [ISettings.Param("property_tax_rate", "float")]
        public float property_tax_rate = 0;
        [ISettings.Param("prop_tax_cost_assessed_percent", "float")]
        public float prop_tax_cost_assessed_percent = 100;
        [ISettings.Param("prop_tax_assessed_decline", "float")]
        public float prop_tax_assessed_decline = 0;
        [ISettings.Param("sales_tax_rate", "float")]
        public float sales_tax_rate = 5;
        [ISettings.Param("real_discount_rate", "float")]
        public float real_discount_rate = 8;
        [ISettings.Param("inflation_rate", "float")]
        public float inflation_rate = 2.5f;
        [ISettings.Param("insurance_rate", "float")]
        public float insurance_rate = 0;
        [ISettings.Param("system_capacity", "float")]
        public float system_capacity = 0;
        [ISettings.Param("market", "float")]
        public float market = 0;
        [ISettings.Param("mortgage", "float")]
        public float mortgage = 0;
        [ISettings.Param("total_installed_cost", "float")]
        public float total_installed_cost = 0;
        [ISettings.Param("salvage_percentage", "float")]
        public float salvage_percentage = 0;
        [ISettings.Param("om_fixed", "array")]
        public float[] om_fixed = new float[] {0};
        [ISettings.Param("om_fixed_escal", "float")]
        public float om_fixed_escal = 0;
        [ISettings.Param("om_production", "array")]
        public float[] om_production = new float[] {0};
        [ISettings.Param("om_production_escal", "float")]
        public float om_production_escal = 0;
        [ISettings.Param("om_capacity", "array")]
        public float[] om_capacity = new float[] {20};
        [ISettings.Param("om_capacity_escal", "float")]
        public float om_capacity_escal = 0;
        [ISettings.Param("om_fuel_cost", "array")]
        public float[] om_fuel_cost = new float[] {0};
        [ISettings.Param("om_fuel_cost_escal", "float")]
        public float om_fuel_cost_escal = 0;
        [ISettings.Param("annual_fuel_usage", "float")]
        public float annual_fuel_usage = 0;
        [ISettings.Param("itc_fed_percent", "float")]
        public float itc_fed_percent = 30;
        [ISettings.Param("itc_fed_percent_maxvalue", "float")]
        public float itc_fed_percent_maxvalue = 1e+038f;
        [ISettings.Param("itc_fed_percent_deprbas_fed", "float")]
        public float itc_fed_percent_deprbas_fed = 0;
        [ISettings.Param("itc_fed_percent_deprbas_sta", "float")]
        public float itc_fed_percent_deprbas_sta = 0;
        [ISettings.Param("analysis_years", "float")]
        public float analysis_years = 25;
        [ISettings.Param("loan_term", "float")]
        public float loan_term = 25;
        [ISettings.Param("loan_rate", "float")]
        public float loan_rate = 7.5f;
        [ISettings.Param("loan_debt", "float")]
        public float loan_debt = 100;
        [ISettings.Param("energy_value", "array")]
        public float[] energy_value = new float[25];
        [ISettings.Param("energy_net", "array")]
        public float[] energy_net = new float[25];
        [ISettings.Param("pbi_sta_amount", "array")]
        public float[] pbi_sta_amount = new float[] {0.05f};
        [ISettings.Param("pbi_sta_term", "float")]
        public float pbi_sta_term = 10;
        [ISettings.Param("pbi_sta_escal", "float")]
        public float pbi_sta_escal = 0;
        [ISettings.Param("pbi_sta_tax_fed", "float")]
        public float pbi_sta_tax_fed = 1;
        [ISettings.Param("pbi_sta_tax_sta", "float")]
        public float pbi_sta_tax_sta = 1;

 
        private CashLoanSettings() { }
        public static CashLoanSettings getDefault()
        {
            return new CashLoanSettings();
        }

        public void setValuesFromPriorOutput(SizeAndCostSettings sc, UtilityRateOutput uro)
        {
            

            analysis_years = uro.getAnalysisYears();
            energy_value = uro.getAnnualValueOfNetEnergy();
            energy_net = uro.getAnnualNetEnergy();
            system_capacity = sc.dc_rating/1000;
            total_installed_cost = sc.total_cost;
            salvage_percentage = uro.getAnalysisYears() * 30 / 25.0f;
        }

        public override string getModuleName()
        {
            return "cashloan";
        }

        public override Output getOutputClass(Data data)
        {
            return new CashLoanOutput(data);
        }
    }
}
