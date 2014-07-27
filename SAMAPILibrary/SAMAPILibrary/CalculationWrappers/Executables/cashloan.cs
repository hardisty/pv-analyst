using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;
using SAMAPILibrary.DataObjects.FinancialModels;
using SAMAPILibrary.DataObjects.OutputData;
using SAMAPILibrary.FinancialModels;

namespace SAMAPILibrary.CalculationWrappers.Executables
{
    public static class cashloan
    {

        public static CashLoanOutput run(CashLoanParams clparams)
        {
            Data data = new Data();
            Module module = new Module("cashloan");

            clparams.setDataParameters(data);

            if (module.Exec(data))
            {
                return new CashLoanOutput(data);
            }
            else
            {
                int idx = 0;
                String msg;
                int type;
                float time;
                while (module.Log(idx, out msg, out type, out time))
                {
                    String stype = "NOTICE";
                    if (type == API.WARNING) stype = "WARNING";
                    else if (type == API.ERROR) stype = "ERROR";
                    Console.WriteLine("[ " + stype + " at time:" + time + " ]: " + msg + "\n");
                    idx++;
                }
                Console.WriteLine("cashloan failed\n");

                return null;
            }
        }
    }
}
