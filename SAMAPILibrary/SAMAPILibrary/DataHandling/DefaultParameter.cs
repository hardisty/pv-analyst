using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling
{
    abstract class DefaultParameter<T>
    {
        public bool validate(Parameter<T> p);
    }
}
