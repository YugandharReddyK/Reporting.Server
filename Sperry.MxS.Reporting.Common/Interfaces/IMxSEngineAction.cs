using Sperry.MxS.Core.Common.Models.RulesEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Interfaces
{
    public interface IMxSEngineAction
    {
        string Name { get; }

        string Description { get; }

        bool RunInSimulation { get; }

        bool Execute(RuleSet ruleSet, RuleSetResponse ruleSetResponse, ref List<object> objects);
    }
}
