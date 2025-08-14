using RowingApp.Common.Requests;
using Web.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace RowingApp.Web.Controllers.API
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

        public IActionResult GetCodigos(DateRequest request)
        {

            var codigos = (_dataContext.CodigosProduccion
                .Where(o => o.FECHAMINIMA.AddDays(30) > request.Fecha && o.FECHAMAXIMA.AddDays(-15) < request.Fecha)
                .OrderBy(o => o.CODIGO));
            return Ok(codigos);
        }
    }
}