using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.CustomerReport;
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
    [Export(typeof(IMxSReportTemplateRepository))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MxSReportTemplateRepository : IMxSReportTemplateRepository
    {
        private readonly IMxSReportingService _reportingService;
        private const string TemplateExtension = ".docx";

        private readonly object _fileLock = new object();

        private readonly ILogger _logger;

        [ImportingConstructor]
        public MxSReportTemplateRepository(ILoggerFactory loggerFactory, IMxSReportingService reportingService)
        {
            _logger = loggerFactory.CreateLogger<MxSReportTemplateRepository>();
            _reportingService = reportingService;
            if (!SetTemplateStore())
            {
                throw new Exception("Template Store is not initialized");
            }

        }
        public bool Add(string imageToSave, string id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string imageToRemove, string storeId)
        {
            throw new NotImplementedException();
        }

        //public FileInfo Get(Guid templateId)
        //{
        //    var files = LocalStore.GetFiles();

        //    string id = templateId.ToString();
        //    if (IsExist(id))
        //    {
        //        _logger.LogCritical("reporting FileInfo IsExist");
        //        return files.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f.Name).Equals(id));
        //    }

        //    lock (_fileLock)
        //    {
        //        if (!IsExist(id))
        //        {
        //            CustomerReportTemplateData reportTemplate = _reportingService.GetReportTemplateData(templateId).Data;
        //            _logger.LogCritical($"CustomerReportTemplateData _fileLock and IsExist false");
        //            if (reportTemplate != null)
        //            {
        //                var templatePath = GetFileFullNameById(id);
        //                File.WriteAllBytes(templatePath, reportTemplate.Data);
        //            }
        //        }
        //    }
        //    return Get(templateId);
        //}

        private bool IsExist(string id)
        {
            return File.Exists(GetFileFullNameById(id));
        }

        private string GetFileFullNameById(string id)
        {

            return Path.Combine(LocalStore.FullName, $"{id}{TemplateExtension}");
        }

        public IEnumerable<FileInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Clear(Guid storeId)
        {
            throw new NotImplementedException();
        }

        public DirectoryInfo LocalStore { get; private set; }
        public bool Initialize(string storePath = null)
        {
            throw new NotImplementedException();
        }


        private bool SetTemplateStore(string storePath = null)
        {
            var isStoreInitialized = false;
            if (string.IsNullOrWhiteSpace(storePath))
            {
                var appDataPath = AppDomain.CurrentDomain.BaseDirectory;
                storePath = $@"{appDataPath}\MaxSurvey\TemplateStore\";
            }
            else
            {
                storePath = $@"{storePath}\MaxSurvey\TemplateStore\";
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
                isStoreInitialized = SetTemplateStore(tempPath);
            }
            return isStoreInitialized;
        }

        #region MyRegion

        public FileInfo Get(CustomerReportTemplateData templateData)
        {
            var files = LocalStore.GetFiles();

            string id = templateData.Id.ToString();
            if (IsExist(id))
            {
                _logger.LogCritical("reporting FileInfo IsExist");
                return files.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f.Name).Equals(id));
            }

            lock (_fileLock)
            {
                if (!IsExist(id))
                {
                    //CustomerReportTemplateData reportTemplate = _reportingService.GetReportTemplateData(templateId).Data;
                    _logger.LogCritical($"CustomerReportTemplateData _fileLock and IsExist false");
                    if (templateData != null)
                    {
                        var templatePath = GetFileFullNameById(id);
                        File.WriteAllBytes(templatePath, templateData.Data);
                    }
                }
            }
            return Get(templateData);
        }

        #endregion
    }
}
