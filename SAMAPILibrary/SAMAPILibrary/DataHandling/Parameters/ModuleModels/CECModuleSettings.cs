using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SAMAPILibrary.DataHandling.Parameters.ModuleModels
{


    public class CECModuleSettings: IModuleSettings
    {
        [ISettings.Param("cec_area", "float")]
        public float cec_area;
        [ISettings.Param("cec_a_ref", "float")]
        public float cec_a_ref;
        [ISettings.Param("cec_adjust", "float")]
        public float cec_adjust;
        [ISettings.Param("cec_alpha_sc", "float")]
        public float cec_alpha_sc;
        [ISettings.Param("cec_beta_oc", "float")]
        public float cec_beta_oc;
        [ISettings.Param("cec_gamma_r", "float")]
        public float cec_gamma_r;
        [ISettings.Param("cec_i_l_ref", "float")]
        public float cec_i_l_ref;
        [ISettings.Param("cec_i_mp_ref", "float")]
        public float cec_i_mp_ref;
        [ISettings.Param("cec_i_o_ref", "float")]
        public float cec_i_o_ref;
        [ISettings.Param("cec_i_sc_ref", "float")]
        public float cec_i_sc_ref;
        [ISettings.Param("cec_n_s", "float")]
        public float cec_n_s;
        [ISettings.Param("cec_r_s", "float")]
        public float cec_r_s;
        [ISettings.Param("cec_r_sh_ref", "float")]
        public float cec_r_sh_ref;
        [ISettings.Param("cec_t_noct", "float")]
        public float cec_t_noct;
        [ISettings.Param("cec_v_mp_ref", "float")]
        public float cec_v_mp_ref;
        [ISettings.Param("cec_v_oc_ref", "float")]
        public float cec_v_oc_ref;
        [ISettings.Param("cec_temp_corr_mode", "float")]
        public float cec_temp_corr_mode;
        [ISettings.Param("cec_standoff", "float")]
        public float cec_standoff;
        [ISettings.Param("cec_height", "float")]
        public float cec_height;
        [ISettings.Param("cec_mounting_config", "float")]
        public float cec_mounting_config;
        [ISettings.Param("cec_heat_transfer", "float")]
        public float cec_heat_transfer;
        [ISettings.Param("cec_mounting_orientation", "float")]
        public float cec_mounting_orientation;
        [ISettings.Param("cec_gap_spacing", "float")]
        public float cec_gap_spacing;
        [ISettings.Param("cec_module_width", "float")]
        public float cec_module_width;
        [ISettings.Param("cec_module_length", "float")]
        public float cec_module_length;
        [ISettings.Param("cec_array_rows", "float")]
        public float cec_array_rows;
        [ISettings.Param("cec_array_cols", "float")]
        public float cec_array_cols;
        [ISettings.Param("cec_backside_temp", "float")]
        public float cec_backside_temp;
        [ISettings.Param("module_model", "float")]
        public float module_model;
        
        
        public CECModuleSettings(string module_model_identifier){
            module_model = 1;
            if (module_model_identifier == "default")
            {
                // SunPower SPR-210-BLK-U
                cec_area = 1.244f;
                cec_a_ref = 1.9816f;
                cec_adjust = 20.8f;
                cec_alpha_sc = 0.002651f;
                cec_beta_oc = -0.14234f;
                cec_gamma_r = -0.407f;
                cec_i_l_ref = 5.754f;
                cec_i_mp_ref = 5.25f;
                cec_i_o_ref = 1.919e-010f;
                cec_i_sc_ref = 5.75f;
                cec_n_s = 72f;
                cec_r_s = 0.105f;
                cec_r_sh_ref = 160.48f;
                cec_t_noct = 49.2f;
                cec_v_mp_ref = 41f;
                cec_v_oc_ref = 47.7f;
                cec_temp_corr_mode = 0f;
                cec_standoff = 6f;
                cec_height = 0f;
                cec_mounting_config = 0f;
                cec_heat_transfer = 0f;
                cec_mounting_orientation = 0f;
                cec_gap_spacing = 0.05f;
                cec_module_width = 0.799f; //Read from actual Datasheet, SAM Default is 1.0
                cec_module_length = 1.561f; //Read from actual Datasheet, SAM Default is 1.244
                //cec_module_width = 1.0f; //Read from actual Datasheet, SAM Default is 1.0
                //cec_module_length = 1.244f; //Read from actual Datasheet, SAM Default is 1.244
                cec_array_rows = 1f;
                cec_array_cols = 10f;
                cec_backside_temp = 20f;
            }
            else
            {
                var reader = new StreamReader(File.OpenRead("Data\\CEC Modules.csv"));
                //Read 3 bogus lines
                var line = reader.ReadLine();
                line = reader.ReadLine();
                line = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var strings = line.Split(',');
                    String name = strings[0];

                    if (name.Equals(module_model_identifier))
                    {
                        int i = 3;
                        cec_t_noct = float.Parse(strings[i]); i++;
                        cec_area = float.Parse(strings[i]); i++;
                        cec_n_s = float.Parse(strings[i]); i++;
                        cec_i_sc_ref = float.Parse(strings[i]); i++;
                        cec_v_oc_ref = float.Parse(strings[i]); i++;
                        cec_i_mp_ref = float.Parse(strings[i]); i++;
                        cec_v_mp_ref = float.Parse(strings[i]); i++;
                        cec_alpha_sc = float.Parse(strings[i]); i++;
                        cec_beta_oc = float.Parse(strings[i]); i++;
                        cec_a_ref = float.Parse(strings[i]); i++;
                        cec_i_l_ref = float.Parse(strings[i]); i++;
                        cec_i_o_ref = float.Parse(strings[i]); i++;
                        cec_r_s = float.Parse(strings[i]); i++;
                        cec_r_sh_ref = float.Parse(strings[i]); i++;
                        cec_adjust = float.Parse(strings[i]); i++;
                        cec_gamma_r = float.Parse(strings[i]); i++;
                        
                        String version = strings[i]; i++;
                        float ptc = float.Parse(strings[i]); i++;
                        String technology = strings[i]; i++;


                        // SunPower SPR-210-BLK-U
                        cec_temp_corr_mode = 0f;
                        cec_standoff = 6f;
                        cec_height = 0f;
                        cec_mounting_config = 0f;
                        cec_heat_transfer = 0f;
                        cec_mounting_orientation = 0f;
                        cec_gap_spacing = 0.05f;
                        cec_module_width = 0.799f; //Read from actual Datasheet, SAM Default is 1.0
                        cec_module_length = 1.561f; //Read from actual Datasheet, SAM Default is 1.244
                        //cec_module_width = 1.0f; //Read from actual Datasheet, SAM Default is 1.0
                        //cec_module_length = 1.244f; //Read from actual Datasheet, SAM Default is 1.244
                        cec_array_rows = 1f;
                        cec_array_cols = 10f;
                        cec_backside_temp = 20f;
                        return;
                    }
                }

                throw new Exception("Module Model Identifier not Found");
  
            }
        }
        public void getDefault() { }

        /// <summary>
        /// Get the module's width
        /// </summary>
        /// <returns>Width in meters</returns>
        public override float getWidth()
        {
            return cec_module_width;
        }
        /// <summary>
        /// Get the module's height/length
        /// </summary>
        /// <returns>Height in meters</returns>
        public override float getHeight()
        {
            return cec_module_length;
        }
        /// <summary>
        /// Get the module's Voltage at Max Power
        /// </summary>
        /// <returns>Voltage in Volts</returns>
        public override float getVmax()
        {
            return cec_v_mp_ref;
        }
        /// <summary>
        /// Get the rated power for the module
        /// </summary>
        /// <returns>Power in W</returns>
        public override float getRatedPower()
        {
            return cec_v_mp_ref * cec_i_mp_ref; 
        }
    }
    
}
