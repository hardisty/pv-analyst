using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataObjects.InverterModels
{
    public abstract class InverterModelParams: IDataParamSetter
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
            else
            {
                //TODO implement other Inverter Models
                throw new NotImplementedException("Must be 0,1 or 2");
            }
        }

        public abstract void setDataParameters(Data data);

        /// <summary>
        /// Get the maximum rated voltage for the inverter
        /// </summary>
        /// <returns>Voltage in volts</returns>
        public abstract float getMaxRatedVoltage();
    }
}
