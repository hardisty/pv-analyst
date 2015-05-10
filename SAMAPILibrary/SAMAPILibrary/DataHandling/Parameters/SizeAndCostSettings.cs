using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.OutputData;

namespace SAMAPILibrary.DataHandling.Parameters
{
    public class SizeAndCostSettings
    {
        public float pv_cost_per_watt_dc = 0.72f;
        public float inv_cost_per_watt_ac = 0.41f;
        public float other_system_cost_per_watt_dc = 0.49f;
        public float installation_cost_per_watt_dc = 0.77f;
        public float installer_overhead_per_watt_dc = 0.91f;
        public float sales_tax_rate = 5;
        public float sales_tax_cost_basis_as_pct = 100;
        public float permitting_env_cost_per_watt_dc = 0.18f;
        public float engineering_cost_per_watt_dc = 0.12f;
        public float grid_intercon_cost_per_watt_dc = 0;
        public float land_cost_per_watt_dc = 0;
        public float ac_rating = 0;
        public float dc_rating = 0;
        public float overall_cost_per_watt_dc = 0;
        public float use_overall_cost_per_watt_dc = 0; //boolean
        float direct_cost;
        float indirect_cost;
        public float total_cost
        {
            get
            {
                return direct_cost + indirect_cost;
            }
        }
        public float cost_per_watt_dc
        {
            get
            {
                return total_cost / dc_rating;
            }
        }

        private SizeAndCostSettings() { }

        public void setValuesFromPriorOutput(PVSAMV1Output smo)
        {
            ac_rating = smo.inv_ac_rating;
            dc_rating = smo.sys_dc_rating * 1000;

            direct_cost = dc_rating * (pv_cost_per_watt_dc + other_system_cost_per_watt_dc + installation_cost_per_watt_dc + installer_overhead_per_watt_dc)
                              + ac_rating * inv_cost_per_watt_ac;
            float sales_tax = (direct_cost) * (sales_tax_rate / 100f) * (sales_tax_cost_basis_as_pct / 100f);
            indirect_cost = sales_tax + dc_rating * (permitting_env_cost_per_watt_dc + engineering_cost_per_watt_dc + grid_intercon_cost_per_watt_dc + land_cost_per_watt_dc);


            if (use_overall_cost_per_watt_dc > 0)
            {
                direct_cost *= overall_cost_per_watt_dc / cost_per_watt_dc;
                indirect_cost *= overall_cost_per_watt_dc / cost_per_watt_dc;
            }
        }
        public static SizeAndCostSettings getDefault(){
            return new SizeAndCostSettings();
        }

    }
}
