using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using HDHDC.Speedwave.SpeedyConsts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace HDHDC.Speedwave.Controllers
{
    [Route("apiv1/[controller]")]
    public class OrderController : SpeedwaveController
    {
        private IDeliveryChargeAppService DeliveryChargeAppService { get; }
        private IOrderAppService OrderAppService { get; }

        public OrderController(
            IOrderAppService orderAppService,
            IDeliveryChargeAppService deliveryChargeAppService)
        {
            DeliveryChargeAppService = deliveryChargeAppService;
            OrderAppService = orderAppService;            
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> Get([FromRoute]int id)
        {
            return await OrderAppService.GetAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task Remove([FromRoute]int id)
        {
            await OrderAppService.RemoveAsync(id);
        }

        [HttpPost("deliverycharge")]
        public async Task<DeliveryChargeDto> CalculateDeliveryCharge(
            [FromQuery]int addressId, 
            [FromQuery]int deliveryScheduleId, 
            [FromBody]CartItemDto[] itemsArr)
        {
            return await DeliveryChargeAppService.CalculateAsync(addressId, deliveryScheduleId, itemsArr);
        }

        [HttpPost]
        public async Task<OrderDto> Create([FromBody] OrderCreateDto orderCreateDto)
        {
            return await OrderAppService.CreateAsync(orderCreateDto);
        }

        [HttpGet]
        public async Task<OrderDto[]> GetOrders(
            [FromQuery]string type = "history",
            [FromQuery]int cityId = 0,
            [FromQuery]int skipCount = 0, 
            [FromQuery]int maxResultCount = 10)
        {
            var roles = CurrentUser.Roles;
            var orderDtoArr = new OrderDto[0];

            if (type != "history" && type != "active" && type != "selected") throw new BusinessException("Invalid type");

            if (roles.FindIndex(e => e.ToLower() == RolesConsts.Customer) != -1)
            {
                if (type == "history")
                    orderDtoArr = await OrderAppService.GetOrdersForCustomerAsync(skipCount, maxResultCount);
                else if (type == "active")
                    orderDtoArr = await OrderAppService.GetActiveOrdersForCustomerAsync();
            }
            else if (roles.FindIndex(e => e.ToLower() == RolesConsts.Rider) != -1)
            {
                if (type == "history")
                    orderDtoArr = await OrderAppService.GetOrdersForRiderAsync(skipCount, maxResultCount);
                else if (type == "active")
                    orderDtoArr = await OrderAppService.GetActiveOrdersForRiderAsync(skipCount, maxResultCount);
                else if (type == "selected")
                    orderDtoArr = await OrderAppService.GetSelectedOrdersForRiderAsync();
            }
            else if (roles.FindIndex(e => e.ToLower() == RolesConsts.Manager) != -1)
            {
                if (type == "history")
                    orderDtoArr = await OrderAppService.GetOrdersForManagerAsync(cityId, skipCount, maxResultCount);
                else if (type == "active")
                    orderDtoArr = await OrderAppService.GetActiveOrdersForManagerAsync(cityId, skipCount, maxResultCount);
            }
            else
                throw new BusinessException();

            return orderDtoArr;
        }

        [HttpPost("selected")]
        public async Task SelectOrder([FromQuery]int id)
        {
            await OrderAppService.SelectOrderAsync(id);
        }

        [HttpDelete("selected/{id}")]
        public async Task Deselect([FromQuery]int id)
        {
            await OrderAppService.DeselectOrderAsync(id);
        }

        [HttpPost("delivered")]
        public async Task MarkAsDelivered([FromQuery]int id, [FromBody]PaymentDetailDto[] dtoArr)
        {
            await OrderAppService.MarkAsDeliveredOrderAsync(id, dtoArr);
        }

        [HttpGet("cancellationreasons")]
        public async Task<CancellationReasonDto[]> GetAllCR()
        {
            return await OrderAppService.GetAllCancellationReasonAsync();
        }

        [HttpPost("cancellationrequest")]
        public async Task<CancelledOrderDto> SendCancellationRequest([FromBody]CancelledOrderDto dto)
        {
            return await OrderAppService.SendCancellationRequestAsync(dto);
        }

        [HttpDelete("cancellationrequest/{id}")]
        public async Task<OrderDto> RollbackCancellationRequest([FromRoute]int id)
        {
            return await OrderAppService.RollbackCancellationRequestAsync(id);
        }
    }
}
