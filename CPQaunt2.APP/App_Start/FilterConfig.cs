using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC;

namespace CPQaunt2.APP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //TODO:上线时启用
            filters.Add(new GlobalErrorAttribute());
          // filters.Add(new Winner.Framework.MVC.AuthRightAttribute());
        }
    }
}
