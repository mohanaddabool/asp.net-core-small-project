using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DutchTreat.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : Controller
    {
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;
        private readonly IDutchRepo _repo;

        public OrderItemsController(ILogger<OrderItemsController> logger , IDutchRepo repo, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _repo.GetOrderById(User.Identity.Name,orderId);
            if(order != null)
            {
                return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId , int id)
        {
            var order = _repo.GetOrderById(User.Identity.Name , orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
            }
            return NotFound();
        }
    }
}
