using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling;
using SAMAPILibrary.DataHandling.InverterModels;

namespace SAMAPILibrary.DataHandling.Parameters
{
    class ArrayParametersComputedList:ParameterList
    {

        new static Dictionary<string, IDefaultParameter> defaults = new Dictionary<string, IDefaultParameter>() { 
            {"weather_file", new DefaultStringParameter("weather_file","The weather file to use","ExampleFiles\\PA Philadelphia.tm2")},    
            {"modules_per_string", new DefaultFloatParameter("modules_per_string","# of modules per string",9)},
            {"strings_in_parallel", new DefaultFloatParameter("strings_in_parallel","# of strings in array",2)},
            {"subarray1_tilt", new DefaultFloatParameter("subarray1_tilt","The tilt of the array",0f)},
            {"subarray1_azimuth", new DefaultFloatParameter("subarray1_azimuth","The azimuth of the array",0f)},
            {"inverter_count", new DefaultFloatParameter("inverter_count","The number of inverters", 1)}
            
        
        };

        public ArrayParametersComputedList(GISData gis, ArrayParameterList array):base(new List<FloatParameter>(),defaults)
        {

            int numModules = calcNumPanelsOnRoof(gis.width, gis.height, array.module_model_params.getWidth(), array.module_model_params.getHeight());
            if (array.inverter_model_params == null)
            {
                InverterModelParams imp = new DatasheetInverterModel("default", numModules * array.module_model_params.getRatedPower() * 1.15f);
                array.setInverterModelParams(imp);
            }
            
            int[] shape = calcArrayWiring(numModules, array.inverter_model_params.getMaxRatedVoltage(), array.module_model_params.getVmax());
            

            // TODO - Find closest TMY2 file
            Dictionary<string, IDefaultParameter> update = new Dictionary<string, IDefaultParameter>() { 
                {"weather_file", new DefaultStringParameter("weather_file","The weather file to use","ExampleFiles\\PA Philadelphia.tm2")},    
                {"modules_per_string", new DefaultFloatParameter("modules_per_string","# of modules per string",shape[0])},
                {"strings_in_parallel", new DefaultFloatParameter("strings_in_parallel","# of strings in array",shape[1])},
                {"subarray1_tilt", new DefaultFloatParameter("subarray1_tilt","The tilt of the array",gis.tilt)},
                {"subarray1_azimuth", new DefaultFloatParameter("subarray1_azimuth","The azimuth of the array",gis.azimuth)},
                {"inverter_count", new DefaultFloatParameter("inverter_count","The number of inverters", 1)}
            };
            parameters.Clear();
            foreach (string key in update.Keys)
            {
                parameters.Add(key,update[key]);
            }
        }

        /// <summary>
        /// Find the total number of panels that can fit on the surface (considers both horizontal and vertical panel orientations)
        /// </summary>
        /// <param name="roofWid">Width of the Surface</param>
        /// <param name="roofHt">Height of the Surface</param>
        /// <param name="modWid">Width of the Module</param>
        /// <param name="modHt">Height of the Module</param>
        /// <returns></returns>
        private int calcNumPanelsOnRoof(float roofWid, float roofHt, float modWid, float modHt)
        {
            int numHor = (int)Math.Floor(roofWid / modWid);
            int numVert = (int)Math.Floor(roofHt / modHt);
            int nPanelsHorizontal = numHor * numVert;

            numHor = (int)Math.Floor(roofWid / modHt);
            numVert = (int)Math.Floor(roofHt / modWid);
            int nPanelsVertical = numHor * numVert;

            return Math.Max(nPanelsHorizontal, nPanelsVertical);
        }

        /// <summary>
        /// Find a suitable arrangement of the modules into strings.
        /// </summary>
        /// <param name="nPanels">Total number of panels in array</param>
        /// <param name="invVoltage">Inverter Maximum Voltage</param>
        /// <param name="panelVoltage">Panel Voltage</param>
        /// <returns>Two element array {panels per string, number of strings}</returns>
        private int[] calcArrayWiring(int nPanels, double invVoltage, double panelVoltage)
        {
            int maxPerString = (int)Math.Floor(invVoltage / panelVoltage);

            //Find the point at which integers appear in modules/string * strings = nPanels
            while (nPanels % maxPerString > 0)
            {
                maxPerString--;
            }
            return new int[] { maxPerString, nPanels / maxPerString };
        }
    }
}
