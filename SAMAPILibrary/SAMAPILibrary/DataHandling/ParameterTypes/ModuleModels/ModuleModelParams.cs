using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataObjects.ModuleModels
{
    public abstract class ModuleModelParams
    {
        public static ModuleModelParams getModuleParams(int module_model, String module_model_identifier){
            if (module_model == 1)
            {
                return new CECModuleModel(module_model_identifier);
            }
            else
            {
                //TODO implement other Modules
                throw new NotImplementedException("Must be 0,1 or 2");
            }
        }

        public abstract void setDataParameters(Data data);

        /// <summary>
        /// Get the module's width
        /// </summary>
        /// <returns>Width in meters</returns>
        public abstract float getWidth();
        /// <summary>
        /// Get the module's height/length
        /// </summary>
        /// <returns>Height in meters</returns>
        public abstract float getHeight();
        /// <summary>
        /// Get the module's Voltage at Max Power
        /// </summary>
        /// <returns>Voltage in Volts</returns>
        public abstract float getVmax();
        /// <summary>
        /// Get the rated power for the module
        /// </summary>
        /// <returns>Power in W</returns>
        public abstract float getRatedPower();
    }
}
