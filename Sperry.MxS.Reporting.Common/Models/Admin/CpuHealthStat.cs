using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Admin
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class CpuHealthStat
    {
        public TimeSpan UpTime { get; set; }

        public TimeSpan ProcessUpTime { get; set; }

        public Double TotalMemory { get; set; }
       
        public Double AvailableFreeMemory { get; set; }

        public int CpuLoadPercentage { get; set; }

        public int UpTimeInDays
        {
            get { return UpTime.Days; }
        }

        public int UpTimeInHours
        {
            get { return UpTime.Hours; }
        }

        public int UpTimeInMinutes
        {
            get { return UpTime.Minutes; }
        }
       
        public string CurrentDateTime
        {
            get { return DateTime.Now.ToShortTimeString(); }
        }

        public ResultStatus ResultStatus { get; set; }
    }
}
