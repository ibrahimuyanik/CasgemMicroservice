﻿using CasgemMicroservice.Services.Order.Core.Application.Features.CQRS.Commands;
using CasgemMicroservice.Services.Order.Core.Application.Interfaces;
using CasgemMicroservice.Services.Order.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasgemMicroservice.Services.Order.Core.Application.Features.CQRS.Handlers
{
    public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommandRequest>
    {
        private readonly IRepository<OrderDetail> _repository;

        public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateOrderDetailCommandRequest request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.OrderDetailID);
            value.ProductName = request.ProductName;
            value.ProductID = request.ProductID;
            value.ProductPrice = request.ProductPrice;
            value.ProductAmount = request.ProductAmount;
            value.OrderingID = request.OrderingID;

            await _repository.UpdateAsync(value);
        }
    }
}
