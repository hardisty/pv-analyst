using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.Parameters.ModuleModels;
using SAMAPILibrary.DataHandling.Parameters.InverterModels;
using SAMAPILibrary.DataHandling.OutputData;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.Parameters
{
    public class PVSAMV1Settings:IRunnableSettings
    {
        //Specified
        [ISettings.Param("use_wf_albedo","float")]
        public float use_wf_albedo = 0;

        [ISettings.Param("weather_file", "string")]
        public string weather_file = "ExampleFiles\\PA Philadelphia.tm2";

        [ISettings.Param("albedo", "array")]
        public float[] albedo = new float[] { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f };

        [ISettings.Param("irrad_mode", "float")]
        public float irrad_mode = 0;

        [ISettings.Param("sky_model", "float")]
        public float sky_model = 2;

        [ISettings.Param("ac_derate", "float")]
        public float ac_derate = 0.99f;

        [ISettings.Param("subarray1_soiling", "array")]
        public float[] subarray1_soiling = new float[] { 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f };

        [ISettings.Param("subarray1_derate", "float")]
        public float subarray1_derate = 0.955598f;

        [ISettings.Param("self_shading_enabled", "float")]
        public float self_shading_enabled = 0;

        [ISettings.Param("subarray2_tilt", "float")]
        public float subarray2_tilt = 0;

        [ISettings.Param("subarray3_tilt", "float")]
        public float subarray3_tilt = 0;

        [ISettings.Param("subarray4_tilt", "float")]
        public float subarray4_tilt = 0;

        [ISettings.Param("subarray1_track_mode", "float")]
        public float subarray1_track_mode = 0;

        //Computed
        [ISettings.Param("modules_per_string", "float")]
        public float modules_per_string = 9;

        [ISettings.Param("strings_in_parallel", "float")]
        public float strings_in_parallel = 2;

        [ISettings.Param("subarray1_tilt", "float")]
        public float subarray1_tilt = 0;

        [ISettings.Param("subarray1_azimuth", "float")]
        public float subarray1_azimuth = 0;

        [ISettings.Param("inverter_count", "float")]
        public float inverter_count = 1;

        //Composite
        [ISettings.Param("module_model", "composite")]
        public IModuleSettings module_model = new CECModuleSettings("default");

        [ISettings.Param("inverter_model", "composite")]
        public IInverterSettings inverter_model = new SNLInverterSettings("default");

        private PVSAMV1Settings() { }

        public static PVSAMV1Settings getDefault()
        {
            return new PVSAMV1Settings();
        }

        public override string getModuleName()
        {
            return "pvsamv1";
        }

        public override Output getOutputClass(Data data)
        {
            return new PVSAMV1Output(data);
        }
    }
}
