using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Utilities
{
      public  class UnitConverter
      {
            public static double Convert(MxSUnitSystemEnum source, MxSUnitSystemEnum target, double value, MxSFormatType formatType)
            {
                  if (source == target)
                        return value;

                  if (formatType != MxSFormatType.Temperature
                      && formatType != MxSFormatType.MagneticFluxDensity
                      && formatType != MxSFormatType.MeasuredDepthDistance
                      && formatType != MxSFormatType.Area
                      && formatType != MxSFormatType.PerDepth
                      && formatType != MxSFormatType.BoreholeDiameter
                      && formatType != MxSFormatType.Density
                      && formatType != MxSFormatType.Uncertainty
                      && formatType != MxSFormatType.UncertaintyCovValue)
                        return value;

                  if (formatType == MxSFormatType.Temperature)
                  {
                        if (source == MxSUnitSystemEnum.English && target == MxSUnitSystemEnum.Metric)
                        {
                              return (value - 32) / 1.8;
                        }

                        return value * 1.8 + 32;
                  }

                  if (formatType == MxSFormatType.MagneticFluxDensity)
                  {
                        if (source == MxSUnitSystemEnum.English && target == MxSUnitSystemEnum.Metric)
                        {
                              return value / (MxSConstant.FEETTOMETRIC * MxSConstant.FEETTOMETRIC);
                        }

                        return value / (MxSConstant.METRICTOFEET * MxSConstant.METRICTOFEET);
                  }

                  if (formatType == MxSFormatType.PerDepth)
                  {
                        if (source == MxSUnitSystemEnum.English && target == MxSUnitSystemEnum.Metric)
                        {
                              return value / MxSConstant.FEETTOMETRIC;
                        }

                        return value / MxSConstant.METRICTOFEET;
                  }


                  if (formatType == MxSFormatType.Area || formatType == MxSFormatType.UncertaintyCovValue)
                  {
                        if (source == MxSUnitSystemEnum.English && target == MxSUnitSystemEnum.Metric)
                        {
                              return value * MxSConstant.FEETTOMETRIC * MxSConstant.FEETTOMETRIC;
                        }

                        return value * MxSConstant.METRICTOFEET * MxSConstant.METRICTOFEET;
                  }

                  if (formatType == MxSFormatType.BoreholeDiameter)
                  {
                        if (source == MxSUnitSystemEnum.English && target == MxSUnitSystemEnum.Metric)
                        {
                              return value * MxSConstant.INCHTOCM;
                        }

                        return value * MxSConstant.CMTOINCH;
                  }


                  if (formatType == MxSFormatType.Density)
                  {
                        if (source == MxSUnitSystemEnum.English && target == MxSUnitSystemEnum.Metric)
                        {
                              return value * MxSConstant.PoundPerCubicFeetToKgPerCubicMeter;
                        }

                        return value * MxSConstant.KgPerCubicMeterToPoundPerCubicFeet;
                  }

                  if (source == MxSUnitSystemEnum.English && target == MxSUnitSystemEnum.Metric)
                  {
                        return value * MxSConstant.FEETTOMETRIC;
                  }

                  return value * MxSConstant.METRICTOFEET;
            }

            public static double[] ConvertDepths(Well currentWell, IEnumerable<double> depths)
            {
                  if (currentWell != null && currentWell.UnitSystem == MxSUnitSystemEnum.Metric)
                  {
                        return depths != null ? depths.Select(depth => depth * MxSConstant.FEETTOMETRIC).ToArray() : null;
                  }
                  return depths != null ? depths.ToArray() : null;
            }
      }
}

