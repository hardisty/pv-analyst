using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.InverterModels;
using SAMAPILibrary.DataHandling.ModuleModels;
using SAMAPILibrary.SAMAPI;
using SAMAPILibrary.DataHandling;
using SAMAPILibrary.DataHandling.OutputData;

namespace SAMAPILibrary.DataHandling.Parameters
{
    public class ArrayParameterList:ParameterList
    {
        new static Dictionary<string, IDefaultParameter> defaults = new Dictionary<string, IDefaultParameter>() { 
            {"use_wf_albedo", new DefaultFloatParameter("use_wf_albedo","Should the weather file albedo be used? 0/1",0)},    
            {"weather_file", new DefaultStringParameter("weather_file","The weather file to use","ExampleFiles\\PA Philadelphia.tm2")}, 
            {"albedo", new DefaultFloatArrayParameter("albedo","The monthly albedo (length 12)",new float[] { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f })},
            {"irrad_mode", new DefaultFloatParameter("irrad_mode","The Irradiance Model Mode (0 - b&d, 1 - g&d)",0)},
            {"sky_model", new DefaultFloatParameter("sky_model","The Irradiance Sky Model (0 - Isotropic, 1 - HDKR, 2 - Perez)",2)},
            {"ac_derate", new DefaultFloatParameter("ac_derate","The AC Derate (0-1)",0.99f)},
            {"subarray1_soiling", new DefaultFloatArrayParameter("subarray1_soiling","The monthly soiling on the array (0-1)",new float[] { 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f })},
            {"subarray1_derate", new DefaultFloatParameter("subarray1_derate","The array derate (0-1)",0.955598f)},       
            {"self_shading_enabled", new DefaultFloatParameter("self_shading_enabled","Use Self Shading? 0/1",0)},
            {"subarray2_tilt", new DefaultFloatParameter("subarray2_tilt","Req'd but Unused",0)},
            {"subarray3_tilt", new DefaultFloatParameter("subarray3_tilt","Req'd but Unused",0)},
            {"subarray4_tilt", new DefaultFloatParameter("subarray4_tilt","Req'd but Unused",0)},
            {"subarray1_track_mode", new DefaultFloatParameter("subarray1_track_mode","The array tracking mode (0 - none, 1 - one axis, 2 - two axis, 3 - azi)",0)}
        };

        public InverterModelParams inverter_model_params;
        public readonly ModuleModelParams module_model_params;
        public readonly GISData gis;

        public ArrayParameterList(IEnumerable<IParameter> input, GISData gis) : base(input,defaults) 
        {
            this.gis = gis;

            string module_model_identifier;
            string inverter_model_identifier;
            
            module_model_identifier = "default";
            //module_model_identifier = "BEoptCA Default Module";
            inverter_model_identifier = "default";
            //inverter_model_identifier = "ABB: MICRO-0.25-I-OUTD-US-208 208V [CEC 2014]";

            //TODO Get the appropriate ones based on the identifiers
            int module_model = 1;
            int inverter_model = 1;

            //inverter_model_params = InverterModelParams.getInverterParams(inverter_model, inverter_model_identifier);
            inverter_model_params = null;
            module_model_params = ModuleModelParams.getModuleParams(module_model, module_model_identifier);
        }

        public void setInverterModelParams(InverterModelParams imp)
        {
            inverter_model_params = imp;
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

            new ArrayParametersComputedList(gis, this).setValues(data);
            this.setValues(data);
            
            
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

        /// <summary>
        /// The monthly soiling derate factor, ranging from 0-1. Represents percent of energy available from soiling perspective.
        /// </summary>
        /// <param name="soiling">Array of float, must have length of 12. Defaults to 0.95 for each month.</param>
        public void subarray1_soiling(float[] soiling)
        {
            list.Add(new FloatArrayParameter("subarray1_soiling", soiling));
        }

        /// <summary>
        /// The Irradiance Sky Model to use
        /// </summary>
        /// <param name="model">0 - Isotropic, 1 - HDKR, 2 - Perez. Default 2 (Perez).</param>
        public void sky_model(int model)
        {
            list.Add(new FloatParameter("sky_model", model));
        }

        /// <summary>
        /// The AC interconnection derate for the system
        /// </summary>
        /// <param name="derate">Listed as percent available from 0-1. Default 0.99.</param>
        public void ac_derate(float derate)
        {
            list.Add(new FloatParameter("ac_derate", derate));
        }

        /// <summary>
        /// The DC derate for the array
        /// </summary>
        /// <param name="derate">Listed as percent available from 0-1. Default 0.955598.</param>
        public void subarray1_derate(float derate)
        {
            list.Add(new FloatParameter("subarray1_derate", derate));
        }

        /// <summary>
        /// The filename for the weather file
        /// </summary>
        /// <param name="name">relative path to file</param>
        public void weather_file(string name)
        {
            list.Add(new StringParameter("weather_file", name));
        }

        public ArrayParameterList build()
        {
            if (isInit)
            {
                return new ArrayParameterList(list, gis);
            }
            else
            {
                throw new ArgumentNullException("GISData must be set before ArrayParameterList.Builder can build");
            }
        }
    }
}
