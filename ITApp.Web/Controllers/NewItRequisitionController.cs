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
    public class NewItRequisitionController : BaseController
    {
        #region Constructor

        private readonly INewItRequisitionRepository _INewItRequisitionRepository;

        public NewItRequisitionController(INewItRequisitionRepository NewItRequisitionRepository, ICommonRepository _iCommonRepository) : base(_iCommonRepository)
        {
            _INewItRequisitionRepository = NewItRequisitionRepository;

        }
        #endregion

        // GET: NewItRequisition
        public ActionResult NewItRequisitionForms(long paramNewITRequisitionID = 0,bool IsPending = false)
        {


            try
            {
                NewITRequisitionFormVM objNewITRequisitionFormVM = new NewITRequisitionFormVM();
                objNewITRequisitionFormVM = _INewItRequisitionRepository.getNewITRequisitionFormId(paramNewITRequisitionID, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString(), IsPending);
                return View(objNewITRequisitionFormVM);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "NewItRequisitionController", paramMethodName = "NewItRequisitionForms", paramRegion = "", paramIsAjaxRequest = true });
            }

            //return View();
        }

        [HttpPost]
        public ActionResult SubmitNewItRequisitionFromValue(NewITRequisitionFormVM paramObjNewITRequisitionFormVM)
        {

            try
            {
                //ResponseModel objResponseModel = _IUSBRequestRepository.sumbitUSBRequestForm(paramObjUSBRequestFormsVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                ResponseModel objResponseModel = _INewItRequisitionRepository.sumbitNewItRequisitionForm(paramObjNewITRequisitionFormVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequestController", paramMethodName = "SubmitUSBRequestFromValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult ApproveNewItRequisitionFromValue(NewITRequisitionFormVM paramObjNewITRequisitionFormVM)
        {
            try
            {               
                ResponseModel objResponseModel = _INewItRequisitionRepository.ApproveNewItRequisitionForm(paramObjNewITRequisitionFormVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequestController", paramMethodName = "ApproveUSBRequestFromValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult UnderProcessFromValue(NewITRequisitionFormVM paramObjNewITRequisitionFormVM)
        {
            try
            {
                ResponseModel objResponseModel = _INewItRequisitionRepository.UnderProcessNewItRequisitionForm(paramObjNewITRequisitionFormVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequestController", paramMethodName = "ApproveUSBRequestFromValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult CompleteNewITForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVM)
        {
            try
            {
                ResponseModel objResponseModel = _INewItRequisitionRepository.CompleteNewITForm(paramObjNewITRequisitionFormVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequestController", paramMethodName = "ApproveUSBRequestFromValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult ReturnNewItFromValue(NewITRequisitionFormVM paramObjNewITRequisitionFormVM)
        {

            try
            {
                ResponseModel objResponseModel = _INewItRequisitionRepository.ReturnNewITForm(paramObjNewITRequisitionFormVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequestController", paramMethodName = "ReturnUSBRequestFromValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpPost]
        public ActionResult RejectNewItFromValue(NewITRequisitionFormVM paramObjNewITRequisitionFormVM)
        {

            try
            {
                ResponseModel objResponseModel = _INewItRequisitionRepository.RejectNewITForm(paramObjNewITRequisitionFormVM, Session[Helper.SessionKey.UPSNO].ToString(), Session[Helper.SessionKey.UserName].ToString());
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequestController", paramMethodName = "ReturnUSBRequestFromValue", paramRegion = "", paramIsAjaxRequest = true });
            }


        }

        [HttpGet]
        public ActionResult GetDeliveryByData()
        {
            try
            {
                string PsNo = Session[Helper.SessionKey.UPSNO].ToString();
                ResponseModel objResponseModel = _INewItRequisitionRepository.GetDeliveryByData(PsNo);
                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "USBRequestController", paramMethodName = "ReturnUSBRequestFromValue", paramRegion = "", paramIsAjaxRequest = true });
            }
        }
    }
}