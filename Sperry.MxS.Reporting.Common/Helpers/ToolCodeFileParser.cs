using Sperry.MxS.Core.Common.Models.Odisseus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sperry.MxS.Core.Common.Ex.Helpers
{
    public class ToolCodeFileParser
    {
        public static List<OdisseusToolCodeParams> Parse(string filePath)
        {
            List<OdisseusToolCodeParams> odisseusToolCodeFileParams = null;
            if (!string.IsNullOrEmpty(filePath))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(OdisseusToolCodes));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    odisseusToolCodeFileParams = ParseToolCodeParamaters((OdisseusToolCodes)deserializer.Deserialize(reader));
                }
            }
            return odisseusToolCodeFileParams;
        }

        static List<OdisseusToolCodeParams> ParseToolCodeParamaters(OdisseusToolCodes odisseusToolCodes)
        {
            List<OdisseusToolCodeParams> odisseusToolCodesList = new List<OdisseusToolCodeParams>();
            if ((odisseusToolCodes.ToolCode != null))
            {
                foreach (var toolCode in odisseusToolCodes.ToolCode)
                {
                    OdisseusToolCodeParams toolCodeParams = new OdisseusToolCodeParams(toolCode);
                    odisseusToolCodesList.Add(toolCodeParams);
                }
            }
            return odisseusToolCodesList;
        }
    }
}
