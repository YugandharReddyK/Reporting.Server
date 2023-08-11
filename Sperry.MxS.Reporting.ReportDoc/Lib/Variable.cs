using Sperry.MxS.Reporting.Infrastructure.ReportingDatabase.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    public class Variable : INotifyPropertyChanged
    {
        private string _name;
        private string _sqlStatement;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

      
        public string SqlStatement
        {
            get { return _sqlStatement; }
            set
            {
                _sqlStatement = value;
                RaisePropertyChanged("SqlStatement");
            }
        }

        
        [XmlIgnore()]
        public RunState State { get; set; }

        [XmlIgnore()]
        public string Value { get; set; }

        
        [XmlIgnore()]
        public Type DataType { get; set; }

     
        [XmlIgnore()]
        public string Error { get; set; }

        
        public void RefreshValue(bool enableLog)
        {
            State = RunState.Running;

            Value = string.Empty;
            Error = string.Empty;

            string error;

            DataTable datatable = DataAccessSingleton.Instance.GetData(SqlStatement, out error, enableLog);

            if (string.IsNullOrEmpty(error))
            {
                if (datatable != null && datatable.Columns.Count > 0 && datatable.Rows.Count > 0)
                {
                    var value = datatable.Rows[0][0];

                    if (value != null)
                    {
                        Value = value.ToString();
                        DataType = datatable.Columns[0].DataType;
                    }
                }

                State = RunState.Completed;
            }
            else
            {
                State = RunState.Error;
                Error = error;
            }
        }


       
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        
        public enum RunState
        {
           
            Stale = 0,
           
            Running = 1,
           
            Completed = 2,
           
            Error = 3
        }
    }
}
