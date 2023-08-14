using AutoMapper;
using CasgemMicroservice.Services.Discount.Context;
using CasgemMicroservice.Services.Discount.Dtos;
using CasgemMicroservice.Services.Discount.Models;
using CasgemMicroservice.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CasgemMicroservice.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context;
        private readonly IMapper _mapper;

        public DiscountService(DapperContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<NoContent>> CreateDiscountCouponsAsync(CreateDiscountDto createDiscountDto)
        {
            var result = _mapper.Map<DiscountCoupons>(createDiscountDto);
            result.CreatedTime = DateTime.Now;
            await _context.DiscountCouponses.AddAsync(result);
            await _context.SaveChangesAsync();
            return Response<NoContent>.Success(201);
        }

        public async Task<Response<NoContent>> DeleteDiscountCouponsAsync(int id)
        {
            var value = await _context.DiscountCouponses.FindAsync(id);
            if (value == null)
            {
                return Response<NoContent>.Fail("Silinecek Kupon Bulunamadı", 404);
            }

            _context.DiscountCouponses.Remove(value);
            await _context.SaveChangesAsync();
            return Response<NoContent>.Success(204);

        }

        public async Task<Response<List<ResultDiscountDto>>> GetAllDiscountCouponsAsync()
        {
            var values = await _context.DiscountCouponses.ToListAsync();
            return Response<List<ResultDiscountDto>>.Success(_mapper.Map<List<ResultDiscountDto>>(values), 200);
        }

        public async Task<Response<ResultDiscountDto>> GetByIdDiscountCouponAsync(int id)
        {
            var value = await _context.DiscountCouponses.FindAsync(id);
            if (value == null)
            {
                return Response<ResultDiscountDto>.Fail("Kupon Bulunamadı", 404);
            }

            return Response<ResultDiscountDto>.Success(_mapper.Map<ResultDiscountDto>(value), 200);
        }

        public async Task<Response<NoContent>> UpdateDiscountCouponsAsync(UpdateDiscountDto updateDiscountDto)
        {
            var existingResponse = await _context.DiscountCouponses.FindAsync(updateDiscountDto.DiscountCouponsID);
            
            if (existingResponse == null)
            {
                return Response<NoContent>.Fail("Güncellenecek Kupon Bulunamadı", 404);
            }

            _mapper.Map(updateDiscountDto, existingResponse);
            _context.DiscountCouponses.Update(existingResponse);
            await _context.SaveChangesAsync();

            return Response<NoContent>.Success(204);
        }
    }
}
