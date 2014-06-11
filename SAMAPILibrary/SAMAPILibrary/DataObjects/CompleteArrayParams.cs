using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataObjects
{
    /// <summary>
    /// All parameters needed to describe the array for the resource calculator
    /// </summary>
    class CompleteArrayParams : IDataParamSetter
    {
        ArrayParams array;
        ComputedArrayParams computedArray;
        
        public CompleteArrayParams(ArrayParams a, ComputedArrayParams a2)
        {
            array = a;
            computedArray = a2;
        }
        public void setDataParameters(SAMAPI.Data data)
        {
            array.setDataParameters(data);
            computedArray.setDataParameters(data);
        }
    }
}
