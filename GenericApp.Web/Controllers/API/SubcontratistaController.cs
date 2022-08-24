using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcontratistaController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly DataContext2 _dataContext2;

        public SubcontratistaController(DataContext dataContext, DataContext2 dataContext2)
        {
            _dataContext = dataContext;
            _dataContext2 = dataContext2;
        }



        [HttpPost]
        [Route("GetSubcontratistas/{ProyectoModulo}")]
        public IActionResult GetObrasReclamos(string ProyectoModulo)
        {
            return Ok(_dataContext.Subcontratistas
                .Where(o => o.MODULO == ProyectoModulo)
                .OrderBy(o => o.subSubcontratista)
                );
        }
    }
}