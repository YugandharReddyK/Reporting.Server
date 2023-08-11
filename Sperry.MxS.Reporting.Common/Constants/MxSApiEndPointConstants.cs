namespace Sperry.MxS.Core.Common.Constants
{
    public class MxSApiEndPointConstants
    {
        public const string BaseRoot = "api/v3";

        public const string GetVersion = "serverversion";

        public const string GetWell = "wells/{id}";

        public const string GetWellInfo = "wells/wellinfo/{id}";

        public const string GetWellsBasicInfos = "wells/wellsBasicInfos";

        public const string GetCompressedWell = "wells/compressed/{id}/";

        public const string GetWellWithInsiteId = "wells/insite/{insitewellid}";

        public const string GetWells = "wells";

        public const string SaveWell = "wells/save";

        public const string SaveWellFromCompressedRequest = "wells/save/compress";

        public const string DeleteWell = "wells/delete/{id}";

        public const string LockWell = "wells/lock/{id}";

        public const string UnlockWell = "wells/unlock/{id}";

        public const string CopyWell = "wells/copy/{id}";

        public const string ArchiveWell = "wells/archive/{id}";

        public const string UnArchiveWell = "wells/unarchive/{id}";

        public const string GetThirdPartyWellForUser = "wells/getthirdpartywellforuser";

        public const string AssociateWellFor3MWDUser = "wells/associatewellFor3mwduser/users/{wellId}";

        public const string DeleteWellAssociationFor3MWDUser = "wells/DeleteWellAssociationFor3MWDUser/deleteusers/{wellId}";

        public const string ActivateLinkToInsiteWell = "wells/activatelinktoinsitewell/{id}";

        public const string GetWellList = "wells/WellList";

        public const string EditWell = "wells/EditWell";

        public const string DeleteWell_WellManagement = "wells/DeleteWell";

        public const string GetBillingReports = "billings/{startDate}/{endDate}/{isDefinitive}";

        public const string GetBGSWaypoints = "bgswaypoints/{bgssummaryid}";

        public const string GetBgsWaypointsSummaries = "bgswaypoints/summary";

        public const string AddBgsWaypointsSummary = "bgswaypoints/add";

        public const string GetObservatoryStations = "observatorystations";

        public const string GetBgsObservatoryStationWithoutReadings = "observatorystations/station/{stationId}";

        public const string GetBgsObservatoryReadings = "observatorystations/{stationId}/{startTime}/{endTime}";

        public const string GetBgsFirstObservatoryReadingDate = "observatorystation/{stationId}";

        public const string GetObservatoryStationsWithState = "observatorystations/{isActive}";

        public const string UpdateObservatoryGap = "observatorystations/gap";

        public const string SwitchBgsWebsite = "observatorystations/switchbgssite";

        public const string BroadcastWellChange = "obsnotification/BroadcastWellChange/{id}";

        public const string GetFutureObservatoryReadings = "futureobeservatoryreadings/{stationId}";

        public const string DeleteObservatoryReadings = "deleteobeservatoryreadings";

        public const string GetRangingDeterminations = "/rangingdeterminations";

        public const string SubmitSurveys = "receiver/surveys";

        public const string GetAtomicTime = "time";

        public const string GetMasterTemplates = "customerreport/mastertemplates";

        public const string GetReportTemplate = "customerreport/template/{id}";

        public const string GetSubReportTemplate = "customerreport/subreporttemplates";

        public const string GenerateCustomerReportFromMaster = "customerreportmaster/{templateId}/{wellId}";

        public const string GenerateCustomerReportFromSubReport = "customerreportsubReport/{wellId}";

        public const string GetCharts = "customerreportreporttemplate/getcharts";

        public const string GetMetadata = "customerreportmetadata/{wellid}";

        public const string GetImage = "customerreportreporttemplate/image/{wellid}/{imagename}";

        public const string UploadReportContents = "customerreport/{wellId}/{type}";

        public const string AddTemplate = "customerreport/template";

        public const string DeleteTemplate = "customerreport/template/{id}";

        public const string DownloadTemplate = "customerreport/template/download/{id}";

        public const string DeleteImages = "customerreport/images/{wellid}";

        public const string GetCustomerReport = "customerreport/Report/{wellid}";

        public const string DownloadCustomerReport = "customerreport/Report/download/{wellid}";

        public const string GenerateTemplate = "customerreport/template/view/{id}";

        public const string GetImagesForTemplates = "customerreporttemplate/images/{wellId}";

        public const string RetrieveSurvey = "receiver/{insiteWellId}/{universalTime}";

        public const string ParseSurveysFromExcel = "SurveyImporter/ProcessSurvey/{wellId}";

        public const string ThirdPartySubmitSurveys = "SurveyImporter/ThirdPartySubmitSurveys/{wellId}";

        public const string GetCellColors = "SurveyImporter/CellColors";

        public const string GetASAGlobalRules = "asa/globalrules";

        public const string GetASAGlobalMandatoryRules = "asa/globalmandatoryrules";

        public const string AddASAGlobalRule = "asa/addglobalrule";

        public const string ASAGlobalRule = "asa/globalrule/{id}";

        public const string ResetASASession = "asa/resetasasession";

        public const string GetASAGlobalRulesWithId = "asa/globalrulesid";

        public const string GetASAPreConditonRules = "asa/asapreconditonrules";

        public const string AddASAGlobalPreConditionRule = "asa/addglobalruleprecondition";

        public const string ASAGlobalPreConditionRule = "asa/globalruleprecondition/{id}";

        public const string GetBgsWebsites = "observatories/websites";

        public const string BgsWebsite = "observatories/websites/{id}";

        public const string TestBgsWebsite = "observatories/website/test";

        public const string TestBgsWebsiteForClient = "observatories/website/testClient";

        public const string AddBgsWebsite = "observatories/website";

        public const string DeleteBgsWebsite = "observatories/website/{id}";

        public const string Login = "Login";

        public const string LoggedIn = "LoggedIn";

        public const string Logout = "Server/Logout";

        public const string WinLogin = "WinLogin";

        public const string Internal = "Internal";

        public const string GetUserInfo = "user/get";

        public const string RegisterUserInfo = "user/register";

        public const string GetAll3MWDUsers = "user/getall3mwdusers";

        public const string GetUsersByActiveStatus = "user/getusers/{isActive}/{wellid}";

        public const string GetWellMappedFor3MWDUser = "user/getwellmappedfor3mwduser/{userId}";

        public const string UpdateUsersStatus = "user/UpdateUsersStatus/users/{isActive}";

        public const string SaveExportImportTemplate = "exportimporttemplate/savetemplate";

        public const string GetExportImportTemplateDisplayInformation = "exportimporttemplate/gettemplatedisplayinformation/{templateType}";

        public const string DeleteExportImportTemplate = "exportimporttemplate/deletetemplate/{id}";

        public const string GetExportImportTemplate = "exportimporttemplate/gettemplate/{id}";

        public const string GetTemplateWellInformation = "exportimporttemplate/gettemplatewellinformation/{templateType}";

        public const string GetWellStatus = "ServerMonitor/GetWellStatus";

        public const string GetDriveSpaceMonitor = "ServerMonitor/GetDriveSpaceMonitor";

        public const string GetCpuHealthMonitor = "ServerMonitor/GetCpuHealthMonitor";

        public const string PingToServer = "keepalive/ping";

        public const string IsAdminUser = "adminusermanagement/isadmin";

        public const string AddAdminUser = "adminusermanagement/addadminuser";

        public const string RemoveAdminUser = "adminusermanagement/removeadminuser";

        public const string GetAllAdminUsers = "adminusermanagement/getalladminusers";

        public const string DeleteWellForcely = "wells/DeleteWell/{id}";

        public const string ExecuteQuery = "wells/ExecuteQuery";

        public const string GetHyperCubePoint = "BgsHyperCube/GetHyperCubePoint/{HypercubeID}";

        public const string GetBgsHyperCubePoint = "BgsHyperCube/GetHyperCubePoint";

        public const string GetHyperCubePoints = "BgsHyperCube/GetHyperCubePoints/{HypercubeID}";

        public const string GetHyperCubeWebSites = "BgsHyperCube/GetHyperCubeWebSites/{showActiveOnly}";

        public const string AddHyperCubeWebSite = "BgsHyperCube/AddHyperCubeWebSite";

        public const string DeleteHyperCubeWebSite = "BgsHyperCube/DeleteHyperCubeWebSite/{hyperCubeId}";

        public const string GetToolCodeFiles = "toolCodes/toolcodefile";

        public const string GetToolCodeParamsById = "toolCodes/toolcodeParam/{toolCodeId}";

        public const string AddToolCode = "toolCodes/file";

        public const string ToolCode = "toolCodes/file/{id}";

        public const string DownloadToolCode = "toolCodes/file/download/{id}";

        public const string UploadToolCodeContents = "toolCodes/{fileid}";

        public const string ServersRoot = "Servers";

        public const string BackupServer = "Servers/{id}";

        public const string ServerRoot = "Server";
    }
}
