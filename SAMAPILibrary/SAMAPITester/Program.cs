using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;
using SAMAPILibrary.DataObjects;
using SAMAPILibrary.DataObjects.OutputData;
using SAMAPILibrary.CalculationWrappers;
using SAMAPILibrary.DataHandling;
using SAMAPILibrary.DataHandling.ParameterTypes;

namespace SAMAPITester
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //PVSAM();
            SAMRewriteTest();

        }
        static void SAMRewriteTest()
        {
            GISData gis = new GISData();

            ArrayParameterListBuilder ab = new ArrayParameterListBuilder();
            UtilityRateParameterBuilder ub = new UtilityRateParameterBuilder();
            CashLoanParameterBuilder cb = new CashLoanParameterBuilder();
            SizeAndCostParameterBuilder sc = new SizeAndCostParameterBuilder();

            float[] soiling = new float[] { 0.9f, 0.9f, 0.9f, 0.9f, 0.9f, 0.9f, 0.9f, 0.9f, 0.9f, 0.9f, 0.9f, 0.9f };
            ab.subarray1_soiling(soiling);


            ab.initialize(gis);
            SystemModelOutput smo = ab.build().runModule();

            ub.initialize(smo);
            UtilityRateOutput uro = ub.build().runModule();

            sc.initialize(smo);
            cb.initialize(sc.build(), uro);
            CashLoanOutput clo = cb.build().runModule();

            Console.WriteLine("Year One Power Produced: " + smo.ac_annual);
            Console.WriteLine("Value of energy in Year 1: $" + uro.getAnnualValueOfNetEnergy()[0]);
            Console.WriteLine("Net Present Value: $" + clo.npv);
        }

        static void pvWattsTest()
        {
            Data data = new Data();
            data.SetString("file_name", "ExampleFiles\\abilene.tm2");
            data.SetNumber("system_size", 4.0f);
            data.SetNumber("derate", 0.77f);
            data.SetNumber("track_mode", 0);
            data.SetNumber("tilt", 20);
            data.SetNumber("azimuth", 180);

            Module mod = new Module("pvwattsv1");
            if (mod.Exec(data))
            {
                float tot = data.GetNumber("ac_annual");
                float[] ac = data.GetArray("ac_monthly");
                for (int i = 0; i < ac.Count(); i++)
                    Console.WriteLine("[" + i + "]: " + ac[i] + " kWh\n");
                Console.WriteLine("AC total: " + tot + "\n");
                Console.WriteLine("PVWatts test OK\n");
            }
            else
            {
                int idx = 0;
                String msg;
                int type;
                float time;
                while (mod.Log(idx, out msg, out type, out time))
                {
                    String stype = "NOTICE";
                    if (type == API.WARNING) stype = "WARNING";
                    else if (type == API.ERROR) stype = "ERROR";
                    Console.WriteLine("[ " + stype + " at time:" + time + " ]: " + msg + "\n");
                    idx++;
                }
                Console.WriteLine("PVWatts example failed\n");
            }
        }

        static void ModulesAndVariables(){
            Entry sscEntry = new Entry();
            
            int moduleIndex = 0;
            while (sscEntry.Get())
            {
                String moduleName = sscEntry.Name();
                String description = sscEntry.Description();
                int version = sscEntry.Version();
                Console.WriteLine("\nModule: " + moduleName + ", version: " + version + "\n");
                Console.WriteLine(" " + description + "\n");
                moduleIndex++;

                Module sscModule = new Module(moduleName);
                Info sscInfo = new Info(sscModule);

                while (sscInfo.Get())
                {
                    Console.WriteLine("\t" + sscInfo.VarType() + ": \"" + sscInfo.Name() + "\" " + " [" + sscInfo.DataType() + "] " + sscInfo.Label() + " (" + sscInfo.Units() + ")\n");
                }
            }
        }

        static void PVSAM()
        {
            #region System Model and Climate File
            // Run Default SAM Flat Plate PV with Residential financing

            Data data = new Data();
            data.SetString("weather_file", "ExampleFiles\\AZ Phoenix.tm2");
            data.SetNumber("use_wf_albedo", 1f);
            data.SetArray("albedo", new float[] { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f });
            data.SetNumber("irrad_mode", 0f);
            data.SetNumber("sky_model", 2f);
            data.SetNumber("ac_derate", 0.99f);
            data.SetNumber("modules_per_string", 9f);
            data.SetNumber("strings_in_parallel", 2f);
            data.SetNumber("inverter_count", 1f);
            //data.SetNumber("enable_mismatch_vmax_calc", 0f);
            data.SetNumber("subarray1_tilt", 20);
            //data.SetNumber("subarray1_tilt_eq_lat", 0f);
            data.SetNumber("subarray1_azimuth", 180f);
            data.SetNumber("subarray1_track_mode", 0f);
            //data.SetNumber("subarray1_rotlim", 45f);
            //data.SetNumber("subarray1_enable_backtracking", 0f);
            //data.SetNumber("subarray1_btwidth", 2f);
            //data.SetNumber("subarray1_btspacing", 1f);
            data.SetArray("subarray1_soiling", new float[] { 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f });
            data.SetNumber("subarray1_derate", 0.955598f);
            //data.SetNumber("subarray2_enable", 0f);
            //data.SetNumber("subarray2_nstrings", 0f);
            data.SetNumber("subarray2_tilt", 90f);
            //data.SetNumber("subarray2_tilt_eq_lat", 0f);
            //data.SetNumber("subarray2_azimuth", 180f);
            //data.SetNumber("subarray2_track_mode", 0f);
            //data.SetNumber("subarray2_rotlim", 45f);
            //data.SetNumber("subarray2_enable_backtracking", 0f);
            //data.SetNumber("subarray2_btwidth", 2f);
            //data.SetNumber("subarray2_btspacing", 1f);
            //data.SetArray("subarray2_soiling", new float[] { 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f });
            //data.SetNumber("subarray2_derate", 0.955598f);
            //data.SetNumber("subarray3_enable", 0f);
            //data.SetNumber("subarray3_nstrings", 0f);
            data.SetNumber("subarray3_tilt", 20f);
            //data.SetNumber("subarray3_tilt_eq_lat", 0f);
            //data.SetNumber("subarray3_azimuth", 180f);
            //data.SetNumber("subarray3_track_mode", 0f);
            //data.SetNumber("subarray3_rotlim", 45f);
            //data.SetNumber("subarray3_enable_backtracking", 0f);
            //data.SetNumber("subarray3_btwidth", 2f);
            //data.SetNumber("subarray3_btspacing", 1f);
            //data.SetArray("subarray3_soiling", new float[] { 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f });
            //data.SetNumber("subarray3_derate", 0.955598f);
            //data.SetNumber("subarray4_enable", 0f);
            //data.SetNumber("subarray4_nstrings", 0f);
            data.SetNumber("subarray4_tilt", 20f);
            //data.SetNumber("subarray4_tilt_eq_lat", 0f);
            //data.SetNumber("subarray4_azimuth", 180f);
            //data.SetNumber("subarray4_track_mode", 0f);
            //data.SetNumber("subarray4_rotlim", 45f);
            //data.SetNumber("subarray4_enable_backtracking", 0f);
            //data.SetNumber("subarray4_btwidth", 2f);
            //data.SetNumber("subarray4_btspacing", 1f);
            //data.SetArray("subarray4_soiling", new float[] { 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f });
            //data.SetNumber("subarray4_derate", 0.955598f);
            data.SetNumber("module_model", 1f);
            //data.SetNumber("spe_area", 0.74074f);
            //data.SetNumber("spe_rad0", 200f);
            //data.SetNumber("spe_rad1", 400f);
            //data.SetNumber("spe_rad2", 600f);
            //data.SetNumber("spe_rad3", 800f);
            //data.SetNumber("spe_rad4", 1000f);
            //data.SetNumber("spe_eff0", 13.5f);
            //data.SetNumber("spe_eff1", 13.5f);
            //data.SetNumber("spe_eff2", 13.5f);
            //data.SetNumber("spe_eff3", 13.5f);
            //data.SetNumber("spe_eff4", 13.5f);
            //data.SetNumber("spe_reference", 4f);
            //data.SetNumber("spe_module_structure", 0f);
            //data.SetNumber("spe_a", -3.56f);
            //data.SetNumber("spe_b", -0.075f);
            //data.SetNumber("spe_dT", 3f);
            //data.SetNumber("spe_temp_coeff", -0.5f);
            //data.SetNumber("spe_fd", 1f);
            data.SetNumber("cec_area", 1.244f);
            data.SetNumber("cec_a_ref", 1.9816f);
            data.SetNumber("cec_adjust", 20.8f);
            data.SetNumber("cec_alpha_sc", 0.002651f);
            data.SetNumber("cec_beta_oc", -0.14234f);
            data.SetNumber("cec_gamma_r", -0.407f);
            data.SetNumber("cec_i_l_ref", 5.754f);
            data.SetNumber("cec_i_mp_ref", 5.25f);
            data.SetNumber("cec_i_o_ref", 1.919e-010f);
            data.SetNumber("cec_i_sc_ref", 5.75f);
            data.SetNumber("cec_n_s", 72f);
            data.SetNumber("cec_r_s", 0.105f);
            data.SetNumber("cec_r_sh_ref", 160.48f);
            data.SetNumber("cec_t_noct", 49.2f);
            data.SetNumber("cec_v_mp_ref", 41f);
            data.SetNumber("cec_v_oc_ref", 47.7f);
            data.SetNumber("cec_temp_corr_mode", 0f);
            data.SetNumber("cec_standoff", 6f);
            data.SetNumber("cec_height", 0f);
            data.SetNumber("cec_mounting_config", 0f);
            data.SetNumber("cec_heat_transfer", 0f);
            data.SetNumber("cec_mounting_orientation", 0f);
            data.SetNumber("cec_gap_spacing", 0.05f);
            data.SetNumber("cec_module_width", 1f);
            data.SetNumber("cec_module_length", 1.244f);
            data.SetNumber("cec_array_rows", 1f);
            data.SetNumber("cec_array_cols", 10f);
            data.SetNumber("cec_backside_temp", 20f);
            //data.SetNumber("6par_celltech", 1f);
            //data.SetNumber("6par_vmp", 30f);
            //data.SetNumber("6par_imp", 6f);
            //data.SetNumber("6par_voc", 37f);
            //data.SetNumber("6par_isc", 7f);
            //data.SetNumber("6par_bvoc", -0.11f);
            //data.SetNumber("6par_aisc", 0.004f);
            //data.SetNumber("6par_gpmp", -0.41f);
            //data.SetNumber("6par_nser", 60f);
            //data.SetNumber("6par_area", 1.3f);
            //data.SetNumber("6par_tnoct", 46f);
            //data.SetNumber("6par_standoff", 6f);
            //data.SetNumber("6par_mounting", 0f);
            //data.SetNumber("snl_module_structure", 0f);
            //data.SetNumber("snl_a", -3.62f);
            //data.SetNumber("snl_b", -0.075f);
            //data.SetNumber("snl_dtc", 3f);
            //data.SetNumber("snl_ref_a", -3.62f);
            //data.SetNumber("snl_ref_b", -0.075f);
            //data.SetNumber("snl_ref_dT", 3f);
            //data.SetNumber("snl_fd", 1f);
            //data.SetNumber("snl_a0", 0.94045f);
            //data.SetNumber("snl_a1", 0.052641f);
            //data.SetNumber("snl_a2", -0.0093897f);
            //data.SetNumber("snl_a3", 0.00072623f);
            //data.SetNumber("snl_a4", -1.9938e-005f);
            //data.SetNumber("snl_aimp", -0.00038f);
            //data.SetNumber("snl_aisc", 0.00061f);
            //data.SetNumber("snl_area", 1.244f);
            //data.SetNumber("snl_b0", 1f);
            //data.SetNumber("snl_b1", -0.002438f);
            //data.SetNumber("snl_b2", 0.0003103f);
            //data.SetNumber("snl_b3", -1.246e-005f);
            //data.SetNumber("snl_b4", 2.112e-007f);
            //data.SetNumber("snl_b5", -1.359e-009f);
            //data.SetNumber("snl_bvmpo", -0.139f);
            //data.SetNumber("snl_bvoco", -0.136f);
            //data.SetNumber("snl_c0", 1.0039f);
            //data.SetNumber("snl_c1", -0.0039f);
            //data.SetNumber("snl_c2", 0.291066f);
            //data.SetNumber("snl_c3", -4.73546f);
            //data.SetNumber("snl_c4", 0.9942f);
            //data.SetNumber("snl_c5", 0.0058f);
            //data.SetNumber("snl_c6", 1.0723f);
            //data.SetNumber("snl_c7", -0.0723f);
            //data.SetNumber("snl_impo", 5.25f);
            //data.SetNumber("snl_isco", 5.75f);
            //data.SetNumber("snl_ixo", 5.65f);
            //data.SetNumber("snl_ixxo", 3.85f);
            //data.SetNumber("snl_mbvmp", 0f);
            //data.SetNumber("snl_mbvoc", 0f);
            //data.SetNumber("snl_n", 1.221f);
            //data.SetNumber("snl_series_cells", 72f);
            //data.SetNumber("snl_vmpo", 40f);
            //data.SetNumber("snl_voco", 47.7f);
            data.SetNumber("inverter_model", 0f);
            data.SetNumber("inv_snl_paco", 4000f);
            data.SetNumber("inv_snl_c0", -6.57929e-006f);
            data.SetNumber("inv_snl_c1", 4.72925e-005f);
            data.SetNumber("inv_snl_c2", 0.00202195f);
            data.SetNumber("inv_snl_c3", 0.000285321f);
            data.SetNumber("inv_snl_paco", 4000f);
            data.SetNumber("inv_snl_pdco", 4186f);
            data.SetNumber("inv_snl_pnt", 0.17f);
            data.SetNumber("inv_snl_pso", 19.7391f);
            data.SetNumber("inv_snl_vdco", 310.67f);
            data.SetNumber("inv_snl_vdcmax", 0f);
            //data.SetNumber("self_shading_enabled", 0f);
            //data.SetNumber("self_shading_length", 1.84844f);
            //data.SetNumber("self_shading_width", 0.673f);
            //data.SetNumber("self_shading_mod_orient", 1f);
            //data.SetNumber("self_shading_str_orient", 0f);
            //data.SetNumber("self_shading_ncellx", 6f);
            //data.SetNumber("self_shading_ncelly", 12f);
            //data.SetNumber("self_shading_ndiode", 3f);
            //data.SetNumber("self_shading_nmodx", 2f);
            //data.SetNumber("self_shading_nstrx", 1f);
            //data.SetNumber("self_shading_nmody", 3f);
            //data.SetNumber("self_shading_nrows", 3f);
            //data.SetNumber("self_shading_rowspace", 5f);
#endregion 
            float[] ac_hourly = new float[] { 0 };
            Module module = new Module("pvsamv1");
            if (module.Exec(data))
            {
                ac_hourly = data.GetArray("hourly_ac_net");
                float[] ac_monthly = data.GetArray("monthly_ac_net");
                float ac_annual = data.GetNumber("annual_ac_net");
                Console.WriteLine(ac_annual);
                //for (int i = 0; i < ac_monthly.Length; i++)
                //    Console.WriteLine("ac_monthly[" + i + "] (kWh) = " + ac_monthly[i] + "\n");
                //Console.WriteLine("ac_annual (kWh) = " + ac_annual + "\n");
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
                Console.WriteLine("pvsamv1 example failed\n");
            }

            #region // annualoutput input variables  //This doesn't affect the final output it doesn't seem lik.
            data.SetNumber("analysis_years", 25f);
            data.SetArray("energy_availability", new float[] { 100f });
            data.SetArray("energy_degradation", new float[] { 0.5f });
            data.SetMatrix("energy_curtailment", new float[,]
{ { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, 
{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f } });
            data.SetNumber("system_use_lifetime_output", 0f);
            data.SetArray("energy_net_hourly", ac_hourly);
            #endregion 
            float[] net_hourly = new float[] { 0 };
            module = new Module("annualoutput");
            if (module.Exec(data))
            {
                net_hourly = data.GetArray("hourly_e_net_delivered");
                float[] net_annual = new float[] { };
                net_annual = data.GetArray("annual_e_net_delivered");
                for (int i=0;i<net_annual.Length;i++){
                Console.WriteLine(net_annual[i]);
                }
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
                Console.WriteLine("annualoutput example failed\n");
            }


            #region // utilityrate input variables
            data.SetNumber("analysis_years", 25f);
            data.SetArray("e_with_system", net_hourly);
            data.SetArray("system_availability", new float[] { 100f });
            data.SetArray("system_degradation", new float[] { 0.5f });
            data.SetArray("load_escalation", new float[] { 2.5f });
            data.SetArray("rate_escalation", new float[] { 2.5f });
            data.SetNumber("ur_sell_eq_buy", 1f);
            data.SetNumber("ur_monthly_fixed_charge", 0f);
            data.SetNumber("ur_flat_buy_rate", 0.12f);
            data.SetNumber("ur_flat_sell_rate", 0f);
            data.SetNumber("ur_tou_enable", 0f);
            data.SetNumber("ur_dc_enable", 0f);
            data.SetNumber("ur_tr_enable", 0f);
            //data.SetNumber("ur_tou_p1_buy_rate", 0.12f);
            //data.SetNumber("ur_tou_p1_sell_rate", 0f);
            //data.SetNumber("ur_tou_p2_buy_rate", 0.12f);
            //data.SetNumber("ur_tou_p2_sell_rate", 0f);
            //data.SetNumber("ur_tou_p3_buy_rate", 0.12f);
            //data.SetNumber("ur_tou_p3_sell_rate", 0f);
            //data.SetNumber("ur_tou_p4_buy_rate", 0.12f);
            //data.SetNumber("ur_tou_p4_sell_rate", 0f);
            //data.SetNumber("ur_tou_p5_buy_rate", 0.12f);
            //data.SetNumber("ur_tou_p5_sell_rate", 0f);
            //data.SetNumber("ur_tou_p6_buy_rate", 0.12f);
            //data.SetNumber("ur_tou_p6_sell_rate", 0f);
            //data.SetNumber("ur_tou_p7_buy_rate", 0.12f);
            //data.SetNumber("ur_tou_p7_sell_rate", 0f);
            //data.SetNumber("ur_tou_p8_buy_rate", 0.12f);
            //data.SetNumber("ur_tou_p8_sell_rate", 0f);
            //data.SetNumber("ur_tou_p9_buy_rate", 0.12f);
            //data.SetNumber("ur_tou_p9_sell_rate", 0f);
            //data.SetString("ur_tou_sched_weekday", "111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
            //data.SetString("ur_tou_sched_weekend", "111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
            
            //data.SetNumber("ur_dc_fixed_m1", 0f);
            //data.SetNumber("ur_dc_fixed_m2", 0f);
            //data.SetNumber("ur_dc_fixed_m3", 0f);
            //data.SetNumber("ur_dc_fixed_m4", 0f);
            //data.SetNumber("ur_dc_fixed_m5", 0f);
            //data.SetNumber("ur_dc_fixed_m6", 0f);
            //data.SetNumber("ur_dc_fixed_m7", 0f);
            //data.SetNumber("ur_dc_fixed_m8", 0f);
            //data.SetNumber("ur_dc_fixed_m9", 0f);
            //data.SetNumber("ur_dc_fixed_m10", 0f);
            //data.SetNumber("ur_dc_fixed_m11", 0f);
            //data.SetNumber("ur_dc_fixed_m12", 0f);
            //data.SetNumber("ur_dc_p1", 0f);
            //data.SetNumber("ur_dc_p2", 0f);
            //data.SetNumber("ur_dc_p3", 0f);
            //data.SetNumber("ur_dc_p4", 0f);
            //data.SetNumber("ur_dc_p5", 0f);
            //data.SetNumber("ur_dc_p6", 0f);
            //data.SetNumber("ur_dc_p7", 0f);
            //data.SetNumber("ur_dc_p8", 0f);
            //data.SetNumber("ur_dc_p9", 0f);
            //data.SetString("ur_dc_sched_weekday", "444444443333333333334444444444443333333333334444444444443333333333334444444444443333333333334444222222221111111111112222222222221111111111112222222222221111111111112222222222221111111111112222222222221111111111112222222222221111111111112222444444443333333333334444444444443333333333334444");
            //data.SetString("ur_dc_sched_weekend", "444444443333333333334444444444443333333333334444444444443333333333334444444444443333333333334444222222221111111111112222222222221111111111112222222222221111111111112222222222221111111111112222222222221111111111112222222222221111111111112222444444443333333333334444444444443333333333334444");
            
            //data.SetNumber("ur_tr_sell_mode", 1f);
            //data.SetNumber("ur_tr_sell_rate", 0f);
            //data.SetNumber("ur_tr_s1_energy_ub1", 1e+038f);
            //data.SetNumber("ur_tr_s1_energy_ub2", 1e+038f);
            //data.SetNumber("ur_tr_s1_energy_ub3", 1e+038f);
            //data.SetNumber("ur_tr_s1_energy_ub4", 1e+038f);
            //data.SetNumber("ur_tr_s1_energy_ub5", 1e+038f);
            //data.SetNumber("ur_tr_s1_energy_ub6", 1e+038f);
            //data.SetNumber("ur_tr_s1_rate1", 0f);
            //data.SetNumber("ur_tr_s1_rate2", 0f);
            //data.SetNumber("ur_tr_s1_rate3", 0f);
            //data.SetNumber("ur_tr_s1_rate4", 0f);
            //data.SetNumber("ur_tr_s1_rate5", 0f);
            //data.SetNumber("ur_tr_s1_rate6", 0f);
            //data.SetNumber("ur_tr_s2_energy_ub1", 1e+038f);
            //data.SetNumber("ur_tr_s2_energy_ub2", 1e+038f);
            //data.SetNumber("ur_tr_s2_energy_ub3", 1e+038f);
            //data.SetNumber("ur_tr_s2_energy_ub4", 1e+038f);
            //data.SetNumber("ur_tr_s2_energy_ub5", 1e+038f);
            //data.SetNumber("ur_tr_s2_energy_ub6", 1e+038f);
            //data.SetNumber("ur_tr_s2_rate1", 0f);
            //data.SetNumber("ur_tr_s2_rate2", 0f);
            //data.SetNumber("ur_tr_s2_rate3", 0f);
            //data.SetNumber("ur_tr_s2_rate4", 0f);
            //data.SetNumber("ur_tr_s2_rate5", 0f);
            //data.SetNumber("ur_tr_s2_rate6", 0f);
            //data.SetNumber("ur_tr_s3_energy_ub1", 1e+038f);
            //data.SetNumber("ur_tr_s3_energy_ub2", 1e+038f);
            //data.SetNumber("ur_tr_s3_energy_ub3", 1e+038f);
            //data.SetNumber("ur_tr_s3_energy_ub4", 1e+038f);
            //data.SetNumber("ur_tr_s3_energy_ub5", 1e+038f);
            //data.SetNumber("ur_tr_s3_energy_ub6", 1e+038f);
            //data.SetNumber("ur_tr_s3_rate1", 0f);
            //data.SetNumber("ur_tr_s3_rate2", 0f);
            //data.SetNumber("ur_tr_s3_rate3", 0f);
            //data.SetNumber("ur_tr_s3_rate4", 0f);
            //data.SetNumber("ur_tr_s3_rate5", 0f);
            //data.SetNumber("ur_tr_s3_rate6", 0f);
            //data.SetNumber("ur_tr_s4_energy_ub1", 1e+038f);
            //data.SetNumber("ur_tr_s4_energy_ub2", 1e+038f);
            //data.SetNumber("ur_tr_s4_energy_ub3", 1e+038f);
            //data.SetNumber("ur_tr_s4_energy_ub4", 1e+038f);
            //data.SetNumber("ur_tr_s4_energy_ub5", 1e+038f);
            //data.SetNumber("ur_tr_s4_energy_ub6", 1e+038f);
            //data.SetNumber("ur_tr_s4_rate1", 0f);
            //data.SetNumber("ur_tr_s4_rate2", 0f);
            //data.SetNumber("ur_tr_s4_rate3", 0f);
            //data.SetNumber("ur_tr_s4_rate4", 0f);
            //data.SetNumber("ur_tr_s4_rate5", 0f);
            //data.SetNumber("ur_tr_s4_rate6", 0f);
            //data.SetNumber("ur_tr_s5_energy_ub1", 1e+038f);
            //data.SetNumber("ur_tr_s5_energy_ub2", 1e+038f);
            //data.SetNumber("ur_tr_s5_energy_ub3", 1e+038f);
            //data.SetNumber("ur_tr_s5_energy_ub4", 1e+038f);
            //data.SetNumber("ur_tr_s5_energy_ub5", 1e+038f);
            //data.SetNumber("ur_tr_s5_energy_ub6", 1e+038f);
            //data.SetNumber("ur_tr_s5_rate1", 0f);
            //data.SetNumber("ur_tr_s5_rate2", 0f);
            //data.SetNumber("ur_tr_s5_rate3", 0f);
            //data.SetNumber("ur_tr_s5_rate4", 0f);
            //data.SetNumber("ur_tr_s5_rate5", 0f);
            //data.SetNumber("ur_tr_s5_rate6", 0f);
            //data.SetNumber("ur_tr_s6_energy_ub1", 1e+038f);
            //data.SetNumber("ur_tr_s6_energy_ub2", 1e+038f);
            //data.SetNumber("ur_tr_s6_energy_ub3", 1e+038f);
            //data.SetNumber("ur_tr_s6_energy_ub4", 1e+038f);
            //data.SetNumber("ur_tr_s6_energy_ub5", 1e+038f);
            //data.SetNumber("ur_tr_s6_energy_ub6", 1e+038f);
            //data.SetNumber("ur_tr_s6_rate1", 0f);
            //data.SetNumber("ur_tr_s6_rate2", 0f);
            //data.SetNumber("ur_tr_s6_rate3", 0f);
            //data.SetNumber("ur_tr_s6_rate4", 0f);
            //data.SetNumber("ur_tr_s6_rate5", 0f);
            //data.SetNumber("ur_tr_s6_rate6", 0f);
            //data.SetNumber("ur_tr_sched_m1", 0f);
            //data.SetNumber("ur_tr_sched_m2", 0f);
            //data.SetNumber("ur_tr_sched_m3", 0f);
            //data.SetNumber("ur_tr_sched_m4", 0f);
            //data.SetNumber("ur_tr_sched_m5", 0f);
            //data.SetNumber("ur_tr_sched_m6", 0f);
            //data.SetNumber("ur_tr_sched_m7", 0f);
            //data.SetNumber("ur_tr_sched_m8", 0f);
            //data.SetNumber("ur_tr_sched_m9", 0f);
            //data.SetNumber("ur_tr_sched_m10", 0f);
            //data.SetNumber("ur_tr_sched_m11", 0f);
            //data.SetNumber("ur_tr_sched_m12", 0f);
            #endregion
            module = new Module("utilityrate");
            if (module.Exec(data))
            {
                float[] energy_net = data.GetArray("energy_net");
                for (int i = 0; i < energy_net.Length; i++)
                {
                    Console.WriteLine("energy_net_annual (kWh) = " + energy_net[i] + "\n");
                }
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
                Console.WriteLine("utilityrate example failed\n");
            }


            #region // cashloan input variables
            data.SetNumber("analysis_years", 25f);
            data.SetNumber("federal_tax_rate", 28f);
            data.SetNumber("state_tax_rate", 7f);
            data.SetNumber("property_tax_rate", 0f);
            data.SetNumber("prop_tax_cost_assessed_percent", 100f);
            data.SetNumber("prop_tax_assessed_decline", 0f);
            data.SetNumber("sales_tax_rate", 5f);
            data.SetNumber("real_discount_rate", 8f);
            data.SetNumber("inflation_rate", 2.5f);
            data.SetNumber("insurance_rate", 0f);
            data.SetNumber("system_capacity", 3.8745f);
            data.SetNumber("system_heat_rate", 0f);
            data.SetNumber("loan_term", 25f);
            data.SetNumber("loan_rate", 7.5f);
            data.SetNumber("loan_debt", 100f);
            data.SetArray("om_fixed", new float[] { 0f });
            data.SetNumber("om_fixed_escal", 0f);
            data.SetArray("om_production", new float[] { 0f });
            data.SetNumber("om_production_escal", 0f);
            data.SetArray("om_capacity", new float[] { 20f });
            data.SetNumber("om_capacity_escal", 0f);
            data.SetArray("om_fuel_cost", new float[] { 0f });
            data.SetNumber("om_fuel_cost_escal", 0f);
            data.SetNumber("annual_fuel_usage", 0f);
            data.SetNumber("itc_fed_amount", 0f);
            data.SetNumber("itc_fed_amount_deprbas_fed", 0f);
            data.SetNumber("itc_fed_amount_deprbas_sta", 0f);
            data.SetNumber("itc_sta_amount", 0f);
            data.SetNumber("itc_sta_amount_deprbas_fed", 0f);
            data.SetNumber("itc_sta_amount_deprbas_sta", 0f);
            data.SetNumber("itc_fed_percent", 30f);
            data.SetNumber("itc_fed_percent_maxvalue", 1e+038f);
            data.SetNumber("itc_fed_percent_deprbas_fed", 0f);
            data.SetNumber("itc_fed_percent_deprbas_sta", 0f);
            data.SetNumber("itc_sta_percent", 0f);
            data.SetNumber("itc_sta_percent_maxvalue", 1e+038f);
            data.SetNumber("itc_sta_percent_deprbas_fed", 0f);
            data.SetNumber("itc_sta_percent_deprbas_sta", 0f);
            data.SetArray("ptc_fed_amount", new float[] { 0f });
            data.SetNumber("ptc_fed_term", 10f);
            data.SetNumber("ptc_fed_escal", 2.5f);
            data.SetArray("ptc_sta_amount", new float[] { 0f });
            data.SetNumber("ptc_sta_term", 10f);
            data.SetNumber("ptc_sta_escal", 2.5f);
            data.SetNumber("ibi_fed_amount", 0f);
            data.SetNumber("ibi_fed_amount_tax_fed", 1f);
            data.SetNumber("ibi_fed_amount_tax_sta", 1f);
            data.SetNumber("ibi_fed_amount_deprbas_fed", 0f);
            data.SetNumber("ibi_fed_amount_deprbas_sta", 0f);
            data.SetNumber("ibi_sta_amount", 0f);
            data.SetNumber("ibi_sta_amount_tax_fed", 1f);
            data.SetNumber("ibi_sta_amount_tax_sta", 1f);
            data.SetNumber("ibi_sta_amount_deprbas_fed", 0f);
            data.SetNumber("ibi_sta_amount_deprbas_sta", 0f);
            data.SetNumber("ibi_uti_amount", 0f);
            data.SetNumber("ibi_uti_amount_tax_fed", 1f);
            data.SetNumber("ibi_uti_amount_tax_sta", 1f);
            data.SetNumber("ibi_uti_amount_deprbas_fed", 0f);
            data.SetNumber("ibi_uti_amount_deprbas_sta", 0f);
            data.SetNumber("ibi_oth_amount", 0f);
            data.SetNumber("ibi_oth_amount_tax_fed", 1f);
            data.SetNumber("ibi_oth_amount_tax_sta", 1f);
            data.SetNumber("ibi_oth_amount_deprbas_fed", 0f);
            data.SetNumber("ibi_oth_amount_deprbas_sta", 0f);
            data.SetNumber("ibi_fed_percent", 0f);
            data.SetNumber("ibi_fed_percent_maxvalue", 1e+038f);
            data.SetNumber("ibi_fed_percent_tax_fed", 1f);
            data.SetNumber("ibi_fed_percent_tax_sta", 1f);
            data.SetNumber("ibi_fed_percent_deprbas_fed", 0f);
            data.SetNumber("ibi_fed_percent_deprbas_sta", 0f);
            data.SetNumber("ibi_sta_percent", 0f);
            data.SetNumber("ibi_sta_percent_maxvalue", 1e+038f);
            data.SetNumber("ibi_sta_percent_tax_fed", 1f);
            data.SetNumber("ibi_sta_percent_tax_sta", 1f);
            data.SetNumber("ibi_sta_percent_deprbas_fed", 0f);
            data.SetNumber("ibi_sta_percent_deprbas_sta", 0f);
            data.SetNumber("ibi_uti_percent", 0f);
            data.SetNumber("ibi_uti_percent_maxvalue", 1e+038f);
            data.SetNumber("ibi_uti_percent_tax_fed", 1f);
            data.SetNumber("ibi_uti_percent_tax_sta", 1f);
            data.SetNumber("ibi_uti_percent_deprbas_fed", 0f);
            data.SetNumber("ibi_uti_percent_deprbas_sta", 0f);
            data.SetNumber("ibi_oth_percent", 0f);
            data.SetNumber("ibi_oth_percent_maxvalue", 1e+038f);
            data.SetNumber("ibi_oth_percent_tax_fed", 1f);
            data.SetNumber("ibi_oth_percent_tax_sta", 1f);
            data.SetNumber("ibi_oth_percent_deprbas_fed", 0f);
            data.SetNumber("ibi_oth_percent_deprbas_sta", 0f);
            data.SetNumber("cbi_fed_amount", 0f);
            data.SetNumber("cbi_fed_maxvalue", 1e+038f);
            data.SetNumber("cbi_fed_tax_fed", 1f);
            data.SetNumber("cbi_fed_tax_sta", 1f);
            data.SetNumber("cbi_fed_deprbas_fed", 0f);
            data.SetNumber("cbi_fed_deprbas_sta", 0f);
            data.SetNumber("cbi_sta_amount", 0f);
            data.SetNumber("cbi_sta_maxvalue", 1e+038f);
            data.SetNumber("cbi_sta_tax_fed", 1f);
            data.SetNumber("cbi_sta_tax_sta", 1f);
            data.SetNumber("cbi_sta_deprbas_fed", 0f);
            data.SetNumber("cbi_sta_deprbas_sta", 0f);
            data.SetNumber("cbi_uti_amount", 0f);
            data.SetNumber("cbi_uti_maxvalue", 1e+038f);
            data.SetNumber("cbi_uti_tax_fed", 1f);
            data.SetNumber("cbi_uti_tax_sta", 1f);
            data.SetNumber("cbi_uti_deprbas_fed", 0f);
            data.SetNumber("cbi_uti_deprbas_sta", 0f);
            data.SetNumber("cbi_oth_amount", 0f);
            data.SetNumber("cbi_oth_maxvalue", 1e+038f);
            data.SetNumber("cbi_oth_tax_fed", 1f);
            data.SetNumber("cbi_oth_tax_sta", 1f);
            data.SetNumber("cbi_oth_deprbas_fed", 0f);
            data.SetNumber("cbi_oth_deprbas_sta", 0f);
            data.SetArray("pbi_fed_amount", new float[] { 0f });
            data.SetNumber("pbi_fed_term", 0f);
            data.SetNumber("pbi_fed_escal", 0f);
            data.SetNumber("pbi_fed_tax_fed", 1f);
            data.SetNumber("pbi_fed_tax_sta", 1f);
            data.SetArray("pbi_sta_amount", new float[] { 0f });
            data.SetNumber("pbi_sta_term", 0f);
            data.SetNumber("pbi_sta_escal", 0f);
            data.SetNumber("pbi_sta_tax_fed", 1f);
            data.SetNumber("pbi_sta_tax_sta", 1f);
            data.SetArray("pbi_uti_amount", new float[] { 0f });
            data.SetNumber("pbi_uti_term", 0f);
            data.SetNumber("pbi_uti_escal", 0f);
            data.SetNumber("pbi_uti_tax_fed", 1f);
            data.SetNumber("pbi_uti_tax_sta", 1f);
            data.SetArray("pbi_oth_amount", new float[] { 0f });
            data.SetNumber("pbi_oth_term", 0f);
            data.SetNumber("pbi_oth_escal", 0f);
            data.SetNumber("pbi_oth_tax_fed", 1f);
            data.SetNumber("pbi_oth_tax_sta", 1f);
            data.SetNumber("market", 0f);
            data.SetNumber("mortgage", 0f);
            data.SetNumber("total_installed_cost", 22194.2f);
            data.SetNumber("salvage_percentage", 0f);
            #endregion
            module = new Module("cashloan");
            if (module.Exec(data))
            {
                float lcoe_real = data.GetNumber("lcoe_real");
                float lcoe_nom = data.GetNumber("lcoe_nom");
                float npv = data.GetNumber("npv");
                Console.WriteLine("LCOE real (cents/kWh) = " + lcoe_real + "\n");
                Console.WriteLine("LCOE nominal (cents/kWh) = " + lcoe_nom + "\n");
                Console.WriteLine("NPV = $" + npv + "\n");

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
                Console.WriteLine("cashloan example failed\n");
            }
            // end PV Residential
        }
    }
}
