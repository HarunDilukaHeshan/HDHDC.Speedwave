using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.UtilityServices
{
    public interface IMd5Hasher : ITransientDependency
    {
        string GenerateBase64Hash(byte[] buffer);
        string GenerateBase16Hash(byte[] buffer);
    }
}
