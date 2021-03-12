using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.Options;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveAppServices;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedy.SpeedyAppServices
{
    public class CityAppService
        : CrudAppService<
            CityEntity,
            CityDto,
            int,
            PagedAndSortedResultRequestDto,
            CityCreateDto,
            CityUpdateDto>,
        ICityAppService
    {
        protected ICityRepository CityRepository { get; }     
        
        protected GeoDistanceOptions GeoDistanceOptions { get; }

        public CityAppService(
            IOptions<GeoDistanceOptions> options,
            ICityRepository repository)
            : base(repository)
        {
            CityRepository = repository;
            GeoDistanceOptions = options.Value;
        }

        public async Task<CityDto[]> SearchAsync(int skipCount = 0, int maxResultCount = 10, string keyword = "")
        {
            var cityEs = await CityRepository.SearchAsync(skipCount, maxResultCount, keyword);

            return ObjectMapper.Map<CityEntity[], CityDto[]>(cityEs);
        }

        public async Task<CityDto[]> GetListAsync(string districtId)
        {
            var cityEs = await CityRepository.GetListAsync(districtId);
            return ObjectMapper.Map<CityEntity[], CityDto[]>(cityEs);
        }

        public async Task<CityDto[]> GetCityListWithinTheRadiusAsync(int cityId)
        {
            _ = Repository.GetAsync(cityId) ?? throw new EntityNotFoundException();

            var cEs = await CityRepository.GetCityListWithinTheRadiusAsync(cityId, GeoDistanceOptions.MaxRadius);

            return ObjectMapper.Map<CityEntity[], CityDto[]>(cEs);
        }
    }
}
