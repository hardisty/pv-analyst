using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.OutputData;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.Parameters
{
    public class UtilityRateParameterList: ParameterList
    {

        new static Dictionary<string, IDefaultParameter> defaults = new Dictionary<string, IDefaultParameter>() { 
            {"e_with_system", new DefaultFloatArrayParameter("e_with_system",
                                "The hourly net energy at the grid with the system (positive out?).",
                                null)},    
            //{"e_without_system", new DefaultFloatArrayParameter("e_without_system","The hourly net energy at the grid without the system (i.e. the load).",new float[]{0})},    
            {"load_escalation", new DefaultFloatArrayParameter("load_escalation",
                                "The annual rate of load escalation (as percent, 100 = 100%). Length = 12 or single value.", 
                                new float[] { 0 })},
            {"rate_escalation", new DefaultFloatArrayParameter("rate_escalation",
                                "The annual rate of electricity cost escalation (as percent, 100 = 100%) . Length = 12 or single value. Inflation must be"+ 
                                "manually added into the rate, if you want inflation to be reflected therein.",
                                new float[] { 0.5f + 2.5f })},
            {"ur_sell_eq_buy", new DefaultFloatParameter("ur_sell_eq_buy",
                                "Whether the sell rate should be forced to match the buy rate ('Enforce Net Metering'). Boolean 1/0 - T/F",
                                1)},
            {"ur_monthly_fixed_charge", new DefaultFloatParameter("ur_monthly_fixed_charge",
                                "A monthly fixed charge in dollars.",
                                0f)},
            {"ur_flat_buy_rate", new DefaultFloatParameter("ur_flat_buy_rate",
                                "A flat buy rate for electricity in $/kWh.",
                                0.12f )},
            {"ur_flat_sell_rate", new DefaultFloatParameter("ur_flat_sell_rate",
                                "A flat sell rate for electricity, in $/kWh. Ignored if 'ur_sell_eq_buy' is true.",
                                0.12f)},       
            {"ur_tou_enable", new DefaultFloatParameter("ur_tou_enable","Req'd but Unused",0)},
            {"ur_ec_enable", new DefaultFloatParameter("ur_ec_enable","Req'd but Unused",0)},
            {"ur_dc_enable", new DefaultFloatParameter("ur_dc_enable","Req'd but Unused",0)},
            {"ur_tr_enable", new DefaultFloatParameter("ur_tr_enable","Req'd but Unused",0)},
            {"analysis_years", new DefaultFloatParameter("analysis_years",
                                "The number of years for the analysis"
                                ,25)},
            {"system_degradation", new DefaultFloatArrayParameter("system_degradation",
                                "The rate of system degradation",
                                new float[] {0.5f})},
            {"system_availability", new DefaultFloatArrayParameter("system_availability",
                                "The rate of system availability",
                                new float[] {100f})},
        };


        /// Never use these (?) 
        /// TODO decide on this
        //public readonly int ur_tou_enable = 0;
        //public readonly int ur_ec_enable = 0; //utilityrate2 only
        //public readonly int ur_dc_enable = 0;
        //public readonly int ur_tr_enable = 0;

        public UtilityRateParameterList(IEnumerable<IParameter> input) : base(input, defaults) 
        { 
            if (!input.Any(item=>item.name.Equals("e_with_system")))
            {
                throw new ArgumentException("UtilityRateParameter input must specify e_with_system!");
            }
        }

        public UtilityRateOutput runModule()
        {
            Data data = new Data();
            Module module = new Module("utilityrate");

            this.setValues(data);
            
            if (module.Exec(data))
            {
                return new UtilityRateOutput(data);
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

    public class UtilityRateParameterBuilder
    {
        private List<IParameter> list = new List<IParameter>();
        private bool isInit = false;

        public bool isInitialized()
        {
            return this.isInit;
        }

        public UtilityRateParameterBuilder()
        {
        }

        public UtilityRateParameterBuilder(SystemModelOutput smo)
        {
            initialize(smo);
        }
        public void initialize(SystemModelOutput smo)
        {
            isInit = true;
            list.Add(new FloatArrayParameter("e_with_system", smo.getHourlyElectricityProuction()));
        }

        //TODO add other parameters
        /// <summary>
        /// The number of years for the analysis
        /// </summary>
        /// <param name="years">Default: 25</param>
        public void analysis_years(float years)
        {
            list.Add(new FloatParameter("analysis_years", years));
        }

        /// <summary>
        /// The annual rate of utility escalation INCLUDING inflation. Enter as a percent (e.g. 50 = 50%)
        /// If you want 0.5% above a 2.5% inflation rate, the value here must be entered as 3%
        /// </summary>
        /// <param name="rate">Default: 3%</param>
        public void rate_escalation(float rate)
        {
            list.Add(new FloatArrayParameter("rate_escalation", new float[] {rate}));
        }

        /// <summary>
        /// The monthly fixed charge in the utility rate, in $/month
        /// </summary>
        /// <param name="charge">Default: $0/month</param>
        public void ur_monthly_fixed_charge(float charge)
        {
            list.Add(new FloatParameter("ur_monthly_fixed_charge", charge));
        }

        /// <summary>
        /// The buy rate for electricity. Enter in $/kWh
        /// </summary>
        /// <param name="charge">Default: $0.12/kWh</param>
        public void ur_flat_buy_rate(float rate)
        {
            list.Add(new FloatParameter("ur_flat_buy_rate", rate));
        }

        /// <summary>
        /// The sell rate for electricity. Enter in $/kWh
        /// </summary>
        /// <param name="charge">Default: $0.12/kWh</param>
        public void ur_flat_sell_rate(float rate)
        {
            list.Add(new FloatParameter("ur_flat_sell_rate", rate));
        }

        /// <summary>
        /// True/False, should the sell rate be set to match the buy rate? If so, sell rate is ignored.
        /// </summary>
        /// <param name="flag">Default: true</param>
        public void ur_sell_eq_buy(bool flag)
        {
            list.Add(new FloatParameter("ur_sell_eq_buy", Convert.ToInt16(flag)));
        }

        public UtilityRateParameterList build()
        {
            if (isInit)
            {
                return new UtilityRateParameterList(list);
            }
            else
            {
                throw new ArgumentNullException("Must initialize first!");
            }
        }
    }
}
