using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDataLibrary.Infrastructure
{
    public class FileStorageProvider
    {
        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
        public Stream GetReadStream(string filePath)
        {
            return new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read, 4096, true);
        }
        public Stream GetWriteStream(string filePath)
        {
            return new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
        }
    }
}
