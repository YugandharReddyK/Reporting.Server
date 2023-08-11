using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sperry.MxS.Reporting.Common.Models.Chart
{
    public class LineStyles
    {
        private SolidColorBrush seriesColorBrush;
        private double? lineWeight;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="seriesColor"></param>
        /// <param name="lineWeight"></param>
        public LineStyles(SolidColorBrush seriesColor, double? lineWeight)
        {
            this.seriesColorBrush = seriesColor;
            this.lineWeight = lineWeight;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Series GetCircleLine()
        {
            return CreateSeriesFromType(MarkerType.Circle);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Series GetTriangleLine()
        {
            return CreateSeriesFromType(MarkerType.Triangle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Series GetPyramidLine()
        {
            return CreateSeriesFromType(MarkerType.Pyramid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Series GetPentagonLine()
        {
            return CreateSeriesFromType(MarkerType.Pentagon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Series GetTetragonLine()
        {
            return CreateSeriesFromType(MarkerType.Tetragram);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Series GetDiamondLine()
        {
            return CreateSeriesFromType(MarkerType.Diamond);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Series GetHexaGramLine()
        {
            return CreateSeriesFromType(MarkerType.Hexagon);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Series GetSquareLine()
        {
            return CreateSeriesFromType(MarkerType.Square);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Series GetSimpleLine()
        {
            return CreateSeriesFromType(MarkerType.None);
        }

        private ScatterLineSeries CreateSeriesFromType(MarkerType markerType)
        {
            var series = new ScatterLineSeries();
            series.MarkerType = markerType;
            SetSeriesColor(series);
            SetSeriesThickness(series);
            return series;
        }
        private void SetSeriesColor(ScatterLineSeries series)
        {
            series.MarkerBrush = seriesColorBrush ?? seriesColorBrush;
        }

        private void SetSeriesThickness(ScatterLineSeries series)
        {
            series.Thickness = lineWeight ?? lineWeight.Value;
        }
    }
}
