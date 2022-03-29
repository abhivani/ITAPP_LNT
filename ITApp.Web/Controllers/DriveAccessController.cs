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
    public class DriveAccessController : BaseController
    {
        #region Constructor

        private readonly IDriveAccessRepository _IDriveAccessRepository;

        public DriveAccessController(IDriveAccessRepository driveAccessRepository, ICommonRepository _iCommonRepository) : base(_iCommonRepository)
        {
            _IDriveAccessRepository = driveAccessRepository;
        }
        #endregion
        // GET: DriveAccess
        public ActionResult DriveAcessForms(long paramDriveAccessFormId = 0, bool IsPending = false)
        {
            try
            {
                DriveAccessFormsMV objDriveAccessFormsMVData = new DriveAccessFormsMV();
                objDriveAccessFormsMVData = _IDriveAccessRepository.getDriveAccessFormId(paramDriveAccessFormId, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString(), IsPending);
                
                return View(objDriveAccessFormsMVData);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DriveAccess", paramMethodName = "DriveAcessForms", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult SubmitDriveAccessFormValue(DriveAccessFormsMV paramObjDriveAccessFormsMV)
        {

            try
            {
                ResponseModel objResponseModel = _IDriveAccessRepository.sumbitDriveAccessForm(paramObjDriveAccessFormsMV, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DriveAccess", paramMethodName = "SubmitDriveAccessFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult ApproveDriveAccessFormValue(DriveAccessFormsMV paramObjDriveAccessFormsMV)
        {

            try
            {
                ResponseModel objResponseModel = _IDriveAccessRepository.ApproveDriveAccessForm(paramObjDriveAccessFormsMV, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DriveAccess", paramMethodName = "ApproveDriveAccessFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult CloseDriveAccessFormValue(DriveAccessFormsMV paramObjDriveAccessFormsMV)
        {

            try
            {
                ResponseModel objResponseModel = _IDriveAccessRepository.CloseDriveAccessForm(paramObjDriveAccessFormsMV, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DriveAccess", paramMethodName = "CloseDriveAccessFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult ReturnDriveAccessFormValue(DriveAccessFormsMV paramObjDriveAccessFormsMV)
        {

            try
            {
                ResponseModel objResponseModel = _IDriveAccessRepository.ReturnDriveAccessForm(paramObjDriveAccessFormsMV, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DriveAccess", paramMethodName = "ReturnDriveAccessFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult RejectDriveAccessFormValue(DriveAccessFormsMV paramObjDriveAccessFormsMV)
        {

            try
            {
                ResponseModel objResponseModel = _IDriveAccessRepository.RejectDriveAccessForm(paramObjDriveAccessFormsMV, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DriveAccess", paramMethodName = "RejectDriveAccessFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        public ActionResult getStepCount(long paramDriveAccessFormId)
        {
            var status = _IDriveAccessRepository.getStatuOfRequest(paramDriveAccessFormId);
            return Json(status);
        }
    }
}