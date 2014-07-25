using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataObjects.FinancialModels
{
    class CostParams
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

        public CostParams()
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
        }

    }
}
