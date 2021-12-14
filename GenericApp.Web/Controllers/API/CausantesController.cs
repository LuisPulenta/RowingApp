using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CausantesController : ControllerBase
    {
        private readonly DataContext2 _dataContext;

        public CausantesController(DataContext2 dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetCausanteByCodigo")]
        public async Task<IActionResult> GetCausante(CausanteRequest codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Data.Entities.Causante user = await _dataContext.Causantes.FirstOrDefaultAsync
                (o => (o.codigo.ToLower() == codigo.Codigo.ToLower() || o.NroSAP.ToLower() == codigo.Codigo.ToLower()) && (o.grupo == "PPR" || o.grupo == "PPC"));

            if (user == null)
            {
                return BadRequest("El Empleado no existe.");
            }

            CausanteResponse response = new CausanteResponse
            {
                codigo = user.codigo,
                nombre = user.nombre,
                encargado = user.encargado,
                NroCausante = user.NroCausante,
                telefono = user.telefono,
                NroSAP=user.NroSAP,
                grupo=user.grupo,
                estado=user.estado
            };

            return Ok(response);
        }

        // GET: api/Users/5
        [HttpGet("GetCausanteByCodigo2/{codigo}")]
        public async Task<ActionResult<Data.Entities.Causante>> GetCausante2(string codigo)
        {
            Data.Entities.Causante causante = await _dataContext.Causantes
                .FirstOrDefaultAsync(o => (o.codigo.ToLower() == codigo.ToLower() || o.NroSAP.ToLower() == codigo.ToLower()) && (o.grupo == "PPR" || o.grupo == "PPC"));

            if (causante == null)
            {
                return NotFound();
            }
            return causante;
        }
    }
}