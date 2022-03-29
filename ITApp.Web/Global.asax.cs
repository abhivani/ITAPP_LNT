using ITApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace ITApp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Private Method
        private string GenerateHashKey()
        {
            StringBuilder myStr = new StringBuilder();

            var userAgent = Request.UserAgent;
            if ((userAgent.Contains("Edge") || userAgent.Contains("Edg")) && Request.Browser.Browser != "Edge")
            {
                myStr.Append("Edge");
            }
            else
            {
                myStr.Append(Request.Browser.Browser);
            }
            myStr.Append(Request.Browser.Platform);
            myStr.Append(Request.Browser.MajorVersion);
            myStr.Append(Request.Browser.MinorVersion);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashdata = sha.ComputeHash(Encoding.UTF8.GetBytes(myStr.ToString()));
            return Convert.ToBase64String(hashdata);
        }
        #endregion

        #region Attribute-Session Check + Role Access

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class SessionTimeoutAttribute : ActionFilterAttribute, IAuthenticationFilter
        {
            public void OnAuthentication(AuthenticationContext filterContext)
            {
                //Helper.LogWrite("5) SessionTimeoutAttribute Check Session");
                if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session[Helper.SessionKey.UPSNO]))
               || string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session[Helper.SessionKey.AuthToken]))
               || string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Request.Cookies[Helper.SessionKey.AuthToken])))
                {
                    //Helper.LogWrite("6) SessionTimeoutAttribute Session null");
                    filterContext.Result = new HttpUnauthorizedResult();
                }
                else
                {
                    //Helper.LogWrite("6) SessionTimeoutAttribute Session not null : " + HttpContext.Current.Session[Helper.SessionKey.AuthToken]);
                    //-----------------Start--Change AuthToken Manully Network---------------------------//
                    if (!HttpContext.Current.Session[Helper.SessionKey.AuthToken].ToString().Equals(HttpContext.Current.Request.Cookies[Helper.SessionKey.AuthToken].Value))
                    {
                        //Helper.LogWrite("7) SessionTimeoutAttribute Session AuthToken Change Browser");
                        filterContext.Result = new HttpUnauthorizedResult();
                    }
                }
            }
            public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
            {
                if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
                {
                    //Helper.LogWrite("12) SessionTimeoutAttribute filterContext Result is null");
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.StatusCode = 401;
                        filterContext.HttpContext.Response.End();
                        // we simply have to tell mvc not to redirect to login page
                        filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                        return;
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Logout" } });
                    }
                }
            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class SessionAllowedRoleAttribute : AuthorizeAttribute
        {
            private readonly Enums.RoleType[] allowedroles;
            public SessionAllowedRoleAttribute(params Enums.RoleType[] roles)
            {
                this.allowedroles = roles;
            }
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                bool authorize = false;
                string _strUserRole = Convert.ToString(httpContext.Session[Helper.SessionKey.Role]);
                if (!string.IsNullOrEmpty(_strUserRole) && allowedroles != null && allowedroles.Length > 0)
                {
                    string[] _strarrRole = _strUserRole.Split(',');
                    if (_strarrRole.Any(x => allowedroles.Any(y => y.ToString() == x.ToString()))) authorize = true;
                }
                return authorize;
            }

            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.HttpContext.Response.End();
                    // we simply have to tell mvc not to redirect to login page
                    filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Logout" } });
                }
            }
        }
        #endregion

        #region Check URL Copy + Past Block
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class NoDirectAccessAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (filterContext.HttpContext.Request.UrlReferrer == null ||
                            filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
                {
                    string _strControllerAction = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower() + "_" + filterContext.ActionDescriptor.ActionName.ToLower();
                    if (_strControllerAction != "home_logout")
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Logout" } });
                        return;
                    }
                }
            }
        }
        #endregion

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //it removes the X-AspNetMvc-Version from the response header
            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_PreSendRequestHeaders()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
                HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
                HttpContext.Current.Response.Headers.Remove("Server");
                MvcHandler.DisableMvcResponseHeader = true;
            }
        }
        protected void Session_Start(Object sender, EventArgs e)
        {
            Session[Helper.SessionKey.init] = 0;
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Check If it is a new session or not, if not then do the further checks
            if (Request.Cookies["ASP.NET_SessionId"] != null && Request.Cookies["ASP.NET_SessionId"].Value != null && Request.Cookies["ASP.NET_SessionId"].Value != "")
            {
                bool _isError = false;
                string newSessionID = Request.Cookies["ASP.NET_SessionID"].Value;
                //Check the valid length of your Generated Session ID
                if (newSessionID.Length <= 24)
                {
                    //Log the attack details here
                    _isError = true;
                    //HttpContext.Current.RewritePath("~/Home/Logout");
                }
                else if (GenerateHashKey() != newSessionID.Substring(24))
                {
                    //Genrate Hash key for this User,Browser and machine and match with the Entered NewSessionID 2qUrNh7W0VnoBIO8gZ+pkSy67Eo=
                    //Log the attack details here
                    _isError = true;
                    //HttpContext.Current.RewritePath("~/Home/Logout");
                }
                if (_isError == false)
                {
                    //Use the default one so application will work as usual//ASP.NET_SessionId
                    Request.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value.Substring(0, 24);
                }
            }

        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //Pass the custom Session ID to the browser.
            if (Response.Cookies["ASP.NET_SessionId"] != null)
            {
                //Response.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value + GenerateHashKey();
                if (Request.Cookies["ASP.NET_SessionId"].Value != null && Request.Cookies["ASP.NET_SessionId"].Value.Length <= 24)
                {
                    Response.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value + GenerateHashKey();
                }
            }
            if (Request.IsSecureConnection)
            {
                if (Response.Cookies.Count > 0)
                {
                    foreach (string s in Response.Cookies.AllKeys)
                    {
                        if (s == FormsAuthentication.FormsCookieName || s.ToLower() == "asp.net_sessionid" || s.ToLower() == Helper.SessionKey.AuthToken.ToLower())
                        {
                            Response.Cookies[s].Secure = true;
                            Response.Cookies[s].Path = Request.ApplicationPath;
                        }
                    }
                }
            }
        }
    }
}
