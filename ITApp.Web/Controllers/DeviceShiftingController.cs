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
    public class DeviceShiftingController : BaseController
    {
        #region Constructor

        private readonly IDeviceShiftingRepository _IDeviceShiftingRepository;

        public DeviceShiftingController(IDeviceShiftingRepository deviceShiftingRepository, ICommonRepository _iCommonRepository) : base(_iCommonRepository)
        {
            _IDeviceShiftingRepository = deviceShiftingRepository;
        }
        #endregion
        // GET: DeviceShifting
        public ActionResult DeviceShiftingForms(long paramDeviceShiftingFormId = 0, bool IsPending = false)
        {
            try
            {
                DeviceShiftingFormsVM objDeviceShiftingFormsVMData = new DeviceShiftingFormsVM();
                objDeviceShiftingFormsVMData = _IDeviceShiftingRepository.getDeviceShiftingFormId(paramDeviceShiftingFormId, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString(), IsPending);

                return View(objDeviceShiftingFormsVMData);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DeviceShifting", paramMethodName = "DeviceShiftingForms", paramRegion = "", paramIsAjaxRequest = true });
            }
        }

        [HttpPost]
        public ActionResult SubmitDeviceShiftingFormValue(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IDeviceShiftingRepository.sumbitDeviceShiftingForm(paramObjDeviceShiftingFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DeviceShifting", paramMethodName = "SubmitDeviceShiftingFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult ApproveDeviceShiftingFormValue(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IDeviceShiftingRepository.ApproveDeviceShiftingForm(paramObjDeviceShiftingFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DeviceShifting", paramMethodName = "ApproveDeviceShiftingFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }
        [HttpPost]
        public ActionResult CloseDeviceShiftingFormValue(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IDeviceShiftingRepository.CloseDeviceShiftingForm(paramObjDeviceShiftingFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DeviceShifting", paramMethodName = "CloseDeviceShiftingFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult ReturnDeviceShiftingFormValue(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IDeviceShiftingRepository.ReturnDeviceShiftingForm(paramObjDeviceShiftingFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DeviceShifting", paramMethodName = "ReturnDeviceShiftingFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult RejectDeviceShiftingFormValue(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IDeviceShiftingRepository.RejectDeviceShiftingForm(paramObjDeviceShiftingFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DeviceShifting", paramMethodName = "RejectDeviceShiftingFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }
    }
}