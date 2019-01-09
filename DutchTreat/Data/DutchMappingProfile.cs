﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.Models;

namespace DutchTreat.Data
{
    public class DutchMappingProfile : Profile
    {
        public DutchMappingProfile()
        {
            CreateMap<Order, OrderModels>()
                    .ForMember(o => o.Id, ex => ex.MapFrom(o => o.Id))
                    .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                    .ForMember(o => o.Id, ex => ex.MapFrom(o => o.Id))
                    .ReverseMap();
        }
    }
}