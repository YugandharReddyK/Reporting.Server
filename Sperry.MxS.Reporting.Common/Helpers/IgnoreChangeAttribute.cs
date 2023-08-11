using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Ex.Helpers
{
    public class IgnoreChangeAttribute : Attribute
    {
        /// <summary>
        /// IgnoreChangeAttribute indicates that the field/property will not be saved into database
        /// so that the field/property should be ignored when judging if the class data are changed
        /// </summary>
        public IgnoreChangeAttribute()
        {

        }

    }
}
