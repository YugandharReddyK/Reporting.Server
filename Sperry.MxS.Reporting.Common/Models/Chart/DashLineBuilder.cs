using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sperry.MxS.Reporting.Common.Models.Chart
{
    public class DashLineBuilder
    {
        /// <summary>
        /// Set Dash Line pattern on series
        /// </summary>
        /// <param name="series"></param>
        /// <param name="dashType"></param>
        public void SetDashLine(Series series, int? dashType)
        {
            switch (dashType)
            {
                case 1:
                    series.DashArray = new DoubleCollection { 10.0, 5.0 };
                    break;
                case 2:
                    series.DashArray = new DoubleCollection { 1, 1, 1 };
                    break;
                case 3:
                    series.DashArray = new DoubleCollection { 3, 1 };
                    break;
                case 4:
                    series.DashArray = new DoubleCollection { 2, 1, 1, 1 };
                    break;
                case 5:
                    series.DashArray = new DoubleCollection { 2, 1, 1, 1, 1, 1 };
                    break;
            }

        }

    }
}
