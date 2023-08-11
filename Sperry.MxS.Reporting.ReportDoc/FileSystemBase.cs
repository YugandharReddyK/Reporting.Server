using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc
{
    public class FileSystemBase
    {
       
        #region "Public Methods"
        public virtual bool Open()
        {
            return false;
        }

       
        public virtual bool Close()
        {
            return false;
        }

       
        public virtual long Seek()
        {
            return 0;
        }

       
        public virtual bool EOF()
        {
            return true;
        }

       
        public virtual long FileLen()
        {
            return 0;
        }

        
        public virtual string Read()
        {
            return String.Empty;
        }

        
        public virtual bool Write()
        {
            return false;
        }

        
        public virtual bool Lock()
        {
            return false;
        }

       
        public virtual bool UnLock()
        {
            return false;
        }

        
        public virtual bool Delete()
        {
            return false;
        }

       
        public virtual bool Move()
        {
            return false;
        }

        
        public virtual bool Copy()
        {
            return false;
        }

        
        public virtual bool Rename()
        {
            return false;
        }

        public virtual bool SetAttrib()
        {
            return false;
        }

        
        public virtual FileAttributes GetAttrib()
        {
            return FileAttributes.Normal;
        }

       
        public virtual FileInfo GetFirst()
        {
            return new FileInfo(@"C:\whatever.txt");
        }

        
        public virtual FileInfo GetNext()
        {
            return new FileInfo(@"C:\whatever.txt");
        }

       
        public virtual DateTime CreationTime()
        {
            return DateTime.Now;
        }

        
        public virtual DateTime LastAccessTime()
        {
            return DateTime.Now;
        }

      
        public virtual DateTime LastWriteTime()
        {
            return DateTime.Now;
        }

       
        public virtual DateTime DirCreationTime()
        {
            return DateTime.Now;
        }

        
        public virtual DateTime DirLastAccessTime()
        {
            return DateTime.Now;
        }

        
        public virtual DateTime DirLastWriteTime()
        {
            return DateTime.Now;
        }

        
        public virtual string UpOneLevel()
        {
            return "";
        }

       
        public virtual string MakePath()
        {
            return "";
        }

       
        public virtual DriveInfo GetFirstDrive()
        {
            return new DriveInfo("C:");
        }

        public virtual DriveInfo GetNextDrive()
        {
            return new DriveInfo("C:");
        }

       
        public virtual DirectoryInfo GetFirstDirectory()
        {
            return new DirectoryInfo(@"C:\");
        }

       
        public virtual DirectoryInfo GetNextDirectory()
        {
            return new DirectoryInfo(@"C:\");
        }

       
        public virtual string GetFileName()
        {
            return "";
        }

        
        public virtual string GetPath()
        {
            return "";
        }

       
        public virtual string GetDriveFromPath()
        {
            return "";
        }

        
        public virtual bool MakeDirectory()
        {
            return false;
        }

       
        public virtual bool RemoveDirectory()
        {
            return false;
        }

       
        public virtual bool ChangeDirectory()
        {
            return false;
        }

        
        public virtual string CurrentDirectory()
        {
            return "";
        }

        
        public virtual string GetUNCPath()
        {
            return "";
        }

        public virtual bool ChangeDrive()
        {
            return false;
        }

       
        public virtual string CurrentDrive()
        {
            return "";
        }

       
        public virtual bool MapDrive()
        {
            return false;
        }

        public virtual bool UnMapDrive()
        {
            return false;
        }
        #endregion

    }
}
