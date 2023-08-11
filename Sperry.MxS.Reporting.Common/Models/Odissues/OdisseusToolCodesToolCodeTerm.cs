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
    public partial class OdisseusToolCodesToolCodeTerm
    {
        // TODO Sandeep Because of Name and Value getting null

        //[XmlAttributeAttribute()]
        //public string Name { get; set; }

        //[XmlAttributeAttribute()]
        //public double Value { get; set; }

        // TODO Sandeep Added
        #region  Private Members

        private string nameField;
        private double valueField;

        #endregion

        #region Public Methods

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

        [XmlAttributeAttribute()]
        public double value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        #endregion
    }
}
