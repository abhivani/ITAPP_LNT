using ITApp.Implement.Repository;
using ITApp.Model;
using ITApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITApp.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Constructor
        public readonly ICommonRepository _iCommonRepository;
        public BaseController(ICommonRepository iCommonRepository)
        {
            _iCommonRepository = iCommonRepository;
        }
        #endregion
        [HttpPost]

        public ActionResult CheckValidFile()
        {
            try
            {
                ResponseModel _objResponseModel = new ResponseModel();
                _objResponseModel.Result = HelperMessage.ResponceResult.WARNING;
                if (Request.Files.Count > 0)
                {
                    string _strMsg = Helper.GetCheckValidFile(Request.Files);
                    if (_strMsg == Enums.YesOrNo.Yes.ToString())
                    {

                        _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                        _objResponseModel.Message = _strMsg;
                    }
                    else
                        _objResponseModel.Message = _strMsg;
                }
                else
                    _objResponseModel.Message = string.Format(HelperMessage.NotFoundByObject, "File");

                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessge"] = ex;
                return RedirectToAction("Index", "Error", new { paramControllerName = "Base", paramMethodName = "FCSPopupFileUpload", paramRegion = "", paramIsAjaxRequest = true });
            }
        }
    }
}