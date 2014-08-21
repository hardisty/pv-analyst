using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.Parameters
{
    public class FloatArrayParameter: Parameter<float[]>
    {
        public FloatArrayParameter(string name, float[] value):base(name, value){}

        public override void setValue(Data data)
        {
            data.SetArray(name, value);
        }
    }
}
