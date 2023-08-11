using Sperry.MxS.Core.Common.Enums;

namespace Sperry.MxS.Core.Common.Models
{
    public static class WayPointExtensions
    {
        //Rick: moved external domain logic back into domain object.
        public static void CalculateIFRValues(this Waypoint waypoint)
        {
            if (waypoint.Type == MxSWaypointType.IFR1)
            {
                waypoint.IFRDip = waypoint.BGGMDip + waypoint.CADipOffset;
                waypoint.IFRDeclination = waypoint.BGGMDeclination + waypoint.CADecOffset;
                waypoint.IFRBTotal = waypoint.BGGMBTotal + waypoint.CABtOffset;
            }
        }

        public static void CalculateTotalCorrection(this Waypoint waypoint, MxSNorthReference nortReference)
        {
            waypoint.BGGMTotalCorrection = CalculateTotalCorrection(waypoint, waypoint.BGGMDeclination, waypoint.GridConvergence, nortReference);
            waypoint.IFRTotalCorrection = CalculateTotalCorrection(waypoint, waypoint.IFRDeclination, waypoint.GridConvergence, nortReference);
        }

        public static double CalculateTotalCorrection(this Waypoint waypoint, double declination, double gridConvergence, MxSNorthReference referenceTo)
        {
            double totalCorrection = declination;
            switch (referenceTo)
            {
                case MxSNorthReference.Magnetic:
                    totalCorrection = 0;
                    break;
                case MxSNorthReference.Grid:
                    totalCorrection = (declination - gridConvergence);
                    break;
                case MxSNorthReference.True:
                    totalCorrection = declination;
                    break;
                default:
                    break;
            }
            return totalCorrection;
        }
        public static void AddForeignKey(this Waypoint waypoint,Well well)
        {
           waypoint.WellId = well.Id;
        }
    }
}
