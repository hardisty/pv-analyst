using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.Parameters;

namespace SAMAPILibrary.CalculationWrappers
{
    public class GUIAdapter
    {
        public static SizeAndCostSettings getSizeAndCostSettings(GUIData gui)
        {
            SizeAndCostSettings sc = SizeAndCostSettings.getDefault();
            sc.overall_cost_per_watt_dc = gui.cost_per_watt_dc;
            sc.use_overall_cost_per_watt_dc = gui.use_cost_per_watt_override ? 1 : 0;
            return sc;
        }

        public static UtilityRateSettings getUtilityRateSettings(GUIData gui)
        {
            UtilityRateSettings ub = UtilityRateSettings.getDefault();


            ub.analysis_years = gui.analysis_years;
            ub.rate_escalation = new float[] {gui.inflation_rate + gui.utility_ann_escal_rate};
            ub.ur_monthly_fixed_charge = gui.utility_monthly_fixed_cost;
            ub.ur_flat_buy_rate = gui.utility_price_to_compare;
            ub.ur_flat_sell_rate = gui.utility_price_to_compare;

            return ub;

            
        }

        public static CashLoanSettings getCashLoanSettings(GUIData gui)
        {
            CashLoanSettings cb = CashLoanSettings.getDefault();
            cb.loan_rate = gui.loan_rate;
            cb.loan_term = gui.loan_term;
            cb.loan_debt = gui.loan_debt;
            cb.inflation_rate = gui.inflation_rate;
            cb.real_discount_rate = gui.discount_rate;

            if (gui.enable_incentives)
            {//Enable
                cb.pbi_sta_amount = new float[] {gui.srec_price / 1000f};
                cb.itc_fed_percent = 30;
            }
            else
            {//Disable
                cb.pbi_sta_amount = new float[] {0};
                cb.itc_fed_percent = 0;
            }

            return cb;
        }
    }
}
