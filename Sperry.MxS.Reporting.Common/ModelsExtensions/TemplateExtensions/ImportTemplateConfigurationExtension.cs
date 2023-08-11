using System.Linq;

namespace Sperry.MxS.Core.Common.Models.Template
{
    public static class ImportTemplateConfigurationExtension
    {
        public static void SyncConfigurationUnitSystem(this ImportTemplateConfiguration importTemplateConfiguration)
        {
            if (importTemplateConfiguration.MappingConfigurations != null)
                importTemplateConfiguration.MappingConfigurations.ToList().ForEach(item => item.CurrentUnitSystem = importTemplateConfiguration.UnitSystem);
        }

        public static ImportTemplateConfiguration Create()
        {
            return new ImportTemplateConfiguration { IsActive = true };
        }
    }
}
