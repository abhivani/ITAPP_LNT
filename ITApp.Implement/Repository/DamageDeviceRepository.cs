using ITApp.Model;
using ITApp.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Implement.Repository
{
    #region Interface
    public interface IDamageDeviceRepository
    {
        DamageDeviceFormsVM getDamageDeviceFormId(long paramDamageDeviceFormId, string paramPsNo, string paramUserName, bool IsPending = false);
        ResponseModel sumbitDamageDeviceForm(DamageDeviceFormsVM paramObjDamageDeviceFormsVM, string paramPsNo, string paramUserName);
        ResponseModel ApproveDamageDeviceForm(DamageDeviceFormsVM paramObjDamageDeviceFormsVM, string paramPsNo, string paramUserName);
        ResponseModel CloseDamageDeviceForm(DamageDeviceFormsVM paramObjDamageDeviceFormsVM, string paramPsNo, string paramUserName);
        ResponseModel ReturnDamageDeviceForm(DamageDeviceFormsVM paramObjDamageDeviceFormsVM, string paramPsNo, string paramUserName);
        ResponseModel RejectDamageDeviceForm(DamageDeviceFormsVM paramObjDamageDeviceFormsVM, string paramPsNo, string paramUserName);
    }
    #endregion
    public class DamageDeviceRepository : IDamageDeviceRepository
    {
        private readonly ITAPPFORMSDbContext dbContext;
        public DamageDeviceRepository(ITAPPFORMSDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        #region Danage Device Form  
        public DamageDeviceFormsVM getDamageDeviceFormId(long paramDamageDeviceFormId, string paramPsNo, string paramUserName, bool IsPending = false)
        {
            DamageDeviceFormsVM objDamageDeviceFormsVMData = new DamageDeviceFormsVM();
            if (paramDamageDeviceFormId > 0)
            {
                var tblDamageDeviceFormData = dbContext.tblDamageDeviceForm.Where(x => x.DamageDeviceFormId == paramDamageDeviceFormId && x.IsDeleted == false).FirstOrDefault();
                if (tblDamageDeviceFormData != null)
                {
                    var RequestFormData = dbContext.tblRequestForms.Where(x => x.RequestFormId == tblDamageDeviceFormData.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (RequestFormData != null)
                    {
                        objDamageDeviceFormsVMData.requestFomsVM.RequestFormId = RequestFormData.RequestFormId;
                        objDamageDeviceFormsVMData.requestFomsVM.RequestNo = RequestFormData.RequestNo;
                        objDamageDeviceFormsVMData.requestFomsVM.RequestDate = RequestFormData.RequestDate;
                        objDamageDeviceFormsVMData.requestFomsVM.PsNo = RequestFormData.PsNo;
                        objDamageDeviceFormsVMData.requestFomsVM.RequestFor = RequestFormData.RequestFor;
                        objDamageDeviceFormsVMData.requestFomsVM.PendingWith = RequestFormData.PendingWith;
                        objDamageDeviceFormsVMData.requestFomsVM.Status = RequestFormData.Status;
                        objDamageDeviceFormsVMData.requestFomsVM.ReturnRejectWith = RequestFormData.ReturnRejectWith;
                        objDamageDeviceFormsVMData.requestFomsVM.LastPendingWith = RequestFormData.LastPendingWith;
                    }
                    objDamageDeviceFormsVMData.DamageDeviceFormId = tblDamageDeviceFormData.DamageDeviceFormId;
                    objDamageDeviceFormsVMData.RequestFormId = tblDamageDeviceFormData.RequestFormId;
                    objDamageDeviceFormsVMData.Name = tblDamageDeviceFormData.Name;
                    objDamageDeviceFormsVMData.DepartmentName = tblDamageDeviceFormData.DepartmentName;
                    objDamageDeviceFormsVMData.DepartmentHead = tblDamageDeviceFormData.DepartmentHead;
                    objDamageDeviceFormsVMData.OnBehalfof = tblDamageDeviceFormData.OnBehalfof;
                    objDamageDeviceFormsVMData.Location = tblDamageDeviceFormData.Location;
                    objDamageDeviceFormsVMData.DamagedDeviceName = tblDamageDeviceFormData.DamagedDeviceName;
                    objDamageDeviceFormsVMData.DamagedDeviceNo = tblDamageDeviceFormData.DamagedDeviceNo;
                    objDamageDeviceFormsVMData.DamageReason = tblDamageDeviceFormData.DamageReason;
                    objDamageDeviceFormsVMData.DHRemarks = tblDamageDeviceFormData.DHRemarks;
                    objDamageDeviceFormsVMData.ITHelpdeskAdminRemarks = tblDamageDeviceFormData.ITHelpdeskAdminRemarks;
                    objDamageDeviceFormsVMData.ITHelpdeskRemarks = tblDamageDeviceFormData.ITHelpdeskRemarks;
                    objDamageDeviceFormsVMData.DStatus = tblDamageDeviceFormData.DStatus;


                    var DamageDeviceformHistoryData = dbContext.tblDamageDeviceFormStatusHistory.Where(x => x.DamageDeviceFormId == paramDamageDeviceFormId && x.IsDeleted == false).ToList();

                    foreach (var item in DamageDeviceformHistoryData)
                    {
                        if (item.StatusName == "Initiator Submit")
                        {
                            objDamageDeviceFormsVMData.damageDeviceFormProcessBarVM.Initiate = "Initiate By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "Department Head Approve")
                        {
                            objDamageDeviceFormsVMData.damageDeviceFormProcessBarVM.DepartmentHeadApprove = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "IT Helpdesk Admin Approve")
                        {
                            objDamageDeviceFormsVMData.damageDeviceFormProcessBarVM.ITHelpdeskAdminApprove = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "Close")
                        {
                            objDamageDeviceFormsVMData.damageDeviceFormProcessBarVM.ITHelpdeskApprove = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                    }
                }
                else
                {
                    #region Request No Logic || Prakash Patel

                    string lastRequestNumber = dbContext.tblRequestForms.OrderByDescending(x => x.RequestFormId).Select(x => x.RequestNo).FirstOrDefault();
                    if (lastRequestNumber == null)
                    {
                        int appliNo = 1;
                        DateTime TodayDate = DateTime.Now;
                        string Year = TodayDate.ToString("yyyy");
                        string Month = TodayDate.ToString("MM");
                        lastRequestNumber = Month + "/" + Year + "-" + appliNo;
                    }
                    else
                    {
                        DateTime TodayDate = DateTime.Now;
                        string Date = TodayDate.ToString("dd");
                        string Month = TodayDate.ToString("MM");
                        string Year = TodayDate.ToString("yyyy");
                        var alldata = lastRequestNumber.Split('-');
                        if (alldata[0].Length != 4)
                        {
                            var splitYear = alldata[0].Split('/');

                            alldata[0] = splitYear[1];
                        }
                        if (alldata[0] == Year)
                        {
                            string val = alldata[1];
                            int appliNo = Convert.ToInt32(val) + 1;
                            lastRequestNumber = Month + "/" + Year + "-" + appliNo;
                        }
                        else
                        {
                            int appliNo = 1;
                            lastRequestNumber = Month + "/" + Year + "-" + appliNo;
                        }
                    }
                    #endregion
                    objDamageDeviceFormsVMData.requestFomsVM.RequestNo = lastRequestNumber;
                    objDamageDeviceFormsVMData.requestFomsVM.RequestDate = DateTime.Now;
                    objDamageDeviceFormsVMData.requestFomsVM.PsNo = paramPsNo;
                    objDamageDeviceFormsVMData.Name = paramUserName;
                   // objDamageDeviceFormsVMData.DepartmentName = "Test Department";
                    var vwEmpdata = dbContext.vwEmpInfo.Where(x => x.t_psno == paramPsNo).FirstOrDefault();
                    if (vwEmpdata != null)
                    {
                        objDamageDeviceFormsVMData.DepartmentName = vwEmpdata.t_dsca;// vwEmpdata.t_depc;
                        if (!string.IsNullOrEmpty(vwEmpdata.t_dhps))
                        {
                            var headempdata = dbContext.vwEmpInfo.Where(x => x.t_psno == vwEmpdata.t_dhps).FirstOrDefault();
                            if (headempdata != null)
                            {
                                objDamageDeviceFormsVMData.DepartmentHead = vwEmpdata.t_dhps + "-" + headempdata.t_name;
                            }

                        }
                    }
                    // objDamageDeviceFormsVMData.DepartmentHead = "Test Department Head";
                }

            }
            else
            {
                #region Request No Logic || Prakash Patel

                string lastRequestNumber = dbContext.tblRequestForms.OrderByDescending(x => x.RequestFormId).Select(x => x.RequestNo).FirstOrDefault();
                if (lastRequestNumber == null)
                {
                    int appliNo = 1;
                    DateTime TodayDate = DateTime.Now;
                    string Year = TodayDate.ToString("yyyy");
                    string Month = TodayDate.ToString("MM");
                    lastRequestNumber = Month + "/" + Year + "-" + appliNo;
                }
                else
                {
                    DateTime TodayDate = DateTime.Now;
                    string Date = TodayDate.ToString("dd");
                    string Month = TodayDate.ToString("MM");
                    string Year = TodayDate.ToString("yyyy");
                    var alldata = lastRequestNumber.Split('-');
                    if (alldata[0].Length != 4)
                    {
                        var splitYear = alldata[0].Split('/');
                        alldata[0] = splitYear[1];
                    }
                    if (alldata[0] == Year)
                    {
                        string val = alldata[1];
                        int appliNo = Convert.ToInt32(val) + 1;
                        lastRequestNumber = Month + "/" + Year + "-" + appliNo;
                    }
                    else
                    {
                        int appliNo = 1;
                        lastRequestNumber = Month + "/" + Year + "-" + appliNo;
                    }
                }
                #endregion
                objDamageDeviceFormsVMData.requestFomsVM.RequestNo = lastRequestNumber;
                objDamageDeviceFormsVMData.requestFomsVM.RequestDate = DateTime.Now;
                objDamageDeviceFormsVMData.requestFomsVM.PsNo = paramPsNo;
                objDamageDeviceFormsVMData.Name = paramUserName;
               // objDamageDeviceFormsVMData.DepartmentName = "Test Department";
                var vwEmpdata = dbContext.vwEmpInfo.Where(x => x.t_psno == paramPsNo).FirstOrDefault();
                if (vwEmpdata != null)
                {
                    objDamageDeviceFormsVMData.DepartmentName = vwEmpdata.t_dsca;// vwEmpdata.t_depc;
                    if (!string.IsNullOrEmpty(vwEmpdata.t_dhps))
                    {
                        var headempdata = dbContext.vwEmpInfo.Where(x => x.t_psno == vwEmpdata.t_dhps).FirstOrDefault();
                        if (headempdata != null)
                        {
                            objDamageDeviceFormsVMData.DepartmentHead = vwEmpdata.t_dhps + "-" + headempdata.t_name;
                        }

                    }
                }
                //objDamageDeviceFormsVMData.DepartmentHead = "Test Department Head";
            }
            if (IsPending == true)
            {

                objDamageDeviceFormsVMData.requestFomsVM.IsPending = true;
            }
            else
            {
                objDamageDeviceFormsVMData.requestFomsVM.IsPending = false;
            }
            return objDamageDeviceFormsVMData;
        }
        public ResponseModel sumbitDamageDeviceForm(DamageDeviceFormsVM paramObjDamageDeviceFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDamageDeviceFormsVM.RequestFormId == 0)
                {
                    tblRequestForms tblRequestForms = new tblRequestForms();
                    tblRequestForms.PsNo = paramObjDamageDeviceFormsVM.requestFomsVM.PsNo;
                    tblRequestForms.RequestNo = paramObjDamageDeviceFormsVM.requestFomsVM.RequestNo;
                    tblRequestForms.RequestDate = paramObjDamageDeviceFormsVM.requestFomsVM.RequestDate;
                    tblRequestForms.RequestFor = "DAMAGE";
                    tblRequestForms.PendingWith = "Department Head";
                    tblRequestForms.Status = "Pending Department Head Approval";
                    if (!string.IsNullOrEmpty(paramObjDamageDeviceFormsVM.DepartmentHead))
                    {
                        var listdata = paramObjDamageDeviceFormsVM.DepartmentHead.Split('-');
                        if (!string.IsNullOrEmpty(listdata[0]))
                        {
                            tblRequestForms.DepartmentHeadId = listdata[0].Trim();
                        }
                    }
                    tblRequestForms.IsDeleted = false;
                    tblRequestForms.CreateBy = paramPsNo;
                    tblRequestForms.CreatedOn = DateTime.Now;
                    tblRequestForms.CreatedByName = paramUserName;
                    dbContext.tblRequestForms.Add(tblRequestForms);
                    dbContext.SaveChanges();

                    if (tblRequestForms.RequestFormId > 0)
                    {
                        tblDamageDeviceForm tblDamageDeviceFormData = new tblDamageDeviceForm();
                        tblDamageDeviceFormData.RequestFormId = tblRequestForms.RequestFormId;
                        tblDamageDeviceFormData.Name = paramObjDamageDeviceFormsVM.Name;
                        tblDamageDeviceFormData.DepartmentName = paramObjDamageDeviceFormsVM.DepartmentName;
                        tblDamageDeviceFormData.DepartmentHead = paramObjDamageDeviceFormsVM.DepartmentHead;
                        tblDamageDeviceFormData.OnBehalfof = paramObjDamageDeviceFormsVM.OnBehalfof;
                        tblDamageDeviceFormData.Location = paramObjDamageDeviceFormsVM.Location;
                        tblDamageDeviceFormData.DamagedDeviceName = paramObjDamageDeviceFormsVM.DamagedDeviceName;
                        tblDamageDeviceFormData.DamagedDeviceNo = paramObjDamageDeviceFormsVM.DamagedDeviceNo;
                        tblDamageDeviceFormData.DamageReason = paramObjDamageDeviceFormsVM.DamageReason;      
                        tblDamageDeviceFormData.IsReject = false;
                        tblDamageDeviceFormData.IsDeleted = false;
                        tblDamageDeviceFormData.CreateBy = paramPsNo;
                        tblDamageDeviceFormData.CreatedOn = DateTime.Now;
                        dbContext.tblDamageDeviceForm.Add(tblDamageDeviceFormData);
                        dbContext.SaveChanges();
                        if (tblDamageDeviceFormData.DamageDeviceFormId > 0)
                        {
                            tblDamageDeviceFormStatusHistory tblDamageDeviceFormStatusHistoryData = new tblDamageDeviceFormStatusHistory();
                            tblDamageDeviceFormStatusHistoryData.DamageDeviceFormId = tblDamageDeviceFormData.DamageDeviceFormId;
                            tblDamageDeviceFormStatusHistoryData.StatusName = "Initiator Submit";
                            tblDamageDeviceFormStatusHistoryData.IsDeleted = false;
                            tblDamageDeviceFormStatusHistoryData.CreateBy = paramPsNo;
                            tblDamageDeviceFormStatusHistoryData.CreatedOn = DateTime.Now;
                            tblDamageDeviceFormStatusHistoryData.CreateByName = paramUserName;
                            dbContext.tblDamageDeviceFormStatusHistory.Add(tblDamageDeviceFormStatusHistoryData);
                            dbContext.SaveChanges();

                            _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully + "|" + "Request No : " + paramObjDamageDeviceFormsVM.requestFomsVM.RequestNo;
                            _objResponseModel.OtherParameter = paramObjDamageDeviceFormsVM.requestFomsVM.RequestNo;
                        }
                        _objResponseModel.PrimeryKeyId = paramObjDamageDeviceFormsVM.RequestFormId;
                    }
                }
                else
                {
                    tblRequestForms tblRequestFormsExistingdata = new tblRequestForms();
                    tblRequestFormsExistingdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDamageDeviceFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsExistingdata != null)
                    {
                        tblRequestFormsExistingdata.PsNo = paramObjDamageDeviceFormsVM.requestFomsVM.PsNo;
                        tblRequestFormsExistingdata.RequestNo = paramObjDamageDeviceFormsVM.requestFomsVM.RequestNo;
                        tblRequestFormsExistingdata.RequestDate = paramObjDamageDeviceFormsVM.requestFomsVM.RequestDate;
                        tblRequestFormsExistingdata.RequestFor = "DAMAGE";
                        tblRequestFormsExistingdata.PendingWith = "Department Head";
                        tblRequestFormsExistingdata.Status = "Pending Department Head Approval";
                        tblRequestFormsExistingdata.IsDeleted = false;
                        tblRequestFormsExistingdata.UpdateBy = paramPsNo;
                        tblRequestFormsExistingdata.UpdateOn = DateTime.Now;
                        tblRequestFormsExistingdata.UpdateByName = paramUserName;
                        dbContext.Entry(tblRequestFormsExistingdata).State = EntityState.Modified;
                        dbContext.SaveChanges();
                        if (tblRequestFormsExistingdata.RequestFormId > 0)
                        {
                            tblDamageDeviceForm tblDamageDeviceFormExistingdata = new tblDamageDeviceForm();
                            tblDamageDeviceFormExistingdata = dbContext.tblDamageDeviceForm.Where(x => x.DamageDeviceFormId == paramObjDamageDeviceFormsVM.DamageDeviceFormId && x.IsDeleted == false && x.IsReject == false).FirstOrDefault();
                            if (tblDamageDeviceFormExistingdata != null)
                            {
                                tblDamageDeviceFormExistingdata.RequestFormId = tblRequestFormsExistingdata.RequestFormId;
                                tblDamageDeviceFormExistingdata.Name = paramObjDamageDeviceFormsVM.Name;
                                tblDamageDeviceFormExistingdata.DepartmentName = paramObjDamageDeviceFormsVM.DepartmentName;
                                tblDamageDeviceFormExistingdata.DepartmentHead = paramObjDamageDeviceFormsVM.DepartmentHead;
                                tblDamageDeviceFormExistingdata.OnBehalfof = paramObjDamageDeviceFormsVM.OnBehalfof;
                                tblDamageDeviceFormExistingdata.Location = paramObjDamageDeviceFormsVM.Location;
                                tblDamageDeviceFormExistingdata.DamagedDeviceName = paramObjDamageDeviceFormsVM.DamagedDeviceName;
                                tblDamageDeviceFormExistingdata.DamagedDeviceNo = paramObjDamageDeviceFormsVM.DamagedDeviceNo;
                                tblDamageDeviceFormExistingdata.DamageReason = paramObjDamageDeviceFormsVM.DamageReason;
                                tblDamageDeviceFormExistingdata.IsReject = false;
                                tblDamageDeviceFormExistingdata.IsDeleted = false;
                                tblDamageDeviceFormExistingdata.UpdateBy = paramPsNo;
                                tblDamageDeviceFormExistingdata.UpdateOn = DateTime.Now;
                                dbContext.Entry(tblDamageDeviceFormExistingdata).State = EntityState.Modified;
                                dbContext.SaveChanges();
                                if (tblDamageDeviceFormExistingdata.DamageDeviceFormId > 0)
                                {
                                    tblDamageDeviceFormStatusHistory tblDamageDeviceFormStatusHistory = new tblDamageDeviceFormStatusHistory();
                                    tblDamageDeviceFormStatusHistory.DamageDeviceFormId = tblDamageDeviceFormExistingdata.DamageDeviceFormId;
                                    tblDamageDeviceFormStatusHistory.StatusName = "Initiator Submit";
                                    tblDamageDeviceFormStatusHistory.IsDeleted = false;
                                    tblDamageDeviceFormStatusHistory.CreateBy = paramPsNo;
                                    tblDamageDeviceFormStatusHistory.CreatedOn = DateTime.Now;
                                    tblDamageDeviceFormStatusHistory.CreateByName = paramUserName;
                                    dbContext.tblDamageDeviceFormStatusHistory.Add(tblDamageDeviceFormStatusHistory);
                                    dbContext.SaveChanges();

                                    _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                                    _objResponseModel.Message = HelperMessage.SaveSuccessfully + "|" + "Request No : " + paramObjDamageDeviceFormsVM.requestFomsVM.RequestNo;
                                    _objResponseModel.OtherParameter = paramObjDamageDeviceFormsVM.requestFomsVM.RequestNo;
                                }
                                _objResponseModel.PrimeryKeyId = tblDamageDeviceFormExistingdata.RequestFormId;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DamageDevice", "sumbitDamageDeviceForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel ApproveDamageDeviceForm(DamageDeviceFormsVM paramObjDamageDeviceFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDamageDeviceFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDamageDeviceFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        if (paramObjDamageDeviceFormsVM.requestFomsVM.PendingWith == "Department Head")
                        {
                            tblRequestFormsdata.PendingWith = "IT Helpdesk Admin";
                            tblRequestFormsdata.Status = "Pending IT Helpdesk Admin Approval";
                        }
                        if (paramObjDamageDeviceFormsVM.requestFomsVM.PendingWith == "IT Helpdesk Admin")
                        {
                            tblRequestFormsdata.PendingWith = "IT Helpdesk";
                            tblRequestFormsdata.Status = "Pending IT Helpdesk Approval";
                        }

                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        tblDamageDeviceForm tblDamageDeviceFormdata = new tblDamageDeviceForm();
                        tblDamageDeviceFormdata = dbContext.tblDamageDeviceForm.Where(x => x.RequestFormId == paramObjDamageDeviceFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblDamageDeviceFormdata != null)
                        {
                            if (paramObjDamageDeviceFormsVM.requestFomsVM.PendingWith == "Department Head")
                            {
                                tblDamageDeviceFormdata.DHRemarks = paramObjDamageDeviceFormsVM.DHRemarks;
                            }
                            if (paramObjDamageDeviceFormsVM.requestFomsVM.PendingWith == "IT Helpdesk Admin")
                            {
                                tblDamageDeviceFormdata.ITHelpdeskAdminRemarks = paramObjDamageDeviceFormsVM.ITHelpdeskAdminRemarks;
                            }
                            tblDamageDeviceFormdata.UpdateBy = paramPsNo;
                            tblDamageDeviceFormdata.UpdateOn = DateTime.Now;
                            tblDamageDeviceFormdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblDamageDeviceFormdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblDamageDeviceFormStatusHistory tblDamageDeviceFormStatusHistoryData = new tblDamageDeviceFormStatusHistory();
                            tblDamageDeviceFormStatusHistoryData.DamageDeviceFormId = tblDamageDeviceFormdata.DamageDeviceFormId;
                            if (paramObjDamageDeviceFormsVM.requestFomsVM.PendingWith == "Department Head")
                            {
                                tblDamageDeviceFormStatusHistoryData.StatusName = "Department Head Approve";
                            }
                            if (paramObjDamageDeviceFormsVM.requestFomsVM.PendingWith == "IT Helpdesk Admin")
                            {
                                tblDamageDeviceFormStatusHistoryData.StatusName = "IT Helpdesk Admin Approve";
                            }

                            tblDamageDeviceFormStatusHistoryData.IsDeleted = false;
                            tblDamageDeviceFormStatusHistoryData.CreateBy = paramPsNo;
                            tblDamageDeviceFormStatusHistoryData.CreatedOn = DateTime.Now;
                            tblDamageDeviceFormStatusHistoryData.CreateByName = paramUserName;
                            dbContext.tblDamageDeviceFormStatusHistory.Add(tblDamageDeviceFormStatusHistoryData);
                            dbContext.SaveChanges();

                            _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                        }


                        _objResponseModel.PrimeryKeyId = paramObjDamageDeviceFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DamageDevice", "ApproveDamageDeviceForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }
        public ResponseModel CloseDamageDeviceForm(DamageDeviceFormsVM paramObjDamageDeviceFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDamageDeviceFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDamageDeviceFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        if (paramObjDamageDeviceFormsVM.requestFomsVM.PendingWith == "IT Helpdesk")
                        {
                            tblRequestFormsdata.PendingWith = "";
                            tblRequestFormsdata.Status = "Close";
                            tblRequestFormsdata.UpdateBy = paramPsNo;
                            tblRequestFormsdata.UpdateOn = DateTime.Now;
                            tblRequestFormsdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                            dbContext.SaveChanges();
                            tblDamageDeviceForm tblDamageDeviceForm = new tblDamageDeviceForm();
                            tblDamageDeviceForm = dbContext.tblDamageDeviceForm.Where(x => x.RequestFormId == paramObjDamageDeviceFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                            if (tblDamageDeviceForm != null)
                            {
                                tblDamageDeviceForm.ITHelpdeskRemarks = paramObjDamageDeviceFormsVM.ITHelpdeskRemarks;
                                tblDamageDeviceForm.DStatus = paramObjDamageDeviceFormsVM.DStatus;
                                tblDamageDeviceForm.UpdateBy = paramPsNo;
                                tblDamageDeviceForm.UpdateOn = DateTime.Now;
                                tblDamageDeviceForm.UpdateBy = paramUserName;
                                dbContext.Entry(tblDamageDeviceForm).State = EntityState.Modified;
                                dbContext.SaveChanges();

                                tblDamageDeviceFormStatusHistory tblDamageDeviceFormStatusHistoryData = new tblDamageDeviceFormStatusHistory();
                                tblDamageDeviceFormStatusHistoryData.DamageDeviceFormId = tblDamageDeviceForm.DamageDeviceFormId;
                                tblDamageDeviceFormStatusHistoryData.StatusName = "Close";
                                tblDamageDeviceFormStatusHistoryData.IsDeleted = false;
                                tblDamageDeviceFormStatusHistoryData.CreateBy = paramPsNo;
                                tblDamageDeviceFormStatusHistoryData.CreatedOn = DateTime.Now;
                                tblDamageDeviceFormStatusHistoryData.CreateByName = paramUserName;
                                dbContext.tblDamageDeviceFormStatusHistory.Add(tblDamageDeviceFormStatusHistoryData);
                                dbContext.SaveChanges();
                                string EmailId = dbContext.vwEmpInfo.Where(x => x.t_psno == tblRequestFormsdata.PsNo).Select(x => x.t_mail).FirstOrDefault();

                                //dbContext.Sp_Send_Mail_Damage_Device(EmailId, tblRequestFormsdata.RequestNo).FirstOrDefault();
                                _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                                _objResponseModel.Message = HelperMessage.SaveSuccessfully;

                            }
                        }

                        _objResponseModel.PrimeryKeyId = paramObjDamageDeviceFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DamageDevice", "CloseDamageDeviceForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }
        public ResponseModel ReturnDamageDeviceForm(DamageDeviceFormsVM paramObjDamageDeviceFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDamageDeviceFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDamageDeviceFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        tblRequestFormsdata.PendingWith = "Initiator";
                        tblRequestFormsdata.Status = "Returned";
                        tblRequestFormsdata.ReturnRejectWith = paramObjDamageDeviceFormsVM.requestFomsVM.PendingWith;
                        tblRequestFormsdata.ReturnRejectBy = paramPsNo;
                        tblRequestFormsdata.ReturnRejectName = paramUserName;
                        tblRequestFormsdata.ReturnRejectDate = DateTime.Now;
                        tblRequestFormsdata.LastPendingWith = paramObjDamageDeviceFormsVM.requestFomsVM.Status;
                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        tblDamageDeviceForm tblDamageDeviceFormdata = new tblDamageDeviceForm();
                        tblDamageDeviceFormdata = dbContext.tblDamageDeviceForm.Where(x => x.RequestFormId == paramObjDamageDeviceFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblDamageDeviceFormdata != null)
                        {
                            tblDamageDeviceFormdata.DHRemarks = paramObjDamageDeviceFormsVM.DHRemarks;
                            tblDamageDeviceFormdata.ITHelpdeskAdminRemarks = paramObjDamageDeviceFormsVM.ITHelpdeskAdminRemarks;
                            tblDamageDeviceFormdata.ITHelpdeskRemarks = paramObjDamageDeviceFormsVM.ITHelpdeskRemarks;
                            tblDamageDeviceFormdata.DStatus = paramObjDamageDeviceFormsVM.DStatus;
                            tblDamageDeviceFormdata.UpdateBy = paramPsNo;
                            tblDamageDeviceFormdata.UpdateOn = DateTime.Now;
                            tblDamageDeviceFormdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblDamageDeviceFormdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblDamageDeviceFormStatusHistory tblDamageDeviceFormStatusHistoryData = new tblDamageDeviceFormStatusHistory();
                            tblDamageDeviceFormStatusHistoryData.DamageDeviceFormId = tblDamageDeviceFormdata.DamageDeviceFormId;
                            tblDamageDeviceFormStatusHistoryData.StatusName = "Returned";
                            tblDamageDeviceFormStatusHistoryData.IsDeleted = false;
                            tblDamageDeviceFormStatusHistoryData.CreateBy = paramPsNo;
                            tblDamageDeviceFormStatusHistoryData.CreatedOn = DateTime.Now;
                            tblDamageDeviceFormStatusHistoryData.CreateByName = paramUserName;
                            dbContext.tblDamageDeviceFormStatusHistory.Add(tblDamageDeviceFormStatusHistoryData);
                            dbContext.SaveChanges();


                            var tblDamageDeviceFormStatusHistoryList = dbContext.tblDamageDeviceFormStatusHistory.Where(x => x.DamageDeviceFormId == paramObjDamageDeviceFormsVM.DamageDeviceFormId && x.IsDeleted == false).ToList();

                            foreach (var item in tblDamageDeviceFormStatusHistoryList)
                            {
                                var HistoryData = dbContext.tblDamageDeviceFormStatusHistory.Where(x => x.DamageDeviceFormStatusHistoryId == item.DamageDeviceFormStatusHistoryId).FirstOrDefault();
                                if (HistoryData != null)
                                {
                                    HistoryData.IsDeleted = true;
                                    HistoryData.UpdateBy = paramPsNo;
                                    HistoryData.UpdateOn = DateTime.Now;
                                    HistoryData.UpdateBy = paramUserName;
                                    dbContext.Entry(HistoryData).State = EntityState.Modified;
                                    dbContext.SaveChanges();
                                }
                            }
                            _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                        }
                        _objResponseModel.PrimeryKeyId = paramObjDamageDeviceFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DamageDevice", "ReturnDamageDeviceForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }
        public ResponseModel RejectDamageDeviceForm(DamageDeviceFormsVM paramObjDamageDeviceFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDamageDeviceFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDamageDeviceFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        tblRequestFormsdata.PendingWith = "";
                        tblRequestFormsdata.Status = "Reject";

                        tblRequestFormsdata.ReturnRejectWith = paramObjDamageDeviceFormsVM.requestFomsVM.PendingWith;
                        tblRequestFormsdata.ReturnRejectBy = paramPsNo;
                        tblRequestFormsdata.ReturnRejectName = paramUserName;
                        tblRequestFormsdata.ReturnRejectDate = DateTime.Now;
                        tblRequestFormsdata.LastPendingWith = paramObjDamageDeviceFormsVM.requestFomsVM.Status;

                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        tblDamageDeviceForm tblDamageDeviceFormdata = new tblDamageDeviceForm();
                        tblDamageDeviceFormdata = dbContext.tblDamageDeviceForm.Where(x => x.RequestFormId == paramObjDamageDeviceFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblDamageDeviceFormdata != null)
                        {
                            tblDamageDeviceFormdata.DHRemarks = paramObjDamageDeviceFormsVM.DHRemarks;
                            tblDamageDeviceFormdata.ITHelpdeskAdminRemarks = paramObjDamageDeviceFormsVM.ITHelpdeskAdminRemarks;
                            tblDamageDeviceFormdata.ITHelpdeskRemarks = paramObjDamageDeviceFormsVM.ITHelpdeskRemarks;
                            tblDamageDeviceFormdata.DStatus = paramObjDamageDeviceFormsVM.DStatus;
                            tblDamageDeviceFormdata.IsReject = true;
                            tblDamageDeviceFormdata.UpdateBy = paramPsNo;
                            tblDamageDeviceFormdata.UpdateOn = DateTime.Now;
                            tblDamageDeviceFormdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblDamageDeviceFormdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblDamageDeviceFormStatusHistory tblDamageDeviceStatusHistoryData = new tblDamageDeviceFormStatusHistory();
                            tblDamageDeviceStatusHistoryData.DamageDeviceFormId = tblDamageDeviceFormdata.DamageDeviceFormId;
                            tblDamageDeviceStatusHistoryData.StatusName = "Reject";
                            tblDamageDeviceStatusHistoryData.IsDeleted = false;
                            tblDamageDeviceStatusHistoryData.CreateBy = paramPsNo;
                            tblDamageDeviceStatusHistoryData.CreatedOn = DateTime.Now;
                            tblDamageDeviceStatusHistoryData.CreateByName = paramUserName;
                            dbContext.tblDamageDeviceFormStatusHistory.Add(tblDamageDeviceStatusHistoryData);
                            dbContext.SaveChanges();


                            var tblDamageDeviceFormStatusHistoryList = dbContext.tblDamageDeviceFormStatusHistory.Where(x => x.DamageDeviceFormId == paramObjDamageDeviceFormsVM.DamageDeviceFormId && x.IsDeleted == false).ToList();

                            foreach (var item in tblDamageDeviceFormStatusHistoryList)
                            {
                                var HistoryData = dbContext.tblDamageDeviceFormStatusHistory.Where(x => x.DamageDeviceFormStatusHistoryId == item.DamageDeviceFormStatusHistoryId).FirstOrDefault();
                                if (HistoryData != null)
                                {
                                    HistoryData.IsDeleted = true;
                                    HistoryData.UpdateBy = paramPsNo;
                                    HistoryData.UpdateOn = DateTime.Now;
                                    HistoryData.UpdateBy = paramUserName;
                                    dbContext.Entry(HistoryData).State = EntityState.Modified;
                                    dbContext.SaveChanges();
                                }
                            }
                            _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                        }
                        _objResponseModel.PrimeryKeyId = paramObjDamageDeviceFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DamageDevice", "RejectDamageDeviceForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        #endregion
    }
}
