using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.Parameters;

namespace SAMAPILibrary.CalculationWrappers
{
    public class GUIAdapter
    {
        public static void applySizeAndCostSettings(SizeAndCostSettings sc, GUIData gui)
        {
            sc.overall_cost_per_watt_dc = gui.cost_per_watt_dc;
            sc.use_overall_cost_per_watt_dc = gui.use_cost_per_watt_override ? 1 : 0;
        }

        public static void applyUtilityRateSettings(UtilityRateSettings urs, GUIData gui)
        {
            urs.analysis_years = gui.analysis_years;
            urs.rate_escalation = new float[] {gui.inflation_rate + gui.utility_ann_escal_rate};
            urs.ur_monthly_fixed_charge = gui.utility_monthly_fixed_cost;
            urs.ur_flat_buy_rate = gui.utility_price_to_compare;
            urs.ur_flat_sell_rate = gui.utility_price_to_compare;
        }

        public static void applyCashLoanSettings(CashLoanSettings cls, GUIData gui)
        {
            cls.loan_rate = gui.loan_rate;
            cls.loan_term = gui.loan_term;
            cls.loan_debt = gui.loan_debt;
            cls.inflation_rate = gui.inflation_rate;
            cls.real_discount_rate = gui.discount_rate;

            if (gui.enable_incentives)
            {//Enable
                cls.pbi_sta_amount = new float[] {gui.srec_price / 1000f};
                cls.itc_fed_percent = 30;
            }
            else
            {//Disable
                cls.pbi_sta_amount = new float[] {0};
                cls.itc_fed_percent = 0;
            }

        }
    }
}
