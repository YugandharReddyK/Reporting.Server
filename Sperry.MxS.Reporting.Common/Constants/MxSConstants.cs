
using System;
using System.IO;

namespace Sperry.MxS.Core.Common.Constants
{
    public class MxSConstants
    {
        public static readonly DateTime MinDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public const double EarthRadius = 6378.137;

        public const string AllRunsFilter = "All";

        public const string SurveyIndicationFormat = "yyyy-MM-dd HH:mm:ss";

        private const string ServerSettingsFolderLocation = "\\Halliburton\\MaxSurvey\\ServerSettings";

        public const int MFMFlag = 1;

        public const int IFR1Flag = 2;

        public const int IFR2Flag = 4;

        public const int IcaFlag = 8;

        public const int CazFlag = 16;

        public const int ServiceUsedFlagValue = 1;

        public const int ServiceNotUsedFlagValue = 0;

        public const string DefaultCultureCode = "en-US";

        public const string DefaultDatePattern = "dd-MMM-yy";

        public const string DefaultTimePattern = "HH:mm:ss";

        public const string DefaultDateTimeFormat = "dd-MMM-yy HH:mm:ss";

        public const string DefaultImportADIServer = "-LOCAL-";

        public static string PublicUserDataFolder => Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public";

        public static string ServerSettingsFolder => Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + ServerSettingsFolderLocation;

        public static string DefaultExportPath => PublicUserDataFolder + "\\MaxSurvey\\Output";

        public static string DefaultImportPath => PublicUserDataFolder + "\\MaxSurvey\\Input";

        public const string RunFilterSplitterChar = "-";

        public static string UserName { get; set; }

        public static string SaveCaller { get; set; }

        public const string RunsPropertyName = "Runs";

        public const string WaypointsPropertyName = "Waypoints";

        public const string OdisseusToolCodeSectionsPropertyName = "OdisseusToolCodeSection";

        public const string SolutionsPropertyName = "Solutions";

        public const string RawSurveysPropertyName = "RawSurveys";

        public const string ShortSurveysPropertyName = "ShortSurveys";

        public const string PlanSurveysPropertyName = "PlanSurveys";

        public const string ASARulesPropertyName = "ASARules";

        public const string RunPropertyName = "Run";

        public const string SolutionPropertyName = "Solution";

    }
}
