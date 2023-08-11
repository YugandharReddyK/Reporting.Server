using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models.DynamicQC;
using Sperry.MxS.Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sperry.MxS.Core.Common.Helpers
{
    public class DynamicQCParameterHelper
    {
        public static DynamicQCInputParameters GetDynamicQCParamters(MxSMagneticModelType? magneticReference, MxSAzimuthTypeEnum? aziType, MxSMSA? msa)
        {
            DynamicQCInputParameters dynamicQCInputParameters = new DynamicQCInputParameters();
            dynamicQCInputParameters.IPMToolCode = BuildIPMToolCode(magneticReference, aziType, msa);
            string text = dynamicQCInputParameters.IPMToolCode.Replace(MxSDynamicQCParameterHelperConstants.Plus, "");
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(EmbeddedResourceHelper.ReadResource("DynamicQCParameters.xml"));
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/DynamicQCParameters/" + text);

            foreach (XmlNode item in xmlNodeList)
            {
                if (item.SelectSingleNode(MxSConstant.AccelorometerBias) != null)
                {
                    dynamicQCInputParameters.DGxyz = Convert.ToDouble(item[MxSConstant.AccelorometerBias].InnerText);
                }

                if (item.SelectSingleNode(MxSConstant.MagBiases) != null)
                {
                    dynamicQCInputParameters.DBxyz = Convert.ToDouble(item[MxSConstant.MagBiases].InnerText);
                }

                if (item.SelectSingleNode(MxSConstant.AMIL) != null)
                {
                    dynamicQCInputParameters.DBzMod = Convert.ToDouble(item[MxSConstant.AMIL].InnerText);
                }

                if (item.SelectSingleNode(MxSConstant.DEC) != null)
                {
                    dynamicQCInputParameters.DEC = Convert.ToDouble(item[MxSConstant.DEC].InnerText);
                }

                if (item.SelectSingleNode(MxSConstant.DBH) != null)
                {
                    dynamicQCInputParameters.DBH = Convert.ToDouble(item[MxSConstant.DBH].InnerText);
                }

                if (item.SelectSingleNode(MxSConstant.MDI) != null)
                {
                    dynamicQCInputParameters.DDipe = Convert.ToDouble(item[MxSConstant.MDI].InnerText);
                }

                if (item.SelectSingleNode(MxSConstant.MFI) != null)
                {
                    dynamicQCInputParameters.DBe = Convert.ToDouble(item[MxSConstant.MFI].InnerText);
                }

                if (item.SelectSingleNode(MxSConstant.Sxy) != null)
                {
                    dynamicQCInputParameters.Sxy = Convert.ToDouble(item[MxSConstant.Sxy].InnerText);
                }

                if (item.SelectSingleNode(MxSConstant.BGm) != null)
                {
                    dynamicQCInputParameters.BGm = Convert.ToDouble(item[MxSConstant.BGm].InnerText);
                }

                if (item.SelectSingleNode(MxSConstant.DeltaBNoise) != null)
                {
                    dynamicQCInputParameters.DBNoise = Convert.ToDouble(item[MxSConstant.DeltaBNoise].InnerText);
                }

                if (item.SelectSingleNode(MxSConstant.DeltaDipNoise) != null)
                {
                    dynamicQCInputParameters.DDipNoise = Convert.ToDouble(item[MxSConstant.DeltaDipNoise].InnerText);
                }
            }

            return dynamicQCInputParameters;
        }

        private static string BuildIPMToolCode(MxSMagneticModelType? magRef, MxSAzimuthTypeEnum? aziType, MxSMSA? msa)
        {
            string azimuthValue = aziType != MxSAzimuthTypeEnum.LongCollar ? MxSConstant.MWDSC : MxSConstant.MWD;
            if (msa == MxSMSA.Yes)
            {
                return azimuthValue + "+" + magRef.ToString() + "+" + MxSConstant.MS;
            }

            return azimuthValue + "+" + magRef;
        }
    }
}
