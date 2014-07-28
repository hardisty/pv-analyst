using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling
{
    class FloatParameter:Parameter<float>
    {
        FloatParameter(string name, string description, float value):base(name, description, value){}
        
        public override void setValue(Data data)
        {
            data.SetNumber(name, value);
        }
    }
}
