using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedy.SpeedyAppServices
{
    public class ProvinceAppService : ApplicationService, IProvinceAppService
    {
        protected IRepository<ProvinceEntity, string> Repository { get; }

        public ProvinceAppService(IRepository<ProvinceEntity, string> repository)
            : base()
        {
            Repository = repository;
        }

        public virtual async Task<ProvinceDto> CreateAsync(ProvinceDto dto)
        {
            var provinceE = await Repository.FindAsync(dto.Id);

            if (provinceE != null) throw new BusinessException("Province already exists");

            provinceE = await Repository.InsertAsync(new ProvinceEntity(dto.Id), autoSave: true);
            var provinceDto = new ProvinceDto();

            ObjectMapper.Map(provinceE, provinceDto);

            return provinceDto;
        }

        public virtual async Task<ProvinceDto> GetAsync(string provinceId)
        {
            if (provinceId == null) throw new ArgumentNullException();

            var provinceE = await Repository.GetAsync(provinceId);
            if (provinceE == null) return null;

            var provinceDto = new ProvinceDto();

            ObjectMapper.Map(provinceE, provinceDto);

            return provinceDto;
        }

        public virtual async Task<ProvinceDto[]> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var dtoList = await Repository.GetListAsync();

            return ObjectMapper.Map<ProvinceEntity[], ProvinceDto[]>(dtoList.ToArray());
        }

        public virtual async Task DeleteAsync(string id)
        {
            await Repository.DeleteAsync(id);
        }
    }
}
