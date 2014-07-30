using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataObjects.InverterModels;
using SAMAPILibrary.DataObjects.ModuleModels;
using SAMAPILibrary.SAMAPI;
using SAMAPILibrary.DataObjects;
using SAMAPILibrary.DataObjects.OutputData;

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
        public readonly GISData gis;

        public ArrayParameterList(IEnumerable<IParameter> input, GISData gis) : base(input,defaults) 
        {
            this.gis = gis;

            string module_model_identifier;
            string inverter_model_identifier;
            
            module_model_identifier = "default";
            inverter_model_identifier = "default";

            //TODO Get the appropriate ones based on the identifiers
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

        public SystemModelOutput runModule()
        {
            Data data = new Data();
            Module module = new Module("pvsamv1");

            this.setValues(data);
            new ArrayParametersComputedList(gis, this).setValues(data);
            
            if (module.Exec(data))
            {
                return new SystemModelOutput(data);
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
                Console.WriteLine("pvsam1 failed\n");
                return null;
            }
        }

        
    }

    public class ArrayParameterListBuilder
    {
        private List<IParameter> list = new List<IParameter>();
        private GISData gis;
        private bool isInit = false;

        public bool isInitialized()
        {
            return this.isInit;
        }

        public ArrayParameterListBuilder()
        {
        }

        public ArrayParameterListBuilder(GISData gis)
        {
            initialize(gis);
        }

        public void initialize(GISData gis)
        {
            isInit = true;
            this.gis = gis;
        }

        //TODO add other parameters
        public void subarray1_soiling(float[] soiling)
        {
            list.Add(new FloatArrayParameter("subarray1_soiling", soiling));
        }

        public ArrayParameterList build()
        {
            if (isInit)
            {
                return new ArrayParameterList(list, gis);
            }
            else
            {
                throw new ArgumentNullException("GISData must be set for ArrayParameterList.Builder");
            }
        }
    }
}
