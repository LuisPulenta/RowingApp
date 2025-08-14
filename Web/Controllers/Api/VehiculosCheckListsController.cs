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
    public class VehiculosCheckListsController : ControllerBase
    {
        private readonly DataContext2 _dataContext;


        public VehiculosCheckListsController(DataContext2 dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetVehiculosCheckLists/{iDUser}")]
        public async Task<IActionResult> GetVehiculosCheckLists(int iDUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var checkLists = await _dataContext.VistaFlotasChecklistAPP
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
            _dataContext.VehiculosCheckLists.Update(request);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
                
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculosCheckList([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var checkList = await _dataContext.VehiculosCheckLists
                .FirstOrDefaultAsync(p => p.IDCheckList == id);
            if (checkList == null)
            {
                return this.NotFound();
            }

            _dataContext.VehiculosCheckLists.Remove(checkList);
            await _dataContext.SaveChangesAsync();
            return Ok("Check List borrado");
        }
    }
}