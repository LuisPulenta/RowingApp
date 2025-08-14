using Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CausantesObrasController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CausantesObrasController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //---------------------------------------------------------------------------------------------------
     
        [HttpPost]
        [Route("GetCausantesObra/{grupo}")]
        public async Task<IActionResult> GetCausantesObra(string grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var causantesObra = await _dataContext.CausantesObras
           .Where(o => (
           o.grupo == grupo 
           && o.estado == true
           ))
           .OrderBy(o => o.nombre)
           .ToListAsync();
            if (causantesObra == null)
            {
                return BadRequest("No hay CausantesObra.");
            }
            return Ok(causantesObra);
        }
    }
}