using Microsoft.AspNetCore.Mvc;
using Web.Data;

namespace RowingApp.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatesController : ControllerBase
    {
        private readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStates()
        {
            return Ok(_context.States);
        }
    }
}