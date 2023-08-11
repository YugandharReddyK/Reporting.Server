using Sperry.MxS.Reporting.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Services
{
    [Export(typeof(IMxSCustomerReportImageProvider))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MxSCustomerReportImageProvider : IMxSCustomerReportImageProvider
    {
        readonly IMxSCustomerReportImageRepository _imageRepository;

        [ImportingConstructor]
        public MxSCustomerReportImageProvider(IMxSCustomerReportImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }

        public IEnumerable<FileInfo> GetAll(Guid storeId)
        {
            _imageRepository.Initialize(Convert.ToString(storeId));
            return _imageRepository.GetAll();
        }

        public void ResetImageCacheForWell(Guid wellId)
        {
            _imageRepository.Clear(wellId);
        }
    }
}
