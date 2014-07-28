using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling
{
    interface IParameter
    {
    }

    interface IParameter<T>
    {
        void setValue(Data data);
        T getValue();
    }
}
