using RowingApp.Common.Requests;
using RowingApp.Common.Responses;
using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ObrasPostesCajasDetalleController : ControllerBase
    {
        private readonly DataContext _context;


        public ObrasPostesCajasDetalleController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetNroRegistroMax")]
        public IActionResult GetNroRegistroMax()
        {
            int query = _context.ObrasPostesCajasDetalle.Max(c => c.NROREGISTROD);

            return Ok(query);
        }


        [HttpPost]
        [Route("PostObrasPostesCajasDetalle")]
        public async Task<IActionResult> PostObrasPostesCajasDetalle([FromBody] ObrasPostesCajaDetalleResponse request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            ObrasPosteCajaDetalleAPP ObrasPostesCajaDetalle = new ObrasPosteCajaDetalleAPP
            {
                CANTIDAD = request.CANTIDAD,
                CATCODIGO = request.CATCODIGO,
                CODIGOSAP = request.CODIGOSAP,
                NROREGISTROCAB = request.NROREGISTROCAB,
                NROREGISTROD = request.NROREGISTROD,
            };

            _context.ObrasPostesCajasDetalleAPP.Add(ObrasPostesCajaDetalle);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}