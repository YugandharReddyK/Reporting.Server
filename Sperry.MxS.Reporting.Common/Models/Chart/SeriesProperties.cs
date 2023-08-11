using Infragistics.Controls.Charts;
using Sperry.MxS.Reporting.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Sperry.MxS.Reporting.Common.Models.Chart
{
    public class SeriesProperties
    {

        #region "Private Variables"
        //private Dictionary<SeriesType, Series> Series;
        private MxSSeriesType selectedSeriesType;
        private string valueMemberPath;
        private string labelMemberPath;
        private string title;
        private string seriesName;
        private bool isDash;

        #endregion


        #region "Properties"

        public string Title { get; set; }


        public string ValueMemberPath
        {
            get { return valueMemberPath; }
            set
            {
                if (valueMemberPath != value)
                {
                    valueMemberPath = value;
                    //NotifyPropertyChanged("ValueMemberPath");

                    SeriesSetup();
                }
            }
        }


        public string LabelMemberPath
        {
            get { return labelMemberPath; }
            set
            {
                if (labelMemberPath != value)
                {
                    labelMemberPath = value;
                    // NotifyPropertyChanged("LabelMemberPath");

                    SeriesSetup();
                }
            }
        }


        public string SeriesName { get; set; }

        /*
        /// <summary>
        /// 
        /// </summary>
        //public CategoryDateTimeXAxis CategoryDateTimeXAxis { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public CategoryXAxis CategoryXAxis { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public NumericXAxis NumericXAxis { get; set; }

        /// <summary>
        /// 
        /// </summary>
       // public CategoryYAxis CategoryYAxis { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public NumericYAxis NumericYAxis { get; set; }
        */

        /// <summary>
        /// 
        /// </summary>
        public MxSSeriesType SelectedSeriesType
        {
            get { return selectedSeriesType; }
            set
            {
                if (selectedSeriesType != value)
                {
                    selectedSeriesType = value;
                    SeriesSetup();
                }
            }
        }

        Color? selectedSeriesColor = null;

        public Color? SelectedSeriesColor
        {
            get { return selectedSeriesColor; }
            set
            {
                if (selectedSeriesColor != value)
                {
                    selectedSeriesColor = value;
                    SeriesSetup();
                }
            }
        }


        public bool IsDash
        {
            get { return isDash; }
            set
            {
                isDash = value;
                //NotifyPropertyChanged("IsDash");
                SeriesSetup();
            }
        }

        int? dashType = null;

        public int? DashType
        {
            get { return dashType; }
            set
            {
                dashType = value;
                //NotifyPropertyChanged("DashType");
                SeriesSetup();
            }
        }


        [XmlIgnore()]
        public XamDataChart DataChart { get; set; }

        /// <summary>
        /// PieChart
        /// </summary>
        [XmlIgnore()]
        public XamPieChart PieChart { get; set; }


        private double? lelectedLineWeight = 1;
        /// <summary>
        /// 
        /// </summary>
        public double? SelectedLineWeight
        {
            get { return lelectedLineWeight; }
            set
            {
                lelectedLineWeight = value;
                //NotifyPropertyChanged("SelectedLineWeight");
                SeriesSetup();
            }
        }

        #endregion

        // ************************************ CONSTRUCTORS *************************************
        #region "Constructors"

        /// <summary>
        /// 
        /// </summary>
        public SeriesProperties()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="pieChart"></param>
        /// <param name="seriesName"></param>
        public SeriesProperties(XamDataChart chart, XamPieChart pieChart, string seriesName)
        {
            DataChart = chart;
            PieChart = pieChart;
            SeriesName = seriesName;
        }

        #endregion

        // ************************************ DESTRUCTORS **************************************
        #region "Destructors"
        #endregion

        // *********************************** PUBLIC METHODS ************************************
        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        public void Add()
        {
            SeriesSetup();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Remove()
        {
            if (DataChart != null)
            {
                DataChart.Series.Remove(DataChart.Series[SeriesName]);
            }
        }


        #endregion

        // *********************************** PRIVATE METHODS ***********************************

        #region "Private Methods"

        private void SeriesSetup()
        {
            SolidColorBrush seriesColorBrush = null;
            if (SelectedSeriesColor.HasValue)
                seriesColorBrush = new SolidColorBrush(SelectedSeriesColor.Value);

            var lineStyle = new Lazy<LineStyles>(() => new LineStyles(seriesColorBrush, SelectedLineWeight));
            var dashLineBuilder = new Lazy<DashLineBuilder>();
            try
            {
                if (DataChart == null)
                {
                    return;
                }

                Remove();

                Series chartObject = null;

                switch (SelectedSeriesType)
                {
                    //case SeriesType.Area:
                    //    chartObject = new AreaSeries();
                    //    break;
                    //case SeriesType.Column:
                    //    chartObject = new ColumnSeries();
                    //    break;
                    //case SeriesType.Line:
                    //    chartObject = new LineSeries();
                    //    break;
                    //case SeriesType.Point:
                    //    chartObject = new PointSeries();
                    //    break;
                    //case SeriesType.RangeArea:
                    //    chartObject = new RangeAreaSeries();
                    //    break;
                    //case SeriesType.Spline:
                    //    chartObject = new SplineSeries();
                    //    break;
                    //case SeriesType.SplineArea:
                    //    chartObject = new SplineAreaSeries();
                    //    break;
                    case MxSSeriesType.LineCircle:
                        chartObject = lineStyle.Value.GetCircleLine();
                        break;
                    case MxSSeriesType.LineDiamond:
                        chartObject = lineStyle.Value.GetDiamondLine();
                        break;
                    case MxSSeriesType.LineTriangle:
                        chartObject = lineStyle.Value.GetTriangleLine();
                        break;
                    case MxSSeriesType.LineHexagram:
                        chartObject = lineStyle.Value.GetHexaGramLine();
                        break;
                    case MxSSeriesType.LineSquare:
                        chartObject = lineStyle.Value.GetSquareLine();
                        break;
                    case MxSSeriesType.SimpleLine:
                        chartObject = lineStyle.Value.GetSimpleLine();
                        break;
                    case MxSSeriesType.LinePyramid:
                        chartObject = lineStyle.Value.GetPyramidLine();
                        break;
                    case MxSSeriesType.LinePentagon:
                        chartObject = lineStyle.Value.GetPentagonLine();
                        break;
                    case MxSSeriesType.LineTetragon:
                        chartObject = lineStyle.Value.GetTetragonLine();
                        break;
                        //case SeriesType.StackedArea:
                        //    chartObject = new StackedAreaSeries();
                        //    break;
                        //case SeriesType.StackedColumn:
                        //    chartObject = new StackedColumnSeries();
                        //    break;
                        //case SeriesType.StackedLine:
                        //    chartObject = new StackedLineSeries();
                        //    break;
                        //case SeriesType.StackedSpline:
                        //    chartObject = new StackedSplineSeries();
                        //    break;
                        //case SeriesType.StackedSplineArea:
                        //    chartObject = new StackedSplineAreaSeries();
                        //    break;
                        //case SeriesType.Stacked100Area:
                        //    chartObject = new Stacked100AreaSeries();
                        //    break;
                        //case SeriesType.Stacked100Column:
                        //    chartObject = new Stacked100ColumnSeries();
                        //    break;
                        //case SeriesType.Stacked100Line:
                        //    chartObject = new Stacked100LineSeries();
                        //    break;
                        //case SeriesType.Stacked100Spline:
                        //    chartObject = new Stacked100SplineSeries();
                        //    break;
                        //case SeriesType.Stacked100SplineArea:
                        //    chartObject = new Stacked100SplineAreaSeries();
                        //    break;
                        //case SeriesType.StepArea:
                        //    chartObject = new StepAreaSeries();
                        //    break;
                        //case SeriesType.StepLine:
                        //    chartObject = new StepLineSeries();
                        //    break;
                        //case SeriesType.Waterfall:
                        //    chartObject = new WaterfallSeries();
                        //    break;
                        //case SeriesType.FinancialPrice:
                        //    chartObject = new FinancialPriceSeries();
                        //    break;
                }
                chartObject.Brush = seriesColorBrush ?? seriesColorBrush;
                chartObject.Thickness = SelectedLineWeight ?? SelectedLineWeight.Value;


                if (chartObject is ScatterLineSeries)
                {
                    var series = (chartObject as ScatterLineSeries);
                    series.XAxis = (NumericXAxis)DataChart.Axes.FirstOrDefault(x => x.Name == "defaultXAxis" && x.GetType().Name == typeof(NumericXAxis).Name);
                    series.Name = SeriesName;
                    series.Title = Title;
                    //series.ItemsSource = DataChart.DataContext;
                    var binding = new Binding();
                    binding.Source = DataChart.DataContext;

                    series.SetBinding(Series.ItemsSourceProperty, binding);
                    series.YMemberPath = ValueMemberPath;
                    series.XMemberPath = LabelMemberPath;

                    series.YAxis = DataChart.Axes.FirstOrDefault(x => x.Name == "defaultYAxis" && x.GetType().Name == typeof(NumericYAxis).Name) as NumericYAxis;
                    if (IsDash)
                    {
                        dashLineBuilder.Value.SetDashLine(series, DashType);
                    }

                    series.SetBinding(Series.LegendProperty, new Binding() { ElementName = "DefaultLegend" });

                    DataChart.Series.Insert(0, series);
                }
                else if ((chartObject as HorizontalAnchoredCategorySeries) != null)
                {
                    var series = (HorizontalAnchoredCategorySeries)chartObject;

                    // Infragistics.Controls.HierarchicalDataTemplate dt = new Infragistics.Controls.HierarchicalDataTemplate();

                    //series.MarkerStyle= new Style{};


                    series.Name = SeriesName;
                    series.Title = Title;
                    series.SetBinding(Series.ItemsSourceProperty, new Binding());
                    series.ValueMemberPath = ValueMemberPath;

                    if (DataChart.Axes.FirstOrDefault(x => x.GetType().Name == typeof(CategoryXAxis).Name) as CategoryXAxis != null)
                    {
                        series.XAxis = DataChart.Axes.FirstOrDefault(x => x.GetType().Name == typeof(CategoryXAxis).Name) as CategoryXAxis;
                    }
                    else if (DataChart.Axes.FirstOrDefault(x => x.GetType().Name == typeof(CategoryDateTimeXAxis).Name) as CategoryDateTimeXAxis != null)
                    {
                        series.XAxis = DataChart.Axes.FirstOrDefault(x => x.GetType().Name == typeof(CategoryDateTimeXAxis).Name) as CategoryDateTimeXAxis;
                    }

                    series.YAxis = DataChart.Axes.FirstOrDefault(x => x.GetType().Name == typeof(NumericYAxis).Name) as NumericYAxis;

                    if (IsDash)
                    {
                        dashLineBuilder.Value.SetDashLine(series, DashType);
                    }

                    series.SetBinding(Series.LegendProperty, new Binding() { ElementName = "DefaultLegend" });

                    // series.TrendLineZIndex = DataChart.Series.Count + 2000;
                    //series.Index = -1;// DataChart.Series.Count == 0 ? -1 : 13;
                    DataChart.Series.Insert(0, series);
                    //DataChart.Series.Insert(DataChart.Series.Count, series);

                }
                else if ((chartObject as HorizontalStackedSeriesBase) != null)
                {
                    //var series = (HorizontalStackedSeriesBase)chartObject;
                    //series.Name = SeriesName;
                    //series.Title = Title;
                    //series.SetBinding(Series.ItemsSourceProperty, new Binding());
                    //series.ValueMemberPath = ValueMemberPath;
                    //series.XAxis = TheChart.Axes.FirstOrDefault(x => x.GetType().Name == typeof(CategoryXAxis).Name) as CategoryXAxis;
                    //series.YAxis = TheChart.Axes.FirstOrDefault(x => x.GetType().Name == typeof(NumericYAxis).Name) as NumericYAxis;

                    //series.SetBinding(Series.LegendProperty, new Binding() { ElementName = "DefaultLegend" });

                    //TheChart.Series.Insert(0, series);
                }
                else if (SelectedSeriesType == MxSSeriesType.Pie)
                {
                    PieChart.LabelMemberPath = LabelMemberPath;
                    PieChart.ValueMemberPath = ValueMemberPath;
                    //PieChart.SetBinding(XamPieChart.LegendProperty, new Binding() { ElementName = "DefaultLegend" });
                }
            }
            catch (Exception)
            {
                //continue
            }
        }
        #endregion
    }
}
