using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataObjects;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.CalculationWrappers
{
    interface IResourceCalculator
    {
        Data run(GISData gis, ArrayParams array);
    }
}
