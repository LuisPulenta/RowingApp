using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CausantesNovedadesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CausantesNovedadesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("GetTipoNovedades")]
        public async Task<IActionResult> GetTipoNovedades()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var tipoNovedades = await _dataContext.TipoNovedad
                     .OrderBy(o => o.TIPODENOVEDAD)
           .ToListAsync();
            if (tipoNovedades == null)
            {
                return BadRequest("No hay Tipos Novedades.");
            }
            return Ok(tipoNovedades);
        }


        [HttpPost]
        [Route("GetNovedades/{grupo}/{causante}")]
        public async Task<IActionResult> GetNovedades(string Grupo,string Causante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var novedades = await _dataContext.CausantesNovedades
           .Where(o => (o.GRUPO == Grupo) && (o.CAUSANTE == Causante) && (o.FECHACARGA.AddDays(30) >= DateTime.Now))

           .OrderBy(o => o.FECHACARGA)
           .ToListAsync();


            if (novedades == null)
            {
                return BadRequest("No hay Novedades.");
            }
            return Ok(novedades);
        }

        [HttpPost]
        [Route("PostNovedades")]
        public async Task<IActionResult> PostNovedades([FromBody] CausantesNovedade request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dataContext.CausantesNovedades.Add(request);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

    }
}