using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Interfaces.Templates
{
    public interface IMxSExportImportTemplateServerListManager
    {
        ExportImportTemplateInformation PublishTemplate<T>(T template, string username, ref string errorMessage) where T : TemplateConfigurationBase;
        
        List<TemplateDisplayInformation> GetTemplatesDisplayInformation(MxSTemplateType templateType);
        
        bool DeleteTemplate(Guid id, ref string errorMessage);
        
        bool IsValidTemplateName(string templateName, ref string errorMessage);
        
        string GetTemplateExtention(MxSTemplateType templateType);
        
        bool IsTemplateNameExists(string templateName, MxSTemplateType templateType);
        
        T GetTemplateConfiguration<T>(TemplateDisplayInformation templateInformation, MxSTemplateType templateType) where T : TemplateConfigurationBase;
        
        string GetTemplateName(Guid id, MxSTemplateType templateType);
        
        List<TemplateWellInformation> GetTemplateWellInformation(MxSTemplateType templateType);
    }
}
