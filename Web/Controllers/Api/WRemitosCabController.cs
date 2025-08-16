﻿using RowingApp.Common.Helpers;
using RowingApp.Common.Requests;
using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WRemitosCabController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IFilesHelper _filesHelper;

        public WRemitosCabController(DataContext context, IFilesHelper filesHelper)
        {
            _context = context;
            _filesHelper = filesHelper;
        }

        [HttpPost]
        [Route("PostWRemitosCab")]
        public async Task<IActionResult> PostWRemitosCab([FromBody] WRemitosCabAppRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

        var wRemitosCab = new WRemitosCab
            {
                NROOBRA = request.NROOBRA,
                FECHACARGA = DateTime.Now,
                CONTRATISTA = request.CONTRATISTA,
                IDUSUARIO = request.IDUSUARIO,
                CODGRUPOREC = request.CODGRUPOREC,
                CODCAUSANTEREC = request.CODCAUSANTEREC,
                CODCONCEPTO = request.CODCONCEPTO,
                PRIORIDAD = request.PRIORIDAD,
                FALTAMATERIAL = request.FALTAMATERIAL,
                DESPACHADO = request.DESPACHADO,
                PORDIFERENCIA = request.PORDIFERENCIA,
                ENTREGADOCONTRATISTA = "No",
                MODULO = request.MODULO,
                COBRADO602 = request.COBRADO602,
                NROOP = request.NROOP,
                VALORIZACION = request.VALORIZACION,
                TIPO="N",
                CODGRUPOC= request.CODGRUPOC,
                CODCAUSANTEC = request.CODCAUSANTEC,
        };

            _context.WRemitosCab.Add(wRemitosCab);
            await _context.SaveChangesAsync();

            return Ok(wRemitosCab);
        }

        [HttpPost]
        [Route("PostWRemitosDet")]
        public async Task<IActionResult> PostWRemitosDet([FromBody] WRemitosDetAppRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            WRemitosDet wRemitosDet = new WRemitosDet
            {
                NROREMITOCAB = request.NROREMITOCAB,
                NROOBRA = request.NROOBRA,
                catCodigo = request.catCodigo,
                catCatalogo = request.catCatalogo,
                CodigoSap = request.CodigoSap,
                Cantidad = request.Cantidad,
                OBSERVACION="",
                NRORESERVA=0,
                NROGRAFO="",
                TAG = request.TAG,
                COSTOUNIT = request.COSTOUNIT,
                COSTOTOTAL = request.COSTOTOTAL,
            };

            _context.WRemitosDet.Add(wRemitosDet);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("GetNroRemitoMax")]
        public IActionResult GetNroRemitoMax()
        {
            int query = _context.WRemitosCab.Max(c => c.NROREMITO);

            return Ok(query);
        }
    }
}

