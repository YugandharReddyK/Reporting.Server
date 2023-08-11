using Sperry.MxS.Core.Common.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Interfaces
{
    public interface IMxSTemplatePreviewVerification
    {
        PreviewCellModel ValidateCellData(object cellValue, string columnName);
        
        bool ClearData();
        
        string GetOutRangeBackgroundColor();
        
        string GetOutRangeForegroundColor();

        string GetChangedValueForegroundColor();
        
        string GetChangedValueBackgroundColor();

        string GetChangedValueToolTip();
    }
}
