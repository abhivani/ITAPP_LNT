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
    public interface IDriveAccessRepository
    {
        DriveAccessFormsMV getDriveAccessFormId(long paramDriveAccessFormId, string paramPsNo, string paramUserName, bool IsPending = false);
        ResponseModel sumbitDriveAccessForm(DriveAccessFormsMV paramObjDriveAccessFormsVM, string paramPsNo, string paramUserName);
        ResponseModel ApproveDriveAccessForm(DriveAccessFormsMV paramObjDriveAccessFormsVM, string paramPsNo, string paramUserName);
        ResponseModel CloseDriveAccessForm(DriveAccessFormsMV paramObjDriveAccessFormsVM, string paramPsNo, string paramUserName);
        ResponseModel ReturnDriveAccessForm(DriveAccessFormsMV paramObjDriveAccessFormsVM, string paramPsNo, string paramUserName);
        ResponseModel RejectDriveAccessForm(DriveAccessFormsMV paramObjDriveAccessFormsVM, string paramPsNo, string paramUserName);
        List<tblDriveAccessFormStatusHistory> getStatuOfRequest(long paramDriveAccessFormId);
    }
    #endregion

    public class DriveAccessRepository : IDriveAccessRepository
    {
        private readonly ITAPPFORMSDbContext dbContext;
        public DriveAccessRepository(ITAPPFORMSDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        #region Drive Access Form     
        public DriveAccessFormsMV getDriveAccessFormId(long paramDriveAccessFormId, string paramPsNo, string paramUserName, bool IsPending = false)
        {
            DriveAccessFormsMV objDriveAccessFormsVMData = new DriveAccessFormsMV();
            if (paramDriveAccessFormId > 0)
            {
                var tblDriveAccessFormData = dbContext.tblDriveAccessForm.Where(x => x.DriveAccessFormId == paramDriveAccessFormId && x.IsDeleted == false).FirstOrDefault();
                if (tblDriveAccessFormData != null)
                {
                    var RequestFormData = dbContext.tblRequestForms.Where(x => x.RequestFormId == tblDriveAccessFormData.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (RequestFormData != null)
                    {
                        objDriveAccessFormsVMData.requestFomsVM.RequestFormId = RequestFormData.RequestFormId;
                        objDriveAccessFormsVMData.requestFomsVM.RequestNo = RequestFormData.RequestNo;
                        objDriveAccessFormsVMData.requestFomsVM.RequestDate = RequestFormData.RequestDate;
                        objDriveAccessFormsVMData.requestFomsVM.PsNo = RequestFormData.PsNo;
                        objDriveAccessFormsVMData.requestFomsVM.RequestFor = RequestFormData.RequestFor;
                        objDriveAccessFormsVMData.requestFomsVM.PendingWith = RequestFormData.PendingWith;
                        objDriveAccessFormsVMData.requestFomsVM.Status = RequestFormData.Status;
                        objDriveAccessFormsVMData.requestFomsVM.ReturnRejectWith = RequestFormData.ReturnRejectWith;
                        objDriveAccessFormsVMData.requestFomsVM.LastPendingWith = RequestFormData.LastPendingWith;
                    }
                    objDriveAccessFormsVMData.DriveAccessFormId = tblDriveAccessFormData.DriveAccessFormId;
                    objDriveAccessFormsVMData.RequestFormId = tblDriveAccessFormData.RequestFormId;
                    objDriveAccessFormsVMData.Name = tblDriveAccessFormData.Name;
                    objDriveAccessFormsVMData.DepartmentName = tblDriveAccessFormData.DepartmentName;
                    objDriveAccessFormsVMData.DepartmentHead = tblDriveAccessFormData.DepartmentHead;
                    objDriveAccessFormsVMData.OnBehalfof = tblDriveAccessFormData.OnBehalfof;
                    objDriveAccessFormsVMData.Location = tblDriveAccessFormData.Location;
                    objDriveAccessFormsVMData.Reason = tblDriveAccessFormData.Reason;
                    objDriveAccessFormsVMData.Path = tblDriveAccessFormData.Path;
                    objDriveAccessFormsVMData.DHRemarks = tblDriveAccessFormData.DHRemarks;
                    objDriveAccessFormsVMData.ITDHRemarks = tblDriveAccessFormData.ITDHRemarks;
                    objDriveAccessFormsVMData.SARemarks = tblDriveAccessFormData.SARemarks;


                    var DriveAccessformHistoryData = dbContext.tblDriveAccessFormStatusHistory.Where(x => x.DriveAccessFormId == paramDriveAccessFormId && x.IsDeleted == false).ToList();

                    foreach (var item in DriveAccessformHistoryData)
                    {                       
                        if (item.StatusName == "Initiator Submit")
                        {
                            objDriveAccessFormsVMData.driveAccessFormsProcessBarVM.Initiate = "Initiate By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "Department Head Approve")
                        {
                            objDriveAccessFormsVMData.driveAccessFormsProcessBarVM.DepartmentHeadApprove = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "Infra Head Approve")
                        {
                            objDriveAccessFormsVMData.driveAccessFormsProcessBarVM.InfraHeadApprove = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "Close")
                        {
                            objDriveAccessFormsVMData.driveAccessFormsProcessBarVM.ServerAdmin = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
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
                    objDriveAccessFormsVMData.requestFomsVM.RequestNo = lastRequestNumber;
                    objDriveAccessFormsVMData.requestFomsVM.RequestDate = DateTime.Now;
                    objDriveAccessFormsVMData.requestFomsVM.PsNo = paramPsNo;
                    objDriveAccessFormsVMData.Name = paramUserName;
                   // objDriveAccessFormsVMData.DepartmentName = "Test Department";
                    var vwEmpdata = dbContext.vwEmpInfo.Where(x => x.t_psno == paramPsNo).FirstOrDefault();
                    if (vwEmpdata != null)
                    {
                        objDriveAccessFormsVMData.DepartmentName = vwEmpdata.t_dsca;// vwEmpdata.t_depc;
                        if (!string.IsNullOrEmpty(vwEmpdata.t_dhps))
                        {
                            var headempdata = dbContext.vwEmpInfo.Where(x => x.t_psno == vwEmpdata.t_dhps).FirstOrDefault();
                            if (headempdata != null)
                            {
                                objDriveAccessFormsVMData.DepartmentHead = vwEmpdata.t_dhps + "-" + headempdata.t_name;
                            }

                        }
                    }
                    // objDriveAccessFormsVMData.DepartmentHead = "Test Department Head";
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
                objDriveAccessFormsVMData.requestFomsVM.RequestNo = lastRequestNumber;
                objDriveAccessFormsVMData.requestFomsVM.RequestDate = DateTime.Now;
                objDriveAccessFormsVMData.requestFomsVM.PsNo = paramPsNo;
                objDriveAccessFormsVMData.Name = paramUserName;
                //objDriveAccessFormsVMData.DepartmentName = "Test Department";
                var vwEmpdata = dbContext.vwEmpInfo.Where(x => x.t_psno == paramPsNo).FirstOrDefault();
                if (vwEmpdata != null)
                {
                    objDriveAccessFormsVMData.DepartmentName = vwEmpdata.t_dsca; //vwEmpdata.t_depc;
                    if (!string.IsNullOrEmpty(vwEmpdata.t_dhps))
                    {
                        var headempdata = dbContext.vwEmpInfo.Where(x => x.t_psno == vwEmpdata.t_dhps).FirstOrDefault();
                        if (headempdata != null)
                        {
                            objDriveAccessFormsVMData.DepartmentHead = vwEmpdata.t_dhps + "-" + headempdata.t_name;
                        }

                    }
                }
                // objDriveAccessFormsVMData.DepartmentHead = "Test Department Head";
            }
            if (IsPending == true)
            {

                objDriveAccessFormsVMData.requestFomsVM.IsPending = true;
            }
            else
            {
                objDriveAccessFormsVMData.requestFomsVM.IsPending = false;
            }
            return objDriveAccessFormsVMData;
        }

        public ResponseModel sumbitDriveAccessForm(DriveAccessFormsMV paramObjDriveAccessFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDriveAccessFormsVM.RequestFormId == 0)
                {
                    tblRequestForms tblRequestForms = new tblRequestForms();
                    tblRequestForms.PsNo = paramObjDriveAccessFormsVM.requestFomsVM.PsNo;
                    tblRequestForms.RequestNo = paramObjDriveAccessFormsVM.requestFomsVM.RequestNo;
                    tblRequestForms.RequestDate = paramObjDriveAccessFormsVM.requestFomsVM.RequestDate;
                    tblRequestForms.RequestFor = "DRIVE";
                    tblRequestForms.PendingWith = "Department Head";
                    tblRequestForms.Status = "Pending Department Head Approval";
                    if (!string.IsNullOrEmpty(paramObjDriveAccessFormsVM.DepartmentHead))
                    {
                        var listdata = paramObjDriveAccessFormsVM.DepartmentHead.Split('-');
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
                        tblDriveAccessForm tblDriveAccessFormData = new tblDriveAccessForm();
                        tblDriveAccessFormData.RequestFormId = tblRequestForms.RequestFormId;
                        tblDriveAccessFormData.Name = paramObjDriveAccessFormsVM.Name;
                        tblDriveAccessFormData.DepartmentName = paramObjDriveAccessFormsVM.DepartmentName;
                        tblDriveAccessFormData.DepartmentHead = paramObjDriveAccessFormsVM.DepartmentHead;
                        tblDriveAccessFormData.OnBehalfof = paramObjDriveAccessFormsVM.OnBehalfof;
                        tblDriveAccessFormData.Location = paramObjDriveAccessFormsVM.Location;
                        tblDriveAccessFormData.Reason = paramObjDriveAccessFormsVM.Reason;
                        tblDriveAccessFormData.Path = paramObjDriveAccessFormsVM.Path;
                        tblDriveAccessFormData.IsReject = false;
                        tblDriveAccessFormData.IsDeleted = false;
                        tblDriveAccessFormData.CreateBy = paramPsNo;
                        tblDriveAccessFormData.CreatedOn = DateTime.Now;
                        dbContext.tblDriveAccessForm.Add(tblDriveAccessFormData);
                        dbContext.SaveChanges();
                        if (tblDriveAccessFormData.DriveAccessFormId > 0)
                        {
                            tblDriveAccessFormStatusHistory tblDriveAccessFormStatusHistoryData = new tblDriveAccessFormStatusHistory();
                            tblDriveAccessFormStatusHistoryData.DriveAccessFormId = tblDriveAccessFormData.DriveAccessFormId;
                            tblDriveAccessFormStatusHistoryData.StatusName = "Initiator Submit";
                            tblDriveAccessFormStatusHistoryData.IsDeleted = false;
                            tblDriveAccessFormStatusHistoryData.CreateBy = paramPsNo;
                            tblDriveAccessFormStatusHistoryData.CreatedOn = DateTime.Now;
                            tblDriveAccessFormStatusHistoryData.CreateByName = paramUserName;
                            dbContext.tblDriveAccessFormStatusHistory.Add(tblDriveAccessFormStatusHistoryData);
                            dbContext.SaveChanges();

                            _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully + "|" + "Request No : " + paramObjDriveAccessFormsVM.requestFomsVM.RequestNo;
                            _objResponseModel.OtherParameter = paramObjDriveAccessFormsVM.requestFomsVM.RequestNo;
                        }
                        _objResponseModel.PrimeryKeyId = paramObjDriveAccessFormsVM.RequestFormId;
                    }
                }
                else
                {
                    tblRequestForms tblRequestFormsExistingdata = new tblRequestForms();
                    tblRequestFormsExistingdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDriveAccessFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsExistingdata != null)
                    {
                        tblRequestFormsExistingdata.PsNo = paramObjDriveAccessFormsVM.requestFomsVM.PsNo;
                        tblRequestFormsExistingdata.RequestNo = paramObjDriveAccessFormsVM.requestFomsVM.RequestNo;
                        tblRequestFormsExistingdata.RequestDate = paramObjDriveAccessFormsVM.requestFomsVM.RequestDate;
                        tblRequestFormsExistingdata.RequestFor = "DRIVE";
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
                            tblDriveAccessForm tblDriveAccessFormExistingdata = new tblDriveAccessForm();
                            tblDriveAccessFormExistingdata = dbContext.tblDriveAccessForm.Where(x => x.DriveAccessFormId == paramObjDriveAccessFormsVM.DriveAccessFormId && x.IsDeleted == false && x.IsReject == false).FirstOrDefault();
                            if (tblDriveAccessFormExistingdata != null)
                            {
                                tblDriveAccessFormExistingdata.RequestFormId = tblRequestFormsExistingdata.RequestFormId;
                                tblDriveAccessFormExistingdata.Name = paramObjDriveAccessFormsVM.Name;
                                tblDriveAccessFormExistingdata.DepartmentName = paramObjDriveAccessFormsVM.DepartmentName;
                                tblDriveAccessFormExistingdata.DepartmentHead = paramObjDriveAccessFormsVM.DepartmentHead;
                                tblDriveAccessFormExistingdata.OnBehalfof = paramObjDriveAccessFormsVM.OnBehalfof;
                                tblDriveAccessFormExistingdata.Location = paramObjDriveAccessFormsVM.Location;
                                tblDriveAccessFormExistingdata.Reason = paramObjDriveAccessFormsVM.Reason;
                                tblDriveAccessFormExistingdata.Path = paramObjDriveAccessFormsVM.Path;
                                tblDriveAccessFormExistingdata.IsReject = false;
                                tblDriveAccessFormExistingdata.IsDeleted = false;
                                tblDriveAccessFormExistingdata.UpdateBy = paramPsNo;
                                tblDriveAccessFormExistingdata.UpdateOn = DateTime.Now;
                                dbContext.Entry(tblDriveAccessFormExistingdata).State = EntityState.Modified;
                                dbContext.SaveChanges();
                                if (tblDriveAccessFormExistingdata.DriveAccessFormId > 0)
                                {
                                    tblDriveAccessFormStatusHistory tblDriveAccessFormStatusHistory = new tblDriveAccessFormStatusHistory();
                                    tblDriveAccessFormStatusHistory.DriveAccessFormId = tblDriveAccessFormExistingdata.DriveAccessFormId;
                                    tblDriveAccessFormStatusHistory.StatusName = "Initiator Submit";
                                    tblDriveAccessFormStatusHistory.IsDeleted = false;
                                    tblDriveAccessFormStatusHistory.CreateBy = paramPsNo;
                                    tblDriveAccessFormStatusHistory.CreatedOn = DateTime.Now;
                                    tblDriveAccessFormStatusHistory.CreateByName = paramUserName;
                                    dbContext.tblDriveAccessFormStatusHistory.Add(tblDriveAccessFormStatusHistory);
                                    dbContext.SaveChanges();

                                    _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                                    _objResponseModel.Message = HelperMessage.SaveSuccessfully + "|" + "Request No : " + paramObjDriveAccessFormsVM.requestFomsVM.RequestNo;
                                    _objResponseModel.OtherParameter = paramObjDriveAccessFormsVM.requestFomsVM.RequestNo;
                                }
                                _objResponseModel.PrimeryKeyId = tblDriveAccessFormExistingdata.RequestFormId;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DriveAccess", "sumbitDriveAccessForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel ApproveDriveAccessForm(DriveAccessFormsMV paramObjDriveAccessFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDriveAccessFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDriveAccessFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        if (paramObjDriveAccessFormsVM.requestFomsVM.PendingWith == "Department Head")
                        {
                            tblRequestFormsdata.PendingWith = "Infra Head";
                            tblRequestFormsdata.Status = "Pending Infra Head Approval";
                        }
                        if (paramObjDriveAccessFormsVM.requestFomsVM.PendingWith == "Infra Head")
                        {
                            tblRequestFormsdata.PendingWith = "Server Admin";
                            tblRequestFormsdata.Status = "Pending Server Admin Approval";
                        }

                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        tblDriveAccessForm tblDriveAccessFormdata = new tblDriveAccessForm();
                        tblDriveAccessFormdata = dbContext.tblDriveAccessForm.Where(x => x.RequestFormId == paramObjDriveAccessFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblDriveAccessFormdata != null)
                        {
                            if (paramObjDriveAccessFormsVM.requestFomsVM.PendingWith == "Department Head")
                            {
                                tblDriveAccessFormdata.DHRemarks = paramObjDriveAccessFormsVM.DHRemarks;
                            }
                            if (paramObjDriveAccessFormsVM.requestFomsVM.PendingWith == "Infra Head")
                            {
                                tblDriveAccessFormdata.ITDHRemarks = paramObjDriveAccessFormsVM.ITDHRemarks;
                            }
                            tblDriveAccessFormdata.UpdateBy = paramPsNo;
                            tblDriveAccessFormdata.UpdateOn = DateTime.Now;
                            tblDriveAccessFormdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblDriveAccessFormdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblDriveAccessFormStatusHistory tblDriveAccessFormStatusHistoryData = new tblDriveAccessFormStatusHistory();
                            tblDriveAccessFormStatusHistoryData.DriveAccessFormId = tblDriveAccessFormdata.DriveAccessFormId;
                            if (paramObjDriveAccessFormsVM.requestFomsVM.PendingWith == "Department Head")
                            {
                                tblDriveAccessFormStatusHistoryData.StatusName = "Department Head Approve";
                            }
                            if (paramObjDriveAccessFormsVM.requestFomsVM.PendingWith == "Infra Head")
                            {
                                tblDriveAccessFormStatusHistoryData.StatusName = "Infra Head Approve";
                            }

                            tblDriveAccessFormStatusHistoryData.IsDeleted = false;
                            tblDriveAccessFormStatusHistoryData.CreateBy = paramPsNo;
                            tblDriveAccessFormStatusHistoryData.CreatedOn = DateTime.Now;
                            tblDriveAccessFormStatusHistoryData.CreateByName = paramUserName;
                            dbContext.tblDriveAccessFormStatusHistory.Add(tblDriveAccessFormStatusHistoryData);
                            dbContext.SaveChanges();

                            _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                        }


                        _objResponseModel.PrimeryKeyId = paramObjDriveAccessFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DriveAccess", "ApproveDriveAccessForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel CloseDriveAccessForm(DriveAccessFormsMV paramObjDriveAccessFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDriveAccessFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDriveAccessFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        if (paramObjDriveAccessFormsVM.requestFomsVM.PendingWith == "Server Admin")
                        {
                            tblRequestFormsdata.PendingWith = "";
                            tblRequestFormsdata.Status = "Close";
                            tblRequestFormsdata.UpdateBy = paramPsNo;
                            tblRequestFormsdata.UpdateOn = DateTime.Now;
                            tblRequestFormsdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                            dbContext.SaveChanges();
                            tblDriveAccessForm tblDriveAccessFormdata = new tblDriveAccessForm();
                            tblDriveAccessFormdata = dbContext.tblDriveAccessForm.Where(x => x.RequestFormId == paramObjDriveAccessFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                            if (tblDriveAccessFormdata != null)
                            {
                                tblDriveAccessFormdata.SARemarks = paramObjDriveAccessFormsVM.SARemarks;
                                tblDriveAccessFormdata.UpdateBy = paramPsNo;
                                tblDriveAccessFormdata.UpdateOn = DateTime.Now;
                                tblDriveAccessFormdata.UpdateBy = paramUserName;
                                dbContext.Entry(tblDriveAccessFormdata).State = EntityState.Modified;
                                dbContext.SaveChanges();

                                tblDriveAccessFormStatusHistory tblDriveAccessFormStatusHistoryData = new tblDriveAccessFormStatusHistory();
                                tblDriveAccessFormStatusHistoryData.DriveAccessFormId = tblDriveAccessFormdata.DriveAccessFormId;
                                tblDriveAccessFormStatusHistoryData.StatusName = "Close";
                                tblDriveAccessFormStatusHistoryData.IsDeleted = false;
                                tblDriveAccessFormStatusHistoryData.CreateBy = paramPsNo;
                                tblDriveAccessFormStatusHistoryData.CreatedOn = DateTime.Now;
                                tblDriveAccessFormStatusHistoryData.CreateByName = paramUserName;
                                dbContext.tblDriveAccessFormStatusHistory.Add(tblDriveAccessFormStatusHistoryData);
                                dbContext.SaveChanges();
                                string EmailId=  dbContext.vwEmpInfo.Where(x => x.t_psno == tblRequestFormsdata.PsNo).Select(x => x.t_mail).FirstOrDefault();

                                dbContext.Sp_Send_Mail_Drive_Access(EmailId, tblRequestFormsdata.RequestNo).FirstOrDefault();
                                    _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                                    _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                                
                            }
                        }

                        _objResponseModel.PrimeryKeyId = paramObjDriveAccessFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DriveAccess", "CloseDriveAccessForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel ReturnDriveAccessForm(DriveAccessFormsMV paramObjDriveAccessFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDriveAccessFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDriveAccessFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        tblRequestFormsdata.PendingWith = "Initiator";
                        tblRequestFormsdata.Status = "Returned";

                        tblRequestFormsdata.ReturnRejectWith = paramObjDriveAccessFormsVM.requestFomsVM.PendingWith;
                        tblRequestFormsdata.ReturnRejectBy = paramPsNo;
                        tblRequestFormsdata.ReturnRejectName = paramUserName;
                        tblRequestFormsdata.ReturnRejectDate = DateTime.Now;
                        tblRequestFormsdata.LastPendingWith = paramObjDriveAccessFormsVM.requestFomsVM.Status;
                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        tblDriveAccessForm tblDriveAccessFormdata = new tblDriveAccessForm();
                        tblDriveAccessFormdata = dbContext.tblDriveAccessForm.Where(x => x.RequestFormId == paramObjDriveAccessFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblDriveAccessFormdata != null)
                        {
                            tblDriveAccessFormdata.DHRemarks = paramObjDriveAccessFormsVM.DHRemarks;
                            tblDriveAccessFormdata.ITDHRemarks = paramObjDriveAccessFormsVM.ITDHRemarks;
                            tblDriveAccessFormdata.SARemarks = paramObjDriveAccessFormsVM.SARemarks;
                            tblDriveAccessFormdata.UpdateBy = paramPsNo;
                            tblDriveAccessFormdata.UpdateBy = paramPsNo;
                            tblDriveAccessFormdata.UpdateOn = DateTime.Now;
                            tblDriveAccessFormdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblDriveAccessFormdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblDriveAccessFormStatusHistory tblDriveAccessFormStatusHistoryData = new tblDriveAccessFormStatusHistory();
                            tblDriveAccessFormStatusHistoryData.DriveAccessFormId = tblDriveAccessFormdata.DriveAccessFormId;
                            tblDriveAccessFormStatusHistoryData.StatusName = "Returned";
                            tblDriveAccessFormStatusHistoryData.IsDeleted = false;
                            tblDriveAccessFormStatusHistoryData.CreateBy = paramPsNo;
                            tblDriveAccessFormStatusHistoryData.CreatedOn = DateTime.Now;
                            tblDriveAccessFormStatusHistoryData.CreateByName = paramUserName;
                            dbContext.tblDriveAccessFormStatusHistory.Add(tblDriveAccessFormStatusHistoryData);
                            dbContext.SaveChanges();


                            var tblDriveAccessFormStatusHistoryList = dbContext.tblDriveAccessFormStatusHistory.Where(x => x.DriveAccessFormId == paramObjDriveAccessFormsVM.DriveAccessFormId && x.IsDeleted == false).ToList();

                            foreach (var item in tblDriveAccessFormStatusHistoryList)
                            {
                                var HistoryData = dbContext.tblDriveAccessFormStatusHistory.Where(x => x.DriveAccessFormStatusHistoryId == item.DriveAccessFormStatusHistoryId).FirstOrDefault();
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
                        _objResponseModel.PrimeryKeyId = paramObjDriveAccessFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DriveAccess", "ReturnDriveAccessForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel RejectDriveAccessForm(DriveAccessFormsMV paramObjDriveAccessFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjDriveAccessFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjDriveAccessFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        tblRequestFormsdata.PendingWith = "";
                        tblRequestFormsdata.Status = "Reject";
                        tblRequestFormsdata.ReturnRejectWith = paramObjDriveAccessFormsVM.requestFomsVM.PendingWith;
                        tblRequestFormsdata.ReturnRejectBy = paramPsNo;
                        tblRequestFormsdata.ReturnRejectName = paramUserName;
                        tblRequestFormsdata.ReturnRejectDate = DateTime.Now;
                        tblRequestFormsdata.LastPendingWith = paramObjDriveAccessFormsVM.requestFomsVM.Status;
                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        tblDriveAccessForm tblDriveAccessFormdata = new tblDriveAccessForm();
                        tblDriveAccessFormdata = dbContext.tblDriveAccessForm.Where(x => x.RequestFormId == paramObjDriveAccessFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblDriveAccessFormdata != null)
                        {
                            tblDriveAccessFormdata.DHRemarks = paramObjDriveAccessFormsVM.DHRemarks;
                            tblDriveAccessFormdata.ITDHRemarks = paramObjDriveAccessFormsVM.ITDHRemarks;
                            tblDriveAccessFormdata.SARemarks = paramObjDriveAccessFormsVM.SARemarks;
                            tblDriveAccessFormdata.IsReject = true;
                            tblDriveAccessFormdata.UpdateBy = paramPsNo;
                            tblDriveAccessFormdata.UpdateOn = DateTime.Now;
                            tblDriveAccessFormdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblDriveAccessFormdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblDriveAccessFormStatusHistory tblDriveAccessFormStatusHistoryData = new tblDriveAccessFormStatusHistory();
                            tblDriveAccessFormStatusHistoryData.DriveAccessFormId = tblDriveAccessFormdata.DriveAccessFormId;
                            tblDriveAccessFormStatusHistoryData.StatusName = "Reject";
                            tblDriveAccessFormStatusHistoryData.IsDeleted = false;
                            tblDriveAccessFormStatusHistoryData.CreateBy = paramPsNo;
                            tblDriveAccessFormStatusHistoryData.CreatedOn = DateTime.Now;
                            tblDriveAccessFormStatusHistoryData.CreateByName = paramUserName;
                            dbContext.tblDriveAccessFormStatusHistory.Add(tblDriveAccessFormStatusHistoryData);
                            dbContext.SaveChanges();


                            var tblDriveAccessFormStatusHistoryList = dbContext.tblDriveAccessFormStatusHistory.Where(x => x.DriveAccessFormId == paramObjDriveAccessFormsVM.DriveAccessFormId && x.IsDeleted == false).ToList();

                            foreach (var item in tblDriveAccessFormStatusHistoryList)
                            {
                                var HistoryData = dbContext.tblDriveAccessFormStatusHistory.Where(x => x.DriveAccessFormStatusHistoryId == item.DriveAccessFormStatusHistoryId).FirstOrDefault();
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
                        _objResponseModel.PrimeryKeyId = paramObjDriveAccessFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("DriveAccess", "RejectDriveAccessForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public List<tblDriveAccessFormStatusHistory> getStatuOfRequest(long paramDriveAccessFormId)
        {            
            return dbContext.tblDriveAccessFormStatusHistory.Where(s => s.DriveAccessFormId == paramDriveAccessFormId && s.IsDeleted == false).ToList();
        }

        #endregion
    }
}
