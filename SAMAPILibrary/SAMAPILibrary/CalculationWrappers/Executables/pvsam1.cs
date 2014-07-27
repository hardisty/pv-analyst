using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;
using SAMAPILibrary.DataObjects;
using SAMAPILibrary.DataObjects.OutputData;

namespace SAMAPILibrary.CalculationWrappers.Executables
{
    public static class pvsam1
    {

        public static SystemModelOutput run(DataObjects.GISData gis, DataObjects.ArrayParamsUser array)
        {
            Data data = new Data();
            Module module = new Module("pvsamv1");

            ArrayParams allarray = new ArrayParams(array, new ArrayParamsComputed(gis, array));
            allarray.setDataParameters(data);

            if (module.Exec(data))
            {
                return new SystemModelOutput(data);
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
                Console.WriteLine("pvsam1 failed\n");
                return null;
            }

        }
    }
}
