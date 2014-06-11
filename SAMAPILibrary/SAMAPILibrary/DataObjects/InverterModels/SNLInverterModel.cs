using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataObjects.InverterModels
{
    /// <summary>
    /// Holder for Sandia National Labs inverter model parameters
    /// </summary>
    class SNLInverterModel: InverterModelParams
    {
        public readonly float inv_snl_c0;
        public readonly float inv_snl_c1;
        public readonly float inv_snl_c2;
        public readonly float inv_snl_c3;
        public readonly float inv_snl_paco;
        public readonly float inv_snl_pdco;
        public readonly float inv_snl_pnt;
        public readonly float inv_snl_pso;
        public readonly float inv_snl_vdco;
        public readonly float inv_snl_vdcmax;

        public SNLInverterModel(String inverter_model_identifier){

            if (inverter_model_identifier == "default")
            {
                //SMA America: SB40000US 240V
                inv_snl_c0 = -6.57929e-006f;
                inv_snl_c1 = 4.72925e-005f;
                inv_snl_c2 = 0.00202195f;
                inv_snl_c3 = 0.000285321f;
                inv_snl_paco = 4000f;
                inv_snl_pdco = 4186f;
                inv_snl_pnt = 0.17f;
                inv_snl_pso = 19.7391f;
                inv_snl_vdco = 310.67f;
                inv_snl_vdcmax = 480f;
            }
            else
            {
                //TODO implement inverter database parsing
                throw new NotImplementedException("SNL Inverter Identifier Not Found");
            }
        }

        public override void setDataParameters(SAMAPI.Data data)
        {
            data.SetNumber("inv_snl_c0", inv_snl_c0);
            data.SetNumber("inv_snl_c1", inv_snl_c1);
            data.SetNumber("inv_snl_c2", inv_snl_c2);
            data.SetNumber("inv_snl_c3", inv_snl_c3);
            data.SetNumber("inv_snl_paco", inv_snl_paco);
            data.SetNumber("inv_snl_pdco", inv_snl_pdco);
            data.SetNumber("inv_snl_pnt", inv_snl_pnt);
            data.SetNumber("inv_snl_pso", inv_snl_pso);
            data.SetNumber("inv_snl_vdco", inv_snl_vdco);
            data.SetNumber("inv_snl_vdcmax", inv_snl_vdcmax);
        }

        public override float getMaxRatedVoltage()
        {
            return inv_snl_vdcmax;
        }
    }
}
