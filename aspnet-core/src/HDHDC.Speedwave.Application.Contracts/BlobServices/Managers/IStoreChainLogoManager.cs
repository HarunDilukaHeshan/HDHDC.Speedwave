using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.BlobServices.Managers
{
    public interface IStoreChainLogoManager : ITransientDependency
    {
        Task<string> GetAsync(string name);

        Task<string> SaveAsync(string base64, bool overrideExisting = false);

        Task<string> SaveAsync(byte[] bytes, bool overrideExisting = false);

        Task DeleteAsync(string name);
    }
}
