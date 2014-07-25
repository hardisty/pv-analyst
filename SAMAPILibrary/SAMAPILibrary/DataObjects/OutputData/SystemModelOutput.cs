using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataObjects.OutputData
{
    public class SystemModelOutput
    {
        private Data data;

        public float[] ac_hourly
        {
            get
            {
                return data.GetArray("hourly_ac_net");
            }
        }
        public float[] ac_monthly
        {
            get
            {
                return data.GetArray("monthly_ac_net");
            }
        }
        public float ac_annual
        {
            get
            {
                return data.GetNumber("annual_ac_net");
            }
        }

        public SystemModelOutput(Data data)
        {
            this.data = data;
        }
    }
}
