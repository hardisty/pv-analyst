using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling
{
    public class FloatParameter : Parameter<float>, IParameter
    {
        public FloatParameter(string name, float value):base(name, value){}
        
        public override void setValue(Data data)
        {
            data.SetNumber(name, value);
        }
    }
}
