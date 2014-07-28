using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataObjects.InverterModels;
using SAMAPILibrary.DataObjects.ModuleModels;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling
{
    public class ArrayParameterList:ParameterList
    {
        new static Dictionary<string, IDefaultParameter> defaults = new Dictionary<string, IDefaultParameter>() { 
            {"use_wf_albedo", new DefaultFloatParameter("use_wf_albedo","The AC Derate",1)},    
            {"albedo", new DefaultFloatArrayParameter("albedo","The monthly albedo",new float[] { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f })},
            {"irrad_mode", new DefaultFloatParameter("irrad_mode","The Irradiance Model Mode",0)},
            {"sky_model", new DefaultFloatParameter("sky_model","The Irradiance Sky Model",2)},
            {"ac_derate", new DefaultFloatParameter("ac_derate","The AC Derate",0.99f)},
            {"subarray1_soiling", new DefaultFloatArrayParameter("subarray1_soiling","The monthly soiling",new float[] { 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f })},
            {"subarray1_derate", new DefaultFloatParameter("subarray1_derate","The array rerate",0.955598f)},       
            {"self_shading_enabled", new DefaultFloatParameter("self_shading_enabled","Use Self Shading",0)},
            {"subarray2_tilt", new DefaultFloatParameter("subarray2_tilt","Req'd but Unused",0)},
            {"subarray3_tilt", new DefaultFloatParameter("subarray3_tilt","Req'd but Unused",0)},
            {"subarray4_tilt", new DefaultFloatParameter("subarray4_tilt","Req'd but Unused",0)},
            {"subarray1_track_mode", new DefaultFloatParameter("subarray1_track_mode","The tracking mode",0)}
        };

        public readonly InverterModelParams inverter_model_params;
        public readonly ModuleModelParams module_model_params;

        public ArrayParameterList():this(new List<FloatParameter>()){}

        public ArrayParameterList(IEnumerable<IParameter> input) : base(input,defaults) 
        {
            string module_model_identifier = "default";
            string inverter_model_identifier = "default";
            //Get the appropriate ones based on the identifiers
            int module_model = 1;
            int inverter_model = 0;

            inverter_model_params = InverterModelParams.getInverterParams(inverter_model, inverter_model_identifier);
            module_model_params = ModuleModelParams.getModuleParams(module_model, module_model_identifier);
        }

        public override void setValues(Data data)
        {
            inverter_model_params.setDataParameters(data);
            module_model_params.setDataParameters(data);
            base.setValues(data);
        }
    }
}
