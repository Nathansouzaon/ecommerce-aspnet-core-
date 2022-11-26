using App.WebApp.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApp.Mvc.Extensions
{
    public class PaginacaoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedList modeloPaginado) => View(modeloPaginado);
    }
}
