namespace DentalSystem.Web.Areas.Administrator.Controllers
{
    using DentalSystem.Web.Contants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(AreaConstanst.ADMINISTRATOR)]
    [Authorize(Roles = RolesContants.ADMINISTRATOR)]
    public abstract class AdministatorController : Controller
    {

    }
}