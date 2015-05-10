using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling.Parameters.InverterModels
{
    public class DatasheetInverterSettings : IInverterSettings
    {
        [ISettings.Param("inv_ds_paco", "float")]
        public float inv_ds_paco;
        [ISettings.Param("inv_ds_eff", "float")]
        public float inv_ds_eff;
        [ISettings.Param("inv_ds_pnt", "float")]
        public float inv_ds_pnt;
        [ISettings.Param("inv_ds_pso", "float")]
        public float inv_ds_pso;
        [ISettings.Param("inv_ds_vdco", "float")]
        public float inv_ds_vdco;
        [ISettings.Param("inv_ds_vdcmax", "float")]
        public float inv_ds_vdcmax;
        [ISettings.Param("inverter_model", "float")]
        public float inverter_model;

        public DatasheetInverterSettings(String inverter_model_identifier, float size){

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

        public override float getMaxRatedVoltage()
        {
            return inv_ds_vdcmax;
        }
    }
}
