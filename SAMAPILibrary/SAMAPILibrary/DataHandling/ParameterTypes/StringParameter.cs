using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling
{
    class StringParameter : Parameter<string>
    {
        public StringParameter(string name, string value):base(name, value){}

        public override void setValue(Data data)
        {
            data.SetString(name, value);
        }
    }
}
