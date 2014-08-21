using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.Parameters
{
    class DefaultStringParameter:DefaultParameter<string>
    {
        public DefaultStringParameter(string name, string description, string value):base(name, description, value){}

        public override void setValue(Data data)
        {
            data.SetString(name, value);
        }

        public override bool validate(Parameter<string> p)
        {
            return true;
        }
    }
}
