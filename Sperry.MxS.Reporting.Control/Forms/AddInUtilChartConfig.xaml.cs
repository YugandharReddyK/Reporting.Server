using Hal.Core.XML;
using Infragistics.Controls.Charts;
using Microsoft.Extensions.Logging;
using Sperry.MxS.Reporting.Common.Enums;
using Sperry.MxS.Reporting.Control.Lib.Chart;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Sperry.MxS.Reporting.Control.Forms
{
    /// <summary>
    /// Interaction logic for AddInUtilChartConfig.xaml
    /// </summary>
    public partial class AddInUtilChartConfig : Window
    {
        #region "Private Variables"
        private System.Data.DataSet mainDataSet;
        private AddInUtilChartConfigViewModel viewModel;
        #endregion

        #region "Properties"

        public AddInUtilChartConfigViewModel ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public string XmlDefinition
        {
            get
            {
                string xmlDefinition;

                using (StringWriter writer = new StringWriter())
                {

                    XmlSerializer serializer = new XmlSerializer(viewModel.ChartProps.GetType());
                    serializer.Serialize(writer, viewModel.ChartProps);
                    xmlDefinition = writer.ToString();

                }
                return xmlDefinition;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    viewModel.ChartProps = XMLHelper.DeserializeClass<ChartProperties>(value);
                }
            }
        }

        public Grid ChartFormGrid
        {
            get { return GridMain; }
        }


        public XamDataChart ChartFormChart
        {
            get { return ChartMain; }
        }

        #endregion

        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;

        #region "Constructors"

        public AddInUtilChartConfig(System.Data.DataSet mainDataSet, string xmlDefinition, ILoggerFactory loggerFactory = null)
        {
            if (loggerFactory != null)
            {
                _logger = loggerFactory.CreateLogger<AddInUtilChartConfig>();
            }
            _loggerFactory = loggerFactory;
            _logger.LogCritical($"Yug 168.3 -- Sandy inside AddInUtilChartConfig");
            InitializeComponent();
            _logger.LogCritical($"Yug 168.4 -- Sandy inside AddInUtilChartConfig come to InitializeComponent");
            viewModel = new AddInUtilChartConfigViewModel(mainDataSet, ChartMain, ChartPie, TreeMain, loggerFactory: _loggerFactory);

            _logger.LogCritical($"Yug 168.31 -- Sandy inside AddInUtilChartConfig {viewModel}");

            this.mainDataSet = mainDataSet;

            _logger.LogCritical($"Yug 168.32 -- Sandy inside AddInUtilChartConfig {this.mainDataSet}");

            XmlDefinition = xmlDefinition;

            _logger.LogCritical($"Yug 168.33 -- Sandy inside AddInUtilChartConfig {XmlDefinition}");

            DataContext = viewModel;

            _logger.LogCritical($"Yug 168.34 -- Sandy inside AddInUtilChartConfig {DataContext}");


            ChartMain.SetBinding(DataContextProperty, new Binding("GridData"));
            //ChartPie.SetBinding(DataContextProperty, new Binding("GridData"));

            _logger.LogCritical($"Yug 168.35 -- Sandy inside AddInUtilChartConfig {ChartMain}");

            ChartPie.SetBinding(XamPieChart.ItemsSourceProperty, new Binding("GridData"));

            _logger.LogCritical($"Yug 168.36 -- Sandy inside AddInUtilChartConfig {ChartPie}");

            if (!string.IsNullOrEmpty(xmlDefinition))
            {
                _logger.LogCritical($"Yug 168.37 -- Sandy inside the if conduction !string.IsNullOrEmpty(xmlDefinition)");

                if (!string.IsNullOrEmpty(viewModel.ChartProps.GridDataName) && (viewModel.ChartProps.SelectedChartType == MxSChartType.Pie || (viewModel.ChartProps.SelectedChartType == MxSChartType.Data && !string.IsNullOrEmpty(viewModel.ChartProps.XAxisText))))
                {
                    _logger.LogCritical($"Yug 168.38 -- Sandy inside the if conduction !string.IsNullOrEmpty(xmlDefinition)");

                    viewModel.SelectedChartType = viewModel.ChartProps.SelectedChartType;

                    _logger.LogCritical($"Yug 168.39 -- Sandy {viewModel.SelectedChartType}");

                    viewModel.SelectedTable = viewModel.Tables.Tables[viewModel.ChartProps.GridDataName];
                    _logger.LogCritical($"Yug 168.40 -- Sandy {viewModel.SelectedTable}");

                    if (viewModel.GridData.Table.Columns.Contains(viewModel.ChartProps.XAxisText))
                    {
                        _logger.LogCritical($"Yug 168.41 -- Sandy inside the if conduction (viewModel.GridData.Table.Columns.Contains(viewModel.ChartProps.XAxisText)");

                        viewModel.XAxisLabel = viewModel.GridData.Table.Columns[viewModel.ChartProps.XAxisText];

                        _logger.LogCritical($"Yug 168.42 -- Sandy {viewModel.XAxisLabel}");

                    }

                    XmlDefinition = xmlDefinition;
                    _logger.LogCritical($"Yug 168.43 -- Sandy {XmlDefinition}");

                    //for (int index = viewModel.ChartProps.SeriesPropertiesList.Count-1; index >= 0; --index)
                    //{
                    //    viewModel.ChartProps.SeriesPropertiesList[index].DataChart = ChartMain;
                    //    viewModel.ChartProps.SeriesPropertiesList[index].PieChart = ChartPie;
                    //    viewModel.ChartProps.SeriesPropertiesList[index].Add();
                    //}

                    foreach (var series in viewModel.ChartProps.SeriesPropertiesList)
                    {
                        _logger.LogCritical($"Yug 168.44 -- Sandy inside the foreach loop");

                        series.DataChart = ChartMain;
                        _logger.LogCritical($"Yug 168.45 -- Sandy {series.DataChart}");

                        series.PieChart = ChartPie;
                        _logger.LogCritical($"Yug 168.46 -- Sandy {series.PieChart}");

                        series.Add();
                        _logger.LogCritical($"Yug 168.47 -- Sandy inside the foreach loop");

                    }

                    _logger.LogCritical($"Yug 168.48 -- Sandy inside the foreach loop end");

                    if (viewModel.ChartProps.SeriesPropertiesList.Count > 0)
                    {
                        _logger.LogCritical($"Yug 168.49 -- Sandy inside the if condition (viewModel.ChartProps.SeriesPropertiesList.Count > 0)");

                        viewModel.SelectedSeriesProperties = viewModel.ChartProps.SeriesPropertiesList[0];
                        _logger.LogCritical($"Yug 168.50 -- Sandy {viewModel.SelectedSeriesProperties}");

                    }
                    _logger.LogCritical($"Yug 168.51 -- Sandy outside the foreach loop");

                    viewModel.UpdateSeriesTree();
                    _logger.LogCritical($"Yug 168.54 -- Sandy call back to UpdateSeriesTree");


                }

            }
            _logger.LogCritical($"Yug 168.55 -- Sandy call");

            viewModel.ConfigureChartEvents();
            _logger.LogCritical($"Yug 168.56 -- Sandy call back to ConfigureChartEvents");

            viewModel.SetMargins();

            _logger.LogCritical($"Yug 168.60 -- Sandy call back to SetMargins");

            //viewModel.ChartProps.PropertyChanged += ChartProps_PropertyChanged;

            if (viewModel.SelectedSeriesProperties != null)
            {
                // viewModel.SelectedSeriesProperties.PropertyChanged += SelectedSeriesProperties_PropertyChanged;
            }

            if (viewModel.ChartProps.PieLabelTemplateName != null)
            {
                _logger.LogCritical($"Yug 168.61 -- Sandy inside if condition (viewModel.ChartProps.PieLabelTemplateName != null) is not null");

                RibbonPieLabelTemplateList.ItemsSource = viewModel.PieLabelTemplateList;
                _logger.LogCritical($"Yug 168.62 -- Sandy {RibbonPieLabelTemplateList.ItemsSource}");

                for (int index = 0; index < viewModel.PieLabelTemplateList.Count; index++)
                {
                    _logger.LogCritical($"Yug 168.63 -- Sandy inside for loop");

                    if (viewModel.PieLabelTemplateList[index].Key == viewModel.ChartProps.PieLabelTemplateName.Key)
                    {
                        _logger.LogCritical($"Yug 168.64 -- Sandy inside if conditions");

                        RibbonPieLabelTemplateList.SelectedIndex = index;
                        _logger.LogCritical($"Yug 168.65 -- Sandy {RibbonPieLabelTemplateList.SelectedIndex}");
                        break;
                    }
                }
            }
            _logger.LogCritical($"Yug 168.66 -- Sandy ");

            CmbTables.SelectedItemChanged += CmbTables_SelectedItemChanged;
            _logger.LogCritical($"Yug 168.67 -- Sandy {CmbTables}");

            viewModel.SetYAxis();
            _logger.LogCritical($"Yug 168.68 -- Sandy call back to SetYAxis");

        }


        void CmbTables_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            RibbonPieSeries.SelectedIndex = -1;

            DataColumnCollection columns = viewModel.GridData.Table.Columns;

            // Check DataGridMain values before binding to avoid execption.
            if (DataGridMain != null && DataGridMain.Columns.Count != 0)
            {
                for (int index = 0; index < columns.Count; index++)
                {
                    ((DataGridTextColumn)DataGridMain.Columns[index]).Binding =
                        (new Binding(string.Format("[{0}]", columns[index].ColumnName)));
                }
            }
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
        public void Dispose()
        {
            //  throw new NotImplementedException();
        }

        #endregion

        // *********************************** PRIVATE METHODS ***********************************
        #region "Private Methods"
        private void TreeMain_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string header;

            if (((HeaderedItemsControl)e.NewValue).Header != null)
            {
                header = ((HeaderedItemsControl)e.NewValue).Header.ToString();
                bool isChild = false;

                if (!(new[] { "Data Source", "Series" }.Contains(header)))
                {
                    header = ((HeaderedItemsControl)((HeaderedItemsControl)e.NewValue).Parent).Header.ToString();
                    isChild = true;
                }

                viewModel.SelectedTreeNode = header;

                if (isChild && header == "Series")
                {
                    viewModel.SelectedSeriesProperties = viewModel.ChartProps.SeriesPropertiesList.FirstOrDefault(x => x.Title == ((HeaderedItemsControl)e.NewValue).Header.ToString());
                }
            }
        }

        private void cmdAddSeries_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.AddDefaultSeries();
        }

        private void SeriesTitle_OnTextChanged(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            viewModel.UpdateSeriesTree();
        }

        private void cmdRemoveSeries_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.ChartProps.SeriesPropertiesList.Remove(viewModel.SelectedSeriesProperties);

            if (viewModel.ChartProps.SeriesPropertiesList.Count > 0)
            {
                viewModel.SelectedSeriesProperties = viewModel.ChartProps.SeriesPropertiesList[0];
            }
            else
            {
                viewModel.SelectedSeriesProperties = null;
            }
        }

        //private void ChartProps_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "PieLabelTemplateName":
        //            if (viewModel.ChartProps.PieLabelTemplateName != null)
        //            {
        //                ChartPie.LabelTemplate = this.Resources[viewModel.ChartProps.PieLabelTemplateName.Key] as DataTemplate;
        //            }
        //            break;
        //        case "ChartBackgroundColor":
        //            if (viewModel.ChartProps.ChartBackgroundColor != null)
        //            {
        //                ChartMain.PlotAreaBackground = new System.Windows.Media.SolidColorBrush(viewModel.ChartProps.ChartBackgroundColor);
        //            }
        //            break;
        //    }

        //}

        //private void SelectedSeriesProperties_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "LabelMemberPath":
        //            if (string.IsNullOrEmpty(viewModel.SelectedSeriesProperties.LabelMemberPath))
        //            {
        //                RibbonPieSeries.SelectedIndex = -1;
        //            }
        //            break;
        //    }

        //}

        #endregion

        // ********************************** PROTECTED METHODS **********************************
        #region "Protected Methods"
        #endregion

        // ********************************** INTERNAL METHODS ***********************************
        #region "Internal Methods"
        #endregion

        // ********************************** INTERNAL CLASSES ***********************************
        #region "Internal Classes"
        #endregion
    }
}
