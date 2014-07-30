using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataObjects.OutputData;

namespace SAMAPILibrary.DataHandling
{
    public class SizeAndCostParameterList:ParameterList
    {
        new static Dictionary<string, IDefaultParameter> defaults = new Dictionary<string, IDefaultParameter>() {
            {"pv_cost_per_watt_dc", new DefaultFloatParameter("pv_cost_per_watt_dc",
                                    "Cost of PV per watt dc capacity",
                                    0.72f)},    
            {"inv_cost_per_watt_ac", new DefaultFloatParameter("inv_cost_per_watt_ac",
                                    "Cost of inverter per watt ac capacity", 
                                    0.41f)},
            {"other_system_cost_per_watt_dc", new DefaultFloatParameter("other_system_cost_per_watt_dc",
                                    "Other system costs per watt dc", 
                                    0.49f)},
            {"installation_cost_per_watt_dc", new DefaultFloatParameter("installation_cost_per_watt_dc",
                                    "Cost of installation per watt dc capacity", 
                                    0.77f)},
            {"installer_overhead_per_watt_dc", new DefaultFloatParameter("installer_overhead_per_watt_dc",
                                    "Installer overhead per watt dc", 
                                    0.91f)},
            {"sales_tax_rate", new DefaultFloatParameter("sales_tax_rate",
                                    "Sales tax rate as percent", 
                                    5f)},
            {"sales_tax_cost_basis_as_pct", new DefaultFloatParameter("sales_tax_cost_basis_as_pct",
                                    "Percent of ac capacity sales tax applies to", 
                                    100f)},
            {"permitting_env_cost_per_watt_dc", new DefaultFloatParameter("permitting_env_cost_per_watt_dc",
                                    "Permitting and environmental costs per watt dc", 
                                    0.18f)},
            {"engineering_cost_per_watt_dc", new DefaultFloatParameter("engineering_cost_per_watt_dc",
                                    "Engineering costs per watt dc", 
                                    0.12f)},
            {"grid_intercon_cost_per_watt_dc", new DefaultFloatParameter("grid_intercon_cost_per_watt_dc",
                                    "Grid interconnection costs per watt dc", 
                                    0f)},
            {"land_cost_per_watt_dc", new DefaultFloatParameter("land_cost_per_watt_dc",
                                    "Land costs per watt dc", 
                                    0f)},
            {"ac_rating", new DefaultFloatParameter("ac_rating",
                                    "Inverter AC rated capacity", 
                                    0f)},
            {"dc_rating", new DefaultFloatParameter("dc_rating",
                                    "System DC rated capacity", 
                                    0f)}
        };

        float direct_cost;
        float indirect_cost;
        public float total_costs
        {
            get
            {
                return direct_cost + indirect_cost;
                //return 22194.2f;
            }
        }
        public float dc_rating
        {
            get
            {
                return ((Parameter<float>)parameters["dc_rating"]).value;
            }
        }

        public SizeAndCostParameterList(IEnumerable<IParameter> input) : base(input, defaults) 
        {
            
            float dc_rating = ((Parameter<float>)parameters["dc_rating"]).value;
            float other_system_cost_per_watt_dc = ((Parameter<float>)parameters["other_system_cost_per_watt_dc"]).value;
            float installation_cost_per_watt_dc = ((Parameter<float>)parameters["installation_cost_per_watt_dc"]).value;
            float installer_overhead_per_watt_dc = ((Parameter<float>)parameters["installer_overhead_per_watt_dc"]).value;
            float ac_rating = ((Parameter<float>)parameters["ac_rating"]).value;
            float inv_cost_per_watt_ac = ((Parameter<float>)parameters["inv_cost_per_watt_ac"]).value;
            float pv_cost_per_watt_dc = ((Parameter<float>)parameters["pv_cost_per_watt_dc"]).value;
            float sales_tax_rate = ((Parameter<float>)parameters["sales_tax_rate"]).value;
            float sales_tax_cost_basis_as_pct = ((Parameter<float>)parameters["sales_tax_cost_basis_as_pct"]).value;
            float permitting_env_cost_per_watt_dc = ((Parameter<float>)parameters["permitting_env_cost_per_watt_dc"]).value;
            float engineering_cost_per_watt_dc = ((Parameter<float>)parameters["engineering_cost_per_watt_dc"]).value;
            float grid_intercon_cost_per_watt_dc = ((Parameter<float>)parameters["grid_intercon_cost_per_watt_dc"]).value;
            float land_cost_per_watt_dc = ((Parameter<float>)parameters["land_cost_per_watt_dc"]).value;
            


            direct_cost = dc_rating * (pv_cost_per_watt_dc + other_system_cost_per_watt_dc + installation_cost_per_watt_dc + installer_overhead_per_watt_dc)
                              + ac_rating * inv_cost_per_watt_ac;
            float sales_tax = (direct_cost) * (1 + sales_tax_rate / 100f) * (sales_tax_cost_basis_as_pct / 100f);
            indirect_cost = sales_tax + dc_rating * (permitting_env_cost_per_watt_dc + engineering_cost_per_watt_dc + grid_intercon_cost_per_watt_dc + land_cost_per_watt_dc);
        }

        
    }

    public class SizeAndCostParameterBuilder
    {
        private List<IParameter> list = new List<IParameter>();
        private bool isInit = false;

        public bool isInitialized()
        {
            return this.isInit;
        }

        public SizeAndCostParameterBuilder()
        {

        }


        public SizeAndCostParameterBuilder(SystemModelOutput smo)
        {
            initialize(smo);
        }


        public void initialize(SystemModelOutput smo)
        {
            isInit = true;
            list.Add(new FloatParameter("dc_rating", smo.sys_dc_rating * 1000));
            list.Add(new FloatParameter("ac_rating", smo.inv_ac_rating));
        }

        //TODO add parameter initializers


        public SizeAndCostParameterList build()
        {
            if (isInit)
            {
                return new SizeAndCostParameterList(list);
            }
            else
            {
                throw new ArgumentNullException("Must initialize first");
            }
        }
    }
}
