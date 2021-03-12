using JetBrains.Annotations;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;

namespace HDHDC.Speedwave.BlobStoringServices
{
    public class BlobProviderGetListArgs : BlobProviderArgs
    {
        public BlobProviderGetListArgs(
            [NotNull] string containerName,
            [NotNull] BlobContainerConfiguration configuration,
            [NotNull] string searchPattern,
            [NonNegativeValue]int skipCount,
            [NonNegativeValue]int maxResultCount,
            CancellationToken cancellationToken = default)
            : base(
                containerName,
                configuration,
                "BlobName",
                cancellationToken)
        {
            SearchPattern = searchPattern;
            SkipCount = skipCount;
            MaxResultCount = maxResultCount;
        }


        public string SearchPattern { get; set; }
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; } = 10;
    }
}
