using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataObjects;
using SAMAPILibrary.DataObjects.FinancialModels;

namespace SAMAPILibrary.CalculationWrappers
{
    public class InputParams
    {
        GISData gis;
        ArrayParamsUser array;
        FinancialParamsSimple financial;

        public InputParams(GISData gisdata,ArrayParamsSimple arrayparams,FinancialParamsSimple financialparams)
        {
            gis = gisdata;
            array = new ArrayParamsUser(arrayparams);
            financial = financialparams;
        }

        public GISData getGISData()
        {
            return gis;
        }

        public ArrayParamsUser getArrayParams()
        {
            return array;
        }

        public FinancialParamsSimple getFinancialParams()
        {
            return financial;
        }
    }
}
