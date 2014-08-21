using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling.Parameters
{
    public class GUIData
    {
        /// <summary>
        /// Number of years for the analysis
        /// </summary>
        public readonly int analysis_years; 
        /// <summary>
        /// System installed cost per Watt rated DC capacity
        /// </summary>
        public readonly float cost_per_watt_dc; // in $/W
        /// <summary>
        /// The Price to Compare from a utility bill
        /// </summary>
        public readonly float utility_price_to_compare; // in $/kWh
        /// <summary>
        /// The monthly fixed costs on the utility bill
        /// </summary>
        public readonly float utility_monthly_fixed_cost; // in $
        /// <summary>
        /// The annual escalation rate of the utility bill above and beyond inflation
        /// </summary>
        public readonly float utility_ann_escal_rate; // as a pct (100 = 100%)
        /// <summary>        
        /// The inflation rate as a percent
        /// </summary>
        public readonly float inflation_rate; // as a pct (100 = 100%)
        /// <summary>        
        /// The market discount rate as a percent
        /// </summary>
        public readonly float discount_rate; // as a pct (100 = 100%)
        /// <summary>        
        /// The loan interest rate as a percent
        /// </summary>
        public readonly float loan_rate; // as a pct (100 = 100%)
        /// <summary>
        /// The loan term in years
        /// </summary>
        public readonly int loan_term; //in years
        /// <summary>        
        /// The percent of the total cost that was borrowed as a percent
        /// </summary>
        public readonly float loan_debt; // as a pct (100 = 100%)
        /// <summary>        
        /// The SREC price in $/MWh
        /// </summary>
        public readonly float srec_price; // in $/MWh
        /// <summary>        
        /// Globally enable or disable the use of incentives
        /// </summary>
        public readonly bool enable_incentives; 

        public GUIData(GUIDataBuilder b)
        {
            analysis_years = b.manalysis_years;
            cost_per_watt_dc = b.mcost_per_watt_dc;
            utility_price_to_compare = b.mutility_price_to_compare;
            utility_monthly_fixed_cost = b.mutility_monthly_fixed_cost;
            utility_ann_escal_rate = b.mutility_ann_escal_rate;
            inflation_rate = b.minflation_rate;
            discount_rate = b.mdiscount_rate;
            loan_rate = b.mloan_rate;
            loan_term = b.mloan_term;
            loan_debt = b.mloan_debt;
            srec_price = b.msrec_price;
            enable_incentives = b.menable_incentives;
        }
    }

    public class GUIDataBuilder
    {
            public int manalysis_years = 25;
            public int mloan_term = 25;
            public float mcost_per_watt_dc = 0;
            public float mutility_price_to_compare = 0.12f;
            public float mutility_monthly_fixed_cost = 0;
            public float mutility_ann_escal_rate = 0.5f;
            public float minflation_rate = 2.5f;
            public float mdiscount_rate = 8.0f;
            public float mloan_rate = 7.5f;
            public float mloan_debt = 100f;
            public float msrec_price = 50;
            public bool menable_incentives = true;


        public GUIDataBuilder()
        {
        }

        /// <summary>
        /// Number of years for the analysis
        /// </summary>
        /// <param name="value">Default: 25 years</param>
        public GUIDataBuilder analysis_years(int value)
        {
            manalysis_years = value;
            return this;
        }
        /// <summary>
        /// The payback period for the loan in years
        /// </summary>
        /// <param name="value">Default: 25 years</param>
        public GUIDataBuilder loan_term(int value)
        {
            mloan_term = value;
            return this;
        }

        /// <summary>
        /// The system cost per watt DC in $/W. Set to zero to automatically calculate cost.
        /// </summary>
        /// <param name="value">Default: 0</param>
        public GUIDataBuilder cost_per_watt_dc(float value)
        {
            mcost_per_watt_dc = value;
            return this;
        }

        /// <summary>
        /// The price-to-compare from the electricity bill in $/kWh. This will be used as the electricity sell price.
        /// </summary>
        /// <param name="value">Default: 0.12 $/kWh</param>
        public GUIDataBuilder utility_price_to_compare(float value)
        {
            mutility_price_to_compare = value;
            return this;
        }

        /// <summary>
        /// The annual escalation rate for the electricity rate, above and beyond inflation. Enter as percent (i.e. 50 = 50%)
        /// </summary>
        /// <param name="value">Default: 0.5%</param>
        public GUIDataBuilder utility_ann_escal_rate(float value)
        {
            mutility_ann_escal_rate = value;
            return this;
        }

        /// <summary>
        /// Monthly fixedcosts that appear on the electricity bill. Enter in $/month
        /// </summary>
        /// <param name="value">Default: $0</param>
        public GUIDataBuilder utility_monthly_fixed_cost(float value)
        {
            mutility_monthly_fixed_cost = value;
            return this;
        }

        /// <summary>
        /// The inflation rate as a percent (e.g. 50 = 50%)
        /// </summary>
        /// <param name="value">Default: 2.5%</param>
        public GUIDataBuilder inflation_rate(float value)
        {
            minflation_rate = value;
            return this;
        }

        /// <summary>
        /// The market discount rate as a percent (e.g. 50 = 50%)
        /// </summary>
        /// <param name="value">Default: 8%</param>
        public GUIDataBuilder discount_rate(float value)
        {
            mdiscount_rate = value;
            return this;
        }

        /// <summary>
        /// The interest rate of the loan as a percent (e.g. 50 = 50%)
        /// </summary>
        /// <param name="value">Default: 7.5%</param>
        public GUIDataBuilder loan_rate(float value)
        {
            mloan_rate = value;
            return this;
        }

        /// <summary>
        /// The percent of the total system cost for which money is borrowed (e.g. 50 = 50%)
        /// </summary>
        /// <param name="value">Default: 100%</param>
        public GUIDataBuilder loan_debt(float value)
        {
            mloan_debt = value;
            return this;
        }

        /// <summary>
        /// The price of Solar Renewable Energy Credits (SRECs) in $/MWh
        /// </summary>
        /// <param name="value">Default: $50/MWh</param>
        public GUIDataBuilder srec_price(float value)
        {
            msrec_price = value;
            return this;
        }

        /// <summary>
        /// True/False, consider the financial impact all available incentives (including SRECs).
        /// </summary>
        /// <param name="value">Default: true</param>
        public GUIDataBuilder enable_incentives(bool value)
        {
            menable_incentives = value;
            return this;
        }

        public GUIData build()
        {
            GUIData d = new GUIData(this);
            return d;
        }
    }
}
