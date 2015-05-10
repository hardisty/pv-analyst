using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;

namespace SAMAPILibrary.DataHandling.OutputData
{
    public class CashLoanOutput: Output
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
        public float[] cf_operating_expenses
        {
            get
            {
                return data.GetArray("cf_operating_expenses");
            }
        }
        public float[] cf_debt_payment_total
        {
            get
            {
                return data.GetArray("cf_debt_payment_total");
            }
        }

        public CashLoanOutput(Data data): base(data)
        {
            this.data = data;
        }
    }
}
