using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    [Serializable]
    public class MergeField : INotifyPropertyChanged
    {
        #region "Private Variables"
        private MergeGroup parentGroup;
        private string mergeName;
        private string displayName;
        private string mergeType;
        #endregion

        #region "Events"
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region "Properties"
        [XmlIgnore]
        public MergeGroup ParentGroup
        {
            get
            {
                return parentGroup;
            }
            set
            {
                if (value != parentGroup)
                {
                    parentGroup = value;
                }
            }
        }

        public string MergeName
        {
            get { return mergeName; }
            set
            {
                if (value != mergeName)
                {
                    mergeName = value;
                }
            }
        }

        public string DisplayName
        {
            get { return displayName; }
            set
            {
                if (value != displayName)
                {
                    displayName = value;
                }
            }
        }

        
        public string MergeType
        {
            get { return mergeType; }
            set
            {
                if (value != mergeType)
                {
                    mergeType = value;
                }
            }
        }
        #endregion

       
        #region "Constructors"
       
        public MergeField()
        {
        }

        
        public MergeField(string displayname, string mergename, MergeGroup parentgroup, string mergetype)
        {
            DisplayName = displayname;
            MergeName = mergename ?? displayname;
            ParentGroup = parentgroup;
            MergeType = mergetype;
        }
        #endregion

      
        #region "Public Methods"
      
        public override string ToString()
        {
            return this.MergeName;
        }
        #endregion
    }
}
