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
    public class ElementosEnCalleCabController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFilesHelper _filesHelper;

        public ElementosEnCalleCabController(DataContext dataContext,IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
        }

        [HttpGet]
        [Route("GetElementosEnCalleCab")]
        public async Task<IActionResult> GetElementosEnCalleCab()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var elementosEnCalleCab = await _dataContext.ElemenEnCalleVista
                     .Where(o => o.ESTADO == "PENDIENTE")
                     .OrderBy(o => o.ID)
                        .GroupBy(r => new
                        {
                            r.IDELEMENTOCAB,
                            r.IDUSERCARGA,
                            r.NombreCarga,
                            r.ApellidoCarga,
                            r.FechaCarga,
                            r.NROOBRA,
                            r.NombreObra,
                            r.OBSERVACION,
                            r.GRXX,
                            r.GRYY,
                            r.DOMICILIO,
                            r.LINKFOTO,
                            r.ImageFullPath,
                            r.ESTADO
                        })
                         .Select(g => new
                         {
                             IDELEMENTOCAB = g.Key.IDELEMENTOCAB,
                             IDUSERCARGA = g.Key.IDUSERCARGA,
                             NombreCarga = g.Key.NombreCarga,
                             ApellidoCarga = g.Key.ApellidoCarga,
                             FechaCarga = g.Key.FechaCarga,
                             NROOBRA = g.Key.NROOBRA,
                             NombreObra = g.Key.NombreObra,
                             OBSERVACION = g.Key.OBSERVACION,
                             GRXX = g.Key.GRXX,
                             GRYY = g.Key.GRYY,
                             DOMICILIO = g.Key.DOMICILIO,
                             LINKFOTO = g.Key.LINKFOTO,
                             ImageFullPath = g.Key.ImageFullPath,
                             ESTADO = g.Key.ESTADO,
                             CantItems = g.Count(),
                         })
           .ToListAsync();
            if (elementosEnCalleCab == null)
            {
                return BadRequest("No hay Elementos En Calle.");
            }
            return Ok(elementosEnCalleCab);
        }


        [HttpPost]
        [Route("PostElementosEnCalleCab")]
        public async Task<IActionResult> PostElementosEnCalleCab([FromBody] ElemEnCalleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Foto
            string imageUrl = string.Empty;
            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\ElemEnCalle";
                var fullPath = $"~/images/ElemEnCalle/{file}";
                var response = _filesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }
            
            var elemEnCalleCab = new ElementosEnCalleCab
            
            {
                ESTADO = request.ESTADO,
                FECHARECUPERO = request.FECHARECUPERO,
                IDUSERCARGA = request.IDUSERCARGA,
                IDUSERRECUPERA = request.IDUSERRECUPERA,
                LINKFOTO=imageUrl,
                DOMICILIO = request.DOMICILIO,
                FECHA = request.FECHA,
                NROOBRA = request.NROOBRA,
                OBSERVACION = request.OBSERVACION,
                GRXX=request.GRXX,
                GRYY = request.GRYY,
            };
            _dataContext.ElementosEnCalle.Add(elemEnCalleCab);
            await _dataContext.SaveChangesAsync();

            int query = _dataContext.ElementosEnCalle.Max(c => c.ID);

            return Ok(query);
        }
    }
}