using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling
{

    public abstract class Parameter<T>: IParameter<T>, IParameter
    {
        private String _name;
        public String name { get { return _name; } set { _name = value; } }
        public readonly T value;

        public Parameter(string name, T value)
        {
            this._name = name;
            this.value = value;
        }

        public abstract void setValue(Data data);

        public T getValue()
        {
            return value;
        }

    }
}
