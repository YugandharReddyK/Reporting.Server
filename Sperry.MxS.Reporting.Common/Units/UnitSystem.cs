using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Interfaces;
using Sperry.MxS.Core.Common.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Units
{
    public class UnitSystem : IMxSUnitSystem
    {
        internal const string DefaultMeasurement = "Unitless";
        private readonly IMxSMapInformation _mapInformation;

        public UnitSystem(IMxSMapInformation mapInformation)
        {
            _mapInformation = mapInformation;
        }

        public IEnumerable<string> GetUnitsByVariable(string variable, bool setDefaultValueWhenEmpty = false)
        {
            if (!string.IsNullOrEmpty(variable))
                return
                    _mapInformation.MeasurementWithShortUnits[
                        GetMeasurementByVariable(variable, setDefaultValueWhenEmpty)];
            return null;
        }

        public string GetDefaultUnitByVariable(string variable, string unitSystem, bool setDefaultValueWhenEmpty = false)
        {
            string measurement = GetMeasurementByVariable(variable, setDefaultValueWhenEmpty);
            MxSDefaultUnitSystem map = unitSystem == MxSUnitSystemEnum.Metric.ToString()
                ? _mapInformation.MetricSystem
                : _mapInformation.EnglishSystem;
            MxSUnitSystemMap found = map.Map.ToList().FirstOrDefault(item => item.Measurement == measurement);
            return found != null ? found.Name : _mapInformation.MeasurementWithShortUnits[measurement][0];
        }

        public string GetDefaultUnitHeaderDisplayValueByVariable(string variable, string unitSystem,
            bool setDefaultValueWhenEmpty = false)
        {
            string measurement = GetMeasurementByVariable(variable, setDefaultValueWhenEmpty);
            MxSDefaultUnitSystem map = unitSystem == MxSUnitSystemEnum.Metric.ToString()
                ? _mapInformation.MetricSystem
                : _mapInformation.EnglishSystem;
            MxSUnitSystemMap found = map.Map.ToList().FirstOrDefault(item => item.Measurement == measurement);
            return found != null
                ? found.HeaderDisplayValue
                : _mapInformation.MeasurementWithHeaderDisplayUnits[measurement][0];
        }

        public string GetDefaultUnitByVariable(string variable, bool setDefaultValueWhenEmpty = false)
        {
            string measurement = GetMeasurementByVariable(variable, setDefaultValueWhenEmpty);
            return _mapInformation.MeasurementWithShortUnits[measurement][0];
        }

        private string GetMeasurementByVariable(string variable, bool setDefaultValueWhenEmpty = false)
        {
            if (!_mapInformation.MeasurementWithVariable.ContainsKey(variable.ToLower()))
            {
                if (setDefaultValueWhenEmpty) return DefaultMeasurement;
                throw new ArgumentException("unsupported variable:" + variable);
            }
            return _mapInformation.MeasurementWithVariable[variable.ToLower()];
        }

        private MxSDetailUnitInformation GetDetailUnitInformation(string variable, string shortName)
        {
            string measurement = GetMeasurementByVariable(variable);
            if (!_mapInformation.MeasurementWithDetailInformation.ContainsKey(measurement + shortName))
                throw new ArgumentException("unsupported unit:" + shortName);
            return _mapInformation.MeasurementWithDetailInformation[measurement + shortName];
        }

        private double ConvertToInternal(string variable, string shortName, double value)
        {
            MxSDetailUnitInformation info = GetDetailUnitInformation(variable, shortName);
            return (value - info.Offset) / info.Factor;
        }

        private double ConvertFromInternal(string variable, string shortName, double value)
        {
            MxSDetailUnitInformation info = GetDetailUnitInformation(variable, shortName);
            return value * info.Factor + info.Offset;
        }

        public double Convert(string variable, string fromUnit, double value, string toUnit = "")
        {
            if (toUnit == "") toUnit = GetUnitsByVariable(variable, true).First();
            if (fromUnit == toUnit) return value;
            double result = ConvertToInternal(variable, fromUnit, value);
            return ConvertFromInternal(variable, toUnit, result);
        }

        public IList<TemplateKeyValuePair<string, string>> GetVariableInformation()
        {
            return _mapInformation.AvailableVariables;
        }
    }
}
