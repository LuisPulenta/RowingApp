using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Data;
using System.Collections.Generic;
using System.Linq;

namespace RowingApp.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        

        public IEnumerable<SelectListItem> GetComboUserTypes()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Tipo de Usuario...]",
                Value = "0"
            });

            list.Insert(0, new SelectListItem
            {
                Text = "Admin",
                Value = "1"
            });

            list.Insert(0, new SelectListItem
            {
                Text = "User",
                Value = "2"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboStates()
        {
            List<SelectListItem> list = _context.States.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Value)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un estado...]",
                Value = "0"
            });
            return list;
        }
    }
}