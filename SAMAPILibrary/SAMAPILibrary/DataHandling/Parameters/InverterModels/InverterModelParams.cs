using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.InverterModels
{
    public abstract class InverterModelParams
    {
        /// <summary>
        /// Use the database to get the parameters for the inverter performance model
        /// </summary>
        /// <param name="inverter_model">The model to use (0 = SNL, 1 = datasheet, 2 = partload)</param>
        /// <param name="inverter_model_identifier">The inverter name, must correspond to a product listed in the inverter database</param>
        /// <returns></returns>
        public static InverterModelParams getInverterParams(int inverter_model, String inverter_model_identifier){
            if (inverter_model == 0)
            {
                return new SNLInverterModel(inverter_model_identifier);
            }
            else if (inverter_model == 1)
            {
                return new DatasheetInverterModel(inverter_model_identifier,4000); // Use the default
            }
            else
            {
                //TODO implement other Inverter Models
                throw new NotImplementedException("Must be 0,1 or 2");
            }
        }

        /// <summary>
        /// Get a set of "made up" inverer parameters using the datasheet model
        /// </summary>
        /// <param name="inverter_model">ignored</param>
        /// <param name="inverter_model_identifier"></param>
        /// <param name="size">The rated AC size in Watts</param>
        /// <returns></returns>
        public static InverterModelParams getInverterParams(int inverter_model, String inverter_model_identifier, float size)
        {
            return new DatasheetInverterModel(inverter_model_identifier, size);
        }

        public abstract void setDataParameters(Data data);

        /// <summary>
        /// Get the maximum rated voltage for the inverter
        /// </summary>
        /// <returns>Voltage in volts</returns>
        public abstract float getMaxRatedVoltage();
    }
}
