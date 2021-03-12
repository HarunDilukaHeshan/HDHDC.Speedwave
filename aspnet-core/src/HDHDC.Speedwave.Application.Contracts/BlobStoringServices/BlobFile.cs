using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HDHDC.Speedwave.BlobServices
{
    public class BlobFile
    {
        private readonly byte[] _blobFile;        

        public BlobFile(string fileName, byte[] blobFile)
        {
            _blobFile = blobFile.ToArray();
            FileName = fileName;
            FileExtension = Path.GetExtension(fileName) ?? "";
            Length = blobFile.Length;
        }

        public byte[] Blob { get { return _blobFile.ToArray(); } }
        public string FileName { get; }
        public string FileExtension { get; }
        public long Length { get; }
    }
}
