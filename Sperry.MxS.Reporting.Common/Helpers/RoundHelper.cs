using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class RoundHelper
    {
        public static void CorrectAzimuth(Well well)
        {
            if (well.Runs == null)
                return;
            foreach (Run run in well.Runs)
            {
                run.ToolfaceOffset = Limit360(run.ToolfaceOffset);
            }
        }

        public static double? Limit360(double? value)
        {
            if (value.HasValue)
            {
                return Limit360(value.Value);
            }
            else
            {
                return value;
            }
        }

        public static double Limit360(double value)
        {
            const double limit = 360;
            double remind = value % limit;
            if (remind < 0)
                remind += limit;
            return remind;
        }
    }
}
