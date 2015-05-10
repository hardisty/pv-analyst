using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling.Parameters.ModuleModels
{
    public abstract class IModuleSettings:ISettings
    {
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
