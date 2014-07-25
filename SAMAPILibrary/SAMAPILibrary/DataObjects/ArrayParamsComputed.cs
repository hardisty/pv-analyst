using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataObjects
{
    /// <summary>
    /// Parameters about the array that are computed based on other inputs (e.g. the GISData).
    /// </summary>
    class ArrayParamsComputed:IDataParamSetter
    {
        //Variablevalues
        /// <summary>
        /// The name of the weather file
        /// </summary>
        public readonly String weather_file;
        /// <summary>
        /// The number of modules per string in the array
        /// </summary>
        public readonly int modules_per_string;
        /// <summary>
        /// The number of strings in the array
        /// </summary>
        public readonly int strings_in_parallel;
        /// <summary>
        /// The tilt of the array
        /// </summary>
        public readonly float subarray1_tilt;
        /// <summary>
        /// The azimuth of the array (0° N)
        /// </summary>
        public readonly float subarray1_azimuth;

        //Fixed values
        /// <summary>
        /// The number of inverters
        /// </summary>
        public readonly int inverter_count = 1;

        public ArrayParamsComputed(GISData gis, ArrayParamsUser array)
        {
            // TODO - Find closest TMY2 file
            weather_file = "ExampleFiles\\AZ Phoenix.tm2";
            int numModules = calcNumPanelsOnRoof(gis.width, gis.height, array.module_params.getWidth(), array.module_params.getHeight());
            int[] shape = calcArrayWiring(numModules, array.inverter_params.getMaxRatedVoltage(), array.module_params.getVmax());
            modules_per_string = shape[0]; //9
            strings_in_parallel = shape[1]; //2
            subarray1_tilt = gis.tilt;
            subarray1_azimuth = gis.azimuth;
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
            int numHor = (int) Math.Floor(roofWid / modWid);
            int numVert = (int) Math.Floor(roofHt / modHt);
            int nPanelsHorizontal = numHor * numVert;

            numHor = (int) Math.Floor(roofWid / modHt);
            numVert = (int) Math.Floor(roofHt / modWid);
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
        private int[] calcArrayWiring(int nPanels, double invVoltage, double panelVoltage){
            int maxPerString = (int) Math.Floor(invVoltage / panelVoltage);
            
            //Find the point at which integers appear in modules/string * strings = nPanels
            while (nPanels % maxPerString > 0)
            {
                maxPerString--;
            }
            return new int[] { maxPerString, nPanels / maxPerString };
        }

        public void setDataParameters(Data data)
        {
            data.SetString("weather_file", weather_file);
            data.SetNumber("modules_per_string", modules_per_string);
            data.SetNumber("subarray1_tilt", subarray1_tilt);
            data.SetNumber("subarray1_azimuth", subarray1_azimuth);
            data.SetNumber("inverter_count", inverter_count);
            data.SetNumber("strings_in_parallel", strings_in_parallel);
        }
    }
}
