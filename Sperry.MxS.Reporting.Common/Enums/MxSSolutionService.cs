using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSSolutionService
    {
        
        [Description("MFM")]
        MFM = 1,
        
        [Description("MFM+Caz")]
        MFMCaz = 17,
        
        [Description("MFM+Ica")]
        MFMIca = 9,
        
        [Description("MFM+Caz+Ica")]
        MFMCazIca = 25,
        
        [Description("MFM+IFR1")]
        MFMIFR = 3,
        
        [Description("MFM+IFR1+Caz")]
        MFMIFRCaz = 19,
        
        [Description("MFM+IFR1+Ica")]
        MFMIFRIca = 11,
        
        [Description("MFM+IFR1+Caz+Ica")]
        MFMIFRCazIca = 27,
        
        [Description("MFM+IFR2")]
        MFMIIFR = 5,
        
        [Description("MFM+IFR2+Caz")]
        MFMIIFRCaz = 21,
        
        [Description("MFM+IFR2+Ica")]
        MFMIIFRIca = 13,
        
        [Description("MFM+IFR2+Caz+Ica")]
        MFMIIFRCazIca = 29
    }
}
