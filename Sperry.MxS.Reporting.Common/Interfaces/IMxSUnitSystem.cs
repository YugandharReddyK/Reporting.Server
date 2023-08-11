using Sperry.MxS.Core.Common.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Interfaces
{
    public interface IMxSUnitSystem
    {
        IEnumerable<string> GetUnitsByVariable(string variable, bool setDefaultValueWhenEmpty = false);

        string GetDefaultUnitByVariable(string variable, string unitSystem, bool setDefaultValueWhenEmpty = false);

        string GetDefaultUnitByVariable(string variable, bool setDefaultValueWhenEmpty = false);

        double Convert(string variable, string fromUnit, double value, string toUnit = "");

        string GetDefaultUnitHeaderDisplayValueByVariable(string variable, string unitSystem, bool setDefaultValueWhenEmpty = false);

        IList<TemplateKeyValuePair<string, string>> GetVariableInformation();
    }
}
