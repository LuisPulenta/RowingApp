using GenericApp.Common.Requests;
using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CausanteRecibosController : ControllerBase
    {
        private readonly DataContext2 _dataContext2;

        public CausanteRecibosController(DataContext2 dataContext2)
        {
            _dataContext2 = dataContext2;
        }

        //------------------------------------------------------------------------------
        [HttpPost]
        [Route("GetRecibos")]
        public async Task<IActionResult> GetRecibos(CausanteRequest3 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var recibos = await _dataContext2.CausantesRec
                    .Where(o => o.CAUSANTE == request.Codigo && o.GRUPO == request.Grupo)
                    .OrderBy(o => o.ANIO + o.MES)                       
          .ToListAsync();
            if (recibos == null)
            {
                return BadRequest("No hay Recibos");
            }
            return Ok(recibos);
        }
    }
}