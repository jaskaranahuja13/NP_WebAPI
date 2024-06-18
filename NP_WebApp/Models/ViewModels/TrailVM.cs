using Microsoft.AspNetCore.Mvc.Rendering;
using NP_WebApp.Models;

namespace NP_WebAPP.Models.ViewModels
{
    public class TrailVM
    {
        public Trail Trail { get; set; }
        public IEnumerable<SelectListItem> nationalParkList { get; set;}
    }
}
