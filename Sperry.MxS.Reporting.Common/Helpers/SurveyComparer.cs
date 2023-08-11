using Sperry.MxS.Core.Common.Models.Workbench;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sperry.MxS.Core.Common.Models.Surveys;

namespace Sperry.MxS.Core.Common.Helpers
{
    public class SurveyComparer : IComparer<RawSurvey>, IComparer<CorrectedSurvey>, IComparer<WorkBenchDataModel>, IComparer<PlanSurvey>, IComparer<Waypoint>
    {
        public int Compare(CorrectedSurvey x, CorrectedSurvey y)
        {
            if (x == y)
            {
                return 0;
            }

            IOrderedEnumerable<CorrectedSurvey> source = from a in new CorrectedSurvey[2] { x, y }
                                                         orderby a.Depth, a.DateTime
                                                         select a;
            return (source.FirstOrDefault() != x) ? 1 : (-1);
        }

        public int Compare(RawSurvey x, RawSurvey y)
        {
            if (x == y)
            {
                return 0;
            }

            IOrderedEnumerable<RawSurvey> source = from a in new RawSurvey[2] { x, y }
                                                   orderby a.Depth, a.DateTime
                                                   select a;
            return (source.FirstOrDefault() != x) ? 1 : (-1);
        }

        public int Compare(WorkBenchDataModel x, WorkBenchDataModel y)
        {
            if (x == y)
            {
                return 0;
            }

            IOrderedEnumerable<WorkBenchDataModel> source = from a in new WorkBenchDataModel[2] { x, y }
                                                            orderby a.Depth, a.DateTime
                                                            select a;
            return (source.FirstOrDefault() != x) ? 1 : (-1);
        }

        public int Compare(PlanSurvey x, PlanSurvey y)
        {
            if (x == y)
            {
                return 0;
            }

            IOrderedEnumerable<PlanSurvey> source = from a in new PlanSurvey[2] { x, y }
                                                    orderby a.Depth, a.DateTime
                                                    select a;
            return (source.FirstOrDefault() != x) ? 1 : (-1);
        }

        public int Compare(Waypoint x, Waypoint y)
        {
            if (x == y)
            {
                return 0;
            }

            IOrderedEnumerable<Waypoint> source = new Waypoint[2] { x, y }.OrderBy((Waypoint a) => a.StartDepth);
            return (source.FirstOrDefault() != x) ? 1 : (-1);
        }
    }
}
