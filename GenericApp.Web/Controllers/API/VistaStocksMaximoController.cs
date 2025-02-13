﻿using Microsoft.AspNetCore.Mvc;
using GenericApp.Web.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Web.Data.Entities;

namespace GenericApp.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class VistaStocksMaximoController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public VistaStocksMaximoController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //---------------------------------------------------------------------------
        [HttpPost]
        [Route("GetVistaStocksMaximosByGrupoAndByCodigo")]
        public async Task<IActionResult> GetCausantePPRByCodigo(CausanteRequest3 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var stocksMaximos = await _dataContext.VistaStocksMaximos
          .Where(o => o.GRUPOC == request.Grupo && o.CAUSANTE==request.Codigo
          )

          .OrderBy(o => o.catCatalogo)
          .ToListAsync();

            return Ok(stocksMaximos);
        }
    }
}