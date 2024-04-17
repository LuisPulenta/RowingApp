using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodigosProduccionController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CodigosProduccionController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetCodigos")]

        public IActionResult GetCodigos()
        {
            var talleres = (_dataContext.CodigosProduccion
                .Where(o => o.FECHAMINIMA.AddDays(30) > DateTime.Now && o.FECHAMAXIMA.AddDays(-15) < DateTime.Now)
                .OrderBy(o => o.CODIGO));

            return Ok(talleres);
        }
    }
}