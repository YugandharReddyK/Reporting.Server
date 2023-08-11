using Sperry.MxS.Core.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.RulesEngine
{
    [Serializable]
    public class RuleSet
    {
        public RuleSet()
        {
            RuleSetId = Guid.NewGuid();
        }

        public Guid RuleSetId { get; } = Guid.Empty;
 
        public List<Rule> Rules { get; set; } = new List<Rule>();

        public List<IMxSEngineAction> PassActions { get; set; } = new List<IMxSEngineAction>();

        public List<IMxSEngineAction> FailActions { get; set; } = new List<IMxSEngineAction>();

    }
}
