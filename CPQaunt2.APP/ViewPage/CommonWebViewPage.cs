using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Winner.Framework.MVC;
using Winner.Framework.Utils;

namespace CPQaunt2.APP
{
    public abstract class CommonWebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public SiteConfig Site { get; set; }

        public new SiteUser User { get; set; }

        public ViewUtility<TModel> Utility { get; set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            InitSiteUser();
            InitUtility();
        }

        private void InitSiteUser()
        {
            if (User == null)
            {
                User = new SiteUser();
                if (Winner.Framework.MVC.ApplicationContext.Current.IsLogined)
                {
                    User.IsLogin = true;
                    User.UserId = ApplicationContext.Current.User.UserId;
                    User.UserName = ApplicationContext.Current.User.UserName;
                    User.UserCode = ApplicationContext.Current.User.UserCode.ToMask();
                    User.Nickname = ApplicationContext.Current.User.Nickname;
                    User.EMail = ApplicationContext.Current.User.EMail;
                    User.IdentityAuth = ApplicationContext.Current.User.AuthStatus == 1;
                    User.SexText = ApplicationContext.Current.User.Sex.Switch(0, "女", "男");
                    User.Avatar = "../../Resources/img/user.png";
                }
            }
        }

        public void InitSiteConfig()
        {
            if (Site == null)
            {
                Site = new SiteConfig()
                {
                    SiteName = "会员中心",
                    UserSystemDomain = ConfigProvider.GetAppSetting("User.Domain"),
                    SSODomain = ConfigProvider.GetAppSetting("SSO.Domain"),
                    ShopDemain = ConfigProvider.GetAppSetting("Shop.Domain"),
                };
                Site.SSOLogin = Site.SSOUrl("account/login?service=" + HttpUtility.UrlEncode(this.Request.Url.AbsoluteUri));
                Site.SSOlogout = Site.SSOUrl("account/logout");
                Site.SSORegister = Site.SSOUrl("account/register?service=" + HttpUtility.UrlEncode(this.Request.Url.AbsoluteUri));
            }
        }

        private void InitUtility()
        {
            if (Utility == null)
            {
                Utility = new ViewUtility<TModel>(this);
            }
        }
    }

    public abstract class CommonWebViewPage : CommonWebViewPage<dynamic>
    {

    }

    public class SiteConfig
    {
        public string UserSystemUrl(string url)
        {
            return UserSystemDomain + url;
        }
        public string SSOUrl(string url)
        {
            return SSODomain + url;
        }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 用户中心域名
        /// </summary>
        public string UserSystemDomain { get; set; }

        /// <summary>
        /// SSO域名
        /// </summary>
        public string SSODomain { get; set; }

        /// <summary>
        /// SSO登陆地址
        /// </summary>
        public string SSOLogin { get; set; }

        /// <summary>
        /// SSO退出地址
        /// </summary>
        public string SSOlogout { get; set; }

        public string SSORegister { get; set; }
        public string ShopDemain { get; set; }
    }

    public class SiteUser
    {
        public bool IsLogin { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserCode { get; set; }

        public string Nickname { get; set; }
        public string EMail { get; set; }
        public bool IdentityAuth { get; set; }

        public string SexText { get; set; }
        public string Avatar { get; set; }
    }
}