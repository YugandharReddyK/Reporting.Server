using Sperry.MxS.Core.Common.Constants;

namespace Sperry.MxS.Core.Common.Models.Odisseus
{
    public static class OdisseusToolCodeSectionExtension
    {
        public static void DecideBeforeSetEndDepth(this OdisseusToolCodeSection odisseusToolCodeSection, double value)
        {
            odisseusToolCodeSection.ShowEnd=value == MxSConstant.MaximumDepth;
            
            if (odisseusToolCodeSection.EndDepth != MxSConstant.MaximumDepth && odisseusToolCodeSection.EndDepth != 0)
            {
                odisseusToolCodeSection.PreviousWpEndDepth = odisseusToolCodeSection.EndDepth;
            }
        }
        public static void AddForeignKey(this OdisseusToolCodeSection odisseusToolCodeSection, Well well)
        {
            odisseusToolCodeSection.WellId = well.Id;
        }

    }
}
