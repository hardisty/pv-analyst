using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling
{
    class ArrayParameterList:ParameterList
    {
        Dictionary<string, DefaultParameter> defaults = new Dictionary<string, DefaultParameter>() { 
            {"ac_derate", new DefaultFloatParameter("ac_derate","The AC Derate",0.99f)},
            {"ac_derate", new DefaultFloatParameter("ac_derate","The AC Derate",0.99f)}
        };
    }
}
