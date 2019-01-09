using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.Extensions.Logging;
using DutchTreat.Data.Entities;


namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IDutchRepo _dutchRepo;
        private readonly ILogger<DutchContext> _logger;

        public ProductsController(IDutchRepo dutchRepo , ILogger<DutchContext> logger)
        {
            _dutchRepo = dutchRepo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_dutchRepo.GetAllProducts());
            }
            catch(Exception ex)
            {
                _logger.LogError($"Can't get the products {ex}");
                return BadRequest("Can not Get Product");
            }
        }
    }
}
