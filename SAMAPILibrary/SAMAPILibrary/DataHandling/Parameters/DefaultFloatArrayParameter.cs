using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.Parameters
{
    public class DefaultFloatArrayParameter : DefaultParameter<float[]>
    {
        public DefaultFloatArrayParameter(string name, string description, float[] value):base(name, description, value){}

        public override void setValue(Data data)
        {
            data.SetArray(name, value);
        }

        public override bool validate(Parameter<float[]> p)
        {
            return true;
        }
    }
}
