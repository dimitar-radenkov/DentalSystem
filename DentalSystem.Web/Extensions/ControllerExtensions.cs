namespace DentalSystem.Web.Extensions
{
    using DentalSystem.Common.Contants;
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static void AddSuccessMessage(this Controller controller, string message)
        {
            controller.TempData[Notifications.MESSAGE_TYPE_KEY] = Notifications.SUCCESS_MESSAGE_TYPE;
            controller.TempData[Notifications.MESSAGE_KEY] = message;
        }

        public static void AddWarningMessage(this Controller controller, string message)
        {
            controller.TempData[Notifications.MESSAGE_TYPE_KEY] = Notifications.WARNING_MESSAGE_TYPE;
            controller.TempData[Notifications.MESSAGE_KEY] = message;
        }

        public static void AddDangerMessage(this Controller controller, string message)
        {
            controller.TempData[Notifications.MESSAGE_TYPE_KEY] = Notifications.DANGER_MESSAGE_TYPE;
            controller.TempData[Notifications.MESSAGE_KEY] = message;
        }
    }
}
