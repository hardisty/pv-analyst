using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataObjects.OutputData;

namespace SAMAPILibrary.DataObjects.FinancialModels
{
    public class SizeAndCostParams
    {

        public readonly float pv_cost_per_watt_dc;
        public readonly float inv_cost_per_watt_ac;
        public readonly float other_system_cost_per_watt_dc;
        public readonly float installation_cost_per_watt_dc;
        public readonly float installer_overhead_per_watt_dc;

        public readonly float sales_tax_rate;
        public readonly float sales_tax_cost_basis_as_pct;
        public readonly float permitting_env_cost_per_watt_dc;
        public readonly float engineering_cost_per_watt_dc;
        public readonly float grid_intercon_cost_per_watt_dc;
        public readonly float land_cost_per_watt_dc;

        public readonly float dc_rating;
        public readonly float ac_rating;

        public readonly float direct_cost;
        public readonly float indirect_cost;
        public float total_costs{
            get{
                return direct_cost + indirect_cost;
                //return 22194.2f;
            }
        }

        public SizeAndCostParams(SystemModelOutput smo)
        {
            pv_cost_per_watt_dc = 0.72f;
            inv_cost_per_watt_ac = 0.41f;
            other_system_cost_per_watt_dc = 0.49f;
            installation_cost_per_watt_dc = 0.77f;
            installer_overhead_per_watt_dc = 0.91f;

            sales_tax_rate = 5f;
            sales_tax_cost_basis_as_pct = 100f;
            permitting_env_cost_per_watt_dc = 0.12f;
            engineering_cost_per_watt_dc = 0.18f;
            grid_intercon_cost_per_watt_dc = 0f;
            land_cost_per_watt_dc = 0f;

            dc_rating = smo.sys_dc_rating * 1000;
            ac_rating = smo.inv_ac_rating;

            direct_cost = dc_rating * (pv_cost_per_watt_dc + other_system_cost_per_watt_dc + installation_cost_per_watt_dc + installer_overhead_per_watt_dc)
                              + ac_rating * inv_cost_per_watt_ac;
            float sales_tax = (direct_cost) * (1 + sales_tax_rate / 100f) * (sales_tax_cost_basis_as_pct / 100f);
            indirect_cost = sales_tax + dc_rating * (permitting_env_cost_per_watt_dc + engineering_cost_per_watt_dc + grid_intercon_cost_per_watt_dc + land_cost_per_watt_dc);

        }
    }
}
