using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.BlobServices.Managers
{
    public interface IItemPictureManager : ITransientDependency
    {
        Task<string> GetAsync(string name);
        Task<IList<string>> GetListAsync(int id);
        Task<string> SaveAsync(int id, string base64, bool overrideExisting = false);
        Task<string> SaveAsync(int id, byte[] bytes, bool overrideExisting = false);
        Task<string> SaveThumbnailAsync(string base64, bool overrideExisting = false);
        Task<string> SaveThumbnailAsync(byte[] bytes, bool overrideExisting = false);
        Task DeleteAsync(string name);
        Task<IList<BlobFile>> GetBlobFileListAsync(int id, int skipCount = 0, int maxResultCount = 10);

    }
}
