﻿using Microsoft.AspNetCore.Mvc;
using GenericApp.Web.Data;
using System.Linq;

namespace GenericApp.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly DataContext _context;

        public ClientesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetClientes")]
        public IActionResult GetClientes()
        {
            return Ok(_context.Clientes.OrderBy(o => o.NOMBRE));
        }
    }
}