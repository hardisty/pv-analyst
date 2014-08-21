using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.OutputData;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.Parameters
{
    public class CashLoanParameterList: ParameterList
    {
        new static Dictionary<string, IDefaultParameter> defaults = new Dictionary<string, IDefaultParameter>() {
            {"federal_tax_rate", new DefaultFloatParameter("federal_tax_rate",
                                    "Federal tax rate as percent (e.g. 28% = 28)",
                                    28f)},    
            {"state_tax_rate", new DefaultFloatParameter("state_tax_rate",
                                    "State tax rate as percent (e.g. 28% = 28)", 
                                    4f)},
            {"property_tax_rate", new DefaultFloatParameter("property_tax_rate",
                                    "Property tax rate as percent (e.g. 28% = 28)", 
                                    0f)},
            {"prop_tax_cost_assessed_percent", new DefaultFloatParameter("prop_tax_cost_assessed_percent",
                                    "Percent of property value assessed", 
                                    100f)},
            {"prop_tax_assessed_decline", new DefaultFloatParameter("prop_tax_assessed_decline",
                                    "Percent decline in assessed value (annual)", 
                                    0f)},
            {"sales_tax_rate", new DefaultFloatParameter("sales_tax_rate",
                                    "Sales tax rate as percent (e.g. 28% = 28)", 
                                    5f)},
            {"real_discount_rate", new DefaultFloatParameter("real_discount_rate",
                                    "Real discount rate as percent", 
                                    8f)},                                    
            {"inflation_rate", new DefaultFloatParameter("inflation_rate",
                                    "Annual inflation rate as percent", 
                                    2.5f)},
            {"insurance_rate", new DefaultFloatParameter("insurance_rate",
                                    "Insurance rate as percent of total system cost", 
                                    0)},
            {"system_capacity", new DefaultFloatParameter("system_capacity",
                                    "Total system capacity in kW", 
                                    0)},
            {"market", new DefaultFloatParameter("market",
                                    " Residential (0) or Commercial (1) market type", 
                                    0)},
            {"mortgage", new DefaultFloatParameter("mortgage",
                                    "Boolean (0/1) is it a mortgage loan?  Affects taxability of interest payments.", 
                                    0)},
            {"total_installed_cost", new DefaultFloatParameter("total_installed_cost",
                                    "Total installed system cost in $", 
                                    0)},
            {"salvage_percentage", new DefaultFloatParameter("salvage_percentage",
                                    "Salvage percentage of initial cost", 
                                    0f)},                                    
            {"om_fixed", new DefaultFloatArrayParameter("om_fixed",
                                    "Operating and Maintenance fixed costs by year", 
                                    new float[] { 0f })},
            {"om_fixed_escal", new DefaultFloatParameter("om_fixed_escal",
                                    "Rate of O&M fixed cost escalation", 
                                    0f)},
            {"om_production", new DefaultFloatArrayParameter("om_production",
                                    "Annual O&M based on energy production", 
                                    new float[] {0f})},
            {"om_production_escal", new DefaultFloatParameter("om_production_escal",
                                    "Rate of production based O&M escalation", 
                                    0f)},
            {"om_capacity", new DefaultFloatArrayParameter("om_capacity",
                                    "Annual O&M costs as percent of capacity", 
                                    new float[] {20f})},
            {"om_capacity_escal", new DefaultFloatParameter("om_capacity_escal",
                                    "Rate of capacity based O&M escalation", 
                                    0f)},
            {"om_fuel_cost", new DefaultFloatArrayParameter("om_fuel_cost",
                                    "O&M annual fuel cost", 
                                    new float[] {0f})},
            {"om_fuel_cost_escal", new DefaultFloatParameter("om_fuel_cost_escal",
                                    "Rate of fuel cost O&M escalation", 
                                    0f)},
            {"annual_fuel_usage", new DefaultFloatParameter("annual_fuel_usage",
                                    "Annual O&M fuel usage",
                                    0f)},
            {"itc_fed_percent", new DefaultFloatParameter("itc_fed_percent",
                                    "Federal Investment Tax Credit (ITC) as percent of installed cost",
                                    30f)},
            {"itc_fed_percent_maxvalue", new DefaultFloatParameter("itc_fed_percent_maxvalue",
                                    "Maximum of federal percent based ITC",
                                    1e+038f)},
            {"itc_fed_percent_deprbas_fed", new DefaultFloatParameter("itc_fed_percent_deprbas_sta",
                                    "Federal percent based ITC federal depreciation basis",
                                    0f)},
            {"itc_fed_percent_deprbas_sta", new DefaultFloatParameter("itc_fed_percent_deprbas_sta",
                                    "Federal percent based ITC state depreciation basis",
                                    0f)},
            {"analysis_years", new DefaultFloatParameter("analysis_years",
                                    "Number of years of the analysis",
                                    25f)},
            {"loan_term", new DefaultFloatParameter("loan_term",
                                    "The loan term in years",
                                    25f)},
            {"loan_rate", new DefaultFloatParameter("loan_rate",
                                    "The loan interest rate",
                                    7.5f)},
            {"loan_debt", new DefaultFloatParameter("loan_debt",
                                    "The percent of the total installed cost that was borrowed",
                                    100f)},
            {"energy_value", new DefaultFloatArrayParameter("energy_value",
                                    "The value of the energy produced each year(generated by UtilityRate)",
                                    null)},
            {"energy_net", new DefaultFloatArrayParameter("energy_net",
                                    "The net energy produced each year (generated by UtilityRate)",
                                    null)},
            {"pbi_sta_amount", new DefaultFloatArrayParameter("pbi_sta_amount",
                                    "The state Production Based Incentive amount $/kWh",
                                    new float[] {.05f})},
            {"pbi_sta_term", new DefaultFloatParameter("pbi_sta_term",
                                    "The sate Production Based Incentive term in years",
                                    10)},
            {"pbi_sta_escal", new DefaultFloatParameter("pbi_sta_escal",
                                    "The state Production Based Incentive escalation rate (as pct)",
                                    0f)},
            {"pbi_sta_tax_fed", new DefaultFloatParameter("pbi_sta_tax_fed",
                                    "Boolean, is the state PBI federally taxable",
                                    1)},
            {"pbi_sta_tax_sta", new DefaultFloatParameter("pbi_sta_tax_sta",
                                    "Boolean, is the state PBI state taxable",
                                    1)}
        };

        public CashLoanParameterList(IEnumerable<IParameter> input) : base(input, defaults) 
        {
            checkInputs(input);      
        }

        private void checkInputs(IEnumerable<IParameter> input)
        {
            if (!input.Any(item => item.name.Equals("analysis_years")))
            {
                throw new ArgumentException("CashLoanParameter input must specify analysis_years!");
            }
            if (!input.Any(item => item.name.Equals("energy_value")))
            {
                throw new ArgumentException("CashLoanParameter input must specify energy_value!");
            }
            if (!input.Any(item => item.name.Equals("energy_net")))
            {
                throw new ArgumentException("CashLoanParameter input must specify energy_net!");
            }
            if (!input.Any(item => item.name.Equals("system_capacity")))
            {
                throw new ArgumentException("CashLoanParameter input must specify system_capacity!");
            }
            if (!input.Any(item => item.name.Equals("total_installed_cost")))
            {
                throw new ArgumentException("CashLoanParameter input must specify total_installed_cost!");
            }
        }

        public CashLoanOutput runModule()
        {
            Data data = new Data();
            Module module = new Module("cashloan");

            this.setValues(data);

            if (module.Exec(data))
            {
                return new CashLoanOutput(data);
            }
            else
            {
                int idx = 0;
                String msg;
                int type;
                float time;
                while (module.Log(idx, out msg, out type, out time))
                {
                    String stype = "NOTICE";
                    if (type == API.WARNING) stype = "WARNING";
                    else if (type == API.ERROR) stype = "ERROR";
                    Console.WriteLine("[ " + stype + " at time:" + time + " ]: " + msg + "\n");
                    idx++;
                }
                Console.WriteLine("utilityrate failed\n");
                return null;
            }
        }

        
    }
    public class CashLoanParameterBuilder
    {
        private List<IParameter> list = new List<IParameter>();
        private bool isInit = false;

        public bool isInitialized()
        {
            return this.isInit;
        }

        public CashLoanParameterBuilder()
        {

        }


        public CashLoanParameterBuilder(SystemModelOutput smo, UtilityRateOutput uro)
        {
            initialize(smo, uro);
        }

        public CashLoanParameterBuilder(SizeAndCostParameterList sc, UtilityRateOutput uro)
        {
            initialize(sc, uro);
        }

        public void initialize(SystemModelOutput smo, UtilityRateOutput uro)
        {
            SizeAndCostParameterList sc = new SizeAndCostParameterBuilder(smo).build();
            initialize(sc, uro);
        }

        public void initialize(SizeAndCostParameterList sc, UtilityRateOutput uro)
        {
            isInit = true;
            list.Add(new FloatParameter("analysis_years", uro.getAnalysisYears()));
            list.Add(new FloatArrayParameter("energy_value", uro.getAnnualValueOfNetEnergy()));
            list.Add(new FloatArrayParameter("energy_net", uro.getAnnualNetEnergy()));

            list.Add(new FloatParameter("system_capacity", sc.dc_rating / 1000));
            list.Add(new FloatParameter("total_installed_cost", sc.total_costs));

            list.Add(new FloatParameter("salvage_percentage", uro.getAnalysisYears() * 30 / 25.0f));
        }

        /// <summary>
        /// The duration of the loan, in years
        /// </summary>
        /// <param name="term">Default 25</param>
        public void loan_term(int term)
        {
            list.Add(new FloatParameter("loan_term", term));
        }

        /// <summary>
        /// The loan interest rate, as a percent
        /// </summary>
        /// <param name="rate">Default 7.5</param>
        public void loan_rate(float rate)
        {
            list.Add(new FloatParameter("loan_rate", rate));
        }

        /// <summary>
        /// The percent of the total system cost that was borrowed. Entered as a percent (e.g. 50 = 50%)
        /// </summary>
        /// <param name="debt">Default: 100</param>
        public void loan_debt(float debt)
        {
            list.Add(new FloatParameter("loan_debt", debt));
        }

        /// <summary>
        /// The Production Based Incentive for the state, in $/kWh. Used here to represent the SREC price.
        /// </summary>
        /// <param name="amount">Default 0.05 $/kWh ($50/MWh)</param>
        public void pbi_sta_amount(float amount)
        {
            list.Add(new FloatArrayParameter("pbi_sta_amount",new float[]{amount}));
        }

        /// <summary>
        /// The Federal Investment Tax Credit as a percent of the total cost. Enter as percent (e.g. 50 = 50%).
        /// </summary>
        /// <param name="amount">Default: 30%</param>
        public void itc_fed_percent(float pct)
        {
            list.Add(new FloatParameter("itc_fed_percent", pct));
        }

        /// <summary>
        /// The inflation rate as a percent (e.g. 50 = 50%)
        /// </summary>
        /// <param name="value">Default: 2.5%</param>
        public void inflation_rate(float rate)
        {
            list.Add(new FloatParameter("inflation_rate", rate));
        }

        /// <summary>
        /// The market discount rate as a percent (e.g. 50 = 50%)
        /// </summary>
        /// <param name="value">Default: 8%</param>
        public void discount_rate(float rate)
        {
            list.Add(new FloatParameter("discount_rate", rate));
        }

        public CashLoanParameterList build()
        {
            if (isInit)
            {
                return new CashLoanParameterList(list);
            }
            else
            {
                throw new ArgumentNullException("Must initialize first");
            }
        }
    }
}
