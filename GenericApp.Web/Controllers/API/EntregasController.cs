using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregasController : ControllerBase
    {
        private readonly DataContext2 _dataContext;

        public EntregasController(DataContext2 dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetEntregas/{codigo}")]
        public async Task<IActionResult> GetEntregas(string Codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entregas = await _dataContext.ProductosStock
           .Where(o => o.causante == Codigo)
           
           .OrderBy(o => o.fecha)
           .GroupBy(r => new
           {
               r.fecha
           })
           .Select(g => new
           {
               fecha = g.Key.fecha,
               CantItems = g.Count()
           })
           .ToListAsync();


            if (entregas == null)
            {
                return BadRequest("No hay Entregas.");
            }
            return Ok(entregas);
        }
    }
}