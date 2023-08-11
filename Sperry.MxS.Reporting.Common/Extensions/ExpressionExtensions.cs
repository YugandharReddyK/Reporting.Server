using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Extensions
{
    public static class ExpressionExtensions
    {
        public static bool EvaluateToBool(this Expression expression)
        {
            object result = expression.Evaluate();
            bool flag = false;
            Boolean.TryParse(result == null ? "" : result.ToString(), out flag);
            return flag;
        }
    }
}
