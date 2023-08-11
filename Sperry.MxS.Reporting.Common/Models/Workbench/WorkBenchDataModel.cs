using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class WorkBenchDataModel : IMxSWorkBenchAzimuthProperties
    {
        public double? Depth { get; set; }

        public DateTime DateTime { get; set; }

        //TODO: Suhail - Need to Review this by Kiran Sir
        public double? Highside { get; set; }

        public double? Hsd 
        {
            get
            {
                return Highside;
            }
            set
            {
                Highside = value;
            } 
        }

        public int Flag { get; set; }

        public MxSMsaState? MsaState { get; set; }

        public double? AzSc { get; set; }

        public double? AzLc { get; set; }

        public double? AzLcMAv { get; set; }

        public double? AzScTrue { get; set; }

        public double? AzLcTrue { get; set; }

        public double? AzLcMavTrue { get; set; }

        public double TVDSc { get; set; }

        public double TVDLc { get; set; }

        public double TVDLcMav { get; set; }

        public double NorthingSc { get; set; }

        public double NorthingLc { get; set; }

        public double NorthingLcMav { get; set; }

        public double EastingSc { get; set; }

        public double EastingLc { get; set; }

        public double EastingLcMav { get; set; }

        public double DisplacementSc { get; set; }

        public double DisplacementLc { get; set; }

        public double DisplacementLcMav { get; set; }

        public double EastingXSc { get; set; }

        public double NorthingYSc { get; set; }

        public double EastingXLc { get; set; }

        public double NorthingYLc { get; set; }

        public double EastingXLcMav { get; set; }

        public double NorthingYLcMav { get; set; }

        public double? Decle { get; set; }

        public double? Conve { get; set; }

        public bool ShowStaticMSAState { get; set; }

        //todo: from Lijun name standard
        public bool IsMeanAllvalues { get; set; }

        public string MSAStaticState { get; set; }

        public string DataGridName { get; set; }
    }
}
