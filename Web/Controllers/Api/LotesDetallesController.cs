using RowingApp.Common.Requests;
using Web.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotesDetallesController : ControllerBase
    {
        private readonly DataContext2 _dataContext;

        public LotesDetallesController(DataContext2 dataContext)
        {
            _dataContext = dataContext;
        }

        //---------------------------------------------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoteDetalles([FromRoute] int id, [FromBody] LotesDetalleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.NROREGISTRO)
            {
                return BadRequest();
            }

            var oldLoteDetalle = await _dataContext.LotesDetalle.FindAsync(request.NROREGISTRO);
            if (oldLoteDetalle == null)
            {
                return BadRequest("El Lote Detalle no existe.");
            }

            oldLoteDetalle.IDInstalacionesEquipos = request.IDInstalacionesEquipos;
            if (request.MARCAR == 1)
            {
                oldLoteDetalle.FechaUsada = DateTime.Now;
                oldLoteDetalle.SerieUsada = 1;
            }
            else
            {
                oldLoteDetalle.FechaUsada = null;
                oldLoteDetalle.SerieUsada = 0;
            }

            _dataContext.LotesDetalle.Update(oldLoteDetalle);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }      
    }
}