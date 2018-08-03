namespace DentalSystem.Web.Extensions
{
    using DentalSystem.Common.Contants;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public static class PageExtensions
    {
        public static void AddSuccessMessage(this PageModel pagemodel, string message)
        {
            pagemodel.TempData[Notifications.MESSAGE_TYPE_KEY] = Notifications.SUCCESS_MESSAGE_TYPE;
            pagemodel.TempData[Notifications.MESSAGE_KEY] = message;
        }

        public static void AddWarningMessage(this PageModel pagemodel, string message)
        {
            pagemodel.TempData[Notifications.MESSAGE_TYPE_KEY] = Notifications.WARNING_MESSAGE_TYPE;
            pagemodel.TempData[Notifications.MESSAGE_KEY] = message;
        }

        public static void AddDangerMessage(this PageModel pagemodel, string message)
        {
            pagemodel.TempData[Notifications.MESSAGE_TYPE_KEY] = Notifications.DANGER_MESSAGE_TYPE;
            pagemodel.TempData[Notifications.MESSAGE_KEY] = message;
        }
    }
}