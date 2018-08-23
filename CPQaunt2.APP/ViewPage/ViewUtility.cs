using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPQaunt2.APP
{
    public class ViewUtility<TModel>
    {
        public ViewUtility(WebViewPage<TModel> page)
        {
            Page = page;
        }
        System.Web.Mvc.WebViewPage<TModel> Page { get; set; }

        public IHtmlString ToJson(object model)
        {
            IsoDateTimeConverter dataTimeConvert = new IsoDateTimeConverter();
            dataTimeConvert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            JsonSerializerSettings Settings = new JsonSerializerSettings();
            Settings.Converters = new List<JsonConverter>();
            Settings.Converters.Add(dataTimeConvert);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(model, Settings);

            return Page.Html.Raw(json);
        }

        public string Concat(params object[] args)
        {
            return string.Concat(args);
        }

        public bool IsActive(string controller, string action)
        {
            if (string.IsNullOrEmpty(controller) && string.IsNullOrEmpty(action))
            {
                return false;
            }
            var currentController = Page.ViewContext.RequestContext.RouteData.Values["controller"] + string.Empty;
            var currentAction = Page.ViewContext.RouteData.Values["action"] + string.Empty;

            bool sameController = currentController.Equals(controller, StringComparison.OrdinalIgnoreCase);
            bool emptyAction = string.IsNullOrEmpty(action);
            bool sameAction = currentAction.Equals(action, StringComparison.OrdinalIgnoreCase);
            bool res = (sameController && emptyAction) || (sameController && !emptyAction && sameAction);
            return res ? true : false;
        }
    }
}