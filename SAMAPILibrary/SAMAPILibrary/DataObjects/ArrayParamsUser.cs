using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataObjects.ModuleModels;
using SAMAPILibrary.DataObjects.InverterModels;

namespace SAMAPILibrary.DataObjects
{
    /// <summary>
    /// Parameters about the array that are specified by user input.
    /// </summary>
    public class ArrayParamsUser: IDataParamSetter
    {
        //Parameters that will be based on user input
        /// <summary>
        /// Use the ground albedo in the weather file? [Boolean (0/1)]
        /// </summary>
        public readonly int use_wf_albedo; //0 false, 1 true
        /// <summary>
        /// The ground albedo for each month (length 12)
        /// </summary>
        public readonly float[] albedo; // 0-1
        /// <summary>
        /// The irradiation mode for the anisotropic model [0 - b&d, 1 - g&d]
        /// </summary>
        public readonly int irrad_mode; //0 beam & diffuse, 1 global & diffuse
        /// <summary>
        /// The anisotropic sky model [0 - isotropic, 1 - HDKR, 2 - Perez]
        /// </summary>
        public readonly int sky_model; // 0 isotropic, 1 hdkr, 2 perez
        /// <summary>
        /// The AC derate [from 0 to 1]
        /// </summary>
        public readonly float ac_derate; // 0-1
        /// <summary>
        /// The soiling factor by month [from 0 to 1]
        /// </summary>
        public readonly float[] subarray1_soiling;
        /// <summary>
        /// The Array derate [from 0 to 1]
        /// </summary>
        public readonly float subarray1_derate;    

        /// <summary>
        /// The module performance model [0 - spe, 1 - 6paruser, 2 - cec]
        /// </summary>
        public readonly int module_model;
        /// <summary>
        /// The inverter performance model [0 - SNL, 1 - datasheet, 2 - partload]
        /// </summary>
        public readonly int inverter_model;

        //Parameters with permanent manual values
        /// <summary>
        /// Never use self-shading
        /// </summary>
        public readonly int self_shading_enabled = 0;
        public readonly float subarray2_tilt = 0;
        public readonly float subarray3_tilt = 0;
        public readonly float subarray4_tilt = 0;
        /// <summary>
        /// Never use tracking
        /// </summary>
        public readonly int subarray1_track_mode = 0; //0 fixed, 1 1axis, 2 2axis, 3 azimuth

        //Non-SAM fields
        /// <summary>
        /// A module identifier, must correspond to an entry in the database
        /// </summary>
        public readonly String module_model_identifier; //Will be used to pull relevant model parameters from the file.
        /// <summary>
        /// An automatically generated ModuleModelParams object
        /// </summary>
        public readonly ModuleModelParams module_params;

        /// <summary>
        /// An inverter identifier, must correspond to an entry in the database
        /// </summary>
        public readonly String inverter_model_identifier;
        /// <summary>
        /// An automatically generated InverterModelParams object
        /// </summary>
        public readonly InverterModelParams inverter_params;

        public ArrayParamsUser()
        {
            //defaults
            use_wf_albedo = 1; // Use it
            albedo = new float[] { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f };
            irrad_mode = 0; //beam & diffuse
            sky_model = 2;  //perez
            ac_derate = 0.99f;

            subarray1_soiling = new float[] { 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f };
            subarray1_derate = 0.955598f;

            module_model = 1;
            module_model_identifier = "default";
            module_params = ModuleModelParams.getModuleParams(module_model, module_model_identifier);
            

            inverter_model = 0;
            inverter_model_identifier = "default";
            inverter_params = InverterModelParams.getInverterParams(inverter_model, inverter_model_identifier);
        }


        public void setDataParameters(SAMAPI.Data data)
        {
            data.SetNumber("use_wf_albedo",use_wf_albedo);
            data.SetArray("albedo", albedo);
            data.SetNumber("irrad_mode", irrad_mode);
            data.SetNumber("sky_model", sky_model);
            data.SetNumber("ac_derate", ac_derate);
            data.SetArray("subarray1_soiling", subarray1_soiling);
            data.SetNumber("subarray1_derate", subarray1_derate);

            data.SetNumber("self_shading_enabled", self_shading_enabled);
            data.SetNumber("subarray2_tilt", subarray2_tilt);
            data.SetNumber("subarray3_tilt", subarray3_tilt);
            data.SetNumber("subarray4_tilt", subarray4_tilt);
            data.SetNumber("subarray1_track_mode", subarray1_track_mode);

            data.SetNumber("module_model", module_model);
            module_params.setDataParameters(data);

            data.SetNumber("inverter_model", inverter_model);
            inverter_params.setDataParameters(data);
        }
    }
}
