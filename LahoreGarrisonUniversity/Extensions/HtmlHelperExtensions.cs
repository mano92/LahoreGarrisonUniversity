using System.Linq;
using System.Web.Mvc;

namespace LahoreGarrisonUniversity.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string IfActive(this HtmlHelper helper, params string[] values)
        {
            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();

            if (values.Any(controller => currentController == controller))
            {
                return "active";
            }

            return string.Empty;
        }

        public static string IfActive(this HtmlHelper helper, string controller, string action)
        {
            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

            if (currentController == controller && currentAction == action)
            {
                return "active";
            }

            return string.Empty;
        }
        public static string ActiveList(this HtmlHelper helper, params string[] values)
        {
            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            //string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

            if (values.Any(controller => currentController == controller))
            {
                return "pointer";
            }

            return string.Empty;
        }

        public static string ActiveDropDown(this HtmlHelper helper, params string[] values)
        {
            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            //string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

            foreach (var controller in values)
            {
                if (currentController == controller)
                {
                    //StringBuilder stringBuilder = new StringBuilder();
                    //stringBuilder.Append(@"<div class=\"pointer\">");
                    //return "<div class="pointer"><div class="arrow"></div><div class="arrow_border"></div></div>";
                }
            }

            return string.Empty;
        }
    }
}