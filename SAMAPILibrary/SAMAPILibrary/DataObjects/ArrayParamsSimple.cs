using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataObjects
{
    public class ArrayParamsSimple
    {
        //Non-SAM fields
        /// <summary>
        /// A module identifier, must correspond to an entry in the database
        /// </summary>
        public readonly String module_model_identifier; //Will be used to pull relevant model parameters from the file.

        /// <summary>
        /// An inverter identifier, must correspond to an entry in the database
        /// </summary>
        public readonly String inverter_model_identifier;

        public ArrayParamsSimple(String moduleID, String inverterID)
        {
            module_model_identifier = moduleID;
            inverter_model_identifier = inverterID;
        }
    }
}
