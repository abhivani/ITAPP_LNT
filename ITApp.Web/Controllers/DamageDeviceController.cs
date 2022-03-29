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
    public class DamageDeviceController : BaseController
    {
        #region Constructor

        private readonly IDamageDeviceRepository _IDamageDeviceRepository;

        public DamageDeviceController(IDamageDeviceRepository damageDeviceRepository, ICommonRepository _iCommonRepository) : base(_iCommonRepository)
        {
            _IDamageDeviceRepository = damageDeviceRepository;
        }
        #endregion
        // GET: DamageDevice
        public ActionResult DamageDeviceForms(long paramDamageDeviceFormId = 0, bool IsPending = false)
        {
            try
            {
                DamageDeviceFormsVM objDamageDeviceFormsVMData = new DamageDeviceFormsVM();
                objDamageDeviceFormsVMData = _IDamageDeviceRepository.getDamageDeviceFormId(paramDamageDeviceFormId, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString(), IsPending);

                return View(objDamageDeviceFormsVMData);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DamageDevice", paramMethodName = "DamageDeviceForms", paramRegion = "", paramIsAjaxRequest = true });
            }
        }

        [HttpPost]
        public ActionResult SubmitDamageDeviceFormValue(DamageDeviceFormsVM paramObjDamageDeviceFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IDamageDeviceRepository.sumbitDamageDeviceForm(paramObjDamageDeviceFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DamageDevice", paramMethodName = "SubmitDamageDeviceFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult ApproveDamageDeviceFormValue(DamageDeviceFormsVM paramObjDamageDeviceFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IDamageDeviceRepository.ApproveDamageDeviceForm(paramObjDamageDeviceFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DamageDevice", paramMethodName = "ApproveDamageDeviceFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult CloseDamageDeviceFormValue(DamageDeviceFormsVM paramObjDamageDeviceFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IDamageDeviceRepository.CloseDamageDeviceForm(paramObjDamageDeviceFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DamageDevice", paramMethodName = "CloseDamageDeviceFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult ReturnDamageDeviceFormValue(DamageDeviceFormsVM paramObjDamageDeviceFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IDamageDeviceRepository.ReturnDamageDeviceForm(paramObjDamageDeviceFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DamageDevice", paramMethodName = "ReturnDamageDeviceFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult RejectDamageDeviceFormValue(DamageDeviceFormsVM paramObjDamageDeviceFormsVM)
        {

            try
            {
                ResponseModel objResponseModel = _IDamageDeviceRepository.RejectDamageDeviceForm(paramObjDamageDeviceFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "DamageDevice", paramMethodName = "RejectDamageDeviceFormValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }
    }
}