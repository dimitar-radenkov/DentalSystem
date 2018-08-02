namespace DentalSystem.Web.Areas.Administrator.Controllers
{
    using DentalSystem.Web.Contants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(Areas.ADMINISTRATOR)]
    [Authorize(Roles = Roles.ADMINISTRATOR)]
    public abstract class AdministatorController : Controller
    {
        
    }
}