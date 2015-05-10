using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.Parameters;

namespace SAMAPILibrary.CalculationWrappers
{
    public class GISAdapter
    {
        public static void applySettings(PVSAMV1Settings pvss, GISData gis)
        {
            int numModules = calcNumPanelsOnRoof(gis.width, gis.height, pvss.module_model.getWidth(), pvss.module_model.getHeight());
            if (pvss.inverter_model == null)
            {
                //InverterModelParams imp = new DatasheetInverterModel("default", numModules * pvss.module_model_params.getRatedPower() * 1.15f);
                //pvss.inverter_model = imp;
            }

            int[] shape = calcArrayWiring(numModules, pvss.inverter_model.getMaxRatedVoltage(), pvss.module_model.getVmax());


            pvss.modules_per_string = shape[0];
            pvss.strings_in_parallel = shape[1];

            pvss.subarray1_tilt = gis.tilt;
            pvss.subarray1_azimuth = gis.azimuth;
            pvss.inverter_count = 1;

        }

        /// <summary>
        /// Find the total number of panels that can fit on the surface (considers both horizontal and vertical panel orientations)
        /// </summary>
        /// <param name="roofWid">Width of the Surface</param>
        /// <param name="roofHt">Height of the Surface</param>
        /// <param name="modWid">Width of the Module</param>
        /// <param name="modHt">Height of the Module</param>
        /// <returns></returns>
        private static int calcNumPanelsOnRoof(float roofWid, float roofHt, float modWid, float modHt)
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
        private static int[] calcArrayWiring(int nPanels, double invVoltage, double panelVoltage)
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
