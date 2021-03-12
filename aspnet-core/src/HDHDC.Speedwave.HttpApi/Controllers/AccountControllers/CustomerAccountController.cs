using HDHDC.Speedwave.AccountAppServices;
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HDHDC.Speedwave.Controllers.AccountControllers
{
    [Route("apiv1/[controller]")]
    public class CustomerAccountController : SpeedwaveController
    {
        private ICustomerAccountAppService CustomerAccountAppService { get; } 
        private ICustomerAppService CustomerAppService { get; }

        public CustomerAccountController(
            ICustomerAppService customerAppService,
            ICustomerAccountAppService customerAccountAppService)
        {
            CustomerAccountAppService = customerAccountAppService;
            CustomerAppService = customerAppService;
        }

        [HttpPost]
        public async Task<AppUserDto> Create([FromBody]UserCreateDto dto)
        {            
            return await CustomerAccountAppService.RegisterAsync(dto);
        }

        [HttpGet("address")]
        public async Task<AddressDto[]> GetAllAddress()
        {
            return await CustomerAppService.GetAllAddressDtoAsync();
        }

        [HttpGet("address/{id}")]
        public async Task<AddressDto> GetAddress([FromRoute]int id)
        {
            try
            {
                return await CustomerAppService.GetAddressDtoAsync(id);
            }
            catch (Exception ex)
            {

            }

            return await CustomerAppService.GetAddressDtoAsync(id);
        }

        [HttpPost("address")]
        public async Task<AddressDto> CreateAddress([FromBody]AddressCreateDto dto)
        {
            return await CustomerAppService.CreateAddressDto(dto);
        }

        [HttpPut("address/{id}")]
        public async Task<AddressDto> UpdateAddress([FromRoute]int id, [FromBody]AddressUpdateDto dto)
        {
            try
            {
                var d = await CustomerAppService.UpdateAddressDto(id, dto);
                return d;
            }
            catch(Exception ex)
            {

            }
            return await CustomerAppService.UpdateAddressDto(id, dto);
        }

        [HttpDelete("address/{id}")]
        public async Task DeleteAddress([FromRoute]int id)
        {
            await CustomerAppService.DeleteAddressAsync(id);
        }
    }
}
