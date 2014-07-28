using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling
{
    public interface IDefaultParameter:IParameter
    {
    }
    interface IDefaultParameter<T>:IParameter<T>
    {
        bool validate(Parameter<T> p);
    }
}
