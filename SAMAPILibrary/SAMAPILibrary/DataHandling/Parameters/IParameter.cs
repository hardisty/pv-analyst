using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.Parameters
{
    //The double interface is needed to allow the ParameterList objects to hold a Dictionary of them.
    public interface IParameter
    {
        string name { get; }
        void setValue(Data data);
    }

    public interface IParameter<T>
    {
        string name { get; }
        void setValue(Data data);
        T getValue();
    }
}
