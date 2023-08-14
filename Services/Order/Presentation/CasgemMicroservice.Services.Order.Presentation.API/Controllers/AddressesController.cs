using CasgemMicroservice.Services.Order.Core.Application.Features.CQRS.Commands;
using CasgemMicroservice.Services.Order.Core.Application.Features.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasgemMicroservice.Services.Order.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> AddressList()
        {
            var values = await _mediator.Send(new GetAllAddressQueryRequest());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AddressGetById(int id)
        {
            var value = await _mediator.Send(new GetByIdAddressQueryRequest(id));
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> AddressCreate(CreateAddressCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok("Adres eklendi");    
        }

        [HttpPut]
        public async Task<IActionResult> AddressUpdate(UpdateAddressCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok("Adres güncellendi");
        }

        [HttpDelete]
        public async Task<IActionResult> AddressDelete(int id)
        {
            await _mediator.Send(new RemoveAddressCommandRequest(id));
            return Ok("Adres Silindi");
        }
    }
}
