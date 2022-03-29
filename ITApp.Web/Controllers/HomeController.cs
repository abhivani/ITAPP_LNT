using ITApp.Implement.Repository;
using ITApp.Model;
using ITApp.Utility;
using ITApp.Web.AD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static ITApp.Web.MvcApplication;

namespace ITApp.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Declaration 
        private readonly ICommonRepository _ICommonRepository;

        #endregion

        #region Constructor
        public HomeController(ICommonRepository CommonRepository)
        {
            _ICommonRepository = CommonRepository;
        }
        #endregion

        [SessionTimeout]
        [NoDirectAccess]
        public ActionResult Index()
        {

            string UPSNO = Session[Helper.SessionKey.UPSNO].ToString();
            string Role = Session[Helper.SessionKey.Role].ToString();
            AllRequestFomsList All = new AllRequestFomsList();
            string UserLocation = Convert.ToString(Session[Helper.SessionKey.Location]);
            All.Pending = _ICommonRepository.getPendingRequestFomsData(UPSNO, Role, UserLocation,"");
            All.MyRequest = _ICommonRepository.getRequestFoms(UPSNO);
            All.AllRequest = _ICommonRepository.getAllRequestFoms(UPSNO);
            

            return View(All);
        }

        #region Login through App HUB
        public async Task<ActionResult> Login(string paramupsno = "", string parambpid = "")
        {
            try
            {
                if (Request["paramupsno"] != null)
                {
                    //long _lngValue = Convert.ToInt64(ds_Log.Decrypt(paramupsno), 16);
                    string _strPsno = Convert.ToString(paramupsno);
                    tblRoleMaster _objtblUserRole = _ICommonRepository.getViewUserRolesByUserPsNo(_strPsno);
                    if (_objtblUserRole != null && _objtblUserRole != null)
                    {
                        vwEmpInfo _objvwUserRoleDetails = _ICommonRepository.LoginDetailsData(_strPsno);
                        await LoginSessionData(_objvwUserRoleDetails);
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "Home", paramMethodName = "Login", paramRegion = "Login through App HUB", paramIsAjaxRequest = false });
            }
        }
        #endregion
        #region Login Check
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel paramobjLoginModel)
        {
            try
            {
                DirectoryService ds = new DirectoryService();
                bool _blIsAuth = false;
                //tblRoleMaster _objtblUserRole = _ICommonRepository.getViewUserRolesByUserPsNo(paramobjLoginModel.UserPsno);
                vwEmpInfo _objtblUserRole = _ICommonRepository.getLogingcheckPsNo(paramobjLoginModel.UserPsno);
                if (_objtblUserRole != null && _objtblUserRole != null)
                {
                    if (AppSetting.authenticationMode.Equals("false")) _blIsAuth = true;
                    else _blIsAuth =  ds.getAuthentication(paramobjLoginModel.UserPsno, paramobjLoginModel.Password);
                    if (_blIsAuth == true)
                    {
                        #region User Single Login Check
                        string _strKey = Convert.ToString(paramobjLoginModel.UserPsno);
                        string _strUser = Convert.ToString(HttpRuntime.Cache[_strKey]);
                        string _strpsno = _strKey;//ds_Log.Encrypt(_strKey);
                        //if (_strUser == null || _strUser == String.Empty)
                        //{
                        //    HttpRuntime.Cache.Insert(_strKey, _strKey, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
                        //}
                        //else
                        //{
                        //    return RedirectToAction("InvalidLogin", "Home", new { paramUstokenId = _strpsno });//alreadylogin
                        //}
                        #endregion

                        if (paramobjLoginModel.agreecheckbox) // if checkbox is chcek than add psno & pass in cookie
                        {
                            HttpCookie objHttpCookie = new HttpCookie("PsNo");
                            objHttpCookie.Value = _strpsno;
                            objHttpCookie.Expires = DateTime.Now.AddYears(20); // For a cookie to effectively never expire
                            Response.Cookies.Add(objHttpCookie);
                        }
                        vwEmpInfo _objvwUserRoleDetails = _ICommonRepository.LoginDetailsData(paramobjLoginModel.UserPsno);
                        await LoginSessionData(_objvwUserRoleDetails);

                        return RedirectToAction("Index", "Home");
                    }
                    ViewBag.LoginMsg = "User Name Or Password Wrong";
                }
                else
                {
                    ViewBag.LoginMsg = "UserName or Password is Invalid !!!";
                }
                return View("Login");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "Home", paramMethodName = "Login", paramRegion = "Login Check" });
            }

        }
        #endregion

        public ActionResult Logout()
        {
            try
            {
                if (Session[Helper.SessionKey.UPSNO] != null)
                {
                    HttpRuntime.Cache.Remove(Convert.ToString(Session[Helper.SessionKey.UPSNO]));
                }

                Session.Clear();
                Session.Abandon(); // Session Expire but cookie do exist
                Session.RemoveAll();

                if (Request.Cookies["PsNo"] != null)
                {
                    HttpCookie objHttpCookie = Request.Cookies["PsNo"];
                    objHttpCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(objHttpCookie);
                }
                if (Request.Cookies["ASP.NET_SessionId"] != null)
                {
                    HttpCookie objcokkieASPNET = HttpContext.Request.Cookies["ASP.NET_SessionId"];
                    Response.Cookies.Remove("ASP.NET_SessionId");
                    objcokkieASPNET.Expires = DateTime.Now.AddDays(-10);
                    objcokkieASPNET.Value = null;
                    Response.SetCookie(objcokkieASPNET);
                    //Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                    //Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                }
                if (Request.Cookies[Helper.SessionKey.AuthToken] != null)
                {
                    HttpCookie objcokkieAuthToken = HttpContext.Request.Cookies[Helper.SessionKey.AuthToken];
                    Response.Cookies.Remove(Helper.SessionKey.AuthToken);
                    objcokkieAuthToken.Expires = DateTime.Now.AddDays(-10);
                    objcokkieAuthToken.Value = null;
                    Response.SetCookie(objcokkieAuthToken);
                }
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "Home", paramMethodName = "Logout", paramRegion = "", paramIsAjaxRequest = false });
            }
        }
        public ActionResult InvalidLogin(string paramUstokenId)
        {
            try
            {
                ViewBag.TempUstokenId = paramUstokenId;
                ViewBag.ErrorMsg = "You are already login";
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "Home", paramMethodName = "InvalidLogin", paramRegion = "", paramIsAjaxRequest = false });
            }
        }
        public ActionResult TokenLogout(string paramUstokenId)
        {
            try
            {
                if (!string.IsNullOrEmpty(paramUstokenId))
                {
                    HttpRuntime.Cache.Remove(paramUstokenId);
                }
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "Home", paramMethodName = "TokenLogout", paramRegion = "", paramIsAjaxRequest = false });
            }
        }
        #region Private Method
        #region User Session Data Store
        private async Task LoginSessionData(vwEmpInfo paramobjvwUserRole)
        {
            try
            {

                // createa a new GUID and save into the session
                string _strguid = Guid.NewGuid().ToString();
                Session[Helper.SessionKey.AuthToken] = _strguid;
                // now create a new cookie with this guid value
                Response.Cookies.Add(new HttpCookie(Helper.SessionKey.AuthToken, _strguid));

                Session[Helper.SessionKey.UPSNO] = paramobjvwUserRole.t_psno;
                Session[Helper.SessionKey.UserName] = paramobjvwUserRole.t_name;

                var Rolesdatas = _ICommonRepository.getViewUserRolesByUserPsNo(paramobjvwUserRole.t_psno);
                string roleName = _ICommonRepository.getViewUserRolesByUserPsNoData(paramobjvwUserRole.t_psno);

                Session[Helper.SessionKey.Role] = roleName;
                if (Rolesdatas != null)
                {
                    Session[Helper.SessionKey.Location] = Rolesdatas.Location;
                }
                Session[Helper.SessionKey.UserEmailId] = paramobjvwUserRole.t_mail;
                // Session[Helper.SessionKey.UserImgstring] = await getUserImage(paramobjvwUserRole.t_psno);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        private string getUserNoImage()
        {
            try
            {
                MemoryStream objMemoryStream = new MemoryStream();
                System.Drawing.Image imageIn = System.Drawing.Image.FromFile(Server.MapPath("~\\Content\\images\\user.png"));
                imageIn.Save(objMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
                string _strimreBase64Data = Convert.ToBase64String(objMemoryStream.ToArray());
                string imgDataURL = string.Format("data:image/png;base64,{0}", _strimreBase64Data);
                return imgDataURL;
            }
            catch
            {
                return "Error User Image";
            }
        }
        private async Task<string> getUserImage(string parampsno)
        {
            try
            {
                //byte[] _byteUserImage = await _IRoleRepository.getViewUserImage(parampsno);
                vwUserImage objvwUserImage = await _ICommonRepository.getViewUserImage(parampsno);
                if (objvwUserImage != null)
                {
                    byte[] _byteUserImage = objvwUserImage.ImageData;
                    if (_byteUserImage != null)
                    {
                        string _strimreBase64Data = Convert.ToBase64String(_byteUserImage);
                        string imgDataURL = string.Format("data:image/png;base64,{0}", _strimreBase64Data);
                        return imgDataURL;
                    }
                    else
                        return getUserNoImage();
                }
                else
                    return getUserNoImage();

            }
            catch
            {
                return getUserNoImage();
            }
        }
        #endregion
    }
}