using AutoMapper;
using CasgemMicroservice.Services.Discount.Dtos;
using CasgemMicroservice.Services.Discount.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasgemMicroservice.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountCouponsController : ControllerBase
    {
        private readonly IDiscountService  _discountService;
        private readonly IMapper _mapper;
        public DiscountCouponsController(IDiscountService discountService, IMapper mapper)
        {
            _discountService = discountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscountCoupons()
        {
            var value = await _discountService.GetAllDiscountCouponsAsync();
            return Ok(value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdDiscountCoupon(int id)
        {
            var value = await _discountService.GetByIdDiscountCouponAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountDto createDiscountDto)
        {
            await _discountService.CreateDiscountCouponsAsync(createDiscountDto);
            return Ok("Kupon Başarıyla Oluşturuldu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountDto updateDiscountDto)
        {
            await _discountService.UpdateDiscountCouponsAsync(updateDiscountDto);
            return Ok("Kupon Başarıyla Güncellendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDiscountCoupon(int id)
        {
            await _discountService.DeleteDiscountCouponsAsync(id);
            return Ok("Kupon Başarıyla Silinmiştir");
        }
    }
}
