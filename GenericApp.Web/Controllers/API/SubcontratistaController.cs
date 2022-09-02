using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        [Route("GetSubcontratistas/{proyectomodulo}")]
        public async Task<IActionResult> GetSubcontratistas(string proyectoModulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var subContratistas = await _dataContext.Subcontratistas
            .Where(o => (o.MODULO == proyectoModulo || o.MODULO == "Compartido") && o.subDeshabilitado==0)
            .OrderBy(o => o.subSubcontratista)
            .ToListAsync();
            if (subContratistas == null)
            {
                return BadRequest("No hay SubContratistas.");
            }
            return Ok(subContratistas);
        }
    }
}