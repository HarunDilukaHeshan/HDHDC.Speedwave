using HDHDC.Speedwave.BlobServices;
using Polly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.BlobStoringServices
{
    public class SpeedwaveFileSystemBlobProvider : FileSystemBlobProvider, ISpeedwaveBlobProvider
    {
        public SpeedwaveFileSystemBlobProvider(IBlobFilePathCalculator filePathCalculator)
            : base(filePathCalculator)
        { }

        public async Task<IEnumerable<byte[]>> GetListAsync(BlobProviderGetListArgs args)
        {
            var blobList = await GetListWithFileInfoAsync(args);
            var bytesList = new List<byte[]>();

            foreach (var bl in blobList)
                bytesList.Add(bl.Blob);

            return bytesList;
        }

        public async Task<IEnumerable<BlobFile>> GetListWithFileInfoAsync(BlobProviderGetListArgs args)
        {
            var searchPattern = args.SearchPattern;
            var skipCount = args.SkipCount;
            var maxResultCount = args.MaxResultCount;

            if (string.IsNullOrEmpty(searchPattern) || string.IsNullOrWhiteSpace(searchPattern))
                throw new ArgumentException();

            var path = Directory.GetParent(FilePathCalculator.Calculate(args)).FullName;

            if (!Directory.Exists(path)) return new List<BlobFile>();

            return await Policy.Handle<IOException>()
                .WaitAndRetryAsync(2, retryCount => TimeSpan.FromSeconds(retryCount))
                .ExecuteAsync(async () =>
                {
                    var blobList = new List<BlobFile>();
                    var list = new List<MemoryStream>();
                    var files = Directory.GetFiles(path, searchPattern);
                    var len = (files.Length <= maxResultCount) ? files.Length : maxResultCount;

                    var paged = files.Skip(skipCount).Take(maxResultCount).ToList();

                    foreach (var file in paged)
                    {
                        var bytes = await File.ReadAllBytesAsync(file, args.CancellationToken);
                        blobList.Add(new BlobFile(Path.GetFileName(file), bytes));
                    }

                    return blobList;
                });
        }
    }
}
