using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using fPeerLending.Entities;
using ffWeb.UI.MVC.fMessagingServiceReference;


namespace ffWeb.UI.MVC.Helpers
{
    public static class MyHelpers
    {
        // Render BootStrap menu item with active class
        public static MvcHtmlString MenuItem(this HtmlHelper htmlHelper,
                                             string text, string action,
                                             string controller,
                                             object routeValues = null,
                                             object htmlAttributes = null)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentAction,
                              action,
                              StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController,
                              controller,
                              StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }
            if (routeValues != null)
            {
                li.InnerHtml = (htmlAttributes != null)
                    ? htmlHelper.ActionLink(text,
                                            action,
                                            controller,
                                            routeValues,
                                            htmlAttributes).ToHtmlString()
                    : htmlHelper.ActionLink(text,
                                            action,
                                            controller,
                                            routeValues).ToHtmlString();
            }
            else
            {
                li.InnerHtml = (htmlAttributes != null)
                    ? htmlHelper.ActionLink(text,
                                            action,
                                            controller,
                                            null,
                                            htmlAttributes).ToHtmlString()
                    : htmlHelper.ActionLink(text,
                                            action,
                                            controller).ToHtmlString();
            }
            return MvcHtmlString.Create(li.ToString());
        }


        // As the text the: "<span class='glyphicon glyphicon-plus'></span>" can be entered
        public static MvcHtmlString NoEncodeActionLink(this HtmlHelper htmlHelper,
                                             string text, string title, string action,
                                             string controller,
                                             object routeValues = null,
                                             object htmlAttributes = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = text;
            builder.Attributes["title"] = title;
            builder.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));

            return MvcHtmlString.Create(builder.ToString());
        }

    }

    public static class Messager
    {
        public static void SendSMS(string message, string Telno)
        {
            MessagingServiceClient client = new MessagingServiceClient();

            // Use the 'client' variable to call operations on the service.
            client.SendSmsAsync(message, Telno);

            // Always close the client.
            client.Close();
            
        }

        public static void SendEmail(string message, string addressTo)
        {
            MessagingServiceClient client = new MessagingServiceClient();

            // Use the 'client' variable to call operations on the service.
            client.SendEmailAsync(addressTo,message);

            // Always close the client.
            client.Close();
        }

        public static void Inform(Member member, string message)
        {
            switch (member.InformBy.ToUpper())
            {
                case "SMS":
                    SendSMS(message, member.Telephone);
                    break;
                case "EMAIL":
                    SendEmail(message, member.Email);
                    break;
                case "SMSEMAIL":
                    SendSMS(message, member.Telephone);
                    SendEmail(message, member.Email);
                    break;
            }
        }
    }
}