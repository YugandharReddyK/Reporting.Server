using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    public class VariableSingleton
    {
        private static readonly Lazy<VariableSingleton> _lazyInstance = new Lazy<VariableSingleton>(() => new VariableSingleton());

       
        public static VariableSingleton Instance
        {
            get { return _lazyInstance.Value; }
        }

        private VariableSingleton()
        {
            Variables = new List<Variable>();
        }

      
        public List<Variable> Variables { get; set; }

       
        public void RefreshAllValues(bool enableLog)
        {
            foreach (Variable variable in Variables)
            {
                variable.RefreshValue(enableLog);
            }
        }

       
        public void DeleteVariable(Variable variable)
        {
            Variables.Remove(variable);
        }

       
        public void AddVariable(Variable variable)
        {
            Variables.Add(variable);
        }


        internal string ReplaceText(string expression)
        {
            string regexPattern = Regex.Escape("[Var-") + "(.*?)" + Regex.Escape("]");
            Regex regexText = new Regex(regexPattern);
            MatchCollection matches = regexText.Matches(expression);

            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                {
                    Variable foundVariable = Variables.FirstOrDefault(x => x.Name == match.Groups[1].Value);

                    if (foundVariable != null)
                    {
                        string newValue = foundVariable.Value;

                        if (foundVariable.DataType == typeof(string))
                        {
                            newValue = "'" + newValue + "'";
                        }
                        else if (foundVariable.DataType == typeof(DateTime))
                        {
                            newValue = "#" + newValue + "#";
                        }

                        expression = expression.Replace(match.Groups[0].Value, newValue);
                    }
                }
            }

            return expression;
        }
    }
}
