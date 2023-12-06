using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ElementosEnCalleDetController : ControllerBase
    {
        private readonly DataContext _context;


        public ElementosEnCalleDetController(DataContext context)
        {
            _context = context;
        }

         [HttpGet("{id}")]
        [Route("GetObrasNuevoSuministrosDet/{idElementosEnCalleCab}")]

        public async Task<IActionResult> GetObrasNuevoSuministrosDet(int idElementosEnCalleCab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var elementosEnCalleDet = await _context.ElementosEnCalleD
           .Where(o => (o.IDELEMENTOCAB == idElementosEnCalleCab))
           .OrderBy(o => o.ID)
           .ToListAsync();
            if (elementosEnCalleDet == null)
            {
                return BadRequest("No hay Elementos En Calle.");
            }
            return Ok(elementosEnCalleDet);
        }


        [HttpPost]
        [Route("PostObrasNuevoSuministrosDet")]
        public async Task<IActionResult> PostObrasNuevoSuministrosDet([FromBody] ElementosEnCalleDet request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.ElementosEnCalleD.Add(request);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}