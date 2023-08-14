using CasgemMicroservice.Services.Order.Core.Application.Features.CQRS.Commands;
using CasgemMicroservice.Services.Order.Core.Application.Features.CQRS.Queries;
using CasgemMicroservice.Services.Order.Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CasgemMicroservice.Services.Order.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetailList()
        {
            var values = await _mediator.Send(new GetAllOrderDetailQueryRequest());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> OrderDetailGetById(int id)
        {
            var value = await _mediator.Send(new GetByIdOrderDetailQueryRequest(id));
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> OrderDetailCreate(CreateOrderDetailCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok("OrderDetail eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> OrderDetailUpdate(UpdateOrderDetailCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok("OrderDetail güncellendi");
        }

        [HttpDelete]
        public async Task<IActionResult> OrderDetailDelete(int id)
        {
            await _mediator.Send(new RemoveOrderDetailCommandRequest(id));
            return Ok("OrderDetail Silindi");
        }

        [HttpGet("getuser")]
        public async Task<IActionResult> GetOrdersByUser()
        {
            OrderContext context = new OrderContext();
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]; // Bearer token

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            // "sub" alanını al
            var userId = jwtToken.Claims.First(claim => claim.Type == "sub").Value;

            //var value = await _mediator.Send(new GetByIdOrderDetailQueryRequest(userId));

            var value = context.Orderings.Where(x=> x.UserID == userId).FirstOrDefault();
            return Ok(value);
        }
    }
}
