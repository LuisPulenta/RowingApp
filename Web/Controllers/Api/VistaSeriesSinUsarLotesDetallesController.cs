using Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VistaSeriesSinUsarLotesDetallesController : ControllerBase
    {
        private readonly DataContext2 _dataContext;

        public VistaSeriesSinUsarLotesDetallesController(DataContext2 dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetEquipo/{grupo}/{causante}/{serie}")]
        public async Task<IActionResult> GetEquipo(string Grupo, string Causante, string Serie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var equipo = await _dataContext.VistaSeriesSinUsarLotesDetalles
            .Where(o => ((o.GRUPOH == Grupo) && (o.CAUSANTEH == Causante) && (o.NROSERIESALIDA==Serie))
           )
           .ToListAsync();


            if (equipo == null)
            {
                return BadRequest("El Equipo no existe.");
            }
            return Ok(equipo);
        }
    }
}