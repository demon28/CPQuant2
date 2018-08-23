using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Winner.Framework.MVC;
using Winner.Framework.MVC.Providers.Behavior;
using Winner.Framework.Utils;
using Winner.Mvc.Audit;

namespace CPQaunt2.APP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //TODO:上线时启用
            ProviderManager.RegisterAccountProvider(new UserAccountProvider());
            ProviderManager.RegisterRightProvider(new Winner.Platform.MVC.Provider.RightProvider());
            //TODO:系统配置：登录授权
            ProviderManager.RegisterLoginProvider(new OAuthLoginProvider("plsql", "6644077e55304506be6afc4916b46858", "basic_api", "aa77950a2b1d4a659a75efad86904801", null));
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            Log.Error(ex); //记录日志信息  
        }
    }
}
