using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling.Parameters.InverterModels
{
    public abstract class IInverterSettings : ISettings
    {
        public abstract float getMaxRatedVoltage();
    }
}
