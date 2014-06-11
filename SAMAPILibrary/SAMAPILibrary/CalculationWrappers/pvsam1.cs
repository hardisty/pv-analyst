using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;
using SAMAPILibrary.DataObjects;

namespace SAMAPILibrary.CalculationWrappers
{
    public class pvsam1: IResourceCalculator
    {
        public pvsam1()
        {
        }

        public Data run(DataObjects.GISData gis, DataObjects.ArrayParams array)
        {
            Data data = new Data();
            Module module = new Module("pvsamv1");

            CompleteArrayParams allarray = new CompleteArrayParams(array, new ComputedArrayParams(gis, array));
            allarray.setDataParameters(data);

            if (module.Exec(data))
            {
                return data;
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
                Console.WriteLine("pvsamv1 example failed\n");
                return null;
            }

        }
    }
}
