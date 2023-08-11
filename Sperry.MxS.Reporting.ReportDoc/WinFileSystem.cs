using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sperry.MxS.Reporting.ReportDoc
{
    public class WinFileSystem : FileSystemBase
    {
        #region "Enums"

        public enum FileCompareResultMessage
        {
            
            Same = 0,
           
            Different = 1,
           
            SourceMissing = 2,
           
            TargetMissing = 3,
           
            Error = 4
        }

       
        public enum CopyFile
        {
           
            SyncSourceToTarget = 0,
           
            SyncTargetToSource = 1
        }


        #endregion

        #region "Private Variables"
        private Exception lastException;
        #endregion

      
        #region "Properties"
        public Exception LastException
        {
            get { return lastException; }
            private set { lastException = value; }
        }
        #endregion

       
        #region "Public Methods"
        public static string BrowseForFile(string initialDirectory, string filter, string defaultExtension, string title)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AutoUpgradeEnabled = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Multiselect = false;
            ofd.DefaultExt = defaultExtension;
            ofd.Filter = filter;
            ofd.InitialDirectory = initialDirectory;
            ofd.Title = title;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }

            return String.Empty;
        }

      
        public string ReadAllTextNoException(string path, Encoding encoding = null)
        {
            ClearException();

            try
            {
                if (encoding == null)
                {
                    return File.ReadAllText(path);
                }
                else
                {
                    return File.ReadAllText(path, encoding);
                }
            }
            catch (Exception ex)
            {
                lastException = ex;
            }

            return null;
        }


        public static string GetExtension(string path)
        {
            return System.IO.Path.GetExtension(path);
        }

       
        public static string GetDirectoryName(string path)
        {
            return System.IO.Path.GetDirectoryName(path);
        }
      
        public static string GetFileName(string path)
        {
            return System.IO.Path.GetFileName(path);
        }

        
        public static string GetTempFileName()
        {
            return System.IO.Path.GetTempFileName();
        }

       
        public static Boolean Exists(string path)
        {
            return System.IO.File.Exists(path);
        }
       
        public static string GetTempFileName(string path)
        {
            string extension = WinFileSystem.GetExtension(path);
            string savePath = WinFileSystem.GetDirectoryName(path);

            string randomName = System.IO.Path.GetRandomFileName();
            return WinFileSystem.Copy(path, Path.Combine(savePath, randomName.Replace(WinFileSystem.GetExtension(randomName), extension)));
        }
        /// <summary>
        /// Exists - Does File Exist
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="toPath"></param>
        /// <returns></returns>
        public static string Copy(string fromPath, string toPath)
        {
            if (Exists(fromPath))
            {
                File.Copy(fromPath, toPath, true);
                return toPath;
            }
            return fromPath;

        }
        /// <summary>
        /// Deletes File
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Boolean Delete(string path)
        {
            Boolean returnValue;
            try
            {
                System.IO.File.Delete(path);
                returnValue = true;
            }
            catch
            {
                returnValue = false;
            }
            return returnValue;
        }

        /// <summary>
        /// Create File at path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileString"></param>
        /// <returns></returns>
        public static Boolean WriteAllText(string path, string fileString)
        {
            Boolean returnValue;
            try
            {
                System.IO.File.WriteAllText(path, fileString);
                returnValue = true;
            }
            catch
            {
                returnValue = false;
            }
            return returnValue;
        }

        /// <summary>
        /// Read From Path to FileStream
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileMode"></param>
        /// <returns></returns>
        public static FileStream ReadAllFileStream(string path, FileMode fileMode)
        {
            return new FileStream(path, fileMode);
        }

        /// <summary>
        /// GetMD5HashFromFile Method - Reads file and returns md5 hash.
        /// </summary>
        /// <param name="filename">The file.</param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string filename)
        {
            FileStream file = new FileStream(filename, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// GetFilesFromDirectory - Gets all files from a folder
        /// </summary>
        /// <returns></returns>
        public static List<Uri> GetFilesFromDirectory(string fileDirectory)
        {
            var returnList = new List<Uri>();
            try
            {
                if (!Directory.Exists(fileDirectory))
                    return returnList;

                foreach (var files in Directory.GetFiles(fileDirectory))
                {
                    var fileInfo = new FileInfo(files);
                    if (!fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
                        returnList.Add(new Uri(files));
                }

                foreach (var directory in Directory.GetDirectories(fileDirectory))
                {
                    returnList.AddRange(GetFilesFromDirectory(directory));
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            return returnList;

        }

        /// <summary>
        /// CopyDifferenceFromSourceToTarget Method - Sync multiple files from one location to another based on the copyfile parameter
        /// </summary>
        /// <param name="filecompareresults"></param>
        /// <param name="copyfile"></param>
        public static void CopyDifferenceFromSourceToTarget(List<FileCompareResult> filecompareresults, CopyFile copyfile)
        {
            foreach (FileCompareResult fileCompareResult in filecompareresults)
            {
                CopyDifferenceFromSourceToTarget(fileCompareResult, copyfile);
            }

        }
        /// <summary>
        /// CopyDifferenceFromSourceToTarget Method - Sync single file from one location to another based on the copyfile parameter
        /// </summary>
        /// <param name="filecompareresult"></param>
        /// <param name="copyfile"></param>
        public static void CopyDifferenceFromSourceToTarget(FileCompareResult filecompareresult, CopyFile copyfile)
        {
            if (copyfile == CopyFile.SyncSourceToTarget)
            {
                if (filecompareresult.ResultMessage == FileCompareResultMessage.TargetMissing ||
                    filecompareresult.ResultMessage == FileCompareResultMessage.Different)
                {
                    string subFolder = filecompareresult.SourceFile.Replace(filecompareresult.SourcePath, string.Empty);
                    string targetFile = filecompareresult.TargetPath + subFolder;
                    try
                    {
                        Directory.CreateDirectory(targetFile.Substring(0, targetFile.LastIndexOf(@"\")));
                        File.Copy(filecompareresult.SourceFile, targetFile, true);

                        filecompareresult.TargetFile = targetFile;
                        filecompareresult.TargetMd5Hash = filecompareresult.SourceMd5Hash;
                        filecompareresult.ErrorMessage = string.Empty;
                    }
                    catch (Exception exception)
                    {
                        filecompareresult.ErrorMessage = exception.Message;
                    }
                }
            }

            if (copyfile == CopyFile.SyncTargetToSource)
            {
                if (filecompareresult.ResultMessage == FileCompareResultMessage.SourceMissing ||
                    filecompareresult.ResultMessage == FileCompareResultMessage.Different)
                {
                    string subFolder = filecompareresult.TargetFile.Replace(filecompareresult.TargetPath, string.Empty);
                    string sourceFile = filecompareresult.SourcePath + subFolder;
                    try
                    {
                        Directory.CreateDirectory(sourceFile.Substring(0, sourceFile.LastIndexOf(@"\")));
                        File.Copy(filecompareresult.TargetFile, sourceFile, true);

                        filecompareresult.SourceFile = sourceFile;
                        filecompareresult.SourceMd5Hash = filecompareresult.TargetMd5Hash;
                        filecompareresult.ErrorMessage = string.Empty;
                    }
                    catch (Exception exception)
                    {
                        filecompareresult.ErrorMessage = exception.Message;
                    }
                }
            }
        }

        /// <summary>
        /// IsFileLocked
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsFileLocked(string filePath)
        {
            bool returnValue = false;
            FileStream stream = null;


            try
            {
                stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                returnValue = true;

            }
            catch
            {
                returnValue = false;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return returnValue;

        }


        /// <summary>
        /// SaveStreamToFile - Save IO Stream to file location
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <param name="stream"></param>
        public static void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }
        #endregion

        // *********************************** PRIVATE METHODS ***********************************
        #region "Private Methods"
        private void ClearException()
        {
            LastException = null;
        }
        #endregion

        // ********************************** PROTECTED METHODS **********************************
        #region "Protected Methods"
        #endregion

        // ********************************** INTERNAL METHODS ***********************************
        #region "Internal Methods"
        #endregion

        // ********************************** INTERNAL CLASSES ***********************************
        #region "Internal Classes"
        /// <summary>
        /// 
        /// </summary>
        public class FileCompareResult
        {
            /// <summary>
            /// Selected - Selected
            /// </summary>
            public bool Selected { get; set; }
            /// <summary>
            /// SourcePath - Source Path
            /// </summary>
            public string SourcePath { get; set; }
            /// <summary>
            /// SourceFile - Source File
            /// </summary>
            public string SourceFile { get; set; }

            /// <summary>
            /// SourceSubFolderFile - Source SubFolder File
            /// </summary>
            public string SourceSubFolderFile
            {
                get { return SourceFile == null ? null : SourceFile.Replace(SourcePath, string.Empty); }
            }

            /// <summary>
            /// SourceMd5Hash - Source MD5 Hash
            /// </summary>
            public string SourceMd5Hash { get; set; }
            /// <summary>
            /// TargetPath - Target Path
            /// </summary>
            public string TargetPath { get; set; }
            /// <summary>
            /// TargetFile - Target File
            /// </summary>
            public string TargetFile { get; set; }

            /// <summary>
            /// TargetSubFolderFile - Target SubFolder File
            /// </summary>
            public string TargetSubFolderFile
            {
                get { return TargetFile == null ? null : TargetFile.Replace(TargetPath, string.Empty); }
            }

            /// <summary>
            /// TargetMd5Hash - Target MD5 Hash
            /// </summary>
            public string TargetMd5Hash { get; set; }
            /// <summary>
            /// ErrorMessage - Error Message
            /// </summary>
            public string ErrorMessage { get; set; }

            /// <summary>
            /// ResultMessage - Result Message
            /// </summary>
            public FileCompareResultMessage ResultMessage
            {
                get
                {
                    if (!String.IsNullOrEmpty(ErrorMessage))
                    {
                        return FileCompareResultMessage.Error;
                    }
                    else if (String.IsNullOrEmpty(SourceMd5Hash))
                    {
                        return FileCompareResultMessage.SourceMissing;
                    }
                    else if (String.IsNullOrEmpty(TargetMd5Hash))
                    {
                        return FileCompareResultMessage.TargetMissing;
                    }
                    else if (SourceMd5Hash == TargetMd5Hash)
                    {
                        return FileCompareResultMessage.Same;
                    }
                    else //if (SourceMd5Hash != TargetMd5Hash)
                    {
                        return FileCompareResultMessage.Different;
                    }
                }
            }

            /// <summary>
            /// FileCompareResult - File Compare Result
            /// </summary>
            public FileCompareResult()
            {
                Selected = false;
            }

            /// <summary>
            /// FileCompareResult - File Compare Result
            /// </summary>
            /// <param name="sourcepath">Source File Path</param>
            /// <param name="targetpath">Target Path</param>
            /// <param name="fullname">Full Name</param>
            public FileCompareResult(string sourcepath, string targetpath, string fullname)
            {
                SourcePath = sourcepath;
                TargetPath = targetpath;
                TargetFile = fullname;
            }
        }

        #endregion

    }
}
