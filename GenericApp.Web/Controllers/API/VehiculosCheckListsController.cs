﻿using GenericApp.Web.Data;
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
    public class VehiculosCheckListsController : ControllerBase
    {
        private readonly DataContext2 _dataContext;


        public VehiculosCheckListsController(DataContext2 dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetVehiculosCheckLists/{iDUser}")]
        public async Task<IActionResult> GetNovedades(int iDUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var checkLists = await _dataContext.VehiculosCheckLists
           .Where(o => ((o.IDUser == iDUser) && (o.Fecha.AddDays(7) >= DateTime.Now))
           )

           .OrderBy(o => o.IDCheckList)
           .ToListAsync();


            if (checkLists == null)
            {
                return BadRequest("No hay Check Lists.");
            }
            return Ok(checkLists);
        }

        [HttpPost]
        [Route("PostVehiculosCheckLists")]
        public async Task<IActionResult> PostVehiculosCheckLists([FromBody] VehiculosCheckList request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dataContext.VehiculosCheckLists.Add(request);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculosCheckList([FromRoute] int id, [FromBody] VehiculosCheckList request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.IDCheckList)
            {
                return BadRequest();
            }

            var oldVehiculosCheckList = await _dataContext.VehiculosCheckLists.FindAsync(request.IDCheckList);
            if (oldVehiculosCheckList == null)
            {
                return BadRequest("El CHeck List no existe.");
            }

            oldVehiculosCheckList.AlarmaRetroceso = request.AlarmaRetroceso;
            oldVehiculosCheckList.ApellidoNombre = request.ApellidoNombre;
            oldVehiculosCheckList.ApoyaCabezas = request.ApoyaCabezas;
            oldVehiculosCheckList.DispositivoPAT = request.DispositivoPAT;
            oldVehiculosCheckList.DNI = request.DNI;
            oldVehiculosCheckList.Espejos = request.Espejos;
            oldVehiculosCheckList.IdentificadorEmpresa = request.IdentificadorEmpresa;
            oldVehiculosCheckList.IDUser = request.IDUser;
            oldVehiculosCheckList.IDVehiculo = request.IDVehiculo;
            oldVehiculosCheckList.IndicadoresDeGiro = request.IndicadoresDeGiro;
            oldVehiculosCheckList.JefeDirecto = request.JefeDirecto;
            oldVehiculosCheckList.Limpiavidrios = request.Limpiavidrios;
            oldVehiculosCheckList.LuzEmergencia = request.LuzEmergencia;
            oldVehiculosCheckList.LuzFreno = request.LuzFreno;
            oldVehiculosCheckList.LuzPosicion = request.LuzPosicion;
            oldVehiculosCheckList.LuzRetroceso = request.LuzRetroceso;
            oldVehiculosCheckList.ManguerasCircuitoHidraulico = request.ManguerasCircuitoHidraulico;
            oldVehiculosCheckList.Matafuegos = request.Matafuegos;
            oldVehiculosCheckList.Observaciones = request.Observaciones;
            oldVehiculosCheckList.ResponsableVehiculo = request.ResponsableVehiculo;
            oldVehiculosCheckList.Seguro = request.Seguro;
            oldVehiculosCheckList.SobreSalientesPeligro = request.SobreSalientesPeligro;
            oldVehiculosCheckList.VTH = request.VTH;
            oldVehiculosCheckList.VTV = request.VTV;

            _dataContext.VehiculosCheckLists.Update(oldVehiculosCheckList);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}