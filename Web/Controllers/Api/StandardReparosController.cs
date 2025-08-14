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
    public class StandardReparosController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public StandardReparosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpPost]
        [Route("GetStandardReparos")]
        public async Task<IActionResult> GetStandardReparos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            System.Collections.Generic.List<StandardReparo> standardReparos = await _dataContext.StandardReparos
            .OrderBy(o => o.DESCRIPCIONTAREA)
            .ToListAsync();
            if (standardReparos == null)
            {
                return BadRequest("No hay StandarReparos.");
            }
            return Ok(standardReparos);
        }
    }
}