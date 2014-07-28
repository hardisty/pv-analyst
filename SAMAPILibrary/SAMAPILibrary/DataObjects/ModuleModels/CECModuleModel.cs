using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataObjects.ModuleModels
{
    /// <summary>
    /// Holder for CEC type module model parameters
    /// </summary>
    class CECModuleModel : ModuleModelParams
    {
        public readonly float cec_area;
        public readonly float cec_a_ref;
        public readonly float cec_adjust;
        public readonly float cec_alpha_sc;
        public readonly float cec_beta_oc;
        public readonly float cec_gamma_r;
        public readonly float cec_i_l_ref;
        public readonly float cec_i_mp_ref;
        public readonly float cec_i_o_ref;
        public readonly float cec_i_sc_ref;
        public readonly float cec_n_s;
        public readonly float cec_r_s;
        public readonly float cec_r_sh_ref;
        public readonly float cec_t_noct;
        public readonly float cec_v_mp_ref;
        public readonly float cec_v_oc_ref;
        public readonly float cec_temp_corr_mode;
        public readonly float cec_standoff;
        public readonly float cec_height;
        public readonly float cec_mounting_config;
        public readonly float cec_heat_transfer;
        public readonly float cec_mounting_orientation;
        public readonly float cec_gap_spacing;
        public readonly float cec_module_width;
        public readonly float cec_module_length;
        public readonly float cec_array_rows;
        public readonly float cec_array_cols;
        public readonly float cec_backside_temp;
        public readonly float module_model;

        public CECModuleModel(String module_model_identifier)
        {
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
                cec_module_width = 1f;
                cec_module_length = 1.244f;
                cec_array_rows = 1f;
                cec_array_cols = 10f;
                cec_backside_temp = 20f;
            }
            else
            {
                //TODO implement module database parsing
                throw new NotImplementedException("CEC Module Identifier Not Found");
            }
        }
        

        public override void setDataParameters(Data data)
        {
            data.SetNumber("module_model", module_model);
            data.SetNumber("cec_area", cec_area);
            data.SetNumber("cec_a_ref", cec_a_ref);
            data.SetNumber("cec_adjust", cec_adjust);
            data.SetNumber("cec_alpha_sc", cec_alpha_sc);
            data.SetNumber("cec_beta_oc", cec_beta_oc);
            data.SetNumber("cec_gamma_r", cec_gamma_r);
            data.SetNumber("cec_i_l_ref", cec_i_l_ref);
            data.SetNumber("cec_i_mp_ref", cec_i_mp_ref);
            data.SetNumber("cec_i_o_ref", cec_i_o_ref);
            data.SetNumber("cec_i_sc_ref", cec_i_sc_ref);
            data.SetNumber("cec_n_s", cec_n_s);
            data.SetNumber("cec_r_s", cec_r_s);
            data.SetNumber("cec_r_sh_ref", cec_r_sh_ref);
            data.SetNumber("cec_t_noct", cec_t_noct);
            data.SetNumber("cec_v_mp_ref", cec_v_mp_ref);
            data.SetNumber("cec_v_oc_ref", cec_v_oc_ref);
            data.SetNumber("cec_temp_corr_mode", cec_temp_corr_mode);
            data.SetNumber("cec_standoff", cec_standoff);
            data.SetNumber("cec_height", cec_height);
            data.SetNumber("cec_mounting_config", cec_mounting_config);
            data.SetNumber("cec_heat_transfer", cec_heat_transfer);
            data.SetNumber("cec_mounting_orientation", cec_mounting_orientation);
            data.SetNumber("cec_gap_spacing", cec_gap_spacing);
            data.SetNumber("cec_module_width", cec_module_width);
            data.SetNumber("cec_module_length", cec_module_length);
            data.SetNumber("cec_array_rows", cec_array_rows);
            data.SetNumber("cec_array_cols", cec_array_cols);
            data.SetNumber("cec_backside_temp", cec_backside_temp);
        }

        public override float getWidth()
        {
            return cec_module_width;
        }

        public override float getHeight()
        {
            return cec_module_length;
        }

        public override float getVmax()
        {
            return cec_v_mp_ref;
        }

        public override float getRatedPower()
        {
            throw new NotImplementedException();
        }
    } 
    
}
