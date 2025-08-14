using RowingApp.Common.Requests;
using Web.Data;
using RowingApp.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account3Controller : ControllerBase
    {
        private readonly DataContext2 _dataContext2;
        private readonly IImageHelper _imageHelper;

        public Account3Controller(DataContext2 dataContext2, IImageHelper imageHelper)
        {
            _dataContext2 = dataContext2;
            _imageHelper = imageHelper;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario([FromRoute] int id, [FromBody] CausanteFirmaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.NroCausante)
            {
                return BadRequest();
            }



            var oldUsuario = await _dataContext2.Causantes.FindAsync(request.NroCausante);
            if (oldUsuario == null)
            {
                return BadRequest("El Causante no existe.");
            }

            string imageFirmaId = oldUsuario.FirmaDigitalAPP;
            if (request.ImageArrayFirmaUsuario != null && request.ImageArrayFirmaUsuario.Length > 0)
            {
                imageFirmaId = _imageHelper.UploadImage(request.ImageArrayFirmaUsuario, "Recibos");
            }


            oldUsuario.FirmaDigitalAPP = imageFirmaId;


            _dataContext2.Causantes.Update(oldUsuario);
            await _dataContext2.SaveChangesAsync();
            return Ok(imageFirmaId);
        }
    }
}