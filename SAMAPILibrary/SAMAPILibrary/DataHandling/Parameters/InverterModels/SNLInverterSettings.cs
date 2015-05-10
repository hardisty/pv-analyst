using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SAMAPILibrary.DataHandling.Parameters.InverterModels
{
    public class SNLInverterSettings: IInverterSettings
    {   
        [ISettings.Param("inv_snl_c0","float")]
        public float inv_snl_c0;
        [ISettings.Param("inv_snl_c1","float")]
        public float inv_snl_c1;
        [ISettings.Param("inv_snl_c2","float")]
        public float inv_snl_c2;
        [ISettings.Param("inv_snl_c3","float")]
        public float inv_snl_c3;
        [ISettings.Param("inv_snl_paco","float")]
        public float inv_snl_paco;
        [ISettings.Param("inv_snl_pdco","float")]
        public float inv_snl_pdco;
        [ISettings.Param("inv_snl_pnt","float")]
        public float inv_snl_pnt;
        [ISettings.Param("inv_snl_pso","float")]
        public float inv_snl_pso;
        [ISettings.Param("inv_snl_vdco","float")]
        public float inv_snl_vdco;
        [ISettings.Param("inv_snl_vdcmax","float")]
        public float inv_snl_vdcmax;
        [ISettings.Param("inverter_model","float")]
        public float inverter_model;

        public SNLInverterSettings(String inverter_model_identifier){

            inverter_model = 0;

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
                var reader = new StreamReader(File.OpenRead("Data\\CEC Inverters.csv"));
                //Read 3 bogus lines
                var line = reader.ReadLine();
                line = reader.ReadLine();
                line = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var strings = line.Split(',');
                    String name = strings[0];

                    if (name.Equals(inverter_model_identifier))
                    {
                        int i = 1;
                        float inv_snl_ac_voltage = float.Parse(strings[i]); i++;
                        inv_snl_paco = float.Parse(strings[i]); i++;
                        inv_snl_pdco = float.Parse(strings[i]); i++;
                        inv_snl_vdco = float.Parse(strings[i]); i++;
                        inv_snl_pso = float.Parse(strings[i]); i++;
                        inv_snl_c0 = float.Parse(strings[i]); i++;
                        inv_snl_c1 = float.Parse(strings[i]); i++;
                        inv_snl_c2 = float.Parse(strings[i]); i++;
                        inv_snl_c3 = float.Parse(strings[i]); i++;
                        inv_snl_pnt = float.Parse(strings[i]); i++;
                        inv_snl_vdcmax = float.Parse(strings[i]); i++;
                        float inv_snl_idcmax = float.Parse(strings[i]); i++;
                        float inv_snl_mppt_low = float.Parse(strings[i]); i++;
                        float inv_snl_mppt_hi = float.Parse(strings[i]); i++;
                        return;
                    }
                }

                throw new Exception("Inverter Model Identifier not Found");
            }
        }

        public override float getMaxRatedVoltage()
        {
            return inv_snl_vdcmax;
        }
    }
}
