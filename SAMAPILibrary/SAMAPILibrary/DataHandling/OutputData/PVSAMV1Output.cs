using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.OutputData
{
    public class PVSAMV1Output: Output
    {
        private readonly Data data;

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
        public float sys_dc_rating
        {
            get
            {
                return data.GetNumber("nameplate_dc_rating");
            }
        }
        public float inv_ac_rating
        {
            get
            {
                int model = (int)data.GetNumber("inverter_model");
                if (model==0){
                    return data.GetNumber("inv_snl_paco");
                }
                else if (model == 1){
                    return data.GetNumber("inv_ds_paco");
                }
                else if (model == 2)
                {
                    return data.GetNumber("inv_pd_paco");
                }
                throw new Exception();
            }
        }
        public PVSAMV1Output(Data data): base(data)
        {
            this.data = data;
        }

        public float[] getHourlyElectricityProuction()
        {
            return ac_hourly;
        }
    }
}
