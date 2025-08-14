using Web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabeceraCertificacionObjetosController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CabeceraCertificacionObjetosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetObjetos/{Modulo}")]

        public IActionResult GetObjetos(string Modulo)
        {
            var talleres = (_dataContext.CabeceraCertificacionObjetos
                .Where(o => o.Modulo == Modulo)
                .OrderBy(o => o.OBJETOS));

            return Ok(talleres);
        }
    }
}