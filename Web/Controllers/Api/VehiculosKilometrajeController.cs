using RowingApp.Common.Requests;
using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace RowingApp.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosKilometrajeController : ControllerBase
    {
        private readonly DataContext2 _context;
        public VehiculosKilometrajeController(DataContext2 context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostVehiculosKilometraje([FromBody] VehiculosKilometrajeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehiculosKilometraje kilometraje = new VehiculosKilometraje
            {
                CAMBIO = request.CAMBIO,
                FECHAALTA = DateTime.Now,
                HORSAL = request.HORSAL,
                KMFECHAANTERIOR = request.KMFECHAANTERIOR,
                NOPROMEDIAR = request.NOPROMEDIAR,
                PROCESADO = request.PROCESADO,
                CODSUC = request.CODSUC,
                Equipo = request.Equipo,
                Fecha = DateTime.Now,
                HORLLE = request.HORLLE,
                KILFIN = request.KILFIN,
                KILINI = request.KILINI,
                NRODEOT = request.NRODEOT,
                Orden = request.Orden,
            };
            _context.VehiculosKilometrajes.Add(kilometraje);
            await _context.SaveChangesAsync();
            return Ok(kilometraje);
        }

        [HttpGet]
        [Route("GetNroRegistroMax")]
        public IActionResult GetNroRegistroMax()
        {
            int query = _context.VehiculosKilometrajes.Max(c => c.Orden);

            return Ok(query);
        }
    }
}
