using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;
using Sperry.MxS.Reporting.Common.Models.Chart;
using Infragistics.Controls.Charts;
using Sperry.MxS.Reporting.Common.Enums;
using Sperry.MxS.Reporting.Control.Lib;
using Sperry.MxS.Reporting.Control.Lib.Chart;
using Microsoft.Extensions.Logging;

namespace Sperry.MxS.Reporting.Control.Forms
{
    public class AddInUtilChartConfigViewModel : INotifyPropertyChanged
    {
        // ************************************* STRUCTURES **************************************
        #region "Structures"
        #endregion

        // *************************************** ENUMS *****************************************
        #region "Enums"
        #endregion

        // ************************************ DLL IMPORTS **************************************
        #region "DLL Imports"
        #endregion

        // ********************************** PRIVATE VARIABLES **********************************
        #region "Private Variables"
        private DataTable selectedTable;
        private System.Data.DataSet tables;
        private DataView gridData;
        private string selectedTreeNode;
        private SeriesProperties selectedSeriesProperties;
        private XamDataChart chart;
        private XamPieChart pieChart;
        private TreeView tree;

        private TreeViewItem treeViewItemSeries;

        private DataColumn xAxisLabel;
        private MxSChartType selectedChartType;
        private ObservableCollection<MyKeyValuePair> pieLabelTemplateList;

        #endregion

        // ********************************* PROTECTED VARIABLES *********************************
        #region "Protected Variables"
        #endregion

        // ************************************** EVENTS *****************************************
        #region "Events"
        /// <summary>
        /// PropertyChanged - Property Change Event Handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        // ************************************* DELEGATES ***************************************
        #region "Delegates"
        #endregion

        // ************************************* PROPERTIES **************************************
        #region "Properties"
        /// <summary>
        /// MergeGroups - IList of MergeGroup
        /// </summary>
        public DataSet Tables
        {
            get { return tables; }
            set { tables = value; }
        }

        /// <summary>
        /// SelectedMergeGroup - Merge Group
        /// </summary>
        public DataTable SelectedTable
        {
            get { return selectedTable; }
            set
            {
                if (selectedTable != value)
                {
                    selectedTable = value;

                    if (selectedTable != null)
                    {
                        ResetGridData();
                    }

                    NotifyPropertyChanged("SelectedTable");
                }
            }
        }

        /// <summary>
        /// GridData
        /// </summary>
        public DataView GridData
        {
            get { return gridData; }
            set
            {
                gridData = value;
                NotifyPropertyChanged("GridData");
                NotifyPropertyChanged("GridColumns");
                NotifyPropertyChanged("GridNumberColumns");

                if (ChartProps.SelectedChartType != MxSChartType.Pie)
                {
                    for (int index = ChartProps.SeriesPropertiesList.Count - 1; index >= 0; --index)
                    {
                        ChartProps.SeriesPropertiesList.RemoveAt(index);
                    }
                }

                XAxisLabel = null;
                if (SelectedSeriesProperties != null)
                {
                    SelectedSeriesProperties.LabelMemberPath = "";
                    SelectedSeriesProperties.ValueMemberPath = "";
                }
            }
        }


        /// <summary>
        /// GridNumberColumns
        /// </summary>
        public ObservableCollection<string> GridColumns
        {
            get
            {
                var columns = new ObservableCollection<string>();
                if (GridData != null)
                {
                    foreach (DataColumn col in GridData.Table.Columns)
                    {
                        columns.Add(col.ColumnName);
                    }
                }
                return columns;
            }
        }


        /// <summary>
        /// GridNumberColumns
        /// </summary>
        public ObservableCollection<string> GridNumberColumns
        {
            get
            {
                var columns = new ObservableCollection<string>();
                if (GridData != null)
                {
                    foreach (DataColumn col in GridData.Table.Columns)
                    {
                        //Does not work with XSDs
                        //if (col.DataType == typeof(decimal) || col.DataType == typeof(int))
                        {
                            columns.Add(col.ColumnName);
                        }
                    }
                }
                return columns;
            }
        }

        /// <summary>
        /// SelectedTreeNode
        /// </summary>
        public string SelectedTreeNode
        {
            get { return selectedTreeNode; }
            set
            {
                selectedTreeNode = value;
                ResetRibbon();
            }
        }

        /// <summary>
        /// SelectedSeriesProperties
        /// </summary>
        public SeriesProperties SelectedSeriesProperties
        {
            get { return selectedSeriesProperties; }
            set
            {
                selectedSeriesProperties = value;
                NotifyPropertyChanged("SelectedSeriesProperties");
            }
        }

        /// <summary>
        /// Chart
        /// </summary>
        public XamDataChart Chart
        {
            get { return chart; }
            set
            {
                chart = value;
                NotifyPropertyChanged("Chart");
            }
        }

        /// <summary>
        /// Pie Chart
        /// </summary>
        public XamPieChart PieChart
        {
            get { return pieChart; }
            set
            {
                pieChart = value;
                NotifyPropertyChanged("PieChart");
            }
        }

        /// <summary>
        /// Tree
        /// </summary>
        public TreeView Tree
        {
            get { return tree; }
            set { tree = value; }
        }

        /// <summary>
        /// TreeViewItemSeries
        /// </summary>
        public TreeViewItem TreeViewItemSeries
        {
            get { return treeViewItemSeries; }
            set { treeViewItemSeries = value; }
        }

        /// <summary>
        /// SeriesTypes
        /// </summary>
        public IEnumerable<MxSSeriesType> SeriesTypes
        {
            get
            {
                return Enum.GetValues(typeof(MxSSeriesType)).Cast<MxSSeriesType>().Where(x => x != MxSSeriesType.Pie);
            }
        }

        /// <summary>
        /// LegendVerticalAlignmentList
        /// </summary>
        public List<string> LegendVerticalAlignmentList { get; set; }

        /// <summary>
        /// LegendHorizontalAlignment
        /// </summary>
        public List<string> LegendHorizontalAlignmentList { get; set; }

        /// <summary>
        /// ChartTypes
        /// </summary>
        public IEnumerable<MxSChartType> ChartTypes
        {
            get
            {
                return Enum.GetValues(typeof(MxSChartType)).Cast<MxSChartType>();
            }
        }

        /// <summary>
        /// PieLabelPosition
        /// </summary>
        public IEnumerable<LabelsPosition> PieLabelPositions
        {
            get
            {
                return Enum.GetValues(typeof(LabelsPosition)).Cast<LabelsPosition>();
            }
        }

        /// <summary>
        /// PieLabelSize
        /// </summary>
        public IEnumerable<int> PieLabelSizes
        {
            get
            {
                return GetPieLabelSize();
            }
        }

        /// <summary>
        /// SelectedChartType
        /// </summary>
        public MxSChartType SelectedChartType
        {
            get { return selectedChartType; }
            set
            {
                if (selectedChartType != value)
                {
                    selectedChartType = value;

                    ChartProps.SelectedChartType = selectedChartType;

                    PieChart.Visibility = selectedChartType == MxSChartType.Pie ? Visibility.Visible : Visibility.Hidden;
                    Chart.Visibility = selectedChartType == MxSChartType.Data ? Visibility.Visible : Visibility.Hidden;

                    ResetChartAndSeriesProperties();

                    ResetChartType();

                    NotifyPropertyChanged("SelectedChartType");
                    NotifyPropertyChanged("ShowSeriesTab");
                    NotifyPropertyChanged("ShowPieTab");
                }
            }
        }

        /// <summary>
        /// ChartProperties
        /// </summary>
        public ChartProperties ChartProps { get; set; }

        System.Windows.Media.Color chartBackgroundColor;
        /// <summary>
        /// For Series Color
        /// </summary>
        public System.Windows.Media.Color ChartBackgroundColor
        {
            get { return chartBackgroundColor; }
            set
            {
                if (chartBackgroundColor != value)
                {
                    chartBackgroundColor = value;
                    ChartProps.ChartBackgroundColor = value;
                    NotifyPropertyChanged("ChartBackgroundColor");
                }
            }
        }

        /////////////////////////////////////////

        /// <summary>
        /// XAxisLabel
        /// </summary>
        public DataColumn XAxisLabel
        {
            get
            {
                if (xAxisLabel != null)
                {
                    ChartProps.XAxisText = xAxisLabel.ColumnName;
                }
                return xAxisLabel;
            }
            set
            {
                xAxisLabel = value;
                SetXAxis();
                NotifyPropertyChanged("XAxisLabel");
            }
        }


        private void SetXAxisLabelTextWithUnit()
        {
            if (XAxisLabel != null && !string.IsNullOrWhiteSpace(XAxisLabel.ColumnName))
            {
                XAxisLabelWithUnit = GetUnit(XAxisLabel.ColumnName);
            }
        }

        private string xAxisLabelText;

        /// <summary>
        /// 
        /// </summary>
        public string XAxisLabelWithUnit
        {
            get { return xAxisLabelText; }
            set
            {
                xAxisLabelText = value;
                NotifyPropertyChanged("XAxisLabelWithUnit");
            }
        }
        /// <summary>
        /// ShowSeriesTab
        /// </summary>
        public string ShowSeriesGroup
        {
            get
            {
                return ChartProps.SeriesPropertiesList.Count == 0 ? "Hidden" : "Visible";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ShowSeriesTab
        {
            get
            {
                return selectedChartType != MxSChartType.Data ? "Collapsed" : "Visible";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ShowPieTab
        {
            get
            {
                return selectedChartType != MxSChartType.Pie ? "Collapsed" : "Visible";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<MyKeyValuePair> PieLabelTemplateList
        {
            get { return pieLabelTemplateList; }
            set
            {
                pieLabelTemplateList = value;
                NotifyPropertyChanged("PieLabelTemplateName");
            }
        }
        #endregion

        // ************************************ CONSTRUCTORS *************************************
        #region "Constructors"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainDataSet"></param>
        /// <param name="viewChart"></param>
        /// <param name="viewPieChart"></param>
        /// <param name="viewTree"></param>
        /// 
        private readonly ILogger _logger;

        public AddInUtilChartConfigViewModel(System.Data.DataSet mainDataSet, XamDataChart viewChart, XamPieChart viewPieChart, TreeView viewTree, ILoggerFactory loggerFactory = null)
        {
            if (loggerFactory != null)
            {
                _logger = loggerFactory.CreateLogger<AddInUtilChartConfigViewModel>();
            }
            //_logger.LogCritical($"Yug 168.5 -- Sandy inside AddInUtilChartConfigViewModel");  


            LegendVerticalAlignmentList = new List<string> { "Bottom", "Center", "Top" };

            //_logger.LogCritical($"Yug 168.6 -- Sandy inside AddInUtilChartConfig {LegendVerticalAlignmentList}");

            LegendHorizontalAlignmentList = new List<string> { "Center", "Left", "Right" };
            //_logger.LogCritical($"Yug 168.7 -- Sandy inside AddInUtilChartConfig {LegendHorizontalAlignmentList}");


            PieLabelTemplateList = new ObservableCollection<MyKeyValuePair>
            {
                new MyKeyValuePair() {Key = "LabelItem", Value = "Label"},
                new MyKeyValuePair() {Key = "LabelPercent", Value = "Percent"},
                new MyKeyValuePair(){Key = "LabelItemAndPercent", Value = "Label & Percent"}
            };

            //_logger.LogCritical($"Yug 168.8 -- Sandy inside AddInUtilChartConfig {PieLabelTemplateList}");

            ChartProps = new ChartProperties();

            //_logger.LogCritical($"Yug 168.9 -- Sandy inside AddInUtilChartConfig {ChartProps}");

            ChartProps.SeriesPropertiesList = new ObservableCollection<SeriesProperties>();
            //ChartProps.SeriesPropertiesList.CollectionChanged += SeriesPropertiesList_CollectionChanged;

           // _logger.LogCritical($"Yug 168.10 -- Sandy inside AddInUtilChartConfig {ChartProps.SeriesPropertiesList}");


            Chart = viewChart;
            PieChart = viewPieChart;
            Tree = viewTree;


            foreach (TreeViewItem treeItem in Tree.Items)
            {
                //_logger.LogCritical($"Yug 168.11 -- Sandy inside the foreach loop of TreeViewItem {Tree.Items}");

                if (treeItem.Header.ToString() == "Series")
                {
                   // _logger.LogCritical($"Yug 168.12 -- Sandy inside the if condition of treeItem.Header.ToString() == \"Series\"");

                    treeViewItemSeries = treeItem;
                   // _logger.LogCritical($"Yug 168.13 -- Sandy inside the foreach loop of TreeViewItem {treeViewItemSeries}");
                    //_logger.LogCritical($"Yug 168.14 -- Sandy inside the foreach loop of TreeViewItem break the loop");

                    break;
                }
            }

            tables = mainDataSet;
            //_logger.LogCritical($"Yug 168.15 -- Sandy inside the foreach loop of TreeViewItem {tables}");

            SetYAxis();

            //_logger.LogCritical($"Yug 168.26 -- Sandy Comeback to SetAxis()");


            ConfigureChartEvents();
            //_logger.LogCritical($"Yug 168.30 -- SandyConfigureChartEvents()");

        }


        #endregion

        // ************************************ DESTRUCTORS **************************************
        #region "Destructors"
        #endregion

        // *********************************** PUBLIC METHODS ************************************
        #region "Public Methods"
        /// <summary>
        /// UpdateSeriesTree
        /// </summary>
        public void UpdateSeriesTree()
        {
            treeViewItemSeries.Items.Clear();
            foreach (var seriesList in ChartProps.SeriesPropertiesList)
            {
                _logger.LogCritical($"Yug 168.52 -- Sandy inside the foreach loop");

                treeViewItemSeries.Items.Add(new TreeViewItem() { Header = seriesList.Title });
            }
            _logger.LogCritical($"Yug 168.53 -- Sandy outside the foreach loop");

        }

        private string GetUnit(string fieldName)
        {
            string fieldWithUnit = string.Empty;
            if (!string.IsNullOrWhiteSpace(fieldName))
            {
                var res = from row in Tables.Tables["Meta_Data"].AsEnumerable()
                          where row.Field<string>("Table") == SelectedTable.TableName &&
                           row.Field<string>("Field") == fieldName
                          select row["Unit_Label"];

                if (res != null && res.FirstOrDefault() is string && !string.IsNullOrWhiteSpace(res.FirstOrDefault() as string))
                    fieldWithUnit = fieldName + " (" + res.FirstOrDefault() as string + ")";
                else
                    fieldWithUnit = fieldName;
            }
            return fieldWithUnit;
        }

        /// <summary>
        /// ConfigureChartEvents
        /// </summary>
        public void ConfigureChartEvents()
        {
            //_logger.LogCritical($"Yug 168.27 -- Sandy ConfigureChartEvents is started");

            ChartProps.SeriesPropertiesList.CollectionChanged += SeriesPropertiesList_CollectionChanged;

            //_logger.LogCritical($"Yug 168.28 -- Sandy {ChartProps.SeriesPropertiesList}");

            Chart.Axes.CollectionChanged += Axes_CollectionChanged;
            //ChartProps.PropertyChanged += ChartProps_PropertyChanged;
            //_logger.LogCritical($"Yug 168.29 -- Sandy {Chart.Axes}");

        }

        /// <summary>
        /// 
        /// </summary>
        public void SetMargins()
        {
            _logger.LogCritical($"Yug 168.57 -- Sandy inside the SetMargins");

            Chart.Margin = new Thickness(ChartProps.MarginLeft, ChartProps.MarginTop, ChartProps.MarginRight, ChartProps.MarginBottom);
            _logger.LogCritical($"Yug 168.58 -- Sandy {Chart.Margin}");

            pieChart.Margin = new Thickness(ChartProps.MarginLeft, ChartProps.MarginTop, ChartProps.MarginRight, ChartProps.MarginBottom);
            _logger.LogCritical($"Yug 168.59 -- Sandy {Chart.Margin}");

        }
        #endregion

            // *********************************** PRIVATE METHODS ***********************************
            #region "Private Methods"

            /// <summary>
            /// To get pie chart label test size
            /// </summary>
            /// <returns></returns>
        private IEnumerable<int> GetPieLabelSize()
        {
            List<int> data = new List<int>();
            for (int i = 5; i < 30; i++)
            {
                data.Add(i);
            }
            return (IEnumerable<int>)data;
        }

        private void Axes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ResetSeries();
        }
        private void SetXAxis()
        {
            Chart.Axes.Remove(Chart.Axes.FirstOrDefault(x => x.GetType().Name == typeof(CategoryXAxis).Name || x.GetType().Name == typeof(CategoryDateTimeXAxis).Name || x.GetType().Name == typeof(NumericXAxis).Name));

            if (XAxisLabel != null)
            {

                if (XAxisLabel.DataType.Name == typeof(DateTime).Name)
                {
                    var xAxis = new CategoryDateTimeXAxis()
                    {
                        Name = "defaultXAxis"
                    };

                    xAxis.SetBinding(CategoryDateTimeXAxis.ItemsSourceProperty, new Binding());
                    xAxis.Label = "{" + XAxisLabel.ColumnName + "}";
                    xAxis.DateTimeMemberPath = XAxisLabel.ColumnName;

                    //<Settings>
                    xAxis.LabelSettings = new AxisLabelSettings();
                    xAxis.LabelSettings.Angle = ChartProps.XAxisAngle;
                    xAxis.LabelSettings.Extent = ChartProps.XAxisExtent;
                    //</Settings>

                    Chart.Axes.Add(xAxis);
                }
                else if (XAxisLabel.DataType.Name == typeof(Decimal).Name)
                {
                    var xAxis = new NumericXAxis()
                    {
                        Name = "defaultXAxis"
                    };

                    xAxis.SetBinding(NumericXAxis.DataContextProperty, new Binding());
                    //xAxis.Label = "{" + XAxisLabel.ColumnName + "}";
                    xAxis.Label = "{0}";
                    //<Settings>
                    xAxis.Interval = ChartProps.XAxisIntervalNumber;
                    xAxis.LabelSettings = new AxisLabelSettings();
                    xAxis.LabelSettings.Angle = ChartProps.XAxisAngle;
                    xAxis.LabelSettings.Extent = ChartProps.XAxisExtent;
                    //<Settings>

                    if (ChartProps.MinimumValue.HasValue)
                        xAxis.MinimumValue = ChartProps.MinimumValue.Value;

                    if (ChartProps.MaximumValue.HasValue)
                        xAxis.MaximumValue = ChartProps.MaximumValue.Value;

                    Chart.Axes.Add(xAxis);
                }
                else
                {
                    var xAxis = new CategoryXAxis()
                    {
                        Name = "defaultXAxis"
                    };

                    xAxis.SetBinding(CategoryXAxis.ItemsSourceProperty, new Binding());
                    xAxis.Label = "{" + XAxisLabel.ColumnName + "}";

                    //<Settings>
                    xAxis.Interval = ChartProps.XAxisIntervalNumber;
                    xAxis.LabelSettings = new AxisLabelSettings();
                    xAxis.LabelSettings.Angle = ChartProps.XAxisAngle;
                    xAxis.LabelSettings.Extent = ChartProps.XAxisExtent;
                    //</Settings>

                    Chart.Axes.Add(xAxis);
                }
            }
            SetXAxisLabelTextWithUnit();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetYAxis()
        {
            //_logger.LogCritical($"Yug 168.16 -- Sandy inside the SetYAxis start");

            Chart.Axes.Remove(Chart.Axes.FirstOrDefault(x => x.GetType().Name == typeof(NumericYAxis).Name));

            //_logger.LogCritical($"Yug 168.17 -- Sandy remove the chart");


            var yAxis = new NumericYAxis() { Name = "defaultYAxis" };

            //_logger.LogCritical($"Yug 168.18 -- Sandy {yAxis}");

            yAxis.IsInverted = ChartProps.YAxisIsInverted;
            _logger.LogCritical($"Yug 168.19 -- Sandy {yAxis.IsInverted}");

            if (ChartProps.YAxisIsInverted)
            {
                //_logger.LogCritical($"Yug 168.20 -- Sandy inside the if condition ChartProps.YAxisIsInverted");

                yAxis.MinimumValue = yAxis.MinimumValue - (yAxis.MinimumValue * .2);
                //_logger.LogCritical($"Yug 168.21 -- Sandy {yAxis.MinimumValue}");

            }
            else
            {
                _logger.LogCritical($"Yug 168.22 -- Sandy inside theelse block started");

                yAxis.MinimumValue = ChartProps.YaxisMin.HasValue ? ChartProps.YaxisMin.Value : yAxis.MinimumValue;

                _logger.LogCritical($"Yug 168.23 -- Sandy {yAxis.MinimumValue}");

                yAxis.MaximumValue = ChartProps.YaxisMax.HasValue ? ChartProps.YaxisMax.Value : yAxis.MaximumValue;

                _logger.LogCritical($"Yug 168.24 -- Sandy {yAxis.MinimumValue}");

            }

            Chart.Axes.Add(yAxis);
            _logger.LogCritical($"Yug 168.25 -- Sandy Chart is added");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SeriesPropertiesList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {

            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                ((SeriesProperties)e.OldItems[0]).Remove();
            }

            NotifyPropertyChanged("ShowSeriesGroup");
            UpdateSeriesTree();

        }



        private void ResetChartType()
        {


            //TODO: Hide/Show X-Axis Group
            //TODO: Hide/Show Series Tab
            //TODO: Hide/Show Pie Tab
            if (SelectedChartType == MxSChartType.Data)
            {
                int seriesCount = Chart.Series.Count;

                for (int index = seriesCount - 1; index >= 0; --index)
                {
                    Chart.Series.RemoveAt(index);
                }

                ChartProps.SeriesPropertiesList.Clear();
            }
            else if (SelectedChartType == MxSChartType.Pie)
            {
                int seriesCount = Chart.Series.Count;

                for (int index = seriesCount - 1; index >= 0; --index)
                {
                    Chart.Series.RemoveAt(index);
                }

                ChartProps.SeriesPropertiesList.Clear();

                ChartProps.XAxisText = string.Empty;

                SeriesProperties series = new SeriesProperties(Chart, PieChart, string.Empty)
                {
                    SelectedSeriesType = MxSSeriesType.Pie
                };

                ChartProps.SeriesPropertiesList.Add(series);

                SelectedSeriesProperties = ChartProps.SeriesPropertiesList[0];

            }
        }


        private void ResetGridData()
        {
            if (Tables.Tables[selectedTable.TableName] != null)
            {
                ChartProps.GridDataName = selectedTable.TableName;

                GridData = Tables.Tables[selectedTable.TableName].AsDataView();
            }
            else
            {
                GridData = null;
            }

            ResetChartAndSeriesProperties();
        }

        private void ResetRibbon()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetSeries()
        {
            Chart.Series.Clear();

            foreach (var series in ChartProps.SeriesPropertiesList)
            {
                series.Add();
            }
        }

        private void ChartProps_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "YAxisIsInverted":
                case "YaxisMin":
                case "YaxisMax":
                    SetYAxis();
                    //SetYAxixTitleWithUnit();
                    break;
                case "XAxisIntervalNumber":
                case "XAxisAngle":
                case "XAxisExtent":
                case "MinimumValue":
                case "MaximumValue":
                    SetXAxis();
                    break;
                case "PieLabelPosition":
                    pieChart.LabelsPosition = ChartProps.PieLabelPosition;
                    break;
                case "PieLabelSize":
                    pieChart.FontSize = ChartProps.PieLabelSize;
                    break;
                case "MarginBottom":
                case "MarginRight":
                case "MarginLeft":
                case "MarginTop":
                    SetMargins();
                    break;
                case "ChartBackgroundColor":
                    Chart.PlotAreaBackground = new SolidColorBrush(ChartProps.ChartBackgroundColor);
                    break;
            }
        }

        private void ResetChartAndSeriesProperties()
        {
            XAxisLabel = null;
            if (SelectedSeriesProperties != null)
            {
                SelectedSeriesProperties.LabelMemberPath = string.Empty;
                SelectedSeriesProperties.ValueMemberPath = string.Empty;
            }
        }

        /// <summary>
        /// PropertyChanged- Property Changed Event Handler
        /// </summary>        
        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion

        // ********************************** PROTECTED METHODS **********************************
        #region "Protected Methods"
        #endregion

        // ********************************** INTERNAL METHODS ***********************************
        #region "Internal Methods"
        /// <summary>
        /// 
        /// </summary>
        internal void AddDefaultSeries()
        {
            SeriesProperties newSeries = new SeriesProperties(Chart, PieChart, "SN" + Guid.NewGuid().ToString().Substring(0, 10).Replace("-", ""));

            int uniqueTitleCounter = 1;
            string title = "New Series_";

            while (ChartProps.SeriesPropertiesList.Any(x => x.Title == title + uniqueTitleCounter.ToString()))
            {
                ++uniqueTitleCounter;
            }

            newSeries.Title = title + uniqueTitleCounter.ToString();
            newSeries.SelectedSeriesType =MxSSeriesType.SimpleLine;
            if (xAxisLabel != null && xAxisLabel.ColumnName != null)
                newSeries.LabelMemberPath = xAxisLabel.ColumnName;

            SelectedSeriesProperties = newSeries;
            ChartProps.SeriesPropertiesList.Add(newSeries);
        }
        #endregion

        // ********************************** INTERNAL CLASSES ***********************************
        #region "Internal Classes"
        #endregion
    }
}
