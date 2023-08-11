using Sperry.MxS.Core.Common.Models.Units;
using Sperry.MxS.Core.Common.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Interfaces
{
    public interface IMxSMapInformation
    {
        MxSDefaultUnitSystem MetricSystem { get; }

        MxSDefaultUnitSystem EnglishSystem { get; }

        IDictionary<string, string[]> MeasurementWithShortUnits { get; }

        IDictionary<string, string> MeasurementWithVariable { get; }

        IDictionary<string, MxSDetailUnitInformation> MeasurementWithDetailInformation { get; }

        IList<TemplateKeyValuePair<string, string>> AvailableVariables { get; }

        IDictionary<string, string[]> MeasurementWithHeaderDisplayUnits { get; }
    }
}
