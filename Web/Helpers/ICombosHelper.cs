using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace RowingApp.Web.Helpers
{
    public interface ICombosHelper
    {

        IEnumerable<SelectListItem> GetComboUserTypes();

        IEnumerable<SelectListItem> GetComboStates();
    }
}