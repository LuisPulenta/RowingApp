using RowingApp.Common.Helpers;
using RowingApp.Common.Requests;
using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
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
                            //r.ImageFullPath,
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
                             //ImageFullPath = g.Key.ImageFullPath,
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
                FECHARECUPERO = DateTime.Now,
                IDUSERCARGA = request.IDUSERCARGA,
                IDUSERRECUPERA = request.IDUSERRECUPERA,
                LINKFOTO=imageUrl,
                DOMICILIO = request.DOMICILIO,
                FECHA = DateTime.Now,
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


        [HttpPut]
        [Route("PutElementosEnCalleCab/{id}")]
        public async Task<IActionResult> PutElementosEnCalleCab([FromRoute] int id, [FromBody] ElemEnCalleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.ID)
            {
                return BadRequest();
            }
            var oldElemEnCalleCab = await _dataContext.ElementosEnCalle.FindAsync(request.ID);
            if (oldElemEnCalleCab == null)
            {
                return BadRequest("El registro no existe.");
            }

            //Foto
            string imageUrl = oldElemEnCalleCab.LINKFOTO;
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

            oldElemEnCalleCab.GRXX = request.GRXX;
            oldElemEnCalleCab.GRYY = request.GRYY;
            oldElemEnCalleCab.NROOBRA = request.NROOBRA;
            oldElemEnCalleCab.DOMICILIO = request.DOMICILIO;
            oldElemEnCalleCab.OBSERVACION = request.OBSERVACION;
            oldElemEnCalleCab.LINKFOTO = imageUrl;
            oldElemEnCalleCab.ESTADO = request.ESTADO;
            oldElemEnCalleCab.FECHA = DateTime.Now;
            oldElemEnCalleCab.FECHARECUPERO = DateTime.Now;
            oldElemEnCalleCab.IDUSERCARGA = request.IDUSERCARGA;
            oldElemEnCalleCab.IDUSERRECUPERA = request.IDUSERRECUPERA;

            _dataContext.ElementosEnCalle.Update(oldElemEnCalleCab);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElementosEnCalleCab([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var elementosEnCalleCab = await _dataContext.ElementosEnCalle
                .FirstOrDefaultAsync(p => p.ID == id);
            if (elementosEnCalleCab == null)
            {
                return this.NotFound();
            }

            _dataContext.ElementosEnCalle.Remove(elementosEnCalleCab);
            await _dataContext.SaveChangesAsync();
            return Ok("Borrado");
        }

        [HttpGet]
        [Route("GetTotalesPorElemento")]
        public async Task<IActionResult> GetTotalesPorElemento()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var totalesPorElemento = await _dataContext.ElemenEnCalleVista
                     .Where(o => o.ESTADO == "PENDIENTE")
                     .OrderBy(o => o.ID)
                        .GroupBy(r => new
                        {
                            r.CATSIAG,
                            r.CATSAP,
                            r.Elemento
                        })
                         .Select(g => new
                         {
                             CATSIAG = g.Key.CATSIAG,
                             CATSAP = g.Key.CATSAP,
                             Elemento = g.Key.Elemento,
                             CANTDEJADA = g.Sum(s => s.CANTDEJADA),
                             
                         })
           .ToListAsync();
            if (totalesPorElemento == null)
            {
                return BadRequest("No hay Elementos En Calle.");
            }
            return Ok(totalesPorElemento);
        }
    }
}