using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataObjects
{
    interface IDataParamSetter
    {
        /// <summary>
        /// Call SetType(value) on SAM API Data object for all fields held by relevant Parameter Object.
        /// </summary>
        /// <param name="data">The SAM API Data object</param>
        void setDataParameters(Data data);
    }
}
