using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosSiniestrosController : ControllerBase
    {
        private readonly DataContext2 _dataContext;
        private readonly IFilesHelper _filesHelper;

        public VehiculosSiniestrosController(DataContext2 dataContext,IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
        }

        [HttpPost]
        [Route("GetSiniestros/{grupo}/{causante}")]
        public async Task<IActionResult> GetSiniestros(string Grupo,string Causante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var siniestros = await _dataContext.VehiculosSiniestros
            //.Include(p => p.Fotos)
           .Where(o => ((o.GRUPO == Grupo) && (o.CAUSANTE == Causante) && (o.FECHACARGA.AddDays(30) >= DateTime.Now))
           )

           .OrderBy(o => o.FECHACARGA)
           .ToListAsync();


            if (siniestros == null)
            {
                return BadRequest("No hay Siniestros.");
            }
            return Ok(siniestros);
        }

        [HttpPost]
        [Route("PostSiniestros")]
        public async Task<IActionResult> PostSiniestros([FromBody] VehiculosSiniestroRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehiculoSiniestro = new VehiculosSiniestro
            {
                //NROREGISTRO = request.NROREGISTRO,
                FECHACARGA = request.FECHACARGA,
                CAUSANTE =request.CAUSANTE,
                GRUPO = request.GRUPO,
                ALTURA = request.ALTURA,
                APELLIDONOMBRETERCERO = request.APELLIDONOMBRETERCERO,
                CANTIDADLESIONADOS = request.CANTIDADLESIONADOS,
                CIUDAD = request.CIUDAD,
                EMAILTERCERO = request.EMAILTERCERO,
                HORASINIESTRO = request.HORASINIESTRO,
                INTERVINOAMBULANCIA = request.INTERVINOAMBULANCIA,
                INTERVINOPOLICIA = request.INTERVINOPOLICIA,
                LESIONADOS = request.LESIONADOS,
                NOTIFICADOA = request.NOTIFICADOA,
                NOTIFICADOEMPRESA = request.NOTIFICADOEMPRESA,
                NROPOLIZATERCERO = request.NROPOLIZATERCERO,
                PROVINCIA = request.PROVINCIA,
                RELATOSINIESTRO = request.RELATOSINIESTRO,
                TELEFONOCONTACTOTERCERO = request.TELEFONOCONTACTOTERCERO,
                COMPANIASEGUROTERCERO = request.COMPANIASEGUROTERCERO,
                DIRECCIONSINIESTRO = request.DIRECCIONSINIESTRO,
                IDUSUARIOCARGA = request.IDUSUARIOCARGA,
                NUMCHA = request.NUMCHA,
            };
            _dataContext.VehiculosSiniestros.Add(vehiculoSiniestro);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}