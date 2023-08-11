using Newtonsoft.Json;
using Sperry.MxS.Core.Common.MathematicalFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.CoordSys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    // Need to ask Kiran Sir these are service Also about Algorithm 
    public class CoordService
    {
        #region public area

        #region From CoordSys VB project

        /// <summary>
        /// Wrap CoordSys.clsMagCalc.CalculateMagneticParameters
        /// MaxSurvey 3, MaxSurveyMUI.modCalculations.GetDeclAndConv
        /// MaxSurvey 3, MaxSurveyMUI.modCalculations.CalcMagParamsFromMap
        /// </summary>
        /// <param name="para"> OldCoordSys is not required
        /// Mappings:
        ///  para.Date -> MagDate; 
        ///  para.Tvd -> MslTvd; 
        ///  para.Lat -> MapLat; 
        ///  para.Lng -> MapDep</param>
        /// <returns>Calculated MagCoordResults</returns>

        public static MagCoordResults CalculateMagneticParameters(AdvancedCoordInput para)
        {
            try
            {
                //TODO: Suhail - Related to Algorithm

                //var coordSysPath = GetCoordSysPath();
                //var moCoordSys = new clsMagCalc(coordSysPath);
                //moCoordSys.CoordSysPath = coordSysPath;//GetCoordSysPath();
                //moCoordSys.CoordSys = para.CoordSys;
                //moCoordSys.MagModel = para.MagModel;
                //moCoordSys.MagDate = para.Date;
                //moCoordSys.MslTvd = para.Tvd;
                ////moCoordSys.MapLat = para.Lat;
                ////moCoordSys.MapDep = para.Lng;
                //moCoordSys.MapLat = para.North;
                //moCoordSys.MapDep = para.East;
                //moCoordSys.GeoLat = para.Lat;
                //moCoordSys.GeoLng = para.Lng;
                //moCoordSys.CalculateMagneticParameters();
                //var mr = new MagCoordResults(ExtendHelper.Radian2Degree((double)moCoordSys.GridConvergence),
                //    ExtendHelper.Radian2Degree((double)moCoordSys.MagDip), ExtendHelper.Radian2Degree((double)moCoordSys.MagDeclination),
                //moCoordSys.MagFieldStrength);
                // return mr;
                return new MagCoordResults();
            }
            catch (Exception ex)
            {
                var s = ex.Message;
            }
            return new MagCoordResults();
        }

        /// <summary>
        /// Wrap CoordSys.clsMagCalc.CalculateCoordinateParameters
        /// MaxSurvey 3, MaxSurveyMUI.modCalculations.GetEllipsoid
        /// </summary>
        /// <param name="para">CoordSysPath and CoordSys are required</param>
        /// <returns>Coordinate Parameters</returns>


        //TODO: Suhail - Related to Algorithm

        //public static CoordResults CalculateCoordinateParameters(BasicCoordInput para)
        //{
        //    var moCoordSys = new clsMagCalc(GetCoordSysPath());
        //    moCoordSys.CoordSys = para.CoordSys;
        //    moCoordSys.CalculateCoordinateParameters();
        //    var r = GetCalcResults(moCoordSys);

        //    var prjs = _oleDbServer.ParseGridCoordSystem(para.CoordSys);
        //    int type = 0, sysid = 0, zoneid = 0, locationid = 0, utmHemi = 0;
        //    var centreMeridian = double.NaN;
        //    int.TryParse(prjs["Type"], out type);
        //    int.TryParse(prjs["System"], out sysid);
        //    int.TryParse(prjs["Location"], out locationid);
        //    int.TryParse(prjs["Zone"], out zoneid);
        //    int.TryParse(prjs["UTMHemisphere"], out utmHemi);
        //    double.TryParse(prjs["CentreMeridian"], out centreMeridian);

        //    r.Type = type;
        //    r.SystemId = sysid;
        //    r.LocationId = locationid;
        //    r.ZoneId = zoneid;
        //    r.UTMHemisphere = utmHemi;
        //    //CoordResults r = new CoordResults();
        //    //r.GridDescription = moCoordSys.GridDescription;
        //    //r.GridSource = moCoordSys.GridSource;
        //    //r.GridProjectionName = moCoordSys.GridProjectionName;
        //    //r.GridDatumName = moCoordSys.GridDatumName;
        //    //r.GridEllipsoidName = moCoordSys.GridEllipsoidName;
        //    //r.Hemisphere = moCoordSys.Hemisphere;
        //    //r.CentralMeridian = moCoordSys.CentralMeridian;
        //    //r.LatitudeOrigin = moCoordSys.LatitudeOrigin;
        //    //r.LongitudeOrigin = moCoordSys.LongitudeOrigin;
        //    //r.FalseNorthing = moCoordSys.FalseNorthing;
        //    //r.FalseEasting = moCoordSys.FalseEasting;
        //    //r.ScaleReduction = moCoordSys.ScaleReduction;
        //    //r.EquatorialRadius = moCoordSys.EquatorialRadius;
        //    //r.PolarRadius = moCoordSys. PolarRadius;
        //    //r.InverseFlattening = moCoordSys.InverseFlattening;
        //    //r.EccentricitySquared = moCoordSys.EccentricitySquared;
        //    return r;
        //}

        /// <summary>
        /// Wrap CoordSys.clsMagCalc.CalculateGeographicCoordinates
        /// Convert map coordinates to geographic coordinates
        /// </summary>
        /// <param name="para">Mappings: para.Latitude -> MapLat; para.Longitude -> MapDep </param>
        /// <returns>Calculated Geographic Coordinates</returns>

        //TODO: Suhail - Related to Algorithm

        public static Coordinate2D CalculateGeographicCoordinates(BasicCoordInput para)
        {
            //    var moCoordSys = new clsMagCalc(GetCoordSysPath());
            //    moCoordSys.CoordSysPath = GetCoordSysPath();
            //    moCoordSys.CoordSys = para.CoordSys;
            //    moCoordSys.MapLat = para.Lat;
            //    moCoordSys.MapDep = para.Lng;
            //    moCoordSys.CalculateGeographicCoordinates();
            //    var cor = new Coordinate2D(moCoordSys.GeoLng, moCoordSys.GeoLat);
            var cor = new Coordinate2D(0.00, 0.00);
            //    moCoordSys = null;
            return cor;
        }

        /// <summary>
        /// Wrap CoordSys.clsMagCalc.CalculateGeographicCoordinatesFromWgs84 
        /// </summary>
        /// <param name="para">Mappings: para.Latitude -> WgsLat; para.Longitude -> WgsLng</param>
        /// <returns>Calculated Geographic Coordinates</returns>

        //TODO: Suhail - Related to Algorithm

        //public static Coordinate2D CalculateGeographicCoordinatesFromWgs84(BasicCoordInput para)
        //{
        //    var moCoordSys = new clsMagCalc(GetCoordSysPath());
        //    moCoordSys.CoordSysPath = GetCoordSysPath();
        //    moCoordSys.CoordSys = para.CoordSys;
        //    moCoordSys.WgsLat = para.Lat;
        //    moCoordSys.WgsLng = para.Lng;
        //    moCoordSys.CalculateGeographicCoordinatesFromWgs84();
        //    var cor = new Coordinate2D(moCoordSys.GeoLng, moCoordSys.GeoLat);
        //    moCoordSys = null;
        //    return cor;
        //}

        /// <summary>
        /// Wrap CoordSys.clsMagCalc.CalculateMapCoordinates
        /// Convert geographic coordinates to map coordinates
        /// </summary>
        /// <param name="para">Mappings: para.Latitude -> GeoLat; para.Longitude -> GeoLng</param>
        /// <returns>Calculated map coordinates</returns>

        //TODO: Suhail - Related to Algorithm
        //public static Coordinate2D CalculateMapCoordinates(BasicCoordInput para)
        //{
        //    var str = GetCoordSysPath();
        //    var moCoordSys = new clsMagCalc(str);
        //    moCoordSys.CoordSysPath = GetCoordSysPath();
        //    moCoordSys.CoordSys = para.CoordSys;
        //    moCoordSys.GeoLat = para.Lat;
        //    moCoordSys.GeoLng = para.Lng;
        //    moCoordSys.CalculateMapCoordinates();
        //    var cor = new Coordinate2D(moCoordSys.MapDep, moCoordSys.MapLat);
        //    moCoordSys = null;
        //    return cor;
        //}

        //TODO: Suhail - Related to Algorithm
        //public static Coordinate2D CalculateWgsCoordinates(BasicCoordInput para)
        //{
        //    var str = GetCoordSysPath();
        //    var moCoordSys = new clsMagCalc(str);
        //    moCoordSys.CoordSysPath = GetCoordSysPath();
        //    moCoordSys.CoordSys = para.CoordSys;
        //    moCoordSys.MapLat = para.Lat;
        //    moCoordSys.MapDep = para.Lng;
        //    moCoordSys.CalculateWgsCoordinates();
        //    var cor = new Coordinate2D(moCoordSys.WgsLng, moCoordSys.WgsLat);
        //    moCoordSys = null;
        //    return cor;
        //}

        /// <summary>
        /// Wrap CoordSys.clsMagCalc.CalculateMapCoordinatesFromWgs84
        /// </summary>
        /// <param name="para">Mappings: para.Latitude -> WgsLat; para.Longitude -> WgsLng</param>
        /// <returns>Calculated map coordinates</returns>

        //TODO: Suhail - Related to Algorithm
        //public static Coordinate2D CalculateMapCoordinatesFromWgs84(BasicCoordInput para)
        //{
        //    var moCoordSys = GetBasicMagCalc(para);
        //    moCoordSys.GeoLat = para.Lat;
        //    moCoordSys.GeoLng = para.Lng;
        //    moCoordSys.CalculateMapCoordinatesFromWgs84();
        //    var cor = new Coordinate2D(moCoordSys.MapDep, moCoordSys.MapLat);
        //    moCoordSys = null;
        //    return cor;
        //}

        /// <summary>
        /// Wrap CoordSys.clsMagCalc.ConvertGeographicCoordinates
        /// Convert geographic coordinates between different coord systems
        /// </summary>
        /// <param name="para">Date and Tvd are not required</param>
        /// <returns>Converted geographic coordinates</returns>

        //TODO: Suhail - Related to Algorithm
        //public static Coordinate2D ConvertGeographicCoordinates(AdvancedCoordInput para)
        //{
        //    var moCoordSys = new clsMagCalc(GetCoordSysPath());
        //    moCoordSys.CoordSysPath = GetCoordSysPath();
        //    moCoordSys.CoordSys = para.CoordSys;
        //    moCoordSys.OldCoordSys = para.OldCoordSys;
        //    moCoordSys.GeoLat = para.Lat;
        //    moCoordSys.GeoLng = para.Lng;
        //    moCoordSys.ConvertGeographicCoordinates();
        //    var cor = new Coordinate2D(moCoordSys.GeoLng, moCoordSys.GeoLat);
        //    moCoordSys = null;
        //    return cor;
        //}

        /// <summary>
        /// Wrap CoordSys.clsMagCalc.CalculateGridConvergence
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>

        //TODO: Suhail - Related to Algorithm
        //public static double CalculateGridConvergence(BasicCoordInput para)
        //{
        //    var moCoordSys = GetBasicMagCalc(para);
        //    moCoordSys.GeoLat = para.Lat;
        //    moCoordSys.GeoLng = para.Lng;
        //    moCoordSys.CalculateMapCoordinates();
        //    moCoordSys.CalculateGridConvergence();
        //    return moCoordSys.GridConvergence;
        //}

        /// <summary>
        /// Wrap CoordSys.clsMagCalc.CalculateScaleFactor
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>

        //TODO: Suhail - Related to Algorithm
        //public static double CalculateScaleFactor(BasicCoordInput para)
        //{
        //    var moCoordSys = GetBasicMagCalc(para);
        //    moCoordSys.GeoLat = para.Lat;
        //    moCoordSys.GeoLng = para.Lng;
        //    ////TO VERIFY: These three must be invoked before CalculateScaleFactor?
        //    //moCoordSys.ConvertGeographicCoordinates();
        //    //moCoordSys.CalculateMapCoordinates();
        //    //moCoordSys.CalculateGridConvergence();
        //    moCoordSys.CalculateScaleFactor();

        //    var r = moCoordSys.GridScaleFactor;
        //    moCoordSys = null;
        //    return r;
        //}

        #endregion

        #region From MS3 MaxSurveyMUI frmCoordSys

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridId">GetGeographicProjectSystem() get the id</param>
        /// <param name="centerMeridian">User input, optional</param>
        /// <param name="zoneKey"></param>
        /// <param name="south">User input, optional</param>
        /// <returns></returns>

        // TODO: Suhail - Related to CoordSys need to implement this once CoorySys algorithm is added

        //public static string GetCoordSystemID(int gridId, int ellipsoidId, double centerMeridian, int zoneKey, bool hemisphereSouthern)
        //{
        //    //int nMapZone = 0;
        //    //int nHemisphere = 0;
        //    var sCoordSys = "Undefined";
        //    switch (gridId)
        //    {
        //        case 0:
        //            //----- undefined
        //            sCoordSys = "Undefined";
        //            break;

        //        case -1:
        //        //----- International UTM system
        //        //nHemisphere = south ? -1 : +1;
        //        //nMapZone = (int)(((centerMeridian) + 186) / 6) + (-60 * Convert.ToInt32(centerMeridian >= 180));
        //        //sCoordSys = "UTM-" + nMapZone.ToString() + "SNN ".Substring(nHemisphere + 2 - 1, 1);
        //        //break;

        //        case -2:
        //        //----- NAD83 UTM system
        //        //centerMeridian -= 360;
        //        //nMapZone = (int)(((centerMeridian) + 186) / 6) + (-60 * Convert.ToInt32(centerMeridian >= 180));
        //        //sCoordSys = "UTM83-" + nMapZone.ToString();
        //        //break;

        //        case -3:
        //            //----- NAD27 UTM system                    
        //            //centerMeridian -= 360;
        //            //nMapZone = (int)(((centerMeridian) + 186) / 6) + (-60 * Convert.ToInt32(centerMeridian >= 180));
        //            //sCoordSys = "UTM27-" + nMapZone.ToString();
        //            sCoordSys = GetUTMCoordinateSystemString(gridId, centerMeridian, hemisphereSouthern);
        //            break;

        //        case 13:
        //        case 14:
        //            //----- user defined system
        //            //dCMeridian = Val(txtEllipsoidCM.Text)
        //            sCoordSys = GetUserDefinedCoordSys(ellipsoidId, centerMeridian);
        //            break;

        //        default:
        //            var queryName = "Qry - Get Coordinate Zone ID from Zone Key";
        //            var queryPara = "[Primary Key] = " + zoneKey;
        //            var fieldName = "ID";
        //            var res = _oleDbServer.GetFieldValue(queryName, queryPara, fieldName);
        //            if (!string.IsNullOrEmpty(res))
        //            {
        //                sCoordSys = res;
        //            }
        //            break;
        //    }
        //    return sCoordSys;
        //}

        //public static string GetUserDefinedCoordSys(int ellipsoidId, double centerMeridian)
        //{
        //    var sCoordSys = "Undefined";
        //    if (ellipsoidId != 0)
        //    {
        //        var queryName = "Qry - List Ellipsoids";
        //        var queryPara = "[Ellipsoid Key] = " + ellipsoidId;
        //        var fieldName = "ID";
        //        var res = _oleDbServer.GetFieldValue(queryName, queryPara, fieldName);
        //        if (!string.IsNullOrEmpty(res))
        //        {
        //            sCoordSys = res + ":" + centerMeridian.ToString("000.000");
        //        }
        //    }
        //    return sCoordSys;
        //}

        public static string VerifyCentralMeridian(int systemid, double centralMeridian)
        {
            //-1: standard UTM, -2: UTM27, -3: UTM83
            //13: User defined northern hemisphere, 14: User defined southern hemisphere
            if (systemid < 0 || systemid == 13 || systemid == 14)
            {

                centralMeridian = CommonCalculation.Limit360(centralMeridian);

                if (systemid < 0)
                {
                    return StandardizeCentralMeridian(centralMeridian).ToString("0.000");
                }
            }
            return centralMeridian.ToString("0.000");
        }



        private static double StandardizeCentralMeridian(double centralMeridian)
        {
            return Math.Floor(centralMeridian / 6) * 6 + 3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemid"></param>
        /// <param name="centralMeridian"></param>
        /// <param name="hemisphereSouthern"></param>
        /// <returns></returns>
        private static string GetUTMCoordinateSystemString(int systemid, double centralMeridian, bool hemisphereSouthern)
        {
            var coordSys = VerifyCentralMeridian(systemid, centralMeridian);
            if (double.TryParse(coordSys, out centralMeridian))
            {
                var cm = (int)((centralMeridian + 186) / 6) + -60 * Convert.ToInt32(centralMeridian >= 180);
                if (systemid == -2)
                {
                    coordSys = "UTM83-" + cm.ToString();
                }
                else if (systemid == -3)
                {
                    coordSys = "UTM27-" + cm.ToString();
                }
                else
                {
                    coordSys = "UTM-" + cm.ToString() + (hemisphereSouthern ? "S" : "N");
                }
            }
            return coordSys;
        }

        //public static int GetEpsgCode(string sCoordSys)
        //{
        //    int nZone;
        //    var nEpsgCode = 0;
        //    string sEpsgCode;
        //    if (sCoordSys.StartsWith("UTM"))
        //    {
        //        if (sCoordSys.StartsWith("UTM83"))
        //        {
        //            nZone = Convert.ToInt32(sCoordSys.Substring(sCoordSys.LastIndexOf("-") + 1));
        //            nEpsgCode = 26900 + nZone;
        //        }
        //        else if (sCoordSys.StartsWith("UTM27"))
        //        {
        //            nZone = Convert.ToInt32(sCoordSys.Substring(sCoordSys.LastIndexOf("-") + 1));
        //            nEpsgCode = 26700 + nZone;
        //        }
        //        else //standard utm
        //        {
        //            var strTemp = sCoordSys.Substring(sCoordSys.LastIndexOf("-") + 1);
        //            if (Int32.TryParse(strTemp, out nZone) == false)
        //            {
        //                nZone = 0;
        //            }
        //            nEpsgCode = sCoordSys.EndsWith("N") ? 32600 + nZone : 32700 + nZone;
        //        }
        //    }
        //    else
        //    {
        //        var queryName = "Qry - Get EPSG Code from Zone ID";
        //        var queryPara = "[Zone ID] = \'" + sCoordSys + "\'";
        //        var fieldName = "EPSG Code";
        //        sEpsgCode = _oleDbServer.GetFieldValue(queryName, queryPara, fieldName);
        //        if (Int32.TryParse(sEpsgCode, out nEpsgCode) == false)
        //        {
        //            nEpsgCode = 0;
        //        }
        //    }
        //    return nEpsgCode;
        //}

        ///// <summary>
        ///// MS3: ConnectionInfoDocuments
        ///// </summary>
        ///// <returns></returns>
        //public static SortedDictionary<int, string> GetMagModels()
        //{
        //    string queryName = "Qry - List Magnetic Models";
        //    string queryPara = "";
        //    string keyName = "PrimaryKey";
        //    string valueName = "ID";
        //    SortedDictionary<string, string> mod = _oleDbServer.GetKeyValuePairs(queryName, queryPara, keyName, valueName);
        //    SortedDictionary<int, string> models = new SortedDictionary<int, string>();
        //    AddDictionary(mod, ref models);
        //    return models;
        //}

        ///// <summary>
        ///// MS3: clsMagModels
        ///// </summary>
        ///// <returns></returns>
        //public static SortedDictionary<string, string> GetMagneticModels()
        //{
        //    string queryName = "Qry - List Magnetic Models";
        //    string queryPara = "";
        //    string keyName = "ID";
        //    string valueName = "Description";
        //    SortedDictionary<string, string> models = _oleDbServer.GetKeyValuePairs(queryName, queryPara, keyName, valueName);
        //    return models;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        //public static List<MagneticModel> GetMagneticModels()
        //{
        //    var queryName = "Qry - List Magnetic Models";
        //    var queryPara = "";
        //    List<MagneticModel> models = null;
        //    var dt = _oleDbServer.GetDataTable(queryName, queryPara);
        //    if (dt != null)
        //    {
        //        var keyName = "PrimaryKey";
        //        var id = "ID";
        //        var valueName = "Description";
        //        models = new List<MagneticModel>();
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            int k;
        //            if (!row.IsNull(keyName) && int.TryParse(row[keyName].ToString(), out k))
        //            {
        //                models.Add(new MagneticModel(k, row[id].ToString(), Convert.ToString(row[valueName])));
        //            }
        //        }
        //    }
        //    return models;
        //}

        //public static MagneticModel GetMageticModel(string id)
        //{
        //    return _oleDbServer.GetMageticModel(id.ToUpper());
        //}

        //public static bool AddNewMagneticModel(MagneticModel model)
        //{
        //    if (!model.ID.StartsWith("ERRBGGM"))
        //    {
        //        var sSQL = "INSERT INTO [Magnetic Model] (ID, Description) ";
        //        sSQL += "VALUES (\'" + model.ID.ToUpper() + "\', \'" + model.Description + "\');";
        //        return _oleDbServer.ExecuteNonQuery(sSQL);
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        //public static bool UpdateMagneticModel(MagneticModel model)
        //{
        //    var sSQL = "UPDATE [Magnetic Model] ";
        //    sSQL += "SET [Magnetic Model].ID = \'" + model.ID.ToUpper() + "\', [Magnetic Model].Description = \'" + model.Description + "\' ";
        //    sSQL += "WHERE ((([Magnetic Model].PrimaryKey)= " + model.PrimaryKey + "));";
        //    return _oleDbServer.ExecuteNonQuery(sSQL);
        //}

        //public static bool DeleteMagneticModel(string modelID)
        //{
        //    var sSQL = "DELETE FROM [Magnetic Model] WHERE ";
        //    sSQL += "[Magnetic Model].ID = " + "\'" + modelID.ToUpper() + "\';";
        //    if (_oleDbServer.ExecuteNonQuery(sSQL))
        //        return MoveToBackup(modelID);

        //    return false;
        //}

        //public static bool ImportMagneticModel(string fileFullName, string newName, string newDescription, bool bkupExisting)
        //{
        //    if (string.IsNullOrWhiteSpace(fileFullName) || !System.IO.File.Exists(fileFullName) || !fileFullName.ToLower().EndsWith(".dat"))
        //        return false;

        //    var pos = fileFullName.LastIndexOf("\\");
        //    var fileName = fileFullName.Substring(pos + 1, fileFullName.Length - pos - 5);
        //    newName = EnsureMagModelFileName(newName);
        //    var dest = GetCoordSysPath() + (string.IsNullOrWhiteSpace(newName) ? fileName : newName) + ".dat";
        //    try
        //    {
        //        if (fileFullName != dest)
        //        {
        //            if (System.IO.File.Exists(dest) && bkupExisting)
        //            {
        //                var bkupDir = GetCoordSysPath() + "Backup\\";
        //                var bkup = bkupDir + (string.IsNullOrWhiteSpace(newName) ? fileName : newName); //+ ".dat");
        //                var bk = bkup + GetBackupSuffix();
        //                if (!System.IO.Directory.Exists(bkupDir))
        //                    System.IO.Directory.CreateDirectory(bkupDir);
        //                System.IO.File.Copy(dest, bk, true);
        //            }
        //            System.IO.File.Copy(fileFullName, dest, true);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    var m = ParseMagneticModelFile(dest, newName, newDescription);
        //    if (m == null)
        //        return false;
        //    return GetMageticModel(m.ID) != null ? UpdateMagneticModel(m) : AddNewMagneticModel(m);
        //}

        public static MagneticModel ParseMagneticModelFile(string fileFullName, string newName, string newDescription)
        {
            if (string.IsNullOrWhiteSpace(fileFullName) || !System.IO.File.Exists(fileFullName) || !fileFullName.ToLower().EndsWith(".dat"))
                return null;

            var fileName = fileFullName.Substring(fileFullName.LastIndexOf("\\") + 1);
            var ID = fileName.Substring(0, fileName.LastIndexOf(".")).ToUpper();
            if (fileName.EndsWith("_backup.dat"))
            {
                ID = fileName.Substring(0, fileName.Length - GetBackupSuffix().Length).ToUpper();
            }
            var Description = "Unknown Source";
            if (ID.Length >= 8 && ID.Substring(0, 4) == "BGGM")
            {
                var y = 0;
                int.TryParse(ID.Substring(4, 4), out y);
                Description = "British Geological Society (1945-" + (y + 2).ToString() + ")";
            }
            newName = EnsureMagModelFileName(newName);
            if (!string.IsNullOrWhiteSpace(newName))
            {
                ID = newName;
            }
            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                Description = newDescription;
            }
            return new MagneticModel(ID, Description);
        }

        /// <summary>
        /// Get geographic projecting systems
        /// MS3 MaxSurveyMUI frmCoordSys : cboSystem_Load()
        /// </summary>
        /// <returns></returns>

        //public static SortedDictionary<int, string> GetGeographicProjectSystem()
        //{
        //    var queryName = "Qry - List Coordinate Systems";
        //    var queryPara = "";
        //    var sys = _oleDbServer.GetKeyValuePairs(queryName, queryPara, "PrimaryKey", "Name");
        //    var prjSys = new SortedDictionary<int, string>();
        //    AddDictionary(sys, ref prjSys);
        //    prjSys.Add(0, "Undefined");
        //    prjSys.Add(-1, "Standard UTM");
        //    prjSys.Add(-2, "UTM-NAD83");
        //    prjSys.Add(-3, "UTM-NAD27");
        //    return prjSys;
        //}

        /// <summary>
        /// MS3: cboLocations_Load; cboEllipsoids_Load
        /// </summary>
        /// <param name="systemId">GetGeographicProjectSystem() get the id</param>
        /// <returns></returns>

        //public static SortedDictionary<int, string> GetEllipsoids(int systemId)
        //{
        //    var queryName = "Qry - List Coordinate Locations";
        //    var queryPara = "[System Key] = " + systemId.ToString();
        //    var ellis = _oleDbServer.GetKeyValuePairs(queryName, queryPara, "PrimaryKey", "Name");
        //    var aEllipsoids = new SortedDictionary<int, string>();
        //    AddDictionary(ellis, ref aEllipsoids);
        //    return aEllipsoids;
        //}

        /// <summary>
        /// cboZones_Load;
        /// </summary>
        /// <param name="ellipsoidId">GetEllipsoids get the id</param>
        /// <returns></returns>

        //public static SortedDictionary<int, string> GetZones(int ellipsoidId)
        //{
        //    var queryName = "Qry - List Coordinate Zones";
        //    var queryPara = "[Location Key] = " + ellipsoidId.ToString();
        //    var zo = _oleDbServer.GetKeyValuePairs(queryName, queryPara, "PrimaryKey", "Name");
        //    var zones = new SortedDictionary<int, string>();
        //    AddDictionary(zo, ref zones);
        //    return zones;
        //}

        /// <summary>
        /// ZoneID is the CoordSys;
        /// </summary>
        /// <param name="ellipsoidId">GetZoneID get the id</param>
        /// <returns></returns>

        //public static string GetZoneID(int zoneKey)
        //{
        //    var queryName = "Qry - Get Coordinate Zone ID from Zone Key";
        //    var queryPara = "[Primary Key] = " + zoneKey.ToString();
        //    return _oleDbServer.GetFieldValue(queryName, queryPara, "ID");
        //}

        #endregion

        #region From MS3 MaxSurveyMUI modSupport

        public static double ParseGeographicLongitude(string longitude, bool isDecimal)
        {
            return ParseGeographicLongitude(PreprocessGeographicString(longitude, isDecimal));
        }

        public static double ParseGeographicLatitude(string latitude, bool isDecimal)
        {
            return ParseGeographicLatitude(PreprocessGeographicString(latitude, isDecimal));
        }

        /// <summary>
        /// GeoLngFormat
        /// </summary>
        /// <param name="longitude"></param>
        /// <returns></returns>

        public static string ToGeoLongitudeString(double longitude)
        {
            return ToGeoLongitudeString(longitude, false);
        }

        /// <summary>
        /// GeoLatFormat
        /// </summary>
        /// <param name="latitude"></param>
        /// <returns></returns>

        public static string ToGeoLatitudeString(double latitude)
        {
            return ToGeoLatitudeString(latitude, false);
        }

        public static string ToGeoLongitudeString(double longitude, bool isInDecimal)
        {
            if (double.IsInfinity(longitude) || double.IsNaN(longitude) || longitude < -180 || longitude > 360)
                return string.Empty;
            if (longitude > 180)
            {
                longitude -= 360;
            }
            return (isInDecimal ? Math.Abs(longitude).ToString("##0.00000000") + " " : ToDegMinSec(longitude)) + (Math.Sign(longitude) < 0 ? "W" : "E");
        }

        public static string ToGeoLatitudeString(double latitude, bool isInDecimal)
        {
            if (double.IsInfinity(latitude) || double.IsNaN(latitude))
                return string.Empty;
            if (Math.Abs(latitude) > 90)
            {
                latitude = 0;
            }
            return (isInDecimal ? Math.Abs(latitude).ToString("##0.00000000") + " " : ToDegMinSec(latitude)) + (Math.Sign(latitude) < 0 ? "S" : "N");
        }

        #endregion

        #endregion

        #region private area

        #region From MS3 MaxSurveyMUI frmCoordSys

        //private static CoordSysDBServer _oleDbServer = new CoordSysDBServer(GetCoordSysPath());

        private static void AddDictionary(SortedDictionary<string, string> from, ref SortedDictionary<int, string> to)
        {
            int k;
            if (from != null)
            {
                foreach (var p in from)
                {
                    if (int.TryParse(p.Key, out k))
                    {
                        to.Add(k, p.Value);
                    }
                }
            }
        }

        //TODO: Suhail - Related to algorithm
        //private static clsMagCalc GetBasicMagCalc(BasicCoordInput para)
        //{
        //    var moCoordSys = new clsMagCalc(GetCoordSysPath());
        //    moCoordSys.CoordSysPath = GetCoordSysPath();
        //    moCoordSys.CoordSys = para.CoordSys;
        //    return moCoordSys;
        //}

        //TODO: Suhail - Related to algorithm
        //private static CoordResults GetCalcResults(clsMagCalc magCalc)
        //{
        //    var para = new CoordResults();
        //    para.MapLat = magCalc.MapLat;
        //    para.MapDep = magCalc.MapDep;
        //    para.WgsLat = magCalc.WgsLat;
        //    para.WgsLng = magCalc.WgsLng;
        //    para.GeoLat = magCalc.GeoLat;
        //    para.GeoLng = magCalc.GeoLng;
        //    para.GridSource = magCalc.GridSource;
        //    para.GridEllipsoidName = magCalc.GridEllipsoidName;
        //    para.GridDescription = magCalc.GridDescription;
        //    para.GridDatumName = magCalc.GridDatumName;
        //    para.GridProjectionName = magCalc.GridProjectionName;
        //    para.GridConvergence = magCalc.GridConvergence;
        //    para.GridScaleFactor = magCalc.GridScaleFactor;
        //    para.MagDeclination = magCalc.MagDeclination;
        //    para.MagDip = magCalc.MagDip;
        //    para.MagFieldStrength = magCalc.MagFieldStrength;
        //    para.MagHorizontalComponent = magCalc.MagHorizontalComponent;
        //    para.MagVerticalComponent = magCalc.MagVerticalComponent;
        //    para.MagNorthComponent = magCalc.MagNorthComponent;
        //    para.MagEastComponent = magCalc.MagEastComponent;
        //    para.CentralMeridian = Mod360(magCalc.CentralMeridian);
        //    para.LatitudeOrigin = magCalc.LatitudeOrigin;
        //    para.LongitudeOrigin = magCalc.LongitudeOrigin;
        //    para.FalseNorthing = magCalc.FalseNorthing;
        //    para.FalseEasting = magCalc.FalseEasting;
        //    para.ScaleReduction = magCalc.ScaleReduction;
        //    para.EquatorialRadius = magCalc.EquatorialRadius;
        //    para.PolarRadius = magCalc.PolarRadius;
        //    para.InverseFlattening = magCalc.InverseFlattening;
        //    para.EccentricitySquared = magCalc.EccentricitySquared;
        //    para.MagChecksumInsite5 = magCalc.MagChecksumInsite5;
        //    para.MagChecksumInsite6 = magCalc.MagChecksumInsite6;
        //    para.Hemisphere = magCalc.Hemisphere;
        //    return para;
        //}

        //private static DataTable GetDataTable(string tableName)
        //{
        //    return _oleDbServer.GetDataTable(tableName);
        //}

        #endregion

        #region From MS3 MaxSurveyMUI modSupport

        private static string ToDegMinSec(double value)
        {
            return ToDegMinSec(value, 4);
        }

        private static string ToDegMinSec(double value, int digits)
        {
            var s = value * 3600;
            var abs = Math.Abs(s);
            var ns = (int)Math.Round(abs, digits);
            var fr = abs - ns;
            var d = ns / 3600;
            var m = ns % 3600 / 60;
            var se = ns % 60;
            return String.Format("{0:#0}° {1:#0}\' {2:#0}{3:.0000}\" ", d, m, se, fr);
        }

        private static double ParseDegMinSec(string value)
        {
            var dValue = double.NaN;
            var sSearch = " °\'\"";    //" °\'\""; 

            //// Remove all the letters in the string
            //value = new string(value.ToCharArray().Where(c => !char.IsLetter(c)).ToArray());

            if (value.IndexOfAny(sSearch.ToCharArray()) < 0)
            {
                if (!double.TryParse(value, out dValue))
                {
                    return double.NaN;
                }
            }
            else
            {
                double d, m, s;
                var ss = value.Split(sSearch.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (ss.Length >= 3 && double.TryParse(ss[0], out d) && double.TryParse(ss[1], out m) && double.TryParse(ss[2], out s))
                {
                    dValue = d + m / 60.0 + s / 3600.0;
                }
                else
                {
                    return double.NaN;
                }
            }
            return dValue;
        }

        public static double Mod360(double dValue)
        {
            while (dValue < 0)
            {
                dValue += 360;
            }
            while (dValue >= 360)
            {
                dValue -= 360;
            }
            return dValue;
        }

        private static string RemoveIllegalInNumeralString(string numeralString)
        {
            if (string.IsNullOrWhiteSpace(numeralString))
                return numeralString;
            var illegal = new char[] { ' ' };
            return string.Join("", numeralString.Trim().Split(illegal));
        }

        private static string PreprocessGeographicString(string geographicCoordinate, bool isDecimal)
        {
            if (string.IsNullOrWhiteSpace(geographicCoordinate))
                return geographicCoordinate;
            if (isDecimal)
            {
                return RemoveIllegalInNumeralString(geographicCoordinate);
            }
            else
            {
                var nsew = "NSEW";
                var tag = "";
                geographicCoordinate = geographicCoordinate.Trim();
                if (geographicCoordinate.IndexOfAny(nsew.ToCharArray()) == geographicCoordinate.Length - 1)
                {
                    tag = geographicCoordinate.Substring(geographicCoordinate.Length - 1);
                    geographicCoordinate = geographicCoordinate.Substring(0, geographicCoordinate.Length - 1);
                }
                var sa = geographicCoordinate.Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var d = "00";
                var m = "00";
                var s = "00.0000";
                for (var i = 0; i < sa.Length; i++)
                {
                    if (i == 0)
                        d = sa[i];
                    if (i == 1)
                        m = sa[i];
                    if (i == 2)
                        s = sa[i];
                    if (i > 2)
                        s += sa[i];
                }
                geographicCoordinate = d + "°" + m + "\'" + s + "\"" + tag;
            }
            return geographicCoordinate;
        }

        /// <summary>
        /// GeoLngVal
        /// </summary>
        /// <param name="longitude"></param>
        /// <returns></returns>
        private static double ParseGeographicLongitude(string longitude)
        {
            if (string.IsNullOrWhiteSpace(longitude))
            {
                return double.NaN;
            }
            short nHemisphere = 1;
            longitude = longitude.ToUpper().Trim();
            if (longitude.Contains("E"))
            {
                nHemisphere = 1;
                longitude = longitude.Replace("E", "").Trim();
            }
            else if (longitude.Contains("W"))
            {
                nHemisphere = -1;
                longitude = longitude.Replace("W", "").Trim();
            }
            if (longitude.StartsWith("-"))
            {
                nHemisphere = -1;
                longitude = longitude.Substring(1).Trim();
            }
            var dValue = ParseDegMinSec(longitude);
            if (double.IsNaN(dValue))
                return double.NaN;
            return nHemisphere == 1 ? Math.Abs(dValue) : 360 - Math.Abs(dValue);
        }

        /// <summary>
        /// GeoLatVal
        /// </summary>
        /// <param name="latitude"></param>
        /// <returns>Double latitude value, if NaN, input error</returns>
        private static double ParseGeographicLatitude(string latitude)
        {
            if (string.IsNullOrWhiteSpace(latitude))
            {
                return double.NaN;
            }
            short nHemisphere = 1;
            latitude = latitude.ToUpper().Trim();
            if (latitude.Contains("N"))
            {
                nHemisphere = 1;
                latitude = latitude.Replace("N", "").Trim();
            }
            else if (latitude.Contains("S"))
            {
                nHemisphere = -1;
                latitude = latitude.Replace("S", "").Trim();
            }
            if (latitude.StartsWith("-"))
            {
                nHemisphere = -1;
                latitude = latitude.Substring(1).Trim();
            }
            var par = ParseDegMinSec(latitude);
            return double.IsNaN(par) ? double.NaN : nHemisphere * Math.Abs(par);
        }

        #endregion

        #region other private methods

        /// <summary>
        /// Make sure no invalid characters in magnetic file name
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string EnsureMagModelFileName(string filename)
        {
            return string.IsNullOrWhiteSpace(filename) ? filename : string.Join("_", filename.Trim().Split(System.IO.Path.GetInvalidFileNameChars())).ToUpper();
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            var dir = new System.IO.DirectoryInfo(sourceDirName);
            var dirs = dir.GetDirectories();
            if (!dir.Exists)
            {
                throw new System.IO.DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }
            if (!System.IO.Directory.Exists(destDirName))
            {
                System.IO.Directory.CreateDirectory(destDirName);
            }
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var temppath = System.IO.Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }
            if (copySubDirs)
            {
                foreach (var subdir in dirs)
                {
                    var temppath = System.IO.Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private static int GetFileCount(string directory)
        {
            if (!System.IO.Directory.Exists(directory))
            {
                return -1;
            }
            var dir = new System.IO.DirectoryInfo(directory);
            return dir.GetFiles().Length;
        }

        private static bool MoveToBackup(string modelID)
        {
            try
            {
                var bkupDir = GetCoordSysPath() + "Backup\\";
                var tobk = GetCoordSysPath() + modelID + ".dat";
                var bkup = bkupDir + modelID + GetBackupSuffix();
                if (!System.IO.Directory.Exists(bkupDir))
                    System.IO.Directory.CreateDirectory(bkupDir);
                if (System.IO.File.Exists(tobk))
                    System.IO.File.Move(tobk, bkup);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string GetBackupSuffix()
        {
            return DateTime.Now.ToString("_yyyy-MM-dd-HH-mm-ss-ffff") + "_backup.dat";
        }

        #endregion

        #endregion

        #region TODO list

        /// <summary>
        ///        
        /// </summary>
        /// <returns></returns>

        public static string GetCoordSysPath()
        {
            // [CommonAppDataFolder]Halliburton\MaxSurvey\MagUTM For Windows
            // Don't need the ability to put the MXS data in some other location than ProgramData
            var result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Halliburton\\MaxSurvey\\MagUTM For Windows\\");
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }
            return result;

            //string common = Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
            //string envPath = Environment.GetEnvironmentVariable("PATH");
            //string[] paths = envPath.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries); 
            //string coordDataPath = common + "\\Halliburton\\MaxSurvey\\MagUTM For Windows\\";
            //if (!System.IO.Directory.Exists(coordDataPath) || GetFileCount(coordDataPath) < 10)
            //{
            //    DirectoryCopy("C:\\INSITE\\Programs\\MagUTM For Windows\\", coordDataPath, true);                
            //}
            //return coordDataPath;
        }

        #endregion
    }

}

