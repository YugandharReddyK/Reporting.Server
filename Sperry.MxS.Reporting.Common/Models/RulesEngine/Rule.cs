using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.RulesEngine
{
    [Serializable]
    public class Rule 
    {
        public Rule()
        {
            RuleId = Guid.NewGuid();
        }

        public Rule(Rule rule)
        {
            RuleId = Guid.NewGuid();
            LHS = rule.LHS;
            RHS = rule.RHS;
            LogicalCondition = rule.LogicalCondition;
            Operator = rule.Operator;
            RuleName = rule.RuleName;
            PreConditionRule = rule.PreConditionRule;
            IsEnabled = rule.IsEnabled;
        }

        public string LHS { get; set; } = string.Empty;

        public string RHS { get; set; } = string.Empty;

        public MxSLogicalConditionEnum LogicalCondition { get; set; } = MxSLogicalConditionEnum.And;

        public MxSOperatorsEnum Operator { get; set; } = MxSOperatorsEnum.Equals;

        public string RuleName { get; set; } = string.Empty;

        public Rule PreConditionRule { get; set; } = null;

        public Guid RuleId { get; } = Guid.Empty;

        public bool IsEnabled { get; set; } = true;

    }
}
