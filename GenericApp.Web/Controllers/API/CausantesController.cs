﻿using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using GenericApp.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CausantesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly DataContext2 _dataContext2;
        private readonly IImageHelper _imageHelper;

        public CausantesController(DataContext dataContext,DataContext2 dataContext2,IImageHelper imageHelper)
        {
            _dataContext = dataContext;
            _dataContext2 = dataContext2;
            _imageHelper = imageHelper;
        }

        [HttpPost]
        [Route("GetCausanteByCodigo")]
        public async Task<IActionResult> GetCausante(CausanteRequest codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Data.Entities.Causante user = await _dataContext2.Causantes.FirstOrDefaultAsync
                (o => (o.codigo.ToLower() == codigo.Codigo.ToLower() || o.NroSAP.ToLower() == codigo.Codigo.ToLower()) && (o.grupo == "PPR" || o.grupo == "PPC"));

            if (user == null)
            {
                return BadRequest("El Empleado no existe.");
            }

            CausanteResponse response = new CausanteResponse
            {
                codigo = user.codigo,
                nombre = user.nombre,
                encargado = user.encargado,
                NroCausante = user.NroCausante,
                telefono = user.telefono,
                NroSAP=user.NroSAP,
                grupo=user.grupo,
                estado=user.estado,
                LinkFoto=user.LinkFoto,
                direccion = user.direccion,
                Numero = user.Numero,
                TelefonoContacto1 = user.TelefonoContacto1,
                TelefonoContacto2 = user.TelefonoContacto2,
                TelefonoContacto3 = user.TelefonoContacto3,
                fecha = user.fecha,
                NotasCausantes = user.NotasCausantes,
                ciudad=user.ciudad,
                Provincia = user.Provincia,
                CodigoSupervisorObras=user.CodigoSupervisorObras,
                ZonaTrabajo=user.ZonaTrabajo,
                NombreActividad=user.NombreActividad,
                notas=user.notas
            };

            return Ok(response);
        }

        // GET: api/Users/5
        [HttpGet("GetCausanteByCodigo2/{codigo}")]
        public async Task<ActionResult<Data.Entities.Causante>> GetCausante2(string codigo)
        {
            Data.Entities.Causante causante = await _dataContext2.Causantes
                .FirstOrDefaultAsync(o => (o.codigo.ToLower() == codigo.ToLower() || o.NroSAP.ToLower() == codigo.ToLower()) && (o.grupo == "PPR" || o.grupo == "PPC"));

            if (causante == null)
            {
                return NotFound();
            }
            return causante;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCausante([FromRoute] int id, [FromBody] CausanteRequest2 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            

            var oldCausante = await _dataContext2.Causantes.FindAsync(request.Id);
            if (oldCausante == null)
            {
                return BadRequest("El Causante no existe.");
            }

            string imageId = oldCausante.LinkFoto;
            if (request.Image != null && request.Image.Length > 0)
            {
                imageId = _imageHelper.UploadImage(request.Image, "Causantes");
            }

            oldCausante.telefono = request.telefono;
            oldCausante.LinkFoto = imageId;
            oldCausante.direccion = request.direccion;
            oldCausante.Numero = request.Numero;
            oldCausante.TelefonoContacto1 = request.TelefonoContacto1;
            oldCausante.TelefonoContacto2 = request.TelefonoContacto2;
            oldCausante.TelefonoContacto3 = request.TelefonoContacto3;
            oldCausante.fecha = request.fecha;
            oldCausante.NotasCausantes = request.NotasCausantes;
            oldCausante.ciudad = request.ciudad;
            oldCausante.Provincia = request.Provincia;
            oldCausante.ZonaTrabajo = request.ZonaTrabajo;
            oldCausante.NombreActividad = request.ZonaTrabajo;

            _dataContext2.Causantes.Update(oldCausante);
            await _dataContext2.SaveChangesAsync();
            return Ok();
        }


        [HttpPost]
        [Route("GetCausantesByGrupo/{Grupo}")]
        public IActionResult GetCausantesByProyectoModulo(string Grupo)
        {
            return Ok(_dataContext2.Causantes
                .Where(o => o.grupo == Grupo && o.estado==true)
                .OrderBy(o => o.nombre)
                );
        }

        [HttpPost]
        [Route("GetCausantesBySupervisor/{id}")]
        public IActionResult GetCausantesBySupervisor(int id)
        {
            return Ok(_dataContext2.Causantes
                .Where(o => o.CodigoSupervisorObras == id)
                .OrderBy(o => o.nombre)
                );
        }

        [HttpPost]
        [Route("GetCausantesEstados")]
        public IActionResult GetCausantesEstados()
        {
            return Ok(_dataContext2.CausantesEstados
                .Where(o => o.SoloAPP == 1)
                .OrderBy(o => o.NOMENCLADORESTADO)
                );
        }

        [HttpPost]
        [Route("GetCausantesZonas")]
        public IActionResult GetCausantesZonas()
        {
            return Ok(_dataContext2.CausantesZonasZonas
                
                .OrderBy(o => o.NOMBREZONA)
                );
        }

        [HttpPost]
        [Route("PostPresentismos")]
        public async Task<IActionResult> PostPresentismos([FromBody] CausantesPresentismo request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dataContext.CausantesPresentismos.Add(request);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}