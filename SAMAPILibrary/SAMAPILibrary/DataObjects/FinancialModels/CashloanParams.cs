using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;
using SAMAPILibrary.DataObjects.OutputData;
using SAMAPILibrary.CalculationWrappers;
using SAMAPILibrary.FinancialModels;

namespace SAMAPILibrary.DataObjects.FinancialModels
{
    class CashLoanParams : IDataParamSetter{


        /// <summary>
        /// Federal tax rate as percent (e.g. 28% = 28)
        /// </summary>
        public readonly float federal_tax_rate;

        /// <summary>
        /// State tax rate as percent (e.g. 28% = 28)
        /// </summary>
        public readonly float state_tax_rate;

        /// <summary>
        /// Property tax rate as percent (e.g. 28% = 28)
        /// </summary>
        public readonly float property_tax_rate;

        /// <summary>
        /// Percent of property value assessed
        /// </summary>
        public readonly float prop_tax_cost_assessed_percent;

        /// <summary>
        /// Percent decline in assessed value (annual)
        /// </summary>
        public readonly float prop_tax_assessed_decline;

        /// <summary>
        /// Sales tax rate as percent (e.g. 28% = 28)
        /// </summary>
        public readonly float sales_tax_rate;

        /// <summary>
        /// Real discount rate as percent
        /// </summary>
        public readonly float real_discount_rate;

        /// <summary>
        /// Annual inflation rate as percent
        /// </summary>
        public readonly float inflation_rate;

        /// <summary>
        /// Insurance rate as percent of total system cost
        /// </summary>
        public readonly float insurance_rate;

        /// <summary>
        /// Total system capacity
        /// </summary>
        public readonly float system_capacity;

        /// <summary>
        /// 
        /// </summary>
        public readonly float system_heat_rate;

        /// <summary>
        /// Duration of mortgage
        /// </summary>
        public readonly int loan_term;

        /// <summary>
        /// Mortgage rate
        /// </summary>
        public readonly float loan_rate;

        /// <summary>
        /// Percent of system cost that loan is taken for
        /// </summary>
        public readonly float loan_debt;

        /// <summary>
        /// Residential (0) or Commercial (1) market type
        /// </summary>
        public readonly int market;

        /// <summary>
        /// Boolean (0/1) is it a mortgage loan?  Affects taxability of interest payments.
        /// </summary>
        public readonly int mortgage;

        /// <summary>
        /// Total installed system cost
        /// </summary>
        public readonly float total_installed_cost;

        /// <summary>
        /// Percent of initial cost sytem can be sold for at end of analysis
        /// </summary>
        public readonly float salvage_percentage;

        //O&M Costs
        public readonly float[] om_fixed;
        public readonly float om_fixed_escal;
        public readonly float[] om_production;
        public readonly float om_production_escal;
        public readonly float[] om_capacity;
        public readonly float om_capacity_escal;
        public readonly float[] om_fuel_cost;
        public readonly float om_fuel_cost_escal;
        public readonly float annual_fuel_usage;

        //Investment Tax Credits
        public readonly float itc_fed_amount;
        public readonly int itc_fed_amount_deprbas_fed;
        public readonly int itc_fed_amount_deprbas_sta;
        public readonly float itc_sta_amount;
        public readonly int itc_sta_amount_deprbas_fed;
        public readonly int itc_sta_amount_deprbas_sta;
        public readonly float itc_fed_percent;
        public readonly float itc_fed_percent_maxvalue;
        public readonly int itc_fed_percent_deprbas_fed;
        public readonly int itc_fed_percent_deprbas_sta;
        public readonly float itc_sta_percent;
        public readonly float itc_sta_percent_maxvalue;
        public readonly int itc_sta_percent_deprbas_fed;
        public readonly int itc_sta_percent_deprbas_sta;

        //Production Tax Credit
        public readonly float[] ptc_fed_amount;
        public readonly int ptc_fed_term;
        public readonly float ptc_fed_escal;
        public readonly float[] ptc_sta_amount;
        public readonly int ptc_sta_term;
        public readonly float ptc_sta_escal;

        //Investment Based Incentives
        public readonly float ibi_fed_amount;
        public readonly int ibi_fed_amount_tax_fed;
        public readonly int ibi_fed_amount_tax_sta;
        public readonly int ibi_fed_amount_deprbas_fed;
        public readonly int ibi_fed_amount_deprbas_sta;
        public readonly float ibi_sta_amount;
        public readonly int ibi_sta_amount_tax_fed;
        public readonly int ibi_sta_amount_tax_sta;
        public readonly int ibi_sta_amount_deprbas_fed;
        public readonly int ibi_sta_amount_deprbas_sta;
        public readonly float ibi_uti_amount;
        public readonly int ibi_uti_amount_tax_fed;
        public readonly int ibi_uti_amount_tax_sta;
        public readonly int ibi_uti_amount_deprbas_fed;
        public readonly int ibi_uti_amount_deprbas_sta;
        public readonly float ibi_oth_amount;
        public readonly int ibi_oth_amount_tax_fed;
        public readonly int ibi_oth_amount_tax_sta;
        public readonly int ibi_oth_amount_deprbas_fed;
        public readonly int ibi_oth_amount_deprbas_sta;
        public readonly float ibi_fed_percent;
        public readonly float ibi_fed_percent_maxvalue;
        public readonly int ibi_fed_percent_tax_fed;
        public readonly int ibi_fed_percent_tax_sta;
        public readonly int ibi_fed_percent_deprbas_fed;
        public readonly int ibi_fed_percent_deprbas_sta;
        public readonly float ibi_sta_percent;
        public readonly float ibi_sta_percent_maxvalue;
        public readonly int ibi_sta_percent_tax_fed;
        public readonly int ibi_sta_percent_tax_sta;
        public readonly int ibi_sta_percent_deprbas_fed;
        public readonly int ibi_sta_percent_deprbas_sta;
        public readonly float ibi_uti_percent;
        public readonly float ibi_uti_percent_maxvalue;
        public readonly int ibi_uti_percent_tax_fed;
        public readonly int ibi_uti_percent_tax_sta;
        public readonly int ibi_uti_percent_deprbas_fed;
        public readonly int ibi_uti_percent_deprbas_sta;
        public readonly float ibi_oth_percent;
        public readonly float ibi_oth_percent_maxvalue;
        public readonly int ibi_oth_percent_tax_fed;
        public readonly int ibi_oth_percent_tax_sta;
        public readonly int ibi_oth_percent_deprbas_fed;
        public readonly int ibi_oth_percent_deprbas_sta;

        //Capacity Based Incentives
        public readonly float cbi_fed_amount;
        public readonly float cbi_fed_maxvalue;
        public readonly int cbi_fed_tax_fed;
        public readonly int cbi_fed_tax_sta;
        public readonly int cbi_fed_deprbas_fed;
        public readonly int cbi_fed_deprbas_sta;
        public readonly float cbi_sta_amount;
        public readonly float cbi_sta_maxvalue;
        public readonly int cbi_sta_tax_fed;
        public readonly int cbi_sta_tax_sta;
        public readonly int cbi_sta_deprbas_fed;
        public readonly int cbi_sta_deprbas_sta;
        public readonly float cbi_uti_amount;
        public readonly float cbi_uti_maxvalue;
        public readonly int cbi_uti_tax_fed;
        public readonly int cbi_uti_tax_sta;
        public readonly int cbi_uti_deprbas_fed;
        public readonly int cbi_uti_deprbas_sta;
        public readonly float cbi_oth_amount;
        public readonly float cbi_oth_maxvalue;
        public readonly int cbi_oth_tax_fed;
        public readonly int cbi_oth_tax_sta;
        public readonly int cbi_oth_deprbas_fed;
        public readonly int cbi_oth_deprbas_sta;
        
        //Production Based Incentives
        public readonly float[] pbi_fed_amount;
        public readonly int pbi_fed_term;
        public readonly float pbi_fed_escal;
        public readonly int pbi_fed_tax_fed;
        public readonly int pbi_fed_tax_sta;
        public readonly float[] pbi_sta_amount;
        public readonly int pbi_sta_term;
        public readonly float pbi_sta_escal;
        public readonly int pbi_sta_tax_fed;
        public readonly int pbi_sta_tax_sta;
        public readonly float[] pbi_uti_amount;
        public readonly int pbi_uti_term;
        public readonly float pbi_uti_escal;
        public readonly int pbi_uti_tax_fed;
        public readonly int pbi_uti_tax_sta;
        public readonly float[] pbi_oth_amount;
        public readonly int pbi_oth_term;
        public readonly float pbi_oth_escal;
        public readonly int pbi_oth_tax_fed;
        public readonly int pbi_oth_tax_sta;

        //Carryover
        public readonly int analysis_years;
        public readonly float[] energy_value;
        public readonly float[] energy_net;

        public CashLoanParams(ICashLoanInputs inputs, SizeAndCostParams costs)
        {
            analysis_years = inputs.getAnalysisYears();
            energy_value = inputs.getAnnualValueOfNetEnergy();
            energy_net = inputs.getAnnualNetEnergy();   
            

            federal_tax_rate = 28f;
            state_tax_rate = 7f;
            property_tax_rate = 0f;
            prop_tax_cost_assessed_percent = 100f;
            prop_tax_assessed_decline = 0f;
            sales_tax_rate = 5f;
            real_discount_rate = 8f;
            inflation_rate = 2.5f;
            insurance_rate = 0f;
            system_capacity = costs.dc_rating/1000;
            system_heat_rate = 0f;
            loan_term = 25;
            loan_rate = 7.5f;
            loan_debt = 100f;
            market = 0;
            mortgage = 0;
            total_installed_cost = costs.total_costs; //22194.2f default
            salvage_percentage = 0f;

            //O&M
            om_fixed = new float[] { 0f };
            om_fixed_escal = 0f;
            om_production = new float[] { 0f };
            om_production_escal = 0f;
            om_capacity = new float[] { 20f };
            om_capacity_escal = 0f;
            om_fuel_cost = new float[] { 0f };
            om_fuel_cost_escal = 0f;
            annual_fuel_usage = 0f;

            //ITC Fed and State
            itc_fed_amount = 0f;
            itc_fed_amount_deprbas_fed = 0;
            itc_fed_amount_deprbas_sta = 0;
            itc_sta_amount = 0f;
            itc_sta_amount_deprbas_fed = 0;
            itc_sta_amount_deprbas_sta = 0;
            itc_fed_percent = 30f;
            itc_fed_percent_maxvalue = 1e+038f;
            itc_fed_percent_deprbas_fed = 0;
            itc_fed_percent_deprbas_sta = 0;
            itc_sta_percent = 0f;
            itc_sta_percent_maxvalue = 1e+038f;
            itc_sta_percent_deprbas_fed = 0;
            itc_sta_percent_deprbas_sta = 0;

            //PTC Fed/State
            ptc_fed_amount = new float[] { 0f };
            ptc_fed_term = 10;
            ptc_fed_escal = 2.5f;
            ptc_sta_amount = new float[] { 0f };
            ptc_sta_term = 10;
            ptc_sta_escal = 2.5f;

            //IBI Fed/State/Utility/Other
            ibi_fed_amount = 0f;
            ibi_fed_amount_tax_fed = 1;
            ibi_fed_amount_tax_sta = 1;
            ibi_fed_amount_deprbas_fed = 0;
            ibi_fed_amount_deprbas_sta = 0;
            ibi_sta_amount = 0;
            ibi_sta_amount_tax_fed = 1;
            ibi_sta_amount_tax_sta = 1;
            ibi_sta_amount_deprbas_fed = 0;
            ibi_sta_amount_deprbas_sta = 0;
            ibi_uti_amount = 0f;
            ibi_uti_amount_tax_fed = 1;
            ibi_uti_amount_tax_sta = 1;
            ibi_uti_amount_deprbas_fed = 0;
            ibi_uti_amount_deprbas_sta = 0;
            ibi_oth_amount = 0f;
            ibi_oth_amount_tax_fed = 1;
            ibi_oth_amount_tax_sta = 1;
            ibi_oth_amount_deprbas_fed = 0;
            ibi_oth_amount_deprbas_sta = 0;
            ibi_fed_percent = 0f;
            ibi_fed_percent_maxvalue = 1e+038f;
            ibi_fed_percent_tax_fed = 1;
            ibi_fed_percent_tax_sta = 1;
            ibi_fed_percent_deprbas_fed = 0;
            ibi_fed_percent_deprbas_sta = 0;
            ibi_sta_percent = 0f;
            ibi_sta_percent_maxvalue = 1e+038f;
            ibi_sta_percent_tax_fed = 1;
            ibi_sta_percent_tax_sta = 1;
            ibi_sta_percent_deprbas_fed = 0;
            ibi_sta_percent_deprbas_sta = 0;
            ibi_uti_percent = 0f;
            ibi_uti_percent_maxvalue = 1e+038f;
            ibi_uti_percent_tax_fed = 1;
            ibi_uti_percent_tax_sta = 1;
            ibi_uti_percent_deprbas_fed = 0;
            ibi_uti_percent_deprbas_sta = 0;
            ibi_oth_percent = 0f;
            ibi_oth_percent_maxvalue = 1e+038f;
            ibi_oth_percent_tax_fed = 1;
            ibi_oth_percent_tax_sta = 1;
            ibi_oth_percent_deprbas_fed = 0;
            ibi_oth_percent_deprbas_sta = 0;

            // CBI Fed/State/Util/Other
            cbi_fed_amount = 0f;
            cbi_fed_maxvalue = 1e+038f;
            cbi_fed_tax_fed = 1;
            cbi_fed_tax_sta = 1;
            cbi_fed_deprbas_fed = 0;
            cbi_fed_deprbas_sta = 0;
            cbi_sta_amount = 0f;
            cbi_sta_maxvalue = 1e+038f;
            cbi_sta_tax_fed = 1;
            cbi_sta_tax_sta = 1;
            cbi_sta_deprbas_fed = 0;
            cbi_sta_deprbas_sta = 0;
            cbi_uti_amount = 0f;
            cbi_uti_maxvalue = 1e+038f;
            cbi_uti_tax_fed = 1;
            cbi_uti_tax_sta = 1;
            cbi_uti_deprbas_fed = 0;
            cbi_uti_deprbas_sta = 0;
            cbi_oth_amount = 0f;
            cbi_oth_maxvalue = 1e+038f;
            cbi_oth_tax_fed = 1;
            cbi_oth_tax_sta = 1;
            cbi_oth_deprbas_fed = 0;
            cbi_oth_deprbas_sta = 0;

            //PBI Fed/State/Util/Other
            pbi_fed_amount = new float[] { 0f };
            pbi_fed_term = 0;
            pbi_fed_escal = 0f;
            pbi_fed_tax_fed = 1;
            pbi_fed_tax_sta = 1;
            pbi_sta_amount = new float[] { 0f };
            pbi_sta_term = 0;
            pbi_sta_escal = 0f;
            pbi_sta_tax_fed = 1;
            pbi_sta_tax_sta = 1;
            pbi_uti_amount = new float[] { 0f };
            pbi_uti_term = 0;
            pbi_uti_escal = 0f;
            pbi_uti_tax_fed = 1;
            pbi_uti_tax_sta = 1;
            pbi_oth_amount = new float[] { 0f };
            pbi_oth_term = 0;
            pbi_oth_escal = 0f;
            pbi_oth_tax_fed = 1;
            pbi_oth_tax_sta = 1;
        }

        public void  setDataParameters(Data data)
        {
            data.SetNumber("analysis_years", analysis_years);
            data.SetArray("energy_value", energy_value);
            data.SetArray("energy_net", energy_net);

            data.SetNumber("federal_tax_rate", federal_tax_rate);
            data.SetNumber("state_tax_rate", state_tax_rate);
            data.SetNumber("property_tax_rate", property_tax_rate);
            data.SetNumber("prop_tax_cost_assessed_percent", prop_tax_cost_assessed_percent);
            data.SetNumber("prop_tax_assessed_decline", prop_tax_assessed_decline);
            data.SetNumber("sales_tax_rate", sales_tax_rate);
            data.SetNumber("real_discount_rate", real_discount_rate);
            data.SetNumber("inflation_rate", inflation_rate);
            data.SetNumber("insurance_rate", insurance_rate);
            data.SetNumber("system_capacity", system_capacity);
            //data.SetNumber("system_heat_rate", system_heat_rate);
            data.SetNumber("loan_term", loan_term);
            data.SetNumber("loan_rate", loan_rate);
            data.SetNumber("loan_debt", loan_debt);
            data.SetNumber("market", market);
            data.SetNumber("mortgage", mortgage);
            data.SetNumber("total_installed_cost", total_installed_cost);
            data.SetNumber("salvage_percentage", salvage_percentage);

            //O&M
            data.SetArray("om_fixed", om_fixed);
            data.SetNumber("om_fixed_escal", om_fixed_escal);
            data.SetArray("om_production", om_production);
            data.SetNumber("om_production_escal", om_production_escal);
            data.SetArray("om_capacity", om_capacity);
            data.SetNumber("om_capacity_escal", om_capacity_escal);
            data.SetArray("om_fuel_cost", om_fuel_cost);
            data.SetNumber("om_fuel_cost_escal", om_fuel_cost_escal);
            data.SetNumber("annual_fuel_usage", annual_fuel_usage);
            
            //ITC Fed and State
            //data.SetNumber("itc_fed_amount", itc_fed_amount);
            //data.SetNumber("itc_fed_amount_deprbas_fed", itc_fed_amount_deprbas_fed);
            //data.SetNumber("itc_fed_amount_deprbas_sta", itc_fed_amount_deprbas_sta);
            //data.SetNumber("itc_sta_amount", itc_sta_amount);
            //data.SetNumber("itc_sta_amount_deprbas_fed", itc_sta_amount_deprbas_fed);
            //data.SetNumber("itc_sta_amount_deprbas_sta", itc_sta_amount_deprbas_sta);
            data.SetNumber("itc_fed_percent", itc_fed_percent);
            data.SetNumber("itc_fed_percent_maxvalue", itc_fed_percent_maxvalue);
            data.SetNumber("itc_fed_percent_deprbas_fed", itc_fed_percent_deprbas_fed);
            data.SetNumber("itc_fed_percent_deprbas_sta", itc_fed_percent_deprbas_sta);
            //data.SetNumber("itc_sta_percent", itc_sta_percent);
            //data.SetNumber("itc_sta_percent_maxvalue", itc_sta_percent_maxvalue);
            //data.SetNumber("itc_sta_percent_deprbas_fed", itc_sta_percent_deprbas_fed);
            //data.SetNumber("itc_sta_percent_deprbas_sta", itc_sta_percent_deprbas_sta);
            
            /**
            //PTC Fed/State
            data.SetArray("ptc_fed_amount", ptc_fed_amount);
            data.SetNumber("ptc_fed_term", ptc_fed_term);
            data.SetNumber("ptc_fed_escal", ptc_fed_escal);
            data.SetArray("ptc_sta_amount", ptc_sta_amount);
            data.SetNumber("ptc_sta_term", ptc_sta_term);
            data.SetNumber("ptc_sta_escal", ptc_sta_escal);

            //IBI Fed/State/Utility/Other
            data.SetNumber("ibi_fed_amount", ibi_fed_amount);
            data.SetNumber("ibi_fed_amount_tax_fed", ibi_fed_amount_tax_fed);
            data.SetNumber("ibi_fed_amount_tax_sta", ibi_fed_amount_tax_sta);
            data.SetNumber("ibi_fed_amount_deprbas_fed", ibi_fed_amount_deprbas_fed);
            data.SetNumber("ibi_fed_amount_deprbas_sta", ibi_fed_amount_deprbas_sta);
            data.SetNumber("ibi_sta_amount", ibi_sta_amount);
            data.SetNumber("ibi_sta_amount_tax_fed", ibi_sta_amount_tax_fed);
            data.SetNumber("ibi_sta_amount_tax_sta", ibi_sta_amount_tax_sta);
            data.SetNumber("ibi_sta_amount_deprbas_fed", ibi_sta_amount_deprbas_fed);
            data.SetNumber("ibi_sta_amount_deprbas_sta", ibi_sta_amount_deprbas_sta);
            data.SetNumber("ibi_uti_amount", ibi_uti_amount);
            data.SetNumber("ibi_uti_amount_tax_fed", ibi_uti_amount_tax_fed);
            data.SetNumber("ibi_uti_amount_tax_sta", ibi_uti_amount_tax_sta);
            data.SetNumber("ibi_uti_amount_deprbas_fed", ibi_uti_amount_deprbas_fed);
            data.SetNumber("ibi_uti_amount_deprbas_sta", ibi_uti_amount_deprbas_sta);
            data.SetNumber("ibi_oth_amount", ibi_oth_amount);
            data.SetNumber("ibi_oth_amount_tax_fed", ibi_oth_amount_tax_fed);
            data.SetNumber("ibi_oth_amount_tax_sta", ibi_oth_amount_tax_sta);
            data.SetNumber("ibi_oth_amount_deprbas_fed", ibi_oth_amount_deprbas_fed);
            data.SetNumber("ibi_oth_amount_deprbas_sta", ibi_oth_amount_deprbas_sta);
            data.SetNumber("ibi_fed_percent", ibi_fed_percent);
            data.SetNumber("ibi_fed_percent_maxvalue", ibi_fed_percent_maxvalue);
            data.SetNumber("ibi_fed_percent_tax_fed", ibi_fed_percent_tax_fed);
            data.SetNumber("ibi_fed_percent_tax_sta", ibi_fed_percent_tax_sta);
            data.SetNumber("ibi_fed_percent_deprbas_fed", ibi_fed_percent_deprbas_fed);
            data.SetNumber("ibi_fed_percent_deprbas_sta", ibi_fed_percent_deprbas_sta);
            data.SetNumber("ibi_sta_percent", ibi_sta_percent);
            data.SetNumber("ibi_sta_percent_maxvalue", ibi_sta_percent_maxvalue);
            data.SetNumber("ibi_sta_percent_tax_fed", ibi_sta_percent_tax_fed);
            data.SetNumber("ibi_sta_percent_tax_sta", ibi_sta_percent_tax_sta);
            data.SetNumber("ibi_sta_percent_deprbas_fed", ibi_sta_percent_deprbas_fed);
            data.SetNumber("ibi_sta_percent_deprbas_sta", ibi_sta_percent_deprbas_sta);
            data.SetNumber("ibi_uti_percent", ibi_uti_percent);
            data.SetNumber("ibi_uti_percent_maxvalue", ibi_uti_percent_maxvalue);
            data.SetNumber("ibi_uti_percent_tax_fed", ibi_uti_percent_tax_fed);
            data.SetNumber("ibi_uti_percent_tax_sta", ibi_uti_percent_tax_sta);
            data.SetNumber("ibi_uti_percent_deprbas_fed", ibi_uti_percent_deprbas_fed);
            data.SetNumber("ibi_uti_percent_deprbas_sta", ibi_uti_percent_deprbas_sta);
            data.SetNumber("ibi_oth_percent", ibi_oth_percent);
            data.SetNumber("ibi_oth_percent_maxvalue", ibi_oth_percent_maxvalue);
            data.SetNumber("ibi_oth_percent_tax_fed", ibi_oth_percent_tax_fed);
            data.SetNumber("ibi_oth_percent_tax_sta", ibi_oth_percent_tax_sta);
            data.SetNumber("ibi_oth_percent_deprbas_fed", ibi_oth_percent_deprbas_fed);
            data.SetNumber("ibi_oth_percent_deprbas_sta", ibi_oth_percent_deprbas_sta);

            // CBI Fed/State/Util/Other
            data.SetNumber("cbi_fed_amount", cbi_fed_amount);
            data.SetNumber("cbi_fed_maxvalue", cbi_fed_maxvalue);
            data.SetNumber("cbi_fed_tax_fed", cbi_fed_tax_fed);
            data.SetNumber("cbi_fed_tax_sta", cbi_fed_tax_sta);
            data.SetNumber("cbi_fed_deprbas_fed", cbi_fed_deprbas_fed);
            data.SetNumber("cbi_fed_deprbas_sta", cbi_fed_deprbas_sta);
            data.SetNumber("cbi_sta_amount", cbi_sta_amount);
            data.SetNumber("cbi_sta_maxvalue", cbi_sta_maxvalue);
            data.SetNumber("cbi_sta_tax_fed", cbi_sta_tax_fed);
            data.SetNumber("cbi_sta_tax_sta", cbi_sta_tax_sta);
            data.SetNumber("cbi_sta_deprbas_fed", cbi_sta_deprbas_fed);
            data.SetNumber("cbi_sta_deprbas_sta", cbi_sta_deprbas_sta);
            data.SetNumber("cbi_uti_amount", cbi_uti_amount);
            data.SetNumber("cbi_uti_maxvalue", cbi_uti_maxvalue);
            data.SetNumber("cbi_uti_tax_fed", cbi_uti_tax_fed);
            data.SetNumber("cbi_uti_tax_sta", cbi_uti_tax_sta);
            data.SetNumber("cbi_uti_deprbas_fed", cbi_uti_deprbas_fed);
            data.SetNumber("cbi_uti_deprbas_sta", cbi_uti_deprbas_sta);
            data.SetNumber("cbi_oth_amount", cbi_oth_amount);
            data.SetNumber("cbi_oth_maxvalue", cbi_oth_maxvalue);
            data.SetNumber("cbi_oth_tax_fed", cbi_oth_tax_fed);
            data.SetNumber("cbi_oth_tax_sta", cbi_oth_tax_sta);
            data.SetNumber("cbi_oth_deprbas_fed", cbi_oth_deprbas_fed);
            data.SetNumber("cbi_oth_deprbas_sta", cbi_oth_deprbas_sta);

            //PBI Fed/State/Util/Other
            data.SetArray("pbi_fed_amount", pbi_fed_amount);
            data.SetNumber("pbi_fed_term", pbi_fed_term);
            data.SetNumber("pbi_fed_escal", pbi_fed_escal);
            data.SetNumber("pbi_fed_tax_fed", pbi_fed_tax_fed);
            data.SetNumber("pbi_fed_tax_sta", pbi_fed_tax_sta);
            data.SetArray("pbi_sta_amount", pbi_sta_amount);
            data.SetNumber("pbi_sta_term", pbi_sta_term);
            data.SetNumber("pbi_sta_escal", pbi_sta_escal);
            data.SetNumber("pbi_sta_tax_fed", pbi_sta_tax_fed);
            data.SetNumber("pbi_sta_tax_sta", pbi_sta_tax_sta);
            data.SetArray("pbi_uti_amount", pbi_uti_amount);
            data.SetNumber("pbi_uti_term", pbi_uti_term);
            data.SetNumber("pbi_uti_escal", pbi_uti_escal);
            data.SetNumber("pbi_uti_tax_fed", pbi_uti_tax_fed);
            data.SetNumber("pbi_uti_tax_sta", pbi_uti_tax_sta);
            data.SetArray("pbi_oth_amount", pbi_oth_amount);
            data.SetNumber("pbi_oth_term", pbi_oth_term);
            data.SetNumber("pbi_oth_escal", pbi_oth_escal);
            data.SetNumber("pbi_oth_tax_fed", pbi_oth_tax_fed);
            data.SetNumber("pbi_oth_tax_sta", pbi_oth_tax_sta);
             */
        }


    }
}
