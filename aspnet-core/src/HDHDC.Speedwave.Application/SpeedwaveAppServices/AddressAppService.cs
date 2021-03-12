using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace HDHDC.Speedy.SpeedyAppServices
{    
    public class AddressAppService
        : ApplicationService, IAddressAppService
    {
        private readonly IRepository<AddressEntity, int> _addressRepository;
        private readonly int _customerId;

        public AddressAppService(
            IRepository<AddressEntity, int> addressRepository, 
            ICurrentUser currentUser)            
        {
            _addressRepository = addressRepository;            
            var cusClaim = currentUser.FindClaim("customerId") ?? throw new BusinessException("User does not have customerId claim");
            _customerId = int.Parse(cusClaim.Value);
        }

        public async Task<AddressDto> CreateAsync(AddressCreateDto addressCreateDto)
        {            
            var addrE = await _addressRepository.InsertAsync(new AddressEntity(
                 addressCreateDto.AddressLine,
                 addressCreateDto.CityID,
                 _customerId,
                 addressCreateDto.Geolocation)
            { Note = addressCreateDto.Note }, autoSave: true);

            return ObjectMapper.Map<AddressEntity, AddressDto>(addrE);
        }

        public async Task DeleteAsync(int id)
        {
            await _addressRepository.DeleteAsync(id, autoSave: true);
        }

        public async Task<AddressDto> GetAsync(int id)
        {
            var addrE = await _addressRepository.FindAsync(e => e.Id == id && e.CustomerID == _customerId) 
                ?? throw new EntityNotFoundException("Address entity does not exist");
            var addrDto = new AddressDto();

            ObjectMapper.Map(addrE, addrDto);
            
            return addrDto;
        }

        public async Task<IList<AddressDto>> GetAllAsync()
        {
            var query = _addressRepository.Where(e => e.CustomerID == _customerId);
            var addrEs = await AsyncExecuter.ToListAsync(query);
            var addrDtos = new List<AddressDto>();

            ObjectMapper.Map(addrEs, addrDtos);

            return addrDtos;
        }        
    }
}
