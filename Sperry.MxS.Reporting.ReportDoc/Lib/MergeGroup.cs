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
    public class MergeGroup : INotifyPropertyChanged
    {
        #region "Private Variables"
        private MergeGroup parentGroup;
        private BindingList<MergeGroup> subGroups;
        private BindingList<MergeField> fields;
        private string mergeName;
        private string displayName;
        private bool isSelected;
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

        public BindingList<MergeGroup> SubGroups
        {
            get
            {
                if (subGroups == null)
                {
                    SubGroups = new BindingList<MergeGroup>();
                }
                return subGroups;
            }
            set
            {
                if (value != subGroups)
                {
                    if (subGroups != null)
                    {
                        subGroups.ListChanged -= new ListChangedEventHandler(subGroups_ListChanged);
                    }

                    subGroups = value;
                    subGroups.ListChanged += new ListChangedEventHandler(subGroups_ListChanged);
                }
            }
        }

       
        public BindingList<MergeField> Fields
        {
            get
            {

                if (fields == null)
                {
                    Fields = new BindingList<MergeField>();
                }
                return fields;
            }
            set
            {
                if (value != fields)
                {
                    if (fields != null)
                    {
                        fields.ListChanged -= new ListChangedEventHandler(fields_ListChanged);
                    }

                    fields = value;
                    fields.ListChanged += new ListChangedEventHandler(fields_ListChanged);
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

       
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                }
            }
        }
        #endregion

      
        #region "Constructors"
        public MergeGroup()
        {
        }

        public MergeGroup(string displayname, string mergename)
        {
            displayName = displayname;
            mergeName = mergename;
        }
        #endregion
        
        #region "Public Methods"
        
        public override string ToString()
        {
            return this.DisplayName;
        }
        #endregion

        #region "Private Methods"

        private void subGroups_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                SubGroups[e.NewIndex].ParentGroup = this;
            }
        }

        private void fields_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                fields[e.NewIndex].ParentGroup = this;
            }
        }
        #endregion

    }
}
