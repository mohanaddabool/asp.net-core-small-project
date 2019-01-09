using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DutchTreat.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes= JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController :Controller
    {
        private readonly IDutchRepo _repo;
        private readonly ILogger<Order> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrderController(IDutchRepo repo , ILogger<Order> logger , IMapper mapper , UserManager<StoreUser> userManager)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
        
        [HttpGet]
        public IActionResult Get( bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;
                var result = _repo.GetAllOrdersByUser(username,includeItems);

                var tt = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderModels>>(result);
                return Ok(tt);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Can not get the order{ex}");
                return BadRequest("Can not get the order");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repo.GetOrderById(User.Identity.Name ,id);
                if (order != null)
                {
                    return Ok(_mapper.Map<Order , OrderModels>(order));
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can not get the order{ex}");
                return BadRequest("Can not get the order");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderModels, Order>(model);

                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.Users = currentUser;

                    _repo.addEntity(newOrder);
                    if (_repo.Save())
                    {
                        return Created($"api/order/{newOrder.Id}", _mapper.Map<Order, OrderModels>(newOrder));
                        //return Ok(model);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Can not save the order{ex}");

            }
            return BadRequest("Can not save the order");
        }
    }
}
