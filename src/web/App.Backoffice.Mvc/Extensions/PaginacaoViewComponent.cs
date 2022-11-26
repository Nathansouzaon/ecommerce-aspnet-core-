using App.Backoffice.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Backoffice.Mvc.Extensions
{
    public class PaginacaoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedList modeloPaginado) => View(modeloPaginado);
    }
}
