using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.Parameters;
using SAMAPILibrary.SAMAPI;
using SAMAPILibrary.DataHandling;

namespace SAMAPILibrary.CalculationWrappers
{
    class ModuleRunner
    {
        public static Output runModule(IRunnableSettings s)
        {
            Data data = new Data();
            s.applySettings(data);

            Module mod = new Module(s.getModuleName());
            mod.Exec(data);

            Output outdata = s.getOutputClass(data);

            return outdata;
        }
    }
}
