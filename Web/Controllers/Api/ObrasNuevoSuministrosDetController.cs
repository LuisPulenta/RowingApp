using RowingApp.Common.Requests;
using RowingApp.Common.Responses;
using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ObrasNuevoSuministrosDetController : ControllerBase
    {
        private readonly DataContext _context;


        public ObrasNuevoSuministrosDetController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetNroRegistroMax")]
        public IActionResult GetNroRegistroMax()
        {
            int query = _context.ObrasNuevoSuministrosDet.Max(c => c.NROREGISTROD);

            return Ok(query);
        }


        [HttpGet("{id}")]
        [Route("GetObrasNuevoSuministrosDet/{nroSuministroCab}")]

        public async Task<IActionResult> GetObrasNuevoSuministrosDet(int nroSuministroCab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obras = await _context.ObrasNuevoSuministrosDet
           .Where(o => (o.NROSUMINISTROCAB == nroSuministroCab))
           .OrderBy(o => o.CATCODIGO)
           .ToListAsync();
            if (obras == null)
            {
                return BadRequest("No hay Obras.");
            }
            return Ok(obras);
        }


        [HttpPost]
        [Route("PostObrasNuevoSuministrosDet")]
        public async Task<IActionResult> PostObrasNuevoSuministrosDet([FromBody] ObrasNuevoSuministrosDetResponse request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            ObrasNuevoSuministroDe ObrasNuevoSuministrosDet = new ObrasNuevoSuministroDe
            {
                CANTIDAD = request.CANTIDAD,
                CATCODIGO = request.CATCODIGO,
                CODIGOSAP = request.CODIGOSAP,
                NROSUMINISTROCAB = request.NROSUMINISTROCAB,
                NROREGISTROD = request.NROREGISTROD,
            };

            _context.ObrasNuevoSuministrosDet.Add(ObrasNuevoSuministrosDet);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}