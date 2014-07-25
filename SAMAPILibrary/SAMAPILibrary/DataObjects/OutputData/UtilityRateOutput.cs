using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataObjects.OutputData
{

    public class UtilityRateOutput
    {
        private Data data;

        public int analysis_years { 
            get { 
                return (int) data.GetNumber("analysis_years"); 
            } 
        }

        public float[] energy_value{
            get{
                return data.GetArray("energy_value");
            }
        }
        
        public float[] energy_net
        {
            get
            {
                return data.GetArray("energy_net");
            }
        }

        public UtilityRateOutput(Data data)
        {
            this.data = data;
        }
    }
}
