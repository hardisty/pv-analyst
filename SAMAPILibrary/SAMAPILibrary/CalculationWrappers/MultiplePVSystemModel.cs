using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.CalculationWrappers
{
    public class MultiplePVSystemModel
    {
        List<PVSystemModel> models = new List<PVSystemModel>();

        public void Add(PVSystemModel add)
        {
            models.Add(add);
        }

        public delegate float floatReturner();

        public float[] getYearOneOutput()
        {
            float[] output = new float[models.Count];
            for (int i = 0; i < models.Count; i++)
            {
                output[i] = models[i].getYearOneOutput();
            }
            return output;
        }
    }
}
