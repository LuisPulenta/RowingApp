using RowingApp.Common.Requests;
using Web.Data;
using RowingApp.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account2Controller : ControllerBase
    {
        private readonly DataContext _dataContext;

        private readonly IImageHelper _imageHelper;

        public Account2Controller(DataContext dataContext, IImageHelper imageHelper)
        {
            _dataContext = dataContext;
            _imageHelper = imageHelper;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario([FromRoute] int id, [FromBody] UsuarioFirmaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.IDUsuario)
            {
                return BadRequest();
            }

            var oldUsuario = await _dataContext.Usuarios.FindAsync(request.IDUsuario);
            if (oldUsuario == null)
            {
                return BadRequest("El Usuario no existe.");
            }

            string imageFirmaId = oldUsuario.FirmaUsuario;
            if (request.ImageArrayFirmaUsuario != null && request.ImageArrayFirmaUsuario.Length > 0)
            {
                imageFirmaId = _imageHelper.UploadImage(request.ImageArrayFirmaUsuario, "ObrasSuministros");
            }

            oldUsuario.FirmaUsuario = imageFirmaId;

            _dataContext.Usuarios.Update(oldUsuario);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}