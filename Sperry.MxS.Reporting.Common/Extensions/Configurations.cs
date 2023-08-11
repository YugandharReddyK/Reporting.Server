using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Extensions
{
    public static class Configurations
    {
        public static string UnitSets => EmbeddedResourceHelper.ReadResource("UnitSets.xml");

        public static string FormatType => EmbeddedResourceHelper.ReadResource("FormatType.xml");

    }
}
