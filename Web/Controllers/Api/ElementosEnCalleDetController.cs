using RowingApp.Common.Requests;
using RowingApp.Common.Responses;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ElementosEnCalleDetController : ControllerBase
    {
        private readonly DataContext _context;


        public ElementosEnCalleDetController(DataContext context)
        {
            _context = context;
        }

         [HttpGet("{id}")]
        [Route("GetElementosEnCalleDet/{idElementoCab}")]

        public async Task<IActionResult> GetElementosEnCalleDet(int idElementoCab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var elementosEnCalleDet = await _context.ElementosEnCalleD
           .Where(o => (o.IDELEMENTOCAB == idElementoCab))
           .OrderBy(o => o.ID)
           .ToListAsync();
            if (elementosEnCalleDet == null)
            {
                return BadRequest("No hay Elementos En Calle.");
            }
            return Ok(elementosEnCalleDet);
        }


        [HttpPost]
        [Route("PostElementosEnCalleDet")]
        public async Task<IActionResult> PostElementosEnCalleDet([FromBody] ElementosEnCalleDet request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.ElementosEnCalleD.Add(request);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElementosEnCalleDet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var elementosEnCalleDet = await _context.ElementosEnCalleD
                .FirstOrDefaultAsync(p => p.ID == id);
            if (elementosEnCalleDet == null)
            {
                return this.NotFound();
            }

            _context.ElementosEnCalleD.Remove(elementosEnCalleDet);
            await _context.SaveChangesAsync();
            return Ok("Borrado");
        }
    }
}