using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling
{
    abstract class Parameter
    {
        public string name;
    }

    abstract class Parameter<T>: IParameter<T>
    {
        public readonly String name;
        public readonly String description;
        public readonly T value;

        public Parameter(string name, string description, T value)
        {
            this.name = name;
            this.description = description;
            this.value = value;
        }

        public abstract void setValue(Data data);

        public T getValue()
        {
            return value;
        }

    }
}
