using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling
{
    interface IDefaultParameter
    {
    }
    interface IDefaultParameter<T>:IParameter<T>
    {
        bool validate(Parameter<T> p);
    }
}
