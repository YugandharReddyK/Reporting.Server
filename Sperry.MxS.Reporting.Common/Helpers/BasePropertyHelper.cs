using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class BasePropertyHelper
    {
        public static void UpdateBaseProperties<T>(this T obj, string email, bool updateCreater = false)
        {
            if (obj != null)
            {
                if (obj is DataModelBase)
                {
                    DataModelBase data = obj as DataModelBase;

                    if (updateCreater)
                    {
                        data.CreatedTime = DateTime.UtcNow;
                        data.CreatedBy = email;
                    }
                    data.LastEditedTime = DateTime.UtcNow;
                    data.LastEditedBy = email;
                }
            }
        }
    }
}
