﻿using System.Xml.Serialization;

namespace Sperry.MxS.Core.Common.Enums
{
    [XmlRoot("FormatTypeEnum")]
    public enum MxSFormatType
    {
        [XmlEnum(Name = "0")]
        Null = 0,
        [XmlEnum(Name = "1")]
        MeasuredDepthDistance = 1,
        [XmlEnum(Name = "1")]
        TVD = 1,
        [XmlEnum(Name = "1")]
        Elevation = 1,
        [XmlEnum(Name = "2")]
        NorthingEasting = 2,
        [XmlEnum(Name = "3")]
        MagneticFieldStrength = 3,
        [XmlEnum(Name = "3")]
        BxByBz = 3,
        [XmlEnum(Name = "4")]
        MagneticDipAngle = 4,
        [XmlEnum(Name = "4")]
        MagneticDeclination = 4,
        [XmlEnum(Name = "4")]
        TotalCorrection = 4,
        [XmlEnum(Name = "4")]
        GridConvergence = 4,
        [XmlEnum(Name = "5")]
        Gravity = 5,
        [XmlEnum(Name = "6")]
        GxGyGz = 6,
        [XmlEnum(Name = "7")]
        Temperature = 7,
        [XmlEnum(Name = "8")]
        Azimuth = 8,
        [XmlEnum(Name = "8")]
        Inclination = 8,
        [XmlEnum(Name = "9")]
        MagneticFluxDensity = 9,
        [XmlEnum(Name = "10")]
        PerDepth = 10,
        [XmlEnum(Name = "11")]
        Area = 11,
        [XmlEnum(Name = "12")]
        BoreholeDiameter = 12,
        [XmlEnum(Name = "13")]
        Density = 13,
        [XmlEnum(Name = "14")]
        MilliG = 14,
        [XmlEnum(Name = "15")]
        MilliG2 = 15,
        [XmlEnum(Name = "16")]
        SFPercent = 16,
        [XmlEnum(Name = "17")]
        Percent3 = 17,
        [XmlEnum(Name = "18")]
        MagneticFieldStrength4 = 18,
        [XmlEnum(Name = "19")]
        Gravity6 = 19,
        [XmlEnum(Name = "2")]
        Dynamic = 2,
        [XmlEnum(Name = "1")]
        DynamicAzi = 1,
        [XmlEnum(Name = "3")]
        DynamicBt = 3,
        [XmlEnum(Name = "8")]
        RigInclination = 8,
        [XmlEnum(Name = "21")]
        Goxy = 21,
        [XmlEnum(Name = "22")]
        HSSpread = 22,
        [XmlEnum(Name = "23")]
        HS = 23,
        [XmlEnum(Name = "24")]
        dBzdBxdBy = 24,
        [XmlEnum(Name = "25")]
        MagneticFieldStrength3 = 25,
        [XmlEnum(Name = "25")]
        Northing = 26,
        [XmlEnum(Name = "27")]
        Uncertainty = 27,
        [XmlEnum(Name = "28")]
        UncertaintyCovValue = 28,
        [XmlEnum(Name = "29")]
        UnitlessNoPrecision = 29,
        [XmlEnum(Name = "30")]
        UnitlessTwoPrecision = 30,
        [XmlEnum(Name = "31")]
        UnitlessFourPrecision = 31
    }
}
