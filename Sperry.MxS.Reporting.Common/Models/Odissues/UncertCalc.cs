using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Odisseus
{
    public class UncertCalc
    {
        #region Properties

        public static int Nsl = 5;
        public int Nst { get; set; }
        public int LegNo { get; set; }
        public int ToolType { get; set; }
        public bool IscXyGyro { get; set; }
        public virtual bool IscXyzGyro { get; set; }
        public bool IsBpGcSS { get; set; }
        public bool IsBpCtCms { get; set; }
        public bool IsBpCtDms { get; set; }
        public bool IsToolFaceIndependent { get; set; }
        public bool IsUnitMeter { get; set; }
        public double Gtot { get; set; }
        public double Btot { get; set; }
        public double North { get; set; }
        public double East { get; set; }
        public double Vertical { get; set; }
        public double Dip { get; set; }
        public double Declination { get; set; }
        public double Latitude { get; set; }
        public double SigmaLevel { get; set; }
        public double SdAB { get; set; }
        public double SdAS { get; set; }
        public double SdMB { get; set; }
        public double SdMS { get; set; }
        public double SdMX { get; set; }
        public double SdMY { get; set; }
        public double SdSAG { get; set; }
        public double SdMXY1 { get; set; }
        public double SdMXY2 { get; set; }
        public double SdAMIC { get; set; }
        public double MnAMID { get; set; }
        public double SdAMID { get; set; }
        public double SdAMIL { get; set; }
        public double SdMFIr { get; set; }
        public double SdMDIr { get; set; }
        public double SdAZr { get; set; }
        public double SdDBHr { get; set; }
        public double SdAZ { get; set; }
        public double SdDBH { get; set; }
        public double SdMFI { get; set; }
        public double SdMDI { get; set; }
        public double SdMM { get; set; }
        public double MnMM { get; set; }
        public double SdDREF { get; set; }
        public double SdDREFr { get; set; }
        public double SdDSF { get; set; }
        public double MnDST { get; set; }
        public double SdDST { get; set; }
        public double SdAccXYB { get; set; }
        public double SdAccSFE { get; set; }
        public double SdAccMis { get; set; }
        public double SdGxyrN { get; set; }
        public double SdGxyB1 { get; set; }
        public double SdGxyB2 { get; set; }
        public double SdGxyGdep1 { get; set; }
        public double SdGxyGdep2 { get; set; }
        public double SdGxyGdep3 { get; set; }
        public double SdGxyGdep4 { get; set; }
        public double SdAccZB { get; set; }
        public double SdGravB { get; set; }
        public double SdGzrN { get; set; }
        public double SdGzB { get; set; }
        public double SdGzGdep1 { get; set; }
        public double SdGzGdep2 { get; set; }
        public double SdGyroSFE { get; set; }
        public double SdGyroMis { get; set; }
        public double SdAZf { get; set; }
        public double SdGYC { get; set; }
        public double SdCNI { get; set; }
        public double SdCNA { get; set; }
        public double SdMUI { get; set; }
        public double SdMUS { get; set; }
        public double SdGDR { get; set; }
        public int Toolface { get; set; }
        public bool IsBpErrorModel { get; set; }
        public bool IsSccAz { get; set; }
        public bool IsAssignDepth { get; set; }
        public double SdDrift { get; set; }
        public double CantA { get; set; }

        #endregion 
    }
}
