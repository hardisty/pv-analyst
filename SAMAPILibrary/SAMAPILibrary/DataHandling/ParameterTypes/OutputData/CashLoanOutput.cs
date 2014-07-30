using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataObjects.OutputData
{
    public class CashLoanOutput
    {
        private readonly Data data;

        public float lcoe_real
        { 
            get { 
                return data.GetNumber("lcoe_real"); 
            } 
        }
        public float lcoe_nom
        { 
            get { 
                return data.GetNumber("lcoe_nom"); 
            } 
        }
        
        public float npv
        { 
            get { 
                return data.GetNumber("npv"); 
            } 
        }
        public CashLoanOutput(Data data)
        {
            this.data = data;
        }
    }
}
