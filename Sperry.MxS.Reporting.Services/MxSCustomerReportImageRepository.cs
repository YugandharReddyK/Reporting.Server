using Sperry.MxS.Core.Common.Models.CustomerReport;
using Sperry.MxS.Core.Common.Models.Results;
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
    [Export(typeof(IMxSCustomerReportImageRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MxSCustomerReportImageRepository : IMxSCustomerReportImageRepository
    {
        private readonly IMxSReportingService _reportingService;

        public MxSCustomerReportImageRepository()
        {

        }

        public MxSCustomerReportImageRepository(IMxSReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        public DirectoryInfo LocalStore { get; private set; }

        public string StoreId { get; private set; }
        public bool Initialize(string storeId, string storePath = null)
        {
            if (SetImageStore(storeId, storePath))
            {
                StoreId = storeId;
                GetAllImageForStore(storeId);

                return true;
            }
            return false;
        }

        public bool Add(string imageToSave, string id)
        {
            return Add(imageToSave, id, StoreId);
        }


        /// <summary>
        /// Remove from repository
        /// </summary>
        /// <param name="imageToRemove"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public bool Remove(string imageToRemove, string storeId)
        {
            bool result = false;
            SetImageStore(storeId);
            try
            {
                var fileToRemove = LocalStore.GetFiles().First(f => Path.GetFileNameWithoutExtension(f.Name).Equals(imageToRemove));
                if (File.Exists(fileToRemove.FullName))
                {
                    File.Delete(fileToRemove.FullName);
                    result = true;
                }
            }
            catch (Exception)
            {
                // log here
            }

            return result;
        }


        /// <summary>
        /// Get from repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileInfo Get(string id)
        {
            SetImageStore(Convert.ToString(StoreId));

            var files = LocalStore.GetFiles();
            return files.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f.Name).Equals(id));
        }


        public IEnumerable<FileInfo> GetAll()
        {
            SetImageStore(Convert.ToString(new Guid(StoreId)));
            return LocalStore.GetFiles();
        }

        private void GetAllImageForStore(string storeId)
        {
            ResultObject<List<CustomerImages>> images = _reportingService.GetAllCustomerImages(new Guid(StoreId));
            if (images != null && images.Data != null)
            {
                foreach (var image in images.Data)
                {
                    var fileName = image.FileName.IndexOf('.') < 0 ? image.FileName + image.ContentType : image.FileName;
                    var imagePath = Path.Combine(LocalStore.FullName, fileName);
                    File.WriteAllBytes(imagePath, image.Data);
                }
            }
        }

        public void Clear()
        {
            if (LocalStore != null)
            {
                string parent = Directory.GetParent(LocalStore.FullName).FullName;
                parent = Directory.GetParent(parent).FullName;
                DeleteDirectory(parent, true);
            }
        }

        public void Clear(Guid storeId)
        {
            var parent = Directory.GetParent(LocalStore.FullName).FullName;
            DeleteDirectory(parent, true);
        }

        /// <summary>
        /// Setup image store
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="storePath"></param>
        /// <returns></returns>
        private bool SetImageStore(String storeId, string storePath = null)
        {
            bool isStoreInitialized = false;
            if (string.IsNullOrWhiteSpace(storePath))
            {
                var appDataPath = AppDomain.CurrentDomain.BaseDirectory;
                storePath = string.Format(@"{0}\MaxSurvey\{1}\ImageStore\", appDataPath, storeId);
            }
            else
            {
                storePath = string.Format(@"{0}\MaxSurvey\ImageStore\", storePath, storeId);
            }
            try
            {
                if (!Directory.Exists(storePath))
                {
                    LocalStore = Directory.CreateDirectory(storePath);
                }
                else
                {
                    LocalStore = new DirectoryInfo(storePath);
                }
                isStoreInitialized = true;
            }
            catch (Exception)
            {
                // log here
                var tempPath = Path.GetTempPath();
                isStoreInitialized = SetImageStore(storeId, tempPath);
            }
            return isStoreInitialized;
        }

        private bool Add(string imageToSave, string id, string storeId)
        {
            bool result = false;
            SetImageStore(storeId);
            string storeImagePath = LocalStore.FullName + id + Path.GetExtension(imageToSave);
            try
            {
                if (File.Exists(storeImagePath))
                    File.Delete(storeImagePath);
                File.Copy(imageToSave, storeImagePath);
                result = true;
            }
            catch (Exception)
            {
                // log here
            }
            return result;
        }

        private bool DeleteDirectory(string folderPath, bool recursive)
        {
            if (!Directory.Exists(folderPath))
                return false;
            foreach (string file in Directory.GetFiles(folderPath))
            {
                File.Delete(file);
            }

            if (recursive)
            {
                foreach (string dir in Directory.GetDirectories(folderPath))
                {
                    DeleteDirectory(dir, recursive);
                }
            }
            Directory.Delete(folderPath);
            return true;
        }
    }
}
