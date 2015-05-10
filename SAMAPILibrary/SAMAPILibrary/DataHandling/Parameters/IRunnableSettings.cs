using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.Parameters
{
    public abstract class IRunnableSettings: ISettings
    {
        public abstract string getModuleName();
        public abstract Output getOutputClass(Data data);
    }
}
