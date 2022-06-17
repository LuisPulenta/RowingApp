using GenericApp.Common.Requests;
using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSesionsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public WebSesionsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostWebSesion([FromBody] WebSesio request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.WebSesion.Add(request);
            await _dataContext.SaveChangesAsync();

            var newWebSesion = await _dataContext.WebSesion
                .Where(a => a.NROCONEXION == request.NROCONEXION)
                .ToListAsync();

            var response = new List<WebSesionRequest>(newWebSesion.Select(o => new WebSesionRequest
            {
                CONECTAVERAGE = o.CONECTAVERAGE,
                ID_WS = o.ID_WS,
                IP = o.IP,
                LOGINDATE = o.LOGINDATE,
                LOGINTIME = o.LOGINTIME,
                LOGOUTDATE = o.LOGOUTDATE,
                LOGOUTTIME = o.LOGOUTTIME,
                MODULO = o.MODULO,
                NROCONEXION = o.NROCONEXION,
                USUARIO = o.USUARIO,
                VersionSistema=o.VersionSistema

            }).ToList());


            return Ok(response);
        }

              
        [HttpPut("{id}")]
        [Route("PutWebSesion")]
        public async Task<IActionResult> PutWebSesion(int id)
        {
            WebSesio oldWebSesion = await _dataContext.WebSesion
                .FirstOrDefaultAsync(t => t.NROCONEXION == id);

            if (oldWebSesion == null)
            {
                return BadRequest("WebSesion no existe.");
            }

            oldWebSesion.LOGOUTDATE = DateTime.Now;
            oldWebSesion.LOGOUTTIME = (DateTime.Now.Hour*3600 + DateTime.Now.Minute*60 + DateTime.Now.Second)*100 ;
            
            _dataContext.WebSesion.Update(oldWebSesion);
            await _dataContext.SaveChangesAsync();
            return Ok(true);
        }


    }
}