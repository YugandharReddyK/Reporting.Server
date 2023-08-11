using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.ImportSurveys;
using Sperry.MxS.Core.Common.Models.Surveys;
using Sperry.MxS.Core.Common.Models.CoordSys;
using Sperry.MxS.Core.Common.Models.Template;
using Sperry.MxS.Core.Common.Utilities;
using ListHelper = Sperry.MxS.Core.Common.Helpers.ListHelper;
using System.Collections.Generic;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models
{
    public class ThirdPartyWell : DataModelBase
    {
        private ExportTemplateConfiguration _exportTemplateConfiguration;
        private ImportTemplateConfiguration _importTemplateConfiguration;

        public ThirdPartyWell()
        {

        }

        public ThirdPartyWell(Well well, ExportTemplateConfiguration exportTemplateConfiguration, ImportTemplateConfiguration importTemplateConfiguration)
        {
            Id = well.Id;
            WellName = well.Name;
            _exportTemplateConfiguration = exportTemplateConfiguration;
            _importTemplateConfiguration = importTemplateConfiguration;
            Status = well.Status.ToString();
            Customer = well.Customer;
            Company = well.Company;
            Rig = well.Rig;
            Easting = well.Easting;
            Northing = well.Northing;
            Elevation = well.Elevation;
            Latitude = CoordService.ToGeoLatitudeString(well.Latitude);
            Longitude = CoordService.ToGeoLongitudeString(well.Longitude);
            TotalCorrection = well.TotalCorrection;
            NorthReference = well.NorthReference.ToString();
            UnitSystem = well.UnitSystem.ToString();

            CalculateElevationValues();

            UpdateRawSurveyData(well);
            UpdateCorrectedSurveyData(well);
        }

        [JsonProperty]
        public string Status { get; set; }

        [JsonProperty]
        public string Customer { get; set; }

        [JsonProperty]
        public string WellName { get; set; }

        [JsonProperty]
        public string Company { get; set; }

        [JsonProperty]
        public string Rig { get; set; }

        [JsonProperty]
        public double Easting { get; set; }

        [JsonProperty]
        public double Northing { get; set; }

        [JsonProperty]
        public double Elevation { get; set; }

        [JsonProperty]
        public string Latitude { get; set; }

        [JsonProperty]
        public string Longitude { get; set; }

        [JsonProperty]
        public SurveyGrid<ResultSurveyValue> CorrectedDefinitiveSurveys { get; set; } = new SurveyGrid<ResultSurveyValue>();

        [JsonProperty]
        public SurveyGrid<ResultSurveyValue> RawSurveys { get; set; } = new SurveyGrid<ResultSurveyValue>();

        [JsonProperty]
        public string NorthReference { get; set; }

        [JsonProperty]
        public double TotalCorrection { get; set; }

        [JsonProperty]
        public string UnitSystem { get; set; }

        [JsonProperty]
        public WellHeaders Headers { get; set; } = new WellHeaders();

        // TODO: Suhail - Need to move this to extensions 
        public void CalculateElevationValues()
        {
            if (UnitSystem == "English")
            {
                Headers.Northing = "Northings/Y (ft):";
                Headers.Easting = "Eastings/X (ft):";
                Headers.Elevation = "Elevation (ft):";
                Northing = Northing * MxSConstant.METRICTOFEET;
                Easting = Easting * MxSConstant.METRICTOFEET;
            }
            else
            {
                Headers.Northing = "Northings/Y (m):";
                Headers.Easting = "Eastings/X (m):";
                Headers.Elevation = "Elevation (m):";
                Elevation = Elevation / MxSConstant.METRICTOFEET;
            }
        }

        public void UpdateRawSurveyData(Well well)
        {
            List<string> mappedColumnNames = new List<string>();
            foreach (var x in _importTemplateConfiguration.MappingConfigurations)
            {
                if (!string.IsNullOrEmpty(x.Variable.Key))
                    mappedColumnNames.Add(x.Variable.Key);
            }
            List<RawSurvey> rawSurveys = well.AllRawSurveys.ToList();
            well.AllShortSurveys.ToList().ForEach(c => rawSurveys.Add(new RawSurvey(c)));
            this.UpdateRawSurveyColumnNames(well, mappedColumnNames);
            int depthPrecision = FormatHelper.GetPrecision(MxSFormatType.MeasuredDepthDistance, well.UnitSystem).Precision;
            int temperaturePrecision = FormatHelper.GetPrecision(MxSFormatType.Temperature, well.UnitSystem).Precision;
            int GsPrecision = FormatHelper.GetPrecision(MxSFormatType.GxGyGz, well.UnitSystem).Precision;
            int BsPrecision = FormatHelper.GetPrecision(MxSFormatType.BxByBz, well.UnitSystem).Precision;
            int inclinationPrecision = FormatHelper.GetPrecision(MxSFormatType.Inclination, well.UnitSystem).Precision;
            int azimuthPrecision = FormatHelper.GetPrecision(MxSFormatType.Azimuth, well.UnitSystem).Precision;
            foreach (RawSurvey rawSurvey in rawSurveys)
            {
                SurveyRow<ResultSurveyValue> row = new SurveyRow<ResultSurveyValue>();

                double displayDepth = FormatHelper.GetRoundValueByPrecision(UnitConverter.Convert(MxSUnitSystemEnum.English, well.UnitSystem, rawSurvey.Depth, MxSFormatType.MeasuredDepthDistance), depthPrecision);
                double displayTemperature = FormatHelper.GetRoundValueByPrecision(UnitConverter.Convert(MxSUnitSystemEnum.English, well.UnitSystem, rawSurvey.Temperature.HasValue ? rawSurvey.Temperature.Value : 0.0, MxSFormatType.Temperature), temperaturePrecision);
                double? Gx = rawSurvey.Gx.HasValue ? FormatHelper.GetRoundValueByPrecision(rawSurvey.Gx.Value, GsPrecision) : rawSurvey.Gx;
                double? Gy = rawSurvey.Gy.HasValue ? FormatHelper.GetRoundValueByPrecision(rawSurvey.Gy.Value, GsPrecision) : rawSurvey.Gy;
                double? Gz = rawSurvey.Gz.HasValue ? FormatHelper.GetRoundValueByPrecision(rawSurvey.Gz.Value, GsPrecision) : rawSurvey.Gz;
                double? Bx = rawSurvey.Bx.HasValue ? FormatHelper.GetRoundValueByPrecision(rawSurvey.Bx.Value, BsPrecision) : rawSurvey.Bx;
                double? By = rawSurvey.By.HasValue ? FormatHelper.GetRoundValueByPrecision(rawSurvey.By.Value, BsPrecision) : rawSurvey.By;
                double? Bz = rawSurvey.Bz.HasValue ? FormatHelper.GetRoundValueByPrecision(rawSurvey.Bz.Value, BsPrecision) : rawSurvey.Bz;
                double? mwdInclination = rawSurvey.MWDInclination.HasValue ? FormatHelper.GetRoundValueByPrecision(rawSurvey.MWDInclination.Value, inclinationPrecision) : rawSurvey.MWDInclination;
                double? sagInclination = rawSurvey.SagInclination.HasValue ? FormatHelper.GetRoundValueByPrecision(rawSurvey.SagInclination.Value, inclinationPrecision) : rawSurvey.SagInclination;

                double? mwdShortCollar = rawSurvey.MWDShortCollar.HasValue ? FormatHelper.GetRoundValueByPrecision(rawSurvey.MWDShortCollar.Value, azimuthPrecision) : rawSurvey.MWDShortCollar;
                double? mwdLongCollar = rawSurvey.MWDLongCollar.HasValue ? FormatHelper.GetRoundValueByPrecision(rawSurvey.MWDLongCollar.Value, azimuthPrecision) : rawSurvey.MWDLongCollar;


                MxSImportState mwdInclinationState = MxSImportState.None;
                MxSImportState azimLCState = MxSImportState.None;
                MxSImportState azimSCState = MxSImportState.None;
                if (rawSurvey.CorrectedSurvey != null)
                {
                    MxSQCLevel inclinationLevel = rawSurvey.CorrectedSurvey.InclinationLevel;
                    if (inclinationLevel == MxSQCLevel.Error)
                    {
                        mwdInclinationState = MxSImportState.Error;
                    }
                    else if (inclinationLevel == MxSQCLevel.Warning)
                    {
                        mwdInclinationState = MxSImportState.Warning;
                    }
                    MxSQCLevel azimLCLevel = rawSurvey.CorrectedSurvey.AzimLcLevel;
                    if (azimLCLevel == MxSQCLevel.Error)
                    {
                        azimLCState = MxSImportState.Error;
                    }
                    else if (azimLCLevel == MxSQCLevel.Warning)
                    {
                        azimLCState = MxSImportState.Warning;
                    }
                    MxSQCLevel azimSCLevel = rawSurvey.CorrectedSurvey.AzimScLevel;
                    if (azimSCLevel == MxSQCLevel.Error)
                    {
                        azimSCState = MxSImportState.Error;
                    }
                    else if (azimSCLevel == MxSQCLevel.Warning)
                    {
                        azimSCState = MxSImportState.Warning;
                    }
                }

                row.Values.Add(new ResultSurveyValue() { Value = rawSurvey.Run.RunNumber, State = MxSImportState.None, ToolTip = "" });
                row.Values.Add(new ResultSurveyValue() { Value = rawSurvey.DateTime.ToString(MxSConstants.DefaultDatePattern + " " + MxSConstants.DefaultTimePattern), State = MxSImportState.None, ToolTip = "" });
                row.Values.Add(new ResultSurveyValue() { Value = displayDepth, State = MxSImportState.None, ToolTip = "" });
                row.Values.Add(new ResultSurveyValue() { Value = Gx, State = MxSImportState.None, ToolTip = "" });
                row.Values.Add(new ResultSurveyValue() { Value = Gy, State = MxSImportState.None, ToolTip = "" });
                row.Values.Add(new ResultSurveyValue() { Value = Gz, State = MxSImportState.None, ToolTip = "" });
                row.Values.Add(new ResultSurveyValue() { Value = Bx, State = MxSImportState.None, ToolTip = "" });
                row.Values.Add(new ResultSurveyValue() { Value = By, State = MxSImportState.None, ToolTip = "" });
                row.Values.Add(new ResultSurveyValue() { Value = Bz, State = MxSImportState.None, ToolTip = "" });

                if (mappedColumnNames.Contains("MWD Inc"))
                    row.Values.Add(new ResultSurveyValue() { Value = mwdInclination, State = mwdInclinationState, ToolTip = "" });
                if (mappedColumnNames.Contains("Sag Inc"))
                    row.Values.Add(new ResultSurveyValue() { Value = sagInclination, State = MxSImportState.None, ToolTip = "" });
                if (mappedColumnNames.Contains("MWD SC"))
                    row.Values.Add(new ResultSurveyValue() { Value = mwdShortCollar, State = azimSCState, ToolTip = "" });
                if (mappedColumnNames.Contains("MWD LC"))
                    row.Values.Add(new ResultSurveyValue() { Value = mwdLongCollar, State = azimLCState, ToolTip = "" });
                if (mappedColumnNames.Contains("Pump Status"))
                    row.Values.Add(new ResultSurveyValue() { Value = rawSurvey.PumpStatus.ToString(), State = MxSImportState.None, ToolTip = "" });
                if (mappedColumnNames.Contains("Serial Number"))
                    row.Values.Add(new ResultSurveyValue() { Value = rawSurvey.Run.SerialNumber, State = MxSImportState.None, ToolTip = "" });
                if (mappedColumnNames.Contains("Azimuth Type"))
                    row.Values.Add(new ResultSurveyValue() { Value = rawSurvey.AzimuthType.ToString(), State = MxSImportState.None, ToolTip = "" });
                if (mappedColumnNames.Contains("Temperature"))
                    row.Values.Add(new ResultSurveyValue() { Value = displayTemperature, State = MxSImportState.None, ToolTip = "" });

                RawSurveys.Rows.Add(row);
            }
        }


        public void UpdateCorrectedSurveyData(Well well)
        {
            List<DerivedSurveyValues> derviedSurveyValues = new List<DerivedSurveyValues>();

            well.AllCorrectedSurveys.Where(c => c.SurveyType == MxSSurveyType.Definitive).ToList().ForEach(c => derviedSurveyValues.Add(new DerivedSurveyValues(c)));
            well.AllShortSurveys.ToList().ForEach(c => derviedSurveyValues.Add(new DerivedSurveyValues(new CorrectedSurvey(c))));

            List<string[]> rows = ListHelper.ExportDataToExcel(derviedSurveyValues.OfType<object>().ToList(), FormatHelper.GetCorrectedSurveyColumnOptions(), well.UnitSystem, well.TimeZone, null, _exportTemplateConfiguration, true, false);
            if (rows.Count > 0)
            {
                CorrectedDefinitiveSurveys.Columns = new List<string>(rows[0]);
                foreach (string[] correctedSurveyRow in rows.Skip(1))
                {
                    SurveyRow<ResultSurveyValue> row = new SurveyRow<ResultSurveyValue>();
                    foreach (string cellData in correctedSurveyRow)
                    {
                        row.Values.Add(new ResultSurveyValue() { Value = cellData, State = MxSImportState.None, ToolTip = "" });
                    }
                    CorrectedDefinitiveSurveys.Rows.Add(row);
                }
            }
        }

    }
}
