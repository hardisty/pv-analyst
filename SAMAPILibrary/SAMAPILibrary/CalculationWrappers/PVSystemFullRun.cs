using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;
using SAMAPILibrary.DataHandling.Parameters;
using SAMAPILibrary.DataHandling.OutputData;
using SAMAPILibrary.DataHandling.Parameters.InverterModels;

namespace SAMAPILibrary.CalculationWrappers
{
    public class PVSystemFullRun
    {
        public static CompiledOutputData run(GISData gis, GUIData gui)
        {

            PVSAMV1Settings pvss = PVSAMV1Settings.getDefault();
            GISAdapter.applySettings(pvss,gis);
            float arraypower = pvss.modules_per_string * pvss.strings_in_parallel * pvss.module_model.getRatedPower();
            DatasheetInverterSettings inverter = new DatasheetInverterSettings("default", arraypower * 1.15f);
            pvss.inverter_model = inverter;
            PVSAMV1Output pvo = (PVSAMV1Output) ModuleRunner.runModule(pvss);

            UtilityRateSettings urs = UtilityRateSettings.getDefault();
            GUIAdapter.applyUtilityRateSettings(urs,gui);
            urs.setValuesFromPriorOutput(pvo);
            UtilityRateOutput uro = (UtilityRateOutput)ModuleRunner.runModule(urs);

            SizeAndCostSettings sc = SizeAndCostSettings.getDefault();
            GUIAdapter.applySizeAndCostSettings(sc,gui);
            sc.setValuesFromPriorOutput(pvo);

            CashLoanSettings cls = CashLoanSettings.getDefault();
            GUIAdapter.applyCashLoanSettings(cls,gui);
            cls.setValuesFromPriorOutput(sc, uro);
            CashLoanOutput clo = (CashLoanOutput)ModuleRunner.runModule(cls);

            return new CompiledOutputData(pvo, uro, clo, sc);
        }
    }
}
