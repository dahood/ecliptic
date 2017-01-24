using System;
using System.IO;
using System.Linq;

namespace EclipticLib.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static FileInfo LocateFile(this string filePath, string filePattern)
        {
            if (!Directory.Exists(filePath))
                throw new ArgumentException("directory: " + filePath + " does not exist");
            return new DirectoryInfo(filePath).LocateFile(filePattern);
        }

        public static FileInfo LocateFile(this DirectoryInfo dir, string filePattern)
        {
            if (!dir.Exists)
                throw new DirectoryNotFoundException(dir.FullName + " does not exist");
            
            var fileInfo = dir.EnumerateFiles(filePattern).FirstOrDefault();
            if (fileInfo != null)
                return fileInfo;
            
            var parentDir = dir.SafeGetParent();
            if (parentDir == null) 
                throw new ArgumentException(filePattern + " was not found.");
            
            return LocateFile(parentDir, filePattern);
        }

        public static FileInfo LocateFile(this FileInfo file, string filePattern)
        {
            if (!file.Exists)
                throw new DirectoryNotFoundException(file.FullName + " does not exist");
            return file.Directory.LocateFile(filePattern);
        }

        public static void AssertFilePathExists(this string filePath, string category)
        {
            if (File.Exists(filePath)) return;
            throw new FileNotFoundException(category + " file: '" + filePath + "' does not exist.");
        }

        public static DirectoryInfo SafeGetParent(this DirectoryInfo dir)
        {
            try
            {
                return dir.Parent;
            }
            catch (Exception e)
            {
                return null; //no parent
            }
        }
    }
}
