using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GenericApp.Web.Controllers.API
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
        [Route("GetObjetos")]

        public IActionResult GetObjetos()
        {
            var talleres = (_dataContext.CabeceraCertificacionObjetos
                .OrderBy(o => o.OBJETOS));

            return Ok(talleres);
        }
    }
}