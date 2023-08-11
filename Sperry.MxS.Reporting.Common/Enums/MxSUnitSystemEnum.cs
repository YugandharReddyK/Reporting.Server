using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Enums
{
    [DataContract]
    public enum MxSUnitSystemEnum
    {
        [EnumMember]
        [Description("ft")]
        English,

        [EnumMember]
        [Description("m")]
        Metric
    }
}
