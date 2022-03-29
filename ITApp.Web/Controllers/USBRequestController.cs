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
    public class USBRequestController : BaseController
    {
        #region Constructor

        private readonly IUSBRequestRepository _IUSBRequestRepository;

        public USBRequestController(IUSBRequestRepository usbRequestRepository, ICommonRepository _iCommonRepository) : base(_iCommonRepository)
        {
            _IUSBRequestRepository = usbRequestRepository;

        }
        #endregion


        // GET: USBRequest
        public ActionResult USBRequest(long paramUSBRequestFormId = 0, bool IsPending = false)
        {
            try
            {
                USBRequestFormsVM objUSBRequestFormsVMData = new USBRequestFormsVM();               
                objUSBRequestFormsVMData = _IUSBRequestRepository.getUSBRequestFormId(paramUSBRequestFormId, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString(), IsPending);                
                return View(objUSBRequestFormsVMData);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequest", paramMethodName = "USBRequest", paramRegion = "", paramIsAjaxRequest = true });
            }


        }
        [HttpPost]
        public ActionResult SubmitUSBRequestFromValue(USBRequestFormsVM paramObjUSBRequestFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IUSBRequestRepository.sumbitUSBRequestForm(paramObjUSBRequestFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequestController", paramMethodName = "SubmitUSBRequestFromValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult ApproveUSBRequestFromValue(USBRequestFormsVM paramObjUSBRequestFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IUSBRequestRepository.ApproveUSBRequestForm(paramObjUSBRequestFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequestController", paramMethodName = "ApproveUSBRequestFromValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult CloseUSBRequestFromValue(USBRequestFormsVM paramObjUSBRequestFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IUSBRequestRepository.CloseUSBRequestForm(paramObjUSBRequestFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequestController", paramMethodName = "ApproveUSBRequestFromValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult ReturnUSBRequestFromValue(USBRequestFormsVM paramObjUSBRequestFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IUSBRequestRepository.ReturnUSBRequestForm(paramObjUSBRequestFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequestController", paramMethodName = "ReturnUSBRequestFromValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult RejectUSBRequestFromValue(USBRequestFormsVM paramObjUSBRequestFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IUSBRequestRepository.RejectUSBRequestForm(paramObjUSBRequestFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequestController", paramMethodName = "RejectUSBRequestFromValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        public ActionResult getStepCount(long paramUSBRequestFormId) {
            var status = _IUSBRequestRepository.getStatuOfRequest(paramUSBRequestFormId);
            return Json(status);            
        }
    }
}