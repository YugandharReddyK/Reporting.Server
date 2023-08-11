using Infragistics.Controls.Charts;
using Sperry.MxS.Reporting.Common.Enums;
using Sperry.MxS.Reporting.Common.Models.Chart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Control.Lib.Chart
{
    public class ChartProperties
    {
        #region "Properties"

        public string ChartName { get; set; }

        public MxSChartType SelectedChartType { get; set; }

        public string GridDataName { get; set; }

        public string XAxisText { get; set; }


        ///// <summary>
        ///// XAxisLabel
        ///// </summary>
        //public DataColumn XAxisLabel
        //{
        //    get
        //    {
        //        return xAxisLabel;
        //    }
        //    set
        //    {
        //        xAxisLabel = value;
        //        SetXAxis();
        //        NotifyPropertyChanged("XAxisLabel");
        //    }
        //}

        public bool ShowLegendOption { get; set; }

        public string DataLegendVisibility
        {
            get
            {
                return ShowLegendOption && SelectedChartType == MxSChartType.Data ? "Visible" : "Hidden";
            }
        }

        public string YAxisTitle { get; set; }

        public string PieLegendVisibility
        {
            get
            {
                return ShowLegendOption && SelectedChartType == MxSChartType.Pie ? "Visible" : "Hidden";
            }
        }

        public string AxisGroupVisibility
        {
            get
            {
                return SelectedChartType == MxSChartType.Data ? "Visible" : "Collapsed";
            }
        }

        public string LegendVerticalAlignment { get; set; }

        public string LegendHorizontalAlignment { get; set; }

        public ObservableCollection<SeriesProperties> SeriesPropertiesList { get; set; }

        public bool YAxisIsInverted { get; set; }

        public int XAxisIntervalNumber { get; set; }

        public int XAxisAngle { get; set; }

        public int XAxisExtent { get; set; }

        public LabelsPosition PieLabelPosition { get; set; }

        public int PieLabelSize { get; set; }

        public MyKeyValuePair PieLabelTemplateName { get; set; }

        public int MarginRight { get; set; }

        public int MarginLeft { get; set; }

        public int MarginTop { get; set; }

        public int MarginBottom { get; set; }

        System.Windows.Media.Color chartBackgroundColor;

        public System.Windows.Media.Color ChartBackgroundColor
        {
            get { return chartBackgroundColor; }
            set
            {
                if (chartBackgroundColor != value)
                {
                    chartBackgroundColor = value;
                }
            }
        }

        public double? MinimumValue { get; set; }

        public double? MaximumValue { get; set; }

        public double? YaxisMin { get; set; }

        public double? YaxisMax { get; set; }

        #endregion

    }
}
