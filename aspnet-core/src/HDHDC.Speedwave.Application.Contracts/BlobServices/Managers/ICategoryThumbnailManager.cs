using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.BlobServices.Managers
{
    public interface ICategoryThumbnailManager : ITransientDependency
    {
        Task<string> GetAsync(string name);
        Task<string> SaveAsync(string name, string base64, bool overrideExisting = false);
        Task<string> SaveAsync(string name, byte[] bytes, bool overrideExisting = false);
        Task<string> SaveAsync(string base64, bool overrideExisting = false);
        Task<string> SaveAsync(byte[] bytes, bool overrideExisting = false);
        Task DeleteAsync(string name);
    }
}
