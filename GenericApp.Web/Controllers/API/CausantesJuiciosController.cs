using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CausantesJuiciosController : ControllerBase
    {
        private readonly DataContext _dataContext;


        public CausantesJuiciosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetJuicios")]
        public async Task<IActionResult> GetJuicios()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var juicios = await _dataContext.CausantesJuicios
           .Where(o => ((o.CERRADO == 0))
           )

           .OrderBy(o => o.ID_CASO)
           .ToListAsync();


            if (juicios == null)
            {
                return BadRequest("No hay Juicios.");
            }
            return Ok(juicios);
        }

        [HttpPost]
        [Route("GetMediaciones/{codigo}")]
        public async Task<IActionResult> GetMediaciones(int codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mediaciones = await _dataContext.CausantesJuiciosMediaciones
           .Where(o => ((o.IDCAUSANTEJUICIO == codigo))
           )

           .OrderBy(o => o.IDMEDIACION)
           .ToListAsync();


            if (mediaciones == null)
            {
                return BadRequest("No hay Mediaciones.");
            }
            return Ok(mediaciones);
        }

        [HttpPost]
        [Route("GetNotificaciones/{codigo}")]
        public async Task<IActionResult> GetNotificaciones(int codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var notificaciones = await _dataContext.CausantesJuiciosNotificaciones
           .Where(o => ((o.IDJUICIO == codigo))
           )

           .OrderBy(o => o.IDNOTIFICACION)
           .ToListAsync();


            if (notificaciones == null)
            {
                return BadRequest("No hay Notificaciones.");
            }
            return Ok(notificaciones);
        }
    }
}