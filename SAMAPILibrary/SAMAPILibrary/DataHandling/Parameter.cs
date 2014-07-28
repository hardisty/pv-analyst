using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling
{
    abstract class Parameter<T>
    {
        public readonly String name;
        public readonly String description;
        readonly T value;

        public Parameter(string name, string description, T value)
        {
            this.name = name;
            this.description = description;
            this.value = value;
        }

        public void setValue(Data data)
        {
            if (value is int || value is float)
            {
                float v;
                if (value is int)
                {
                    int i = (int)value;
                    v = (float)Convert.ToInt32(value);
                }
                else
                {
                    v = Convert.ToSingle(value);
                }
                data.SetNumber(name, v);
            }
            else if (value is float[])
            {
                data.SetArray(name, value as float[]);
            }
            else if (value is string || value is String)
            {
                data.SetString(name, value as string);
            }
        }


        public T getValue()
        {
            return value;
        }


    }
}
