using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sperry.MxS.Core.Common.Models.Odisseus
{
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public partial class OdisseusToolCodesToolCode
    {
        // TODO Sandeep Because of Name and Term getting null

        //[XmlElementAttribute("Term")]
        //public OdisseusToolCodesToolCodeTerm[] Term { get; set; }

        //[XmlAttributeAttribute()]
        //public string Name { get; set; }

        // TODO Sandeep Added
        #region  Private Members

        private OdisseusToolCodesToolCodeTerm[] termField;
        private string nameField;

        #endregion

        #region Public Methods

        [XmlElementAttribute("Term")]
        public OdisseusToolCodesToolCodeTerm[] Term
        {
            get
            {
                return this.termField;
            }
            set
            {
                this.termField = value;
            }
        }

        [XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        #endregion

    }
}
