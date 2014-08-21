using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling.Parameters
{

    public abstract class DefaultParameter<T>: Parameter<T>, IDefaultParameter<T>, IDefaultParameter
    {
        public readonly string description;

        public DefaultParameter(string name, string description, T value) : base(name, value) 
        {
            this.description = description;
        }
        public abstract bool validate(Parameter<T> p);
    }
}
