using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.Template;
using Sperry.MxS.Core.Common.Models.Units;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Interfaces;
using Sperry.MxS.Core.Common.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sperry.MxS.Core.Common.Models.Template
{
    public class MxSMapInformation : IMxSMapInformation
    {
        public MxSDefaultUnitSystem MetricSystem { get; private set; }
        
        public MxSDefaultUnitSystem EnglishSystem { get; private set; }
        
        public IDictionary<string, string[]> MeasurementWithShortUnits { get; private set; }
        
        public IDictionary<string, string[]> MeasurementWithHeaderDisplayUnits { get; private set; }
        
        public IDictionary<string, string> MeasurementWithVariable { get; private set; }
        
        public IDictionary<string, MxSDetailUnitInformation> MeasurementWithDetailInformation { get; private set; }

        public IList<TemplateKeyValuePair<string, string>> AvailableVariables { get; private set; }

        public MxSMapInformation()
        {
            MxSMeasurements measurements = EmbeddedResourceHelper.ReadResource<MxSMeasurements>("Measurements.json");
            EnglishSystem = EmbeddedResourceHelper.ReadResource<MxSDefaultUnitSystem>("EnglishSystem.json");
            MetricSystem = EmbeddedResourceHelper.ReadResource<MxSDefaultUnitSystem>("MetricSystem.json");
            MxSUnitVariableRelations relations = EmbeddedResourceHelper.ReadResource<MxSUnitVariableRelations>("UnitVariableRealtion.json");

            MeasurementWithVariable = relations.Map.ToDictionary(item => item.Variable.ToLower(),
                item => item.Unit);
            MeasurementWithShortUnits = measurements.Measurements.ToDictionary(item => item.Name,
                item => item.Unit.Select(x => x.Name).ToArray());

            MeasurementWithHeaderDisplayUnits = measurements.Measurements.ToDictionary(item => item.Name,
                item => item.Unit.Select(x => x.HeaderDisplayValue).ToArray());

            AvailableVariables = GetTemplateKeyValuePairList(relations);

            var detailQuery =
                (from uu in measurements.Measurements from item in uu.Unit select new { Name = uu.Name, Unit = item }).ToList();
            MeasurementWithDetailInformation = detailQuery.ToDictionary(item => item.Name + item.Unit.Name,
                item => item.Unit);
        }

        private IList<TemplateKeyValuePair<string, string>> GetTemplateKeyValuePairList(MxSUnitVariableRelations relations)
        {
            List<TemplateKeyValuePair<string, string>> availableVariables =
                new List<TemplateKeyValuePair<string, string>>();
            relations.Map.ToList().ForEach(
                item =>
                {
                    TemplateKeyValuePair<string, string> variablekeyValue = new TemplateKeyValuePair<string, string>();
                    variablekeyValue.Key = item.DisplayName;
                    variablekeyValue.Value = item.Variable;
                    availableVariables.Add(variablekeyValue);
                }
            );

            return availableVariables;
        }

        private T LoadMap<T>(string content) where T : class
        {
            T result;
            using (var reader = new System.IO.StringReader(content))
            {
                var serializer = new XmlSerializer(typeof(T));
                result = serializer.Deserialize(reader) as T;
            }
            if (result == null) throw new ArgumentException(typeof(T).ToString());
            return result;
        }
    }
}
