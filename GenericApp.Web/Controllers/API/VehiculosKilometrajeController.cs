using GenericApp.Common.Requests;
using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
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
                FECHAALTA = request.FECHAALTA,
                HORSAL = request.HORSAL,
                KMFECHAANTERIOR = request.KMFECHAANTERIOR,
                NOPROMEDIAR = request.NOPROMEDIAR,
                PROCESADO = request.PROCESADO,
                CODSUC = request.CODSUC,
                Equipo = request.Equipo,
                Fecha = request.Fecha,
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
        public async Task<IActionResult> GetNroRegistroMax()
        {
            int query = _context.VehiculosKilometrajes.Max(c => c.Orden);

            return Ok(query);
        }
    }
}
