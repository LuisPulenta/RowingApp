using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
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
        [Route("GetSeriesSinUsar/{grupo}/{causante}")]
        public async Task<IActionResult> GetSeriesSinUsar(string Grupo,string Causante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var seriesSinUsar = await _dataContext.VistaSeriesSinUsarLotesDetalles
           .Where(o => ((o.GRUPOH == Grupo) && (o.CAUSANTEH == Causante))
           )

           .OrderBy(o => o.NROLOTECAB)
           .ToListAsync();


            if (seriesSinUsar == null)
            {
                return BadRequest("No hay Series Sin Usar.");
            }
            return Ok(seriesSinUsar);
        }
     }
}