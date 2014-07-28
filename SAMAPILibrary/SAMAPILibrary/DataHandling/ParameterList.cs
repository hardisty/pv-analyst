using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling
{
    abstract class ParameterList
    {
        Dictionary<string,DefaultParameter> defaults;
        Dictionary<string,Parameter> parameters;

        ParameterList(List<Parameter> input)
        {
            parameters = new Dictionary<string,Parameter>();
            foreach (Parameter inp in input)
            {
                
                parameters.Add(inp.name,inp);

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

        void validate()
        {

        }
    }
}
