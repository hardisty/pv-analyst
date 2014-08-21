using SAMAPILibrary.DataHandling.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataHandling.Input
{

    interface IGISInput : IEnumerable<GISData>
    {

    }

    class GISFileInput : IGISInput
    {

    }



}
