using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
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
    }
}