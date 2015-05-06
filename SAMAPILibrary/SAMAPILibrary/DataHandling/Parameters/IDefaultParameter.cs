using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling.Parameters
{
    //The double interface is needed to allow the ParameterList objects to hold a Dictionary of them.
    public interface IDefaultParameter:IParameter
    {
    }
    interface IDefaultParameter<T>:IParameter<T>
    {
        bool validate(Parameter<T> p);
    }
}
