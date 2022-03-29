using ITApp.Implement.Repository;
using ITApp.Model;
using ITApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ITApp.Web.MvcApplication;

namespace ITApp.Web.Controllers
{
    [SessionTimeout]
    [NoDirectAccess]
    public class RoleMasterController : BaseController
    {
        #region Constructor



        //IRoleMasterRepository _IRoleMasterRepository;
        //public RoleMasterController(IRoleMasterRepository roleMaster)
        //{
        //    _IRoleMasterRepository = roleMaster;
        //}



        private readonly IRoleMasterRepository _IRoleMasterRepository;

        public RoleMasterController(IRoleMasterRepository roleMasterRepository, ICommonRepository _iCommonRepository) : base(_iCommonRepository)
        {
            _IRoleMasterRepository = roleMasterRepository;

        }
        #endregion


        #region Get RoleMaster List || Prakash Patel ||


        // GET: RoleMaster
        public ActionResult Index()
        {

            try
            {

                return View(_IRoleMasterRepository.getRoles());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "RoleMaster", paramMethodName = "Index", paramRegion = "", paramIsAjaxRequest = false });
            }

        }

        #endregion

        #region Get RoleMaster By RoleID || Prakash Patel || 
        public ActionResult GetRoleMasterByPsNo(string paramPsNo)
        {

            try
            {                

                try
                {
                    ResponseModel objResponseModel = new ResponseModel();
                    objResponseModel = _IRoleMasterRepository.getRoleByPsNO(paramPsNo);
                    return Json(objResponseModel, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessge"] = ex;
                    return RedirectToAction("Index", "Error", new { paramControllerName = "RoleController", paramMethodName = "GetRoleMasterByPsNo", paramRegion = "", paramIsAjaxRequest = true });
                }

                //return Json(objResponseModel, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                // throw ex;

                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "RoleMaster", paramMethodName = "GetRoleMasterByPsNo", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        public ActionResult GetRoleMasterNameByPsNo(string paramPsNo)
        {

            try
            {

                try
                {
                    ResponseModel objResponseModel = new ResponseModel();
                    objResponseModel = _IRoleMasterRepository.GetRoleMasterNameByPsNo(paramPsNo);
                    return Json(objResponseModel, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessge"] = ex;
                    return RedirectToAction("Index", "Error", new { paramControllerName = "RoleController", paramMethodName = "GetRoleMasterNameByPsNo", paramRegion = "", paramIsAjaxRequest = true });
                }

                //return Json(objResponseModel, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                // throw ex;

                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "RoleMaster", paramMethodName = "GetRoleMasterByPsNo", paramRegion = "", paramIsAjaxRequest = true });
            }


        }
        public ActionResult AddEditRoleMastersData(long paramRoleId = 0)
        {

            try
            {
                tblRoleMaster _objtblRoleMaster = _IRoleMasterRepository.getRoleById(paramRoleId);
                if (_objtblRoleMaster == null)
                {
                    _objtblRoleMaster = new tblRoleMaster();
                    return View();
                }
                return View(_objtblRoleMaster);
            

            }
            catch (Exception ex)
            {
                // throw ex;

                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "RoleMaster", paramMethodName = "GetRoleMasterByRoleID", paramRegion = "", paramIsAjaxRequest = true });
            }


            //
        }
        #endregion

        #region Save RoleMaster 


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRoleMasterValue(tblRoleMaster paramObjtblRoleMaster)
        {

            try
            {
                ResponseModel objResponseModel = _IRoleMasterRepository.saveRole(paramObjtblRoleMaster, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "RoleController", paramMethodName = "AddRole", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        #endregion

        #region Delete Record


        public ActionResult deletRecord(long paramPkId)
        {
            try
            {
                ResponseModel _objRoleMasterModel = _IRoleMasterRepository.deleteRecord(paramPkId, Convert.ToString("90347906"));

                return Json(_objRoleMasterModel, JsonRequestBehavior.AllowGet);
                // return Json(_objRoleMasterModel);
                //return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "RoleController", paramMethodName = "deletRecord", paramRegion = "", paramIsAjaxRequest = true });
            }
        }

        #endregion
    }
}