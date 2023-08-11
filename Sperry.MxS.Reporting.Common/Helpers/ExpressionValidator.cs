using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class ExpressionValidator
    {
        private static string[] _exludedPatterns = new string[] { "$", "@", "#", "\"", "`", ";", "{", "}", "\\" };
        public static bool IsExpressionValid(string expression, ref string errorMessage)
        {
            if (string.IsNullOrEmpty(expression) || _exludedPatterns.Any(x => expression.Contains(x)) || !IsDecimalValid(expression) || !IsBracketCountValid(expression))
                return false;
            Expression exp = new Expression(expression);
            if (exp.HasErrors())
            {
                errorMessage = exp.Error;
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool IsBracketCountValid(string expression)
        {
            return expression.Count(c => c == '[') == expression.Count(c => c == ']');
        }

        private static bool IsDecimalValid(string expression)
        {
            if (!expression.Contains("."))
                return true;
            if (expression.StartsWith(".") || expression.EndsWith("."))
                return false;
            char[] characters = expression.ToCharArray();
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i] == '.' && (!char.IsDigit(characters[i - 1]) || !char.IsDigit(characters[i + 1])))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
