using GenericApp.Common.Requests;
using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosProgramasPrevController : ControllerBase
    {
        private readonly DataContext2 _context;
        public VehiculosProgramasPrevController(DataContext2 context)
        {
            _context = context;
        }

        [HttpPut("{NroInterno}")]
        public async Task<IActionResult> PutVehiculosProgramasPrev([FromRoute] int NroInterno, [FromBody] Vehiculo3Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (NroInterno != request.NroInterno)
            {
                return BadRequest();
            }

            var oldVehiculoProgramaPrev = await _context.VehiculosProgramasPrev.FindAsync(request.NroInterno);
            if (oldVehiculoProgramaPrev == null)
            {
                return BadRequest("El Vehículo no existe.");
            }

            oldVehiculoProgramaPrev.UltimaLectura = DateTime.Now;
            oldVehiculoProgramaPrev.KMDesdeUltimaVerificacion = (int)(oldVehiculoProgramaPrev.KMDesdeUltimaVerificacion + request.KMHSACTUAL);

            _context.VehiculosProgramasPrev.Update(oldVehiculoProgramaPrev);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
