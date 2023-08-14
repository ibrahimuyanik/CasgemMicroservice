﻿using AutoMapper;
using CasgemMicroservice.Services.Order.Core.Application.Dtos.OrderDtos;
using CasgemMicroservice.Services.Order.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasgemMicroservice.Services.Order.Core.Application.Mappings
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<ResultOrderingDto, Domain.Entities.Ordering>().ReverseMap();
            CreateMap<CreateOrderingDto, Domain.Entities.Ordering>().ReverseMap();
            CreateMap<UpdateOrderingDto, Domain.Entities.Ordering>().ReverseMap();
        }
    }
}
