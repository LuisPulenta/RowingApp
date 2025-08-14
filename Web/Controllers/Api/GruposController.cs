using Microsoft.AspNetCore.Mvc;
using Web.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RowingApp.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class GruposController : ControllerBase
    {
        private readonly DataContext2 _dataContext2;

        public GruposController(DataContext2 dataContext2)
        {
            _dataContext2 = dataContext2;
        }

        //---------------------------------------------------------------------------
        [HttpGet]
        [Route("GetGrupos")]
        public async Task<IActionResult> GetGrupos()
        {
          

            var grupos = await _dataContext2.Grupos
            .Where(o => (o.VisualizaAPP == 1 && o.Habilitado == true))
           .OrderBy(o => o.codigo)
           .ToListAsync();
            if (grupos == null)
            {
                return BadRequest("No hay Grupos.");
            }
            return Ok(grupos);
        }
    }
}