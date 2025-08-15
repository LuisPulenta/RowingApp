using RowingApp.Common.Helpers;
using RowingApp.Common.Requests;
using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosSiniestrosController : ControllerBase
    {
        private readonly DataContext2 _dataContext;
        private readonly IFilesHelper _filesHelper;

        public VehiculosSiniestrosController(DataContext2 dataContext, IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
        }

        [HttpPost]
        [Route("GetSiniestros/{grupo}/{causante}")]
        public async Task<IActionResult> GetSiniestros(string Grupo, string Causante)
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
                CAUSANTE = request.CAUSANTE,
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
                DETALLEDANOSTERCERO = request.DETALLEDANOSTERCERO,
                DETALLEDANOSPROPIO = request.DETALLEDANOSPROPIO,
                NUMCHATERCERO = request.NUMCHATERCERO,
                FECHACARGAAPP = DateTime.Now,
                Modulo = request.Modulo,
                TipoDeSiniestro=request.TipoDeSiniestro,    
            };
            _dataContext.VehiculosSiniestros.Add(vehiculoSiniestro);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSiniestro([FromRoute] int id, [FromBody] VehiculosSiniestroRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.NROSINIESTRO)
            {
                return BadRequest();
            }



            var oldSiniestro = await _dataContext.VehiculosSiniestros.FindAsync(request.NROSINIESTRO);
            if (oldSiniestro == null)
            {
                return BadRequest("El Causante no existe.");
            }

            oldSiniestro.ALTURA = request.ALTURA;
            oldSiniestro.APELLIDONOMBRETERCERO = request.APELLIDONOMBRETERCERO;
            oldSiniestro.CANTIDADLESIONADOS = request.CANTIDADLESIONADOS;
            oldSiniestro.CAUSANTE = request.CAUSANTE;
            oldSiniestro.CIUDAD = request.CIUDAD;
            oldSiniestro.COMPANIASEGUROTERCERO = request.COMPANIASEGUROTERCERO;
            oldSiniestro.DIRECCIONSINIESTRO = request.DIRECCIONSINIESTRO;
            oldSiniestro.DETALLEDANOSPROPIO = request.DETALLEDANOSPROPIO;
            oldSiniestro.DETALLEDANOSTERCERO = request.DETALLEDANOSTERCERO;
            oldSiniestro.EMAILTERCERO = request.EMAILTERCERO;
            oldSiniestro.FECHACARGA = request.FECHACARGA;
            oldSiniestro.FECHACARGAAPP = DateTime.Now;
            oldSiniestro.GRUPO = request.GRUPO;
            oldSiniestro.HORASINIESTRO = request.HORASINIESTRO;
            oldSiniestro.IDUSUARIOCARGA = request.IDUSUARIOCARGA;
            oldSiniestro.INTERVINOAMBULANCIA = request.INTERVINOAMBULANCIA;
            oldSiniestro.INTERVINOPOLICIA = request.INTERVINOPOLICIA;
            oldSiniestro.LESIONADOS = request.LESIONADOS;
            oldSiniestro.NOTIFICADOA = request.NOTIFICADOA;
            oldSiniestro.NOTIFICADOEMPRESA = request.NOTIFICADOEMPRESA;
            oldSiniestro.NROPOLIZATERCERO = request.NROPOLIZATERCERO;
            oldSiniestro.NROSINIESTRO = request.NROSINIESTRO;
            oldSiniestro.NUMCHA = request.NUMCHA;
            oldSiniestro.NUMCHATERCERO = request.NUMCHATERCERO;
            oldSiniestro.PROVINCIA = request.PROVINCIA;
            oldSiniestro.RELATOSINIESTRO = request.RELATOSINIESTRO;
            oldSiniestro.TELEFONOCONTACTOTERCERO = request.TELEFONOCONTACTOTERCERO;
            oldSiniestro.Modulo = request.Modulo;
            oldSiniestro.TipoDeSiniestro=request.TipoDeSiniestro;   

            _dataContext.VehiculosSiniestros.Update(oldSiniestro);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}