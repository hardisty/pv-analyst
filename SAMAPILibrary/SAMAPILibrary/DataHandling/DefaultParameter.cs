using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling
{
    abstract class DefaultParameter: Parameter,IDefaultParameter
    {
    }

    abstract class DefaultParameter<T>: Parameter<T>, IDefaultParameter<T>
    {

        public DefaultParameter(string name, string description, T value) : base(name, description, value) 
        {
        }
        public abstract bool validate(Parameter<T> p);
    }
}
