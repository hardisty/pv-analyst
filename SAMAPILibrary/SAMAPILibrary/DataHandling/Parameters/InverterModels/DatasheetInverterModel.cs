using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.InverterModels;

namespace SAMAPILibrary.DataHandling.InverterModels
{
    // Holder for the data for a "made up" inverter
    class DatasheetInverterModel: InverterModelParams
    {
        public readonly float inv_ds_paco;
        public readonly float inv_ds_eff;
        public readonly float inv_ds_pnt;
        public readonly float inv_ds_pso;
        public readonly float inv_ds_vdco;
        public readonly float inv_ds_vdcmax;
        public readonly float inverter_model;

        public DatasheetInverterModel(String inverter_model_identifier, float size){

            inverter_model = 1;

            if (inverter_model_identifier == "default")
            {
                //SAM Defaults
                inv_ds_paco = size;
                inv_ds_eff = 96.1f; //Enter as pct for some reason
                inv_ds_pnt = 0.17f;
                inv_ds_pso = 0f;
                inv_ds_vdco = 310f;
                inv_ds_vdcmax = 600f;
            }
            else
            {
                //TODO implement inverter database parsing
                throw new NotImplementedException("Datasheet Inverter Identifier Not Found");
            }
        }

        public override void setDataParameters(SAMAPI.Data data)
        {
            data.SetNumber("inverter_model",inverter_model);
            data.SetNumber("inv_ds_paco", inv_ds_paco);
            data.SetNumber("inv_ds_eff", inv_ds_eff);
            data.SetNumber("inv_ds_pnt", inv_ds_pnt);
            data.SetNumber("inv_ds_pso", inv_ds_pso);
            data.SetNumber("inv_ds_vdco", inv_ds_vdco);
            data.SetNumber("inv_ds_vdcmax", inv_ds_vdcmax);
        }

        public override float getMaxRatedVoltage()
        {
            return inv_ds_vdcmax;
        }
    }
    }

