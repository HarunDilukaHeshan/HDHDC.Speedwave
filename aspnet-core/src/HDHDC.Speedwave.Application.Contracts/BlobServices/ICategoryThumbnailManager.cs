using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.BlobServices
{    
    public interface ICategoryThumbnailManager : ITransientDependency
    {
        Task SaveAsync(int categoryId, string base64Img);
        Task<string> SaveAsync(string base64Img);
        Task<string> GetAsync(int categoryId);
    }
}
