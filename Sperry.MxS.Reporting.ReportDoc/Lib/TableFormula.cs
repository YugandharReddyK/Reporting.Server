using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    public class TableFormula : INotifyPropertyChanged
    {
        private string table;
        private string formulaName;
        private string formulaType;
        private string formula;
        private string formatString;
        private string dataType;
        private string dataTypeFormat;

       
        public string Table
        {
            get { return table; }
            set
            {
                table = value;
                NotifyPropertyChanged("Table");
            }
        }
       
        public string FormulaName
        {
            get { return formulaName; }
            set
            {
                formulaName = value.Replace(' ', '_');
                NotifyPropertyChanged("FormulaName");
            }
        }
       
        public string FormulaType
        {
            get { return formulaType; }
            set
            {
                formulaType = value;
                NotifyPropertyChanged("FormulaType");
            }
        }

      
        public string Formula
        {
            get
            {
                return formula;
            }
            set
            {
                formula = value;
                NotifyPropertyChanged("Formula");
            }
        }

        
        public string FormatString
        {
            get
            {
                return formatString;
            }
            set
            {
                formatString = value;
                NotifyPropertyChanged("FormatString");
            }
        }

       
        public string DataType
        {
            get
            {
                return dataType;
            }
            set
            {
                dataType = value;
                NotifyPropertyChanged("DataType");
            }
        }

       
        public string DataTypeFormat
        {
            get
            {
                return dataTypeFormat;
            }
            set
            {
                dataTypeFormat = value;
                NotifyPropertyChanged("DataTypeFormat");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
