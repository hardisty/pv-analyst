using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling
{
    public abstract class ParameterList
    {
        protected Dictionary<string,IDefaultParameter> defaults;
        protected Dictionary<string,IParameter> parameters;

        public ParameterList(IEnumerable<IParameter> input, Dictionary<string, IDefaultParameter> defaults)
        {
            this.defaults = defaults;
            parameters = new Dictionary<string,IParameter>();
            foreach (IParameter inp in input)
            {
                parameters.Add(inp.name, inp);
            }

            fillDefaults();
            validate();
        }

        void fillDefaults(){
            foreach (string key in defaults.Keys)
            {
                if (!parameters.ContainsKey(key))
                {
                    parameters.Add(key,defaults[key]);
                }
            }
        }

        public virtual void setValues(Data data)
        {
            foreach (IParameter p in parameters.Values)
            {
                p.setValue(data);
            }
        }

        void validate()
        {

        }
    }
}
