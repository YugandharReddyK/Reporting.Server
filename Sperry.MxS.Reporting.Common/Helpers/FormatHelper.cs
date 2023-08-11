using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using Sperry.MxS.Core.Common.Extensions;
using Sperry.MxS.Core.Common.Utilities;
using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Models;
using System.IO;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class FormatHelper
    {
        private static readonly XmlDocument UnitSetsXml;

        private static readonly List<FormatTypeProperties> Formats;

        static FormatHelper()
        {
            XmlDocument xmlDocument = new XmlDocument();
            UnitSetsXml = new XmlDocument();
            xmlDocument.LoadXml(Configurations.FormatType);
            UnitSetsXml.LoadXml(Configurations.UnitSets);
            Formats = new List<FormatTypeProperties>();
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/FormatTypeFile/FormatType");
            foreach (XmlNode item in xmlNodeList)
            {
                FormatTypeProperties formatTypeProperties = RetrieveXmlDataSet(Encoding.UTF8.GetBytes(item.OuterXml));
                formatTypeProperties.Type = (MxSFormatType)Enum.Parse(typeof(MxSFormatType), item.Attributes["type"].Value);
                Formats.Add(formatTypeProperties);
            }
        }

        public static Format GetPrecision(MxSFormatType formatType, MxSUnitSystemEnum unitSystem)
        {
            Format result = new Format();
            string unitName = GetUnitName(formatType, unitSystem);
            if (string.IsNullOrEmpty(unitName))
            {
                return result;
            }

            FormatTypeProperties formatType2 = GetFormatType(formatType);
            if (formatType2 == null)
            {
                return result;
            }

            foreach (Format format in formatType2.Formats)
            {
                if (string.Equals(format.UnitName, unitName))
                {
                    return format;
                }
            }

            return result;
        }

        public static FormatTypeProperties GetFormatType(MxSFormatType formatType)
        {
            return Formats.FirstOrDefault((FormatTypeProperties format) => format.Type == formatType);
        }

        private static FormatTypeProperties RetrieveXmlDataSet(byte[] binaryData)
        {
            using (MemoryStream stream = new MemoryStream(binaryData))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(FormatTypeProperties));
                return xmlSerializer.Deserialize(stream) as FormatTypeProperties;
            }
        }

        public static string GetUnitName(MxSFormatType formatType, MxSUnitSystemEnum unitSystem)
        {
            XmlDocument unitSetsXml = UnitSetsXml;
            int num = (int)formatType;
            XmlNode xmlNode = unitSetsXml.SelectSingleNode("/UnitSets/UnitSet[@type='" + num + "']");
            if (xmlNode == null)
            {
                return unitSystem.GetDescription();
            }

            if (xmlNode.ChildNodes.Count > 1)
            {
                XmlNode xmlNode2 = xmlNode.SelectSingleNode("Unit[@unitSystem='" + unitSystem.ToString() + "']");
                if (xmlNode2 == null)
                {
                    return unitSystem.GetDescription();
                }

                return xmlNode2.Attributes["name"].Value;
            }

            if (xmlNode.ChildNodes.Count == 1)
            {
                return xmlNode.ChildNodes[0].Attributes["name"].Value;
            }

            return unitSystem.GetDescription();
        }

        public static double GetRoundValueByPrecision(double value, int precision)
        {
            return Math.Round(value, precision, MidpointRounding.AwayFromZero);
        }

        public static FormatTypeProperties GetPrecision(MxSFormatType formatType)
        {
            return GetFormatType(formatType);
        }

        public static List<string> GetUnitNames()
        {
            List<string> list = new List<string>();
            XmlNodeList xmlNodeList = UnitSetsXml.SelectNodes("/UnitSets/UnitSet/Unit");
            if (xmlNodeList == null)
            {
                return list;
            }

            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                XmlNode xmlNode = xmlNodeList.Item(i);
                if (xmlNode != null)
                {
                    string value = xmlNode.Attributes["name"].Value;
                    if (!list.Contains(value))
                    {
                        list.Add(value);
                    }
                }
            }

            return list;
        }


        public static ColumnOptions GetCorrectedSurveyColumnOptions()
        {
            try
            {
                // Suhail - Modified Reading Resources files from Resource.Designer to Embedded resource

                //JsonSerializer serializer = new JsonSerializer
                //{
                //    MissingMemberHandling = MissingMemberHandling.Ignore,
                //    NullValueHandling = NullValueHandling.Include,
                //    DefaultValueHandling = DefaultValueHandling.Include
                //};

                //using (TextReader stringReader = new StringReader(Resources.CorrectedSurveyColumnOptions))
                //{
                //    using (var jsonTextWriter = new JsonTextReader(stringReader))
                //    {
                //        return (ColumnOptions)serializer.Deserialize(jsonTextWriter, typeof(ColumnOptions));
                //    }
                //}
                return EmbeddedResourceHelper.ReadResource<ColumnOptions>("CorrectedSurveyColumnOptions.json");
            }
            catch { }
            return null;
        }

        public static string GetRoundStringValueByPrecision(double value, int precision)
        {
            return Math.Round(value, precision, MidpointRounding.AwayFromZero).ToString("F" + precision);
        }
    }
}
