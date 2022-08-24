using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CatalogosController : ControllerBase
    {
        private readonly DataContext _context;

        public CatalogosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetCatalogosEnergia")]
        public async Task<IActionResult> GetCatalogosEnergia()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            System.Collections.Generic.List<Catalogo> catalogos = await _context.Catalogos
           .Where(o => (o.VerEnReclamosApp == 1)
           && (o.Modulo == "Energia" || o.Modulo == "Compartido"))
           .OrderBy(o => o.catCatalogo)
           .ToListAsync();
            if (catalogos == null)
            {
                return BadRequest("No hay Catálogos.");
            }
            return Ok(catalogos);
        }

        [HttpGet]
        [Route("GetCatalogosRowing")]
        public async Task<IActionResult> GetCatalogosRowing()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            System.Collections.Generic.List<Catalogo> catalogos = await _context.Catalogos
           .Where(o => (o.VerEnReclamosApp == 1)
           && (o.Modulo == "Rowing" || o.Modulo == "Compartido"))
           .OrderBy(o => o.catCatalogo)
           .ToListAsync();
            if (catalogos == null)
            {
                return BadRequest("No hay Catálogos.");
            }
            return Ok(catalogos);
        }

        [HttpGet]
        [Route("GetCatalogosObrasTasa")]
        public async Task<IActionResult> GetCatalogosObrasTasa()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            System.Collections.Generic.List<Catalogo> catalogos = await _context.Catalogos
           .Where(o => (o.VerEnReclamosApp == 1)
           && (o.Modulo == "ObrasTasa" || o.Modulo == "Compartido"))
           .OrderBy(o => o.catCatalogo)
           .ToListAsync();
            if (catalogos == null)
            {
                return BadRequest("No hay Catálogos.");
            }
            return Ok(catalogos);
        }

        [HttpPost]
        [Route("GetCatalogos/{ProyectoModulo}")]
        public async Task<IActionResult> GetCatalogos(string ProyectoModulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            System.Collections.Generic.List<Catalogo> catalogos = await _context.Catalogos
           .Where(o => (o.VerEnReclamosApp == 1)
           && (o.Modulo == ProyectoModulo || o.Modulo == "Compartido"))
           .OrderBy(o => o.catCatalogo)
           .ToListAsync();
            if (catalogos == null)
            {
                return BadRequest("No hay Catálogos.");
            }
            return Ok(catalogos);
        }

        [HttpPost]
        [Route("GetCatalogosEPP/{ProyectoModulo}")]
        public async Task<IActionResult> GetCatalogosEPP(string ProyectoModulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            System.Collections.Generic.List<Catalogo> catalogos = await _context.Catalogos
           .Where(o => (o.VerRequerimientosEPP == 1)
           && (o.Modulo == ProyectoModulo || o.Modulo == "Compartido"))
           .OrderBy(o => o.catCatalogo)
           .ToListAsync();
            if (catalogos == null)
            {
                return BadRequest("No hay Catálogos.");
            }
            return Ok(catalogos);
        }

        [HttpPost]
        [Route("GetCatalogosAPP/{ProyectoModulo}")]
        public async Task<IActionResult> GetCatalogosAPP(string ProyectoModulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            System.Collections.Generic.List<Catalogo> catalogos = await _context.Catalogos
           .Where(o => (o.VerRequerimientosAPP == 1)
           && (o.Modulo == ProyectoModulo || o.Modulo == "Compartido"))
           .OrderBy(o => o.catCatalogo)
           .ToListAsync();
            if (catalogos == null)
            {
                return BadRequest("No hay Catálogos.");
            }
            return Ok(catalogos);
        }

    }
}