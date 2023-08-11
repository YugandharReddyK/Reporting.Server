using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSCazandraSolution
    {
        [Description("Az L MAv(Raw)")]
        AzimuthLongCollarMavRaw = 0,
        
        [Description("Az L MAv(TFC)")]
        AzimuthLongCollarMavTFC = 1,
        
        [Description("Az L MAv(Trans)")]
        AzimuthLongCollarMavTrans = 2,
        
        [Description("Az L MAv(SFE)")]
        AzimuthLongCollarMavSFE = 3,
        
        [Description("Az L MAv(OSS)")]
        AzimuthLongCollarMavOSS = 4,
       
        [Description("Az S(Raw)")]
        AzimuthShortCollarRaw = 5,
        
        [Description("Az L(Raw)")]
        AzimuthLongCollarRaw = 6,
        
        [Description("Az S(TFC)")]
        AzimuthShortCollarTFC = 7,
        
        [Description("Az L(TFC)")]
        AzimuthLongCollarTFC = 8,
        
        [Description("Az S(Trans)")]
        AzimuthShortCollarTrans = 9,
        
        [Description("Az L(Trans)")]
        AzimuthLongCollarTrans = 10,
        
        [Description("Az S(SFE)")]
        AzimuthShortCollarSFE = 11,
        
        [Description("Az L(SFE)")]
        AzimuthLongCollarSFE = 12,
        
        [Description("Az S(OSS)")]
        AzimuthShortCollarOSS = 13,
        
        [Description("Az L(OSS)")]
        AzimuthLongCollarOSS = 14
    }
}
