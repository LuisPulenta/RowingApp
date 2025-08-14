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
    public class UsuariosGeosController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UsuariosGeosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuariosGeo request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.UsuariosGeos.Add(request);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("GetParametro")]
        public async Task<IActionResult> GetParametro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var parametro = await _dataContext.Parametros.FirstOrDefaultAsync(o => o.ID == 1);
            return Ok(parametro);
        }

        [HttpPost]
        [Route("GetUsuarios/{year}/{month}/{day}")]
        public async Task<IActionResult> GetUsuarios(int year,int month,int day)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var usuarios = await _dataContext.UsuariosGeos

           .Where(o => (o.Fecha.Year == year) && (o.Fecha.Month == month) && (o.Fecha.Day == day))
           .OrderBy(o => o.IdUsuario)
           .GroupBy(r => new
           {
               r.IdUsuario,
               r.UsuarioStr,
               r.Modulo
           })
           .Select(g => new
           {
               IdUsuario = g.Key.IdUsuario,
               UsuarioStr = g.Key.UsuarioStr,
               Modulo=g.Key.Modulo,

           }).ToListAsync();


            if (usuarios == null)
            {
                return BadRequest("No hay REgistros para este Usuario.");
            }

            return Ok(usuarios);
        }

        [HttpPost]
        [Route("GetPuntos/{usuarioId}/{year}/{month}/{day}")]
        public async Task<IActionResult> GetPuntos(int usuarioId,int year, int month, int day)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var puntos = await _dataContext.UsuariosGeos

            .Where(o => (o.IdUsuario == usuarioId) && (o.Fecha.Year == year) && (o.Fecha.Month == month) && (o.Fecha.Day == day))
            .OrderBy(o => o.Fecha)
            .ToListAsync();

            if (puntos == null)
            {
                return BadRequest("No hay Puntos.");
            }
            return Ok(puntos);
        }
    }
}