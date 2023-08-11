using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.Odisseus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sperry.MxS.Core.Common.Models.Odisseus
{
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public partial class OdisseusToolCodes
    {
        [XmlElementAttribute("ToolCode")]
        public OdisseusToolCodesToolCode[] ToolCode { get; set; }
    }
}
