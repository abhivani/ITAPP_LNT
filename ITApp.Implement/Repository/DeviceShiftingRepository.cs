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
    public interface IDeviceShiftingRepository
    {
        DeviceShiftingFormsVM getDeviceShiftingFormId(long paramDeviceShiftingFormId, string paramPsNo, string paramUserName, bool IsPending = false);
        ResponseModel sumbitDeviceShiftingForm(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM, string paramPsNo, string paramUserName);
        ResponseModel ApproveDeviceShiftingForm(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM, string paramPsNo, string paramUserName);
        ResponseModel CloseDeviceShiftingForm(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM, string paramPsNo, string paramUserName);
        ResponseModel ReturnDeviceShiftingForm(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM, string paramPsNo, string paramUserName);
        ResponseModel RejectDeviceShiftingForm(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM, string paramPsNo, string paramUserName);
    }
    #endregion
    public class DeviceShiftingRepository : IDeviceShiftingRepository
    {
        private readonly ITAPPFORMSDbContext dbContext;
        public DeviceShiftingRepository(ITAPPFORMSDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        #region Device Shifting Form  
        public DeviceShiftingFormsVM getDeviceShiftingFormId(long paramDeviceShiftingFormId, string paramPsNo, string paramUserName, bool IsPending = false)
        {
            DeviceShiftingFormsVM objDeviceShiftingFormsVMData = new DeviceShiftingFormsVM();
            if (paramDeviceShiftingFormId > 0)
            {
                var tblDeviceShiftingFormData = dbContext.tblDeviceShiftingForm.Where(x => x.DeviceShiftingFormId == paramDeviceShiftingFormId && x.IsDeleted == false).FirstOrDefault();
                if (tblDeviceShiftingFormData != null)
                {
                    var RequestFormData = dbContext.tblRequestForms.Where(x => x.RequestFormId == tblDeviceShiftingFormData.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (RequestFormData != null)
                    {
                        objDeviceShiftingFormsVMData.requestFomsVM.RequestFormId = RequestFormData.RequestFormId;
                        objDeviceShiftingFormsVMData.requestFomsVM.RequestNo = RequestFormData.RequestNo;
                        objDeviceShiftingFormsVMData.requestFomsVM.RequestDate = RequestFormData.RequestDate;
                        objDeviceShiftingFormsVMData.requestFomsVM.PsNo = RequestFormData.PsNo;
                        objDeviceShiftingFormsVMData.requestFomsVM.RequestFor = RequestFormData.RequestFor;
                        objDeviceShiftingFormsVMData.requestFomsVM.PendingWith = RequestFormData.PendingWith;
                        objDeviceShiftingFormsVMData.requestFomsVM.Status = RequestFormData.Status;
                        objDeviceShiftingFormsVMData.requestFomsVM.ReturnRejectWith = RequestFormData.ReturnRejectWith;
                        objDeviceShiftingFormsVMData.requestFomsVM.LastPendingWith = RequestFormData.LastPendingWith;
                    }
                    objDeviceShiftingFormsVMData.DeviceShiftingFormId = tblDeviceShiftingFormData.DeviceShiftingFormId;
                    objDeviceShiftingFormsVMData.RequestFormId = tblDeviceShiftingFormData.RequestFormId;
                    objDeviceShiftingFormsVMData.Name = tblDeviceShiftingFormData.Name;
                    objDeviceShiftingFormsVMData.DepartmentName = tblDeviceShiftingFormData.DepartmentName;
                    objDeviceShiftingFormsVMData.DepartmentHead = tblDeviceShiftingFormData.DepartmentHead;
                    objDeviceShiftingFormsVMData.OnBehalfof = tblDeviceShiftingFormData.OnBehalfof;
                    objDeviceShiftingFormsVMData.Location = tblDeviceShiftingFormData.Location;
                    objDeviceShiftingFormsVMData.DeviceName = tblDeviceShiftingFormData.DeviceName;
                    objDeviceShiftingFormsVMData.DeviceNo = tblDeviceShiftingFormData.DeviceNo;
                    objDeviceShiftingFormsVMData.FromLocation = tblDeviceShiftingFormData.FromLocation;
                    objDeviceShiftingFormsVMData.ToLocation = tblDeviceShiftingFormData.ToLocation;
                    objDeviceShiftingFormsVMData.ShiftingReason = tblDeviceShiftingFormData.ShiftingReason;
                    objDeviceShiftingFormsVMData.OwnerRemarks = tblDeviceShiftingFormData.OwnerRemarks;
                    objDeviceShiftingFormsVMData.DHRemarks = tblDeviceShiftingFormData.DHRemarks;
                    objDeviceShiftingFormsVMData.ITHelpdeskAdminRemarks = tblDeviceShiftingFormData.ITHelpdeskAdminRemarks;
                    objDeviceShiftingFormsVMData.ITHelpdeskRemarks = tblDeviceShiftingFormData.ITHelpdeskRemarks;
                    objDeviceShiftingFormsVMData.UpdatedinSampatti = tblDeviceShiftingFormData.UpdatedinSampatti;
                    objDeviceShiftingFormsVMData.DeviceType = tblDeviceShiftingFormData.DeviceType;
                    objDeviceShiftingFormsVMData.ShiftingType = tblDeviceShiftingFormData.ShiftingType;


                    var DeviceShiftingformHistoryData = dbContext.tblDeviceShiftingFormStatusHistory.Where(x => x.DeviceShiftingFormId == paramDeviceShiftingFormId && x.IsDeleted == false).ToList();

                    foreach (var item in DeviceShiftingformHistoryData)
                    {
                        if (item.StatusName == "Initiator Submit")
                        {
                            objDeviceShiftingFormsVMData.deviceShiftingFormProcessBarVM.Initiate = "Initiate By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        if (item.StatusName == "Owner Approve")
                        {
                            objDeviceShiftingFormsVMData.deviceShiftingFormProcessBarVM.Owner = "Initiate By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "Department Head Approve")
                        {
                            objDeviceShiftingFormsVMData.deviceShiftingFormProcessBarVM.DepartmentHeadApprove = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "IT Helpdesk Admin Approve")
                        {
                            objDeviceShiftingFormsVMData.deviceShiftingFormProcessBarVM.ITHelpdeskAdminApprove = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "Close")
                        {
                            objDeviceShiftingFormsVMData.deviceShiftingFormProcessBarVM.ITHelpdeskApprove = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
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
                    objDeviceShiftingFormsVMData.requestFomsVM.RequestNo = lastRequestNumber;
                    objDeviceShiftingFormsVMData.requestFomsVM.RequestDate = DateTime.Now;
                    objDeviceShiftingFormsVMData.requestFomsVM.PsNo = paramPsNo;
                    objDeviceShiftingFormsVMData.Name = paramUserName;
                   // objDeviceShiftingFormsVMData.DepartmentName = "Test Department";
                    var vwEmpdata = dbContext.vwEmpInfo.Where(x => x.t_psno == paramPsNo).FirstOrDefault();
                    if (vwEmpdata != null)
                    {
                        objDeviceShiftingFormsVMData.DepartmentName = vwEmpdata.t_dsca;// vwEmpdata.t_depc;
                        if (!string.IsNullOrEmpty(vwEmpdata.t_dhps))
                        {
                            var headempdata = dbContext.vwEmpInfo.Where(x => x.t_psno == vwEmpdata.t_dhps).FirstOrDefault();
                            if (headempdata != null)
                            {
                                objDeviceShiftingFormsVMData.DepartmentHead = vwEmpdata.t_dhps + "-" + headempdata.t_name;
                            }

                        }
                    }
                    //objDeviceShiftingFormsVMData.DepartmentHead = "Test Department Head";
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
                    lastRequestNumber = Year + "-" + appliNo;
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
                objDeviceShiftingFormsVMData.requestFomsVM.RequestNo = lastRequestNumber;
                objDeviceShiftingFormsVMData.requestFomsVM.RequestDate = DateTime.Now;
                objDeviceShiftingFormsVMData.requestFomsVM.PsNo = paramPsNo;
                objDeviceShiftingFormsVMData.Name = paramUserName;
               // objDeviceShiftingFormsVMData.DepartmentName = "Test Department";
                var vwEmpdata = dbContext.vwEmpInfo.Where(x => x.t_psno == paramPsNo).FirstOrDefault();
                if (vwEmpdata != null)
                {
                    objDeviceShiftingFormsVMData.DepartmentName = vwEmpdata.t_dsca;// vwEmpdata.t_depc;
                    if (!string.IsNullOrEmpty(vwEmpdata.t_dhps))
                    {
                        var headempdata = dbContext.vwEmpInfo.Where(x => x.t_psno == vwEmpdata.t_dhps).FirstOrDefault();
                        if (headempdata != null)
                        {
                            objDeviceShiftingFormsVMData.DepartmentHead = vwEmpdata.t_dhps + "-" + headempdata.t_name;
                        }

                    }
                }
                //objDeviceShiftingFormsVMData.DepartmentHead = "Test Department Head";
            }
            if (IsPending == true)
            {

                objDeviceShiftingFormsVMData.requestFomsVM.IsPending = true;
            }
            else
            {
                objDeviceShiftingFormsVMData.requestFomsVM.IsPending = false;
            }
            return objDeviceShiftingFormsVMData;
        }
        public ResponseModel sumbitDeviceShiftingForm(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDeviceShiftingFormsVM.RequestFormId == 0)
                {
                    tblRequestForms tblRequestForms = new tblRequestForms();
                    tblRequestForms.PsNo = paramObjDeviceShiftingFormsVM.requestFomsVM.PsNo;
                    tblRequestForms.RequestNo = paramObjDeviceShiftingFormsVM.requestFomsVM.RequestNo;
                    tblRequestForms.RequestDate = paramObjDeviceShiftingFormsVM.requestFomsVM.RequestDate;
                    tblRequestForms.RequestFor = "SHIFTING";
                    tblRequestForms.PendingWith = "Owner";
                    tblRequestForms.Status = "Pending Owner Approval";
                    if (!string.IsNullOrEmpty(paramObjDeviceShiftingFormsVM.DepartmentHead))
                    {
                        var listdata = paramObjDeviceShiftingFormsVM.DepartmentHead.Split('-');
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
                        tblDeviceShiftingForm tblDeviceShiftingFormData = new tblDeviceShiftingForm();
                        tblDeviceShiftingFormData.RequestFormId = tblRequestForms.RequestFormId;
                        tblDeviceShiftingFormData.Name = paramObjDeviceShiftingFormsVM.Name;
                        tblDeviceShiftingFormData.DepartmentName = paramObjDeviceShiftingFormsVM.DepartmentName;
                        tblDeviceShiftingFormData.DepartmentHead = paramObjDeviceShiftingFormsVM.DepartmentHead;
                        tblDeviceShiftingFormData.OnBehalfof = paramObjDeviceShiftingFormsVM.OnBehalfof;
                        tblDeviceShiftingFormData.Location = paramObjDeviceShiftingFormsVM.Location;
                        tblDeviceShiftingFormData.DeviceName = paramObjDeviceShiftingFormsVM.DeviceName;
                        tblDeviceShiftingFormData.DeviceNo = paramObjDeviceShiftingFormsVM.DeviceNo;
                        tblDeviceShiftingFormData.FromLocation = paramObjDeviceShiftingFormsVM.FromLocation;
                        tblDeviceShiftingFormData.ToLocation = paramObjDeviceShiftingFormsVM.ToLocation;
                        tblDeviceShiftingFormData.ShiftingReason = paramObjDeviceShiftingFormsVM.ShiftingReason;
                        tblDeviceShiftingFormData.IsReject = false;
                        tblDeviceShiftingFormData.IsDeleted = false;
                        tblDeviceShiftingFormData.CreateBy = paramPsNo;
                        tblDeviceShiftingFormData.CreatedOn = DateTime.Now;
                        tblDeviceShiftingFormData.DeviceType = paramObjDeviceShiftingFormsVM.DeviceType;
                        tblDeviceShiftingFormData.ShiftingType = paramObjDeviceShiftingFormsVM.ShiftingType;
                        dbContext.tblDeviceShiftingForm.Add(tblDeviceShiftingFormData);
                        dbContext.SaveChanges();
                        if (tblDeviceShiftingFormData.DeviceShiftingFormId > 0)
                        {
                            tblDeviceShiftingFormStatusHistory tblDeviceShiftingFormStatusHistoryData = new tblDeviceShiftingFormStatusHistory();
                            tblDeviceShiftingFormStatusHistoryData.DeviceShiftingFormId = tblDeviceShiftingFormData.DeviceShiftingFormId;
                            tblDeviceShiftingFormStatusHistoryData.StatusName = "Initiator Submit";
                            tblDeviceShiftingFormStatusHistoryData.IsDeleted = false;
                            tblDeviceShiftingFormStatusHistoryData.CreateBy = paramPsNo;
                            tblDeviceShiftingFormStatusHistoryData.CreatedOn = DateTime.Now;
                            tblDeviceShiftingFormStatusHistoryData.CreateByName = paramUserName;
                            dbContext.tblDeviceShiftingFormStatusHistory.Add(tblDeviceShiftingFormStatusHistoryData);
                            dbContext.SaveChanges();

                            _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully + "|" + "Request No : " + paramObjDeviceShiftingFormsVM.requestFomsVM.RequestNo;
                            _objResponseModel.OtherParameter = paramObjDeviceShiftingFormsVM.requestFomsVM.RequestNo;
                        }
                        _objResponseModel.PrimeryKeyId = paramObjDeviceShiftingFormsVM.RequestFormId;
                    }
                }
                else
                {
                    tblRequestForms tblRequestFormsExistingdata = new tblRequestForms();
                    tblRequestFormsExistingdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDeviceShiftingFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsExistingdata != null)
                    {
                        tblRequestFormsExistingdata.PsNo = paramObjDeviceShiftingFormsVM.requestFomsVM.PsNo;
                        tblRequestFormsExistingdata.RequestNo = paramObjDeviceShiftingFormsVM.requestFomsVM.RequestNo;
                        tblRequestFormsExistingdata.RequestDate = paramObjDeviceShiftingFormsVM.requestFomsVM.RequestDate;
                        tblRequestFormsExistingdata.RequestFor = "SHIFTING";
                        tblRequestFormsExistingdata.PendingWith = "Owner";
                        tblRequestFormsExistingdata.Status = "Pending Owner Approval";
                        tblRequestFormsExistingdata.IsDeleted = false;
                        tblRequestFormsExistingdata.UpdateBy = paramPsNo;
                        tblRequestFormsExistingdata.UpdateOn = DateTime.Now;
                        tblRequestFormsExistingdata.UpdateByName = paramUserName;
                        dbContext.Entry(tblRequestFormsExistingdata).State = EntityState.Modified;
                        dbContext.SaveChanges();
                        if (tblRequestFormsExistingdata.RequestFormId > 0)
                        {
                            tblDeviceShiftingForm tblDeviceShiftingFormExistingdata = new tblDeviceShiftingForm();
                            tblDeviceShiftingFormExistingdata = dbContext.tblDeviceShiftingForm.Where(x => x.DeviceShiftingFormId == paramObjDeviceShiftingFormsVM.DeviceShiftingFormId && x.IsDeleted == false && x.IsReject == false).FirstOrDefault();
                            if (tblDeviceShiftingFormExistingdata != null)
                            {
                                tblDeviceShiftingFormExistingdata.RequestFormId = tblRequestFormsExistingdata.RequestFormId;
                                tblDeviceShiftingFormExistingdata.Name = paramObjDeviceShiftingFormsVM.Name;
                                tblDeviceShiftingFormExistingdata.DepartmentName = paramObjDeviceShiftingFormsVM.DepartmentName;
                                tblDeviceShiftingFormExistingdata.DepartmentHead = paramObjDeviceShiftingFormsVM.DepartmentHead;
                                tblDeviceShiftingFormExistingdata.OnBehalfof = paramObjDeviceShiftingFormsVM.OnBehalfof;
                                tblDeviceShiftingFormExistingdata.Location = paramObjDeviceShiftingFormsVM.Location;
                                tblDeviceShiftingFormExistingdata.DeviceName = paramObjDeviceShiftingFormsVM.DeviceName;
                                tblDeviceShiftingFormExistingdata.DeviceNo = paramObjDeviceShiftingFormsVM.DeviceNo;
                                tblDeviceShiftingFormExistingdata.FromLocation = paramObjDeviceShiftingFormsVM.FromLocation;
                                tblDeviceShiftingFormExistingdata.ToLocation = paramObjDeviceShiftingFormsVM.ToLocation;
                                tblDeviceShiftingFormExistingdata.ShiftingReason = paramObjDeviceShiftingFormsVM.ShiftingReason;
                                tblDeviceShiftingFormExistingdata.IsReject = false;
                                tblDeviceShiftingFormExistingdata.IsDeleted = false;
                                tblDeviceShiftingFormExistingdata.UpdateBy = paramPsNo;
                                tblDeviceShiftingFormExistingdata.UpdateOn = DateTime.Now;
                                tblDeviceShiftingFormExistingdata.DeviceType = paramObjDeviceShiftingFormsVM.DeviceType;
                                tblDeviceShiftingFormExistingdata.ShiftingType = paramObjDeviceShiftingFormsVM.ShiftingType;
                                dbContext.Entry(tblDeviceShiftingFormExistingdata).State = EntityState.Modified;
                                dbContext.SaveChanges();
                                if (tblDeviceShiftingFormExistingdata.DeviceShiftingFormId > 0)
                                {
                                    tblDeviceShiftingFormStatusHistory tblDeviceShiftingFormStatusHistory = new tblDeviceShiftingFormStatusHistory();
                                    tblDeviceShiftingFormStatusHistory.DeviceShiftingFormId = tblDeviceShiftingFormExistingdata.DeviceShiftingFormId;
                                    tblDeviceShiftingFormStatusHistory.StatusName = "Initiator Submit";
                                    tblDeviceShiftingFormStatusHistory.IsDeleted = false;
                                    tblDeviceShiftingFormStatusHistory.CreateBy = paramPsNo;
                                    tblDeviceShiftingFormStatusHistory.CreatedOn = DateTime.Now;
                                    tblDeviceShiftingFormStatusHistory.CreateByName = paramUserName;
                                    dbContext.tblDeviceShiftingFormStatusHistory.Add(tblDeviceShiftingFormStatusHistory);
                                    dbContext.SaveChanges();

                                    _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                                    _objResponseModel.Message = HelperMessage.SaveSuccessfully + "|" + "Request No : " + paramObjDeviceShiftingFormsVM.requestFomsVM.RequestNo;
                                    _objResponseModel.OtherParameter = paramObjDeviceShiftingFormsVM.requestFomsVM.RequestNo;
                                }
                                _objResponseModel.PrimeryKeyId = tblDeviceShiftingFormExistingdata.RequestFormId;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DeviceShifting", "sumbitDeviceShiftingForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel ApproveDeviceShiftingForm(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDeviceShiftingFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDeviceShiftingFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        if (paramObjDeviceShiftingFormsVM.requestFomsVM.PendingWith == "Owner")
                        {
                            tblRequestFormsdata.PendingWith = "Department Head";
                            tblRequestFormsdata.Status = "Pending Department Head Approval";
                        }
                        if (paramObjDeviceShiftingFormsVM.requestFomsVM.PendingWith == "Department Head")
                        {
                            tblRequestFormsdata.PendingWith = "IT Helpdesk Admin";
                            tblRequestFormsdata.Status = "Pending IT Helpdesk Admin Approval";
                        }
                        if (paramObjDeviceShiftingFormsVM.requestFomsVM.PendingWith == "IT Helpdesk Admin")
                        {
                            tblRequestFormsdata.PendingWith = "IT Helpdesk";
                            tblRequestFormsdata.Status = "Pending IT Helpdesk Approval";
                        }

                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        tblDeviceShiftingForm tblDeviceShiftingFormdata = new tblDeviceShiftingForm();
                        tblDeviceShiftingFormdata = dbContext.tblDeviceShiftingForm.Where(x => x.RequestFormId == paramObjDeviceShiftingFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblDeviceShiftingFormdata != null)
                        {
                            if (paramObjDeviceShiftingFormsVM.requestFomsVM.PendingWith == "Owner")
                            {
                                tblDeviceShiftingFormdata.OwnerRemarks = paramObjDeviceShiftingFormsVM.OwnerRemarks;
                            }
                            if (paramObjDeviceShiftingFormsVM.requestFomsVM.PendingWith == "Department Head")
                            {
                                tblDeviceShiftingFormdata.DHRemarks = paramObjDeviceShiftingFormsVM.DHRemarks;
                            }
                            if (paramObjDeviceShiftingFormsVM.requestFomsVM.PendingWith == "IT Helpdesk Admin")
                            {
                                tblDeviceShiftingFormdata.ITHelpdeskAdminRemarks = paramObjDeviceShiftingFormsVM.ITHelpdeskAdminRemarks;
                            }
                            tblDeviceShiftingFormdata.UpdateBy = paramPsNo;
                            tblDeviceShiftingFormdata.UpdateOn = DateTime.Now;
                            tblDeviceShiftingFormdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblDeviceShiftingFormdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblDeviceShiftingFormStatusHistory tblDeviceShiftingFormStatusHistoryData = new tblDeviceShiftingFormStatusHistory();
                            tblDeviceShiftingFormStatusHistoryData.DeviceShiftingFormId = tblDeviceShiftingFormdata.DeviceShiftingFormId;
                            if (paramObjDeviceShiftingFormsVM.requestFomsVM.PendingWith == "Owner")
                            {
                                tblDeviceShiftingFormStatusHistoryData.StatusName = "Owner Approve";
                            }
                            if (paramObjDeviceShiftingFormsVM.requestFomsVM.PendingWith == "Department Head")
                            {
                                tblDeviceShiftingFormStatusHistoryData.StatusName = "Department Head Approve";
                            }
                            if (paramObjDeviceShiftingFormsVM.requestFomsVM.PendingWith == "IT Helpdesk Admin")
                            {
                                tblDeviceShiftingFormStatusHistoryData.StatusName = "IT Helpdesk Admin Approve";
                            }

                            tblDeviceShiftingFormStatusHistoryData.IsDeleted = false;
                            tblDeviceShiftingFormStatusHistoryData.CreateBy = paramPsNo;
                            tblDeviceShiftingFormStatusHistoryData.CreatedOn = DateTime.Now;
                            tblDeviceShiftingFormStatusHistoryData.CreateByName = paramUserName;
                            dbContext.tblDeviceShiftingFormStatusHistory.Add(tblDeviceShiftingFormStatusHistoryData);
                            dbContext.SaveChanges();

                            _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                        }


                        _objResponseModel.PrimeryKeyId = paramObjDeviceShiftingFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DeviceShifing", "ApproveDeviceShiftingForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }
        public ResponseModel CloseDeviceShiftingForm(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDeviceShiftingFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDeviceShiftingFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        if (paramObjDeviceShiftingFormsVM.requestFomsVM.PendingWith == "IT Helpdesk")
                        {
                            tblRequestFormsdata.PendingWith = "";
                            tblRequestFormsdata.Status = "Close";
                            tblRequestFormsdata.UpdateBy = paramPsNo;
                            tblRequestFormsdata.UpdateOn = DateTime.Now;
                            tblRequestFormsdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                            dbContext.SaveChanges();
                            tblDeviceShiftingForm tblDeviceShiftingForm = new tblDeviceShiftingForm();
                            tblDeviceShiftingForm = dbContext.tblDeviceShiftingForm.Where(x => x.RequestFormId == paramObjDeviceShiftingFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                            if (tblDeviceShiftingForm != null)
                            {
                                tblDeviceShiftingForm.ITHelpdeskRemarks = paramObjDeviceShiftingFormsVM.ITHelpdeskRemarks;
                                tblDeviceShiftingForm.UpdatedinSampatti = paramObjDeviceShiftingFormsVM.UpdatedinSampatti;
                                tblDeviceShiftingForm.UpdateBy = paramPsNo;
                                tblDeviceShiftingForm.UpdateOn = DateTime.Now;
                                tblDeviceShiftingForm.UpdateBy = paramUserName;
                                dbContext.Entry(tblDeviceShiftingForm).State = EntityState.Modified;
                                dbContext.SaveChanges();

                                tblDeviceShiftingFormStatusHistory tblDeviceShiftingFormStatusHistoryData = new tblDeviceShiftingFormStatusHistory();
                                tblDeviceShiftingFormStatusHistoryData.DeviceShiftingFormId = tblDeviceShiftingForm.DeviceShiftingFormId;
                                tblDeviceShiftingFormStatusHistoryData.StatusName = "Close";
                                tblDeviceShiftingFormStatusHistoryData.IsDeleted = false;
                                tblDeviceShiftingFormStatusHistoryData.CreateBy = paramPsNo;
                                tblDeviceShiftingFormStatusHistoryData.CreatedOn = DateTime.Now;
                                tblDeviceShiftingFormStatusHistoryData.CreateByName = paramUserName;
                                dbContext.tblDeviceShiftingFormStatusHistory.Add(tblDeviceShiftingFormStatusHistoryData);
                                dbContext.SaveChanges();
                                string EmailId = dbContext.vwEmpInfo.Where(x => x.t_psno == tblRequestFormsdata.PsNo).Select(x => x.t_mail).FirstOrDefault();

                                //dbContext.Sp_Send_Mail_Device_Shifting(EmailId, tblRequestFormsdata.RequestNo).FirstOrDefault();
                                _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                                _objResponseModel.Message = HelperMessage.SaveSuccessfully;

                            }
                        }

                        _objResponseModel.PrimeryKeyId = paramObjDeviceShiftingFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DeviceShifting", "CloseDeviceShiftingForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }
        public ResponseModel ReturnDeviceShiftingForm(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDeviceShiftingFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDeviceShiftingFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        tblRequestFormsdata.PendingWith = "Initiator";
                        tblRequestFormsdata.Status = "Returned";
                        tblRequestFormsdata.ReturnRejectWith = paramObjDeviceShiftingFormsVM.requestFomsVM.PendingWith;
                        tblRequestFormsdata.ReturnRejectBy = paramPsNo;
                        tblRequestFormsdata.ReturnRejectName = paramUserName;
                        tblRequestFormsdata.ReturnRejectDate = DateTime.Now;
                        tblRequestFormsdata.LastPendingWith = paramObjDeviceShiftingFormsVM.requestFomsVM.Status;
                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        tblDeviceShiftingForm tblDeviceShiftingFormdata = new tblDeviceShiftingForm();
                        tblDeviceShiftingFormdata = dbContext.tblDeviceShiftingForm.Where(x => x.RequestFormId == paramObjDeviceShiftingFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblDeviceShiftingFormdata != null)
                        {
                            tblDeviceShiftingFormdata.OwnerRemarks = paramObjDeviceShiftingFormsVM.OwnerRemarks;
                            tblDeviceShiftingFormdata.DHRemarks = paramObjDeviceShiftingFormsVM.DHRemarks;
                            tblDeviceShiftingFormdata.ITHelpdeskAdminRemarks = paramObjDeviceShiftingFormsVM.ITHelpdeskAdminRemarks;
                            tblDeviceShiftingFormdata.ITHelpdeskRemarks = paramObjDeviceShiftingFormsVM.ITHelpdeskRemarks;
                            tblDeviceShiftingFormdata.UpdatedinSampatti = paramObjDeviceShiftingFormsVM.UpdatedinSampatti;
                            tblDeviceShiftingFormdata.DeviceType = paramObjDeviceShiftingFormsVM.DeviceType;
                            tblDeviceShiftingFormdata.ShiftingType = paramObjDeviceShiftingFormsVM.ShiftingType;
                            tblDeviceShiftingFormdata.UpdateBy = paramPsNo;
                            tblDeviceShiftingFormdata.UpdateOn = DateTime.Now;
                            tblDeviceShiftingFormdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblDeviceShiftingFormdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblDeviceShiftingFormStatusHistory tblDeviceShiftingFormStatusHistoryData = new tblDeviceShiftingFormStatusHistory();
                            tblDeviceShiftingFormStatusHistoryData.DeviceShiftingFormId = tblDeviceShiftingFormdata.DeviceShiftingFormId;
                            tblDeviceShiftingFormStatusHistoryData.StatusName = "Returned";
                            tblDeviceShiftingFormStatusHistoryData.IsDeleted = false;
                            tblDeviceShiftingFormStatusHistoryData.CreateBy = paramPsNo;
                            tblDeviceShiftingFormStatusHistoryData.CreatedOn = DateTime.Now;
                            tblDeviceShiftingFormStatusHistoryData.CreateByName = paramUserName;
                            dbContext.tblDeviceShiftingFormStatusHistory.Add(tblDeviceShiftingFormStatusHistoryData);
                            dbContext.SaveChanges();


                            var tblDeviceShiftingFormStatusHistoryList = dbContext.tblDeviceShiftingFormStatusHistory.Where(x => x.DeviceShiftingFormId == paramObjDeviceShiftingFormsVM.DeviceShiftingFormId && x.IsDeleted == false).ToList();

                            foreach (var item in tblDeviceShiftingFormStatusHistoryList)
                            {
                                var HistoryData = dbContext.tblDeviceShiftingFormStatusHistory.Where(x => x.DeviceShiftingFormStatusHistoryId == item.DeviceShiftingFormStatusHistoryId).FirstOrDefault();
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
                        _objResponseModel.PrimeryKeyId = paramObjDeviceShiftingFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DeviceShifting", "ReturnDeviceShiftingForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }
        public ResponseModel RejectDeviceShiftingForm(DeviceShiftingFormsVM paramObjDeviceShiftingFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDeviceShiftingFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDeviceShiftingFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        tblRequestFormsdata.PendingWith = "";
                        tblRequestFormsdata.Status = "Reject";
                        tblRequestFormsdata.ReturnRejectWith = paramObjDeviceShiftingFormsVM.requestFomsVM.PendingWith;
                        tblRequestFormsdata.ReturnRejectBy = paramPsNo;
                        tblRequestFormsdata.ReturnRejectName = paramUserName;
                        tblRequestFormsdata.ReturnRejectDate = DateTime.Now;
                        tblRequestFormsdata.LastPendingWith = paramObjDeviceShiftingFormsVM.requestFomsVM.Status;
                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        tblDeviceShiftingForm tblDeviceShiftingFormdata = new tblDeviceShiftingForm();
                        tblDeviceShiftingFormdata = dbContext.tblDeviceShiftingForm.Where(x => x.RequestFormId == paramObjDeviceShiftingFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblDeviceShiftingFormdata != null)
                        {
                            tblDeviceShiftingFormdata.OwnerRemarks = paramObjDeviceShiftingFormsVM.OwnerRemarks;
                            tblDeviceShiftingFormdata.DHRemarks = paramObjDeviceShiftingFormsVM.DHRemarks;
                            tblDeviceShiftingFormdata.ITHelpdeskAdminRemarks = paramObjDeviceShiftingFormsVM.ITHelpdeskAdminRemarks;
                            tblDeviceShiftingFormdata.ITHelpdeskRemarks = paramObjDeviceShiftingFormsVM.ITHelpdeskRemarks;
                            tblDeviceShiftingFormdata.UpdatedinSampatti = paramObjDeviceShiftingFormsVM.UpdatedinSampatti;
                            tblDeviceShiftingFormdata.DeviceType = paramObjDeviceShiftingFormsVM.DeviceType;
                            tblDeviceShiftingFormdata.ShiftingType = paramObjDeviceShiftingFormsVM.ShiftingType;
                            tblDeviceShiftingFormdata.IsReject = true;
                            tblDeviceShiftingFormdata.UpdateBy = paramPsNo;
                            tblDeviceShiftingFormdata.UpdateOn = DateTime.Now;
                            tblDeviceShiftingFormdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblDeviceShiftingFormdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblDeviceShiftingFormStatusHistory tblDeviceShiftingStatusHistoryData = new tblDeviceShiftingFormStatusHistory();
                            tblDeviceShiftingStatusHistoryData.DeviceShiftingFormId = tblDeviceShiftingFormdata.DeviceShiftingFormId;
                            tblDeviceShiftingStatusHistoryData.StatusName = "Reject";
                            tblDeviceShiftingStatusHistoryData.IsDeleted = false;
                            tblDeviceShiftingStatusHistoryData.CreateBy = paramPsNo;
                            tblDeviceShiftingStatusHistoryData.CreatedOn = DateTime.Now;
                            tblDeviceShiftingStatusHistoryData.CreateByName = paramUserName;
                            dbContext.tblDeviceShiftingFormStatusHistory.Add(tblDeviceShiftingStatusHistoryData);
                            dbContext.SaveChanges();


                            var tblDeviceShiftingFormStatusHistoryList = dbContext.tblDeviceShiftingFormStatusHistory.Where(x => x.DeviceShiftingFormId == paramObjDeviceShiftingFormsVM.DeviceShiftingFormId && x.IsDeleted == false).ToList();

                            foreach (var item in tblDeviceShiftingFormStatusHistoryList)
                            {
                                var HistoryData = dbContext.tblDeviceShiftingFormStatusHistory.Where(x => x.DeviceShiftingFormStatusHistoryId == item.DeviceShiftingFormStatusHistoryId).FirstOrDefault();
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
                        _objResponseModel.PrimeryKeyId = paramObjDeviceShiftingFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DeviceShifting", "RejectDeviceShiftingForm", "", ex.Message);
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
