using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.DataHandling.Parameters;

namespace SAMAPILibrary.DataHandling.OutputData
{
    public class CompiledOutputData
    {
        CashLoanOutput mclo;
        PVSAMV1Output mpvo;
        UtilityRateOutput muro;
        SizeAndCostSettings mscs;

        public CompiledOutputData(PVSAMV1Output pvo, UtilityRateOutput uro, CashLoanOutput clo, SizeAndCostSettings scs)
        {
            mclo = clo;
            mpvo = pvo;
            muro = uro;
            mscs = scs;
        }

        public CashLoanOutput getCashLoanOutput()
        {
            return mclo;
        }
        public PVSAMV1Output getPVSAMV1Output()
        {
            return mpvo;
        }
        public UtilityRateOutput getUtilityRateOutput()
        {
            return muro;
        }
        public SizeAndCostSettings getSizeAndCostSettings()
        {
            return mscs;
        }
    }
}
