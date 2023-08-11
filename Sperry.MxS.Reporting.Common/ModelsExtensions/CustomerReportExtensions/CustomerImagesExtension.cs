namespace Sperry.MxS.Core.Common.Models.CustomerReport
{
    public static class CustomerImagesExtension
    {
        public static void UpdateFrom(this CustomerImages customerImages, CustomerImages image)
        {
            customerImages.Data = image.Data;
            customerImages.FileName = image.FileName;
            customerImages.ContentType = image.ContentType;
            customerImages.WellId = image.WellId;
        }
    }
}
