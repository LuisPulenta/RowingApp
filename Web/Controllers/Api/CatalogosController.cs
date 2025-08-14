﻿using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
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
        [Route("GetCatalogosAysa")]
        public async Task<IActionResult> GetCatalogosAysa()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            System.Collections.Generic.List<Catalogo> catalogos = await _context.Catalogos
           .Where(o => (o.VerRequerimientosAPP == 1)
           && (o.Modulo == "Aysa") &&(o.catDeshabilitado==0))
           .OrderBy(o => o.catCatalogo)
           .ToListAsync();
            if (catalogos == null)
            {
                return BadRequest("No hay Catálogos.");
            }
            return Ok(catalogos);
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

        [HttpPost]
        [Route("GetCatalogosSuministros/{ProyectoModulo}")]
        public async Task<IActionResult> GetCatalogosSuministros(string ProyectoModulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            System.Collections.Generic.List<Catalogo> catalogos = await _context.Catalogos
           .Where(o => (o.VerEnSuministros == 1)
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
        [Route("GetCatalogosEnCalle/{ProyectoModulo}")]
        public async Task<IActionResult> GetCatalogosEnCalle(string ProyectoModulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            System.Collections.Generic.List<Catalogo> catalogos = await _context.Catalogos
           .Where(o => (o.VerEnCalle == 1)
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