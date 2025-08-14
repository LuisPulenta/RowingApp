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
    public class ConteoCiclicoCabController : ControllerBase
    {
        private readonly DataContext2 _dataContext;


        public ConteoCiclicoCabController(DataContext2 dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetConteoCiclicoCab/{iDUser}")]
        public async Task<IActionResult> GetConteoCiclicoCab(int iDUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var conteos = await _dataContext.VistaConteoCiclicoCab
           .Where(o => ((o.IdUserAsignado == iDUser) && (o.ProcesadoGaos == 0))
           )

           .OrderBy(o => o.IDREGISTRO)
           .ToListAsync();


            if (conteos == null)
            {
                return BadRequest("No hay Conteos.");
            }
            return Ok(conteos);
        }

        [HttpPost]
        [Route("GetConteoCiclicoDet/{iDConteo}")]
        public async Task<IActionResult> GetConteoCiclicoDet(int iDConteo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var conteosDet = await _dataContext.ConteoCiclicoDet
           .Where(o => ((o.IDCONTEOCAB == iDConteo))
           )

           .OrderBy(o => o.DESCRIPCION)
           .ToListAsync();


            if (conteosDet == null)
            {
                return BadRequest("No hay Detalles.");
            }
            return Ok(conteosDet);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutConteosDet([FromRoute] int id, [FromBody] ConteoCiclicoDe request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.IDCONTEODET)
            {
                return BadRequest();
            }
            _dataContext.ConteoCiclicoDet.Update(request);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}