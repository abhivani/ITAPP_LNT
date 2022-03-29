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
    public interface INewItRequisitionRepository
    {
        NewITRequisitionFormVM getNewITRequisitionFormId(long paramNewITRequisitionID, string paramPsNo, string paramUserName, bool IsPending);
        ResponseModel sumbitNewItRequisitionForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVM, string paramPsNo, string paramUserName);
        ResponseModel ApproveNewItRequisitionForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVMFormsVM, string paramPsNo, string paramUserName);
        ResponseModel UnderProcessNewItRequisitionForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVMFormsVM, string paramPsNo, string paramUserName);
        ResponseModel CompleteNewITForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVM, string paramPsNo, string paramUserName);
        ResponseModel ReturnNewITForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVM, string paramPsNo, string paramUserName);
        ResponseModel RejectNewITForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVM, string paramPsNo, string paramUserName);
        ResponseModel GetDeliveryByData(string paramPsNo);
    }
    #endregion

    #region Implimation

    public class NewItRequisitionRepository : INewItRequisitionRepository
    {

        private readonly ITAPPFORMSDbContext dbContext;
        public NewItRequisitionRepository(ITAPPFORMSDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public NewITRequisitionFormVM getNewITRequisitionFormId(long paramNewITRequisitionID, string paramPsNo, string paramUserName, bool IsPending)
        {
            NewITRequisitionFormVM objNewITRequisitionFormVM = new NewITRequisitionFormVM();
            if (paramNewITRequisitionID > 0)
            {

                var tblNewITFormtblData = dbContext.tblNewITRequisition.Where(x => x.NewITRequisitionID == paramNewITRequisitionID && x.IsDeleted == false).FirstOrDefault();
                if (tblNewITFormtblData != null)
                {
                    var RequestFormData = dbContext.tblRequestForms.Where(x => x.RequestFormId == tblNewITFormtblData.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (RequestFormData != null)
                    {
                        objNewITRequisitionFormVM.requestFomsVM.RequestFormId = RequestFormData.RequestFormId;
                        objNewITRequisitionFormVM.requestFomsVM.RequestNo = RequestFormData.RequestNo;
                        objNewITRequisitionFormVM.requestFomsVM.RequestDate = RequestFormData.RequestDate;
                        objNewITRequisitionFormVM.requestFomsVM.PsNo = RequestFormData.PsNo;
                        objNewITRequisitionFormVM.requestFomsVM.RequestFor = RequestFormData.RequestFor;
                        objNewITRequisitionFormVM.requestFomsVM.PendingWith = RequestFormData.PendingWith;
                        objNewITRequisitionFormVM.requestFomsVM.Status = RequestFormData.Status;
                        objNewITRequisitionFormVM.requestFomsVM.ReturnRejectWith = RequestFormData.ReturnRejectWith;
                        objNewITRequisitionFormVM.requestFomsVM.LastPendingWith = RequestFormData.LastPendingWith;
                    }

                    objNewITRequisitionFormVM.NewITRequisitionID = tblNewITFormtblData.NewITRequisitionID;
                    objNewITRequisitionFormVM.RequestFormId = tblNewITFormtblData.RequestFormId;
                    objNewITRequisitionFormVM.RequestNo = tblNewITFormtblData.RequestNo;
                    objNewITRequisitionFormVM.PSNo = tblNewITFormtblData.PSNo;
                    objNewITRequisitionFormVM.Department = tblNewITFormtblData.Department;
                    objNewITRequisitionFormVM.Name = tblNewITFormtblData.Name;
                    objNewITRequisitionFormVM.DepartmentHead = tblNewITFormtblData.DepartmentHead;
                    objNewITRequisitionFormVM.OnBehalfof = tblNewITFormtblData.OnBehalfof;
                    objNewITRequisitionFormVM.Device = tblNewITFormtblData.Device;
                    objNewITRequisitionFormVM.DHRemarks = tblNewITFormtblData.DHRemarks;
                    objNewITRequisitionFormVM.ITHDAdminRemark = tblNewITFormtblData.ITHDAdminRemark;
                    objNewITRequisitionFormVM.Location = tblNewITFormtblData.Location;
                    objNewITRequisitionFormVM.Reason = tblNewITFormtblData.Reason;
                    objNewITRequisitionFormVM.ITDHRemark = tblNewITFormtblData.ITDHRemark;
                    objNewITRequisitionFormVM.AssentID = tblNewITFormtblData.AssentID;
                    objNewITRequisitionFormVM.Mark = tblNewITFormtblData.Mark;
                    objNewITRequisitionFormVM.Model = tblNewITFormtblData.Model;
                    objNewITRequisitionFormVM.Family = tblNewITFormtblData.Family;
                    objNewITRequisitionFormVM.OperationSystem = tblNewITFormtblData.OperationSystem;
                    objNewITRequisitionFormVM.MonitorSize = tblNewITFormtblData.MonitorSize;
                    objNewITRequisitionFormVM.SerialNO = tblNewITFormtblData.SerialNO;
                    objNewITRequisitionFormVM.MACAddress = tblNewITFormtblData.MACAddress;
                    objNewITRequisitionFormVM.InstallationRemark = tblNewITFormtblData.InstallationRemark;
                    objNewITRequisitionFormVM.HDD = tblNewITFormtblData.HDD;
                    objNewITRequisitionFormVM.RAM = tblNewITFormtblData.RAM;
                    objNewITRequisitionFormVM.DeliveryBY = tblNewITFormtblData.DeliveryBY;
                    objNewITRequisitionFormVM.AntivirusInstalled = tblNewITFormtblData.AntivirusInstalled;
                    objNewITRequisitionFormVM.SoftwareInstalled = tblNewITFormtblData.SoftwareInstalled;
                    objNewITRequisitionFormVM.DomainRegistion = tblNewITFormtblData.DomainRegistion;
                    objNewITRequisitionFormVM.LocationByDelivery = tblNewITFormtblData.LocationByDelivery;
                    objNewITRequisitionFormVM.Area = tblNewITFormtblData.Area;
                    objNewITRequisitionFormVM.OneDriveConfigured = tblNewITFormtblData.OneDriveConfigured;
                    objNewITRequisitionFormVM.BitlockerConfigured = tblNewITFormtblData.BitlockerConfigured;


                    //var USBRequestformHistoryData = dbContext.tblUSBRequestFormStatusHistory.Where(x => x.USBRequestFormId == paramUSBRequestFormId && x.IsDeleted == false).ToList();
                    //foreach (var item in USBRequestformHistoryData)
                    //{
                    //    USBRequestFormsStatusHistoryVM uSBRequestFormsStatusHistoryVM = new USBRequestFormsStatusHistoryVM();
                    //    uSBRequestFormsStatusHistoryVM.StatusName = item.StatusName;
                    //    uSBRequestFormsStatusHistoryVM.CreatedOn = item.CreatedOn;
                    //    uSBRequestFormsStatusHistoryVM.CreateByName = item.CreateByName;
                    //    objUSBRequestFormsVMData.usbrequestFormsStatusHistoryVM.Add(uSBRequestFormsStatusHistoryVM);
                    //}


                    //var NewITHistoryData = dbContext.tblNewITRequisitionStatusHistory.Where(x => x.NewITRequisitionID == paramNewITRequisitionID && x.IsDeleted == false).ToList();
                    //foreach (var item in NewITHistoryData) 
                    //{
                    //    NewITRequisitionStatusHistoryVM NewITRequisitionStatusHistoryVM = new NewITRequisitionStatusHistoryVM();
                    //    NewITRequisitionStatusHistoryVM.StatusName = item.StatusName;
                    //    NewITRequisitionStatusHistoryVM.CreatedOn = item.CreatedOn;
                    //    NewITRequisitionStatusHistoryVM.CreateByName = item.CreateByName;
                    //    objNewITRequisitionFormVM.NewITRequisitionStatusHistoryVM.Add(NewITRequisitionStatusHistoryVM);
                    //}


                    var NewITHistoryData = dbContext.tblNewITRequisitionStatusHistory.Where(x => x.NewITRequisitionID == paramNewITRequisitionID && x.IsDeleted == false).ToList();
                    //var USBRequestformHistoryData = dbContext.tblUSBRequestFormStatusHistory.Where(x => x.USBRequestFormId == paramUSBRequestFormId && x.IsDeleted == false).ToList();

                    foreach (var item in NewITHistoryData)
                    {
                        if (item.StatusName == "Initiator Submit")
                        {
                            objNewITRequisitionFormVM.NewITRequisitionProcessBarVM.Initiate = "Initiate By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "Department Head Approve")
                        {
                            objNewITRequisitionFormVM.NewITRequisitionProcessBarVM.DepartmentHeadApproval = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "IT Department Head Approve")
                        {
                            objNewITRequisitionFormVM.NewITRequisitionProcessBarVM.ITDepartmentHeadApproval = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }


                        else if (item.StatusName == "IT Helpdesk Admin Approve")
                        {
                            objNewITRequisitionFormVM.NewITRequisitionProcessBarVM.ITHelpdeskAdminApprove = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }


                        else if (item.StatusName == "IT Helpdesk Approval")
                        {
                            objNewITRequisitionFormVM.NewITRequisitionProcessBarVM.ITHelpdeskApproval = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }

                        else if (item.StatusName == "Compelte")
                        {
                            objNewITRequisitionFormVM.NewITRequisitionProcessBarVM.Compelte = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
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

                    objNewITRequisitionFormVM.requestFomsVM.RequestNo = lastRequestNumber;
                    objNewITRequisitionFormVM.requestFomsVM.RequestDate = DateTime.Now;
                    objNewITRequisitionFormVM.requestFomsVM.PsNo = paramPsNo;
                    objNewITRequisitionFormVM.Name = paramUserName;
                    //objNewITRequisitionFormVM.Department = "Test Department";
                    var vwEmpdata = dbContext.vwEmpInfo.Where(x => x.t_psno == paramPsNo).FirstOrDefault();
                    if (vwEmpdata != null)
                    {
                        objNewITRequisitionFormVM.Department = vwEmpdata.t_dsca;// vwEmpdata.t_depc;
                        if (!string.IsNullOrEmpty(vwEmpdata.t_dhps))
                        {
                            var headempdata = dbContext.vwEmpInfo.Where(x => x.t_psno == vwEmpdata.t_dhps).FirstOrDefault();
                            if (headempdata != null)
                            {
                                objNewITRequisitionFormVM.DepartmentHead = vwEmpdata.t_dhps + "-" + headempdata.t_name;
                            }

                        }
                    }
                    // objNewITRequisitionFormVM.DepartmentHead = "Test Department Head";
                    //objNewITRequisitionFormVM.Notes = "Note 1\nNote 2";

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
                objNewITRequisitionFormVM.requestFomsVM.RequestNo = lastRequestNumber;
                objNewITRequisitionFormVM.requestFomsVM.RequestDate = DateTime.Now;
                objNewITRequisitionFormVM.requestFomsVM.PsNo = paramPsNo;
                objNewITRequisitionFormVM.Name = paramUserName;
                //objNewITRequisitionFormVM.Department = "Test Department";
                var vwEmpdata = dbContext.vwEmpInfo.Where(x => x.t_psno == paramPsNo).FirstOrDefault();
                if (vwEmpdata != null)
                {
                    objNewITRequisitionFormVM.Department = vwEmpdata.t_dsca;// vwEmpdata.t_depc;
                    if (!string.IsNullOrEmpty(vwEmpdata.t_dhps))
                    {
                        var headempdata = dbContext.vwEmpInfo.Where(x => x.t_psno == vwEmpdata.t_dhps).FirstOrDefault();
                        if (headempdata != null)
                        {
                            objNewITRequisitionFormVM.DepartmentHead = vwEmpdata.t_dhps + "-" + headempdata.t_name;
                        }

                    }
                }
                //objNewITRequisitionFormVM.DepartmentHead = "Test Department Head";
                //objNewITRequisitionFormVM. = "Note 1\nNote 2";
            }


            if (IsPending == true)
            {

                objNewITRequisitionFormVM.requestFomsVM.IsPending = true;
            }
            else
            {
                objNewITRequisitionFormVM.requestFomsVM.IsPending = false;
            }

            return objNewITRequisitionFormVM;
        }

        public ResponseModel sumbitNewItRequisitionForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjNewITRequisitionFormVM.RequestFormId == 0)
                {
                    tblRequestForms tblRequestForms = new tblRequestForms();
                    tblRequestForms.PsNo = paramObjNewITRequisitionFormVM.requestFomsVM.PsNo;
                    tblRequestForms.RequestNo = paramObjNewITRequisitionFormVM.requestFomsVM.RequestNo;
                    tblRequestForms.RequestDate = paramObjNewITRequisitionFormVM.requestFomsVM.RequestDate;
                    tblRequestForms.RequestFor = "New It Requisition";
                    tblRequestForms.PendingWith = "Department Head";
                    tblRequestForms.Status = "Pending Department Head Approval";
                    if (!string.IsNullOrEmpty(paramObjNewITRequisitionFormVM.DepartmentHead))
                    {
                        var listdata = paramObjNewITRequisitionFormVM.DepartmentHead.Split('-');
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
                        tblNewITRequisition tblNewITRequisition = new tblNewITRequisition();
                        tblNewITRequisition.RequestFormId = tblRequestForms.RequestFormId;
                        tblNewITRequisition.RequestNo = paramObjNewITRequisitionFormVM.RequestNo;
                        tblNewITRequisition.PSNo = paramPsNo;
                        tblNewITRequisition.Department = paramObjNewITRequisitionFormVM.Department;
                        tblNewITRequisition.Name = paramObjNewITRequisitionFormVM.Name;
                        tblNewITRequisition.DepartmentHead = paramObjNewITRequisitionFormVM.DepartmentHead;
                        tblNewITRequisition.OnBehalfof = paramObjNewITRequisitionFormVM.OnBehalfof;
                        tblNewITRequisition.Device = paramObjNewITRequisitionFormVM.Device;
                        tblNewITRequisition.Location = paramObjNewITRequisitionFormVM.Location;
                        tblNewITRequisition.Reason = paramObjNewITRequisitionFormVM.Reason;
                        tblNewITRequisition.IsReject = false;
                        tblNewITRequisition.IsDeleted = false;
                        tblNewITRequisition.CreateBy = paramPsNo;
                        tblNewITRequisition.CreatedOn = DateTime.Now;
                        dbContext.tblNewITRequisition.Add(tblNewITRequisition);
                        dbContext.SaveChanges();
                        if (tblNewITRequisition.NewITRequisitionID > 0)
                        {
                            tblNewITRequisitionStatusHistory tblNewITRequisitionStatusHistory = new tblNewITRequisitionStatusHistory();
                            tblNewITRequisitionStatusHistory.NewITRequisitionID = tblNewITRequisition.NewITRequisitionID;
                            tblNewITRequisitionStatusHistory.StatusName = "Initiator Submit";
                            tblNewITRequisitionStatusHistory.IsDeleted = false;
                            tblNewITRequisitionStatusHistory.CreateBy = paramPsNo;
                            tblNewITRequisitionStatusHistory.CreatedOn = DateTime.Now;
                            tblNewITRequisitionStatusHistory.CreateByName = paramUserName;
                            dbContext.tblNewITRequisitionStatusHistory.Add(tblNewITRequisitionStatusHistory);
                            dbContext.SaveChanges();

                            _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully + "|" + "Request No : " + paramObjNewITRequisitionFormVM.requestFomsVM.RequestNo;
                            _objResponseModel.OtherParameter = paramObjNewITRequisitionFormVM.requestFomsVM.RequestNo;
                        }
                        _objResponseModel.PrimeryKeyId = paramObjNewITRequisitionFormVM.RequestFormId;


                    }
                }
                else
                {
                    tblRequestForms tblRequestFormsExistingdata = new tblRequestForms();
                    tblRequestFormsExistingdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsExistingdata != null)
                    {
                        tblRequestFormsExistingdata.PsNo = paramObjNewITRequisitionFormVM.requestFomsVM.PsNo;
                        tblRequestFormsExistingdata.RequestNo = paramObjNewITRequisitionFormVM.requestFomsVM.RequestNo;
                        tblRequestFormsExistingdata.RequestDate = paramObjNewITRequisitionFormVM.requestFomsVM.RequestDate;
                        tblRequestFormsExistingdata.RequestFor = "New It Requisition";
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
                            #region new Code
                            //tblUSBRequestForm tblUSBRequestFormExistingdata = new tblUSBRequestForm();
                            tblNewITRequisition tblNewITRequisitionExistingdata = new tblNewITRequisition();
                            tblNewITRequisitionExistingdata = dbContext.tblNewITRequisition.Where(x => x.NewITRequisitionID == paramObjNewITRequisitionFormVM.NewITRequisitionID && x.IsDeleted == false && x.IsReject == false).FirstOrDefault();
                            if (tblNewITRequisitionExistingdata != null)
                            {
                                tblNewITRequisitionExistingdata.RequestFormId = tblRequestFormsExistingdata.RequestFormId;
                                tblNewITRequisitionExistingdata.Name = paramObjNewITRequisitionFormVM.Name;
                                tblNewITRequisitionExistingdata.Department = paramObjNewITRequisitionFormVM.Department;
                                tblNewITRequisitionExistingdata.DepartmentHead = paramObjNewITRequisitionFormVM.DepartmentHead;
                                tblNewITRequisitionExistingdata.OnBehalfof = paramObjNewITRequisitionFormVM.OnBehalfof;
                                tblNewITRequisitionExistingdata.Reason = paramObjNewITRequisitionFormVM.Reason;
                                tblNewITRequisitionExistingdata.Location = paramObjNewITRequisitionFormVM.Location;
                                tblNewITRequisitionExistingdata.Device = paramObjNewITRequisitionFormVM.Device;


                                tblNewITRequisitionExistingdata.IsReject = false;
                                tblNewITRequisitionExistingdata.IsDeleted = false;
                                tblNewITRequisitionExistingdata.UpdateBy = paramPsNo;
                                tblNewITRequisitionExistingdata.UpdateOn = DateTime.Now;
                                dbContext.Entry(tblNewITRequisitionExistingdata).State = EntityState.Modified;
                                dbContext.SaveChanges();
                                if (tblNewITRequisitionExistingdata.NewITRequisitionID > 0)
                                {
                                    tblNewITRequisitionStatusHistory tblNewITRequisitionStatusHistory = new tblNewITRequisitionStatusHistory();
                                    tblNewITRequisitionStatusHistory.NewITRequisitionID = tblNewITRequisitionExistingdata.NewITRequisitionID;
                                    tblNewITRequisitionStatusHistory.StatusName = "Initiator Submit";
                                    tblNewITRequisitionStatusHistory.IsDeleted = false;
                                    tblNewITRequisitionStatusHistory.CreateBy = paramPsNo;
                                    tblNewITRequisitionStatusHistory.CreatedOn = DateTime.Now;
                                    tblNewITRequisitionStatusHistory.CreateByName = paramUserName;
                                    dbContext.tblNewITRequisitionStatusHistory.Add(tblNewITRequisitionStatusHistory);
                                    dbContext.SaveChanges();
                                    //tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                                    //tblUSBRequestFormStatusHistory.USBRequestFormId = tblNewITRequisitionExistingdata.USBRequestFormId;
                                    //tblUSBRequestFormStatusHistory.StatusName = "Initiator Submit";
                                    //tblUSBRequestFormStatusHistory.IsDeleted = false;
                                    //tblUSBRequestFormStatusHistory.CreateBy = paramPsNo;
                                    //tblUSBRequestFormStatusHistory.CreatedOn = DateTime.Now;
                                    //tblUSBRequestFormStatusHistory.CreateByName = paramUserName;
                                    //dbContext.tblUSBRequestFormStatusHistory.Add(tblUSBRequestFormStatusHistory);
                                    //dbContext.SaveChanges();

                                    _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                                    _objResponseModel.Message = HelperMessage.SaveSuccessfully + "|" + "Request No : " + paramObjNewITRequisitionFormVM.requestFomsVM.RequestNo;
                                    _objResponseModel.OtherParameter = paramObjNewITRequisitionFormVM.requestFomsVM.RequestNo;
                                }
                                _objResponseModel.PrimeryKeyId = paramObjNewITRequisitionFormVM.RequestFormId;
                            }
                            #endregion
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("NewItRequisitionController", "SubmitUSBRequestFromValue", "",  ex.InnerException.ToString());
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel ApproveNewItRequisitionForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVMFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjNewITRequisitionFormVMFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVMFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {

                        #region new Code
                        if (paramObjNewITRequisitionFormVMFormsVM.requestFomsVM.PendingWith == "IT Helpdesk")
                        {
                            tblRequestFormsdata.PendingWith = "IT Helpdesk Delivery";
                            tblRequestFormsdata.Status = "IT Helpdesk Delivery Pending";
                            tblRequestFormsdata.UpdateBy = paramPsNo;
                            tblRequestFormsdata.UpdateOn = DateTime.Now;
                            tblRequestFormsdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                            dbContext.SaveChanges();
                            //tblUSBRequestForm tblUSBRequestFormdata = new tblUSBRequestForm();
                            tblNewITRequisition tblNewITRequisition = new tblNewITRequisition();
                            //tblUSBRequestFormdata = dbContext.tblUSBRequestForm.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                            tblNewITRequisition = dbContext.tblNewITRequisition.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVMFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                            if (tblNewITRequisition != null)
                            {
                                tblNewITRequisition.AssentID = paramObjNewITRequisitionFormVMFormsVM.AssentID;
                                tblNewITRequisition.Mark = paramObjNewITRequisitionFormVMFormsVM.Mark;
                                tblNewITRequisition.Model = paramObjNewITRequisitionFormVMFormsVM.Model;
                                tblNewITRequisition.Family = paramObjNewITRequisitionFormVMFormsVM.Family;
                                tblNewITRequisition.OperationSystem = paramObjNewITRequisitionFormVMFormsVM.OperationSystem;
                                tblNewITRequisition.MonitorSize = paramObjNewITRequisitionFormVMFormsVM.MonitorSize;
                                tblNewITRequisition.SerialNO = paramObjNewITRequisitionFormVMFormsVM.SerialNO;
                                tblNewITRequisition.MACAddress = paramObjNewITRequisitionFormVMFormsVM.MACAddress;
                                tblNewITRequisition.InstallationRemark = paramObjNewITRequisitionFormVMFormsVM.InstallationRemark;
                                tblNewITRequisition.HDD = paramObjNewITRequisitionFormVMFormsVM.HDD;
                                tblNewITRequisition.RAM = paramObjNewITRequisitionFormVMFormsVM.RAM;
                                tblNewITRequisition.DeliveryBY = paramObjNewITRequisitionFormVMFormsVM.DeliveryBY;
                                tblNewITRequisition.AntivirusInstalled = paramObjNewITRequisitionFormVMFormsVM.AntivirusInstalled;
                                tblNewITRequisition.SoftwareInstalled = paramObjNewITRequisitionFormVMFormsVM.SoftwareInstalled;
                                tblNewITRequisition.DomainRegistion = paramObjNewITRequisitionFormVMFormsVM.DomainRegistion;
                                tblNewITRequisition.OneDriveConfigured = paramObjNewITRequisitionFormVMFormsVM.OneDriveConfigured;
                                tblNewITRequisition.BitlockerConfigured = paramObjNewITRequisitionFormVMFormsVM.BitlockerConfigured;





                                tblNewITRequisition.UpdateBy = paramPsNo;
                                tblNewITRequisition.UpdateOn = DateTime.Now;
                                tblNewITRequisition.UpdateBy = paramUserName;
                                dbContext.Entry(tblNewITRequisition).State = EntityState.Modified;
                                dbContext.SaveChanges();

                                //tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                                tblNewITRequisitionStatusHistory tblNewITRequisitionStatusHistory = new tblNewITRequisitionStatusHistory();
                                tblNewITRequisitionStatusHistory.NewITRequisitionID = tblNewITRequisition.NewITRequisitionID;
                                if (paramObjNewITRequisitionFormVMFormsVM.requestFomsVM.PendingWith == "IT Helpdesk")
                                {
                                    tblNewITRequisitionStatusHistory.StatusName = "IT Helpdesk Approval";



                                }

                                tblNewITRequisitionStatusHistory.IsDeleted = false;
                                tblNewITRequisitionStatusHistory.CreateBy = paramPsNo;
                                tblNewITRequisitionStatusHistory.CreatedOn = DateTime.Now;
                                tblNewITRequisitionStatusHistory.CreateByName = paramUserName;
                                dbContext.tblNewITRequisitionStatusHistory.Add(tblNewITRequisitionStatusHistory);
                                dbContext.SaveChanges();

                                // OTP Send Code
                                var tblotpoldData = dbContext.tblOTP.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVMFormsVM.RequestFormId && x.RequestFor == "New It Requisition" && x.IsDeleted == false).ToList();
                                foreach (var item in tblotpoldData)
                                {
                                    tblOTP tblOTPdelete = new tblOTP();
                                    tblOTPdelete = dbContext.tblOTP.Where(x => x.OTPId == item.OTPId).FirstOrDefault();
                                    if (tblOTPdelete != null)
                                    {
                                        tblOTPdelete.IsDeleted = true;
                                        dbContext.Entry(tblOTPdelete).State = EntityState.Modified;
                                        dbContext.SaveChanges();
                                    }
                                }

                                tblOTP tblOTP = new tblOTP();
                                Random r = new Random();
                                int randNum = r.Next(1000000);
                                string sixDigitNumber = randNum.ToString("D6");
                                tblOTP.RequestFormId = paramObjNewITRequisitionFormVMFormsVM.RequestFormId;
                                tblOTP.RequestFor = "New It Requisition";
                                tblOTP.OTP = sixDigitNumber;
                                tblOTP.EmailId = dbContext.vwEmpInfo.Where(x => x.t_psno == tblRequestFormsdata.PsNo).Select(x => x.t_mail).FirstOrDefault();
                                tblOTP.PsNo = tblRequestFormsdata.PsNo;
                                tblOTP.IsDeleted = false;
                                tblOTP.CreatedOn = DateTime.Now;
                                tblOTP.CreateBy = paramPsNo;
                                dbContext.tblOTP.Add(tblOTP);
                                dbContext.SaveChanges();

                                string Location = tblNewITRequisition.Location.Replace("L&T", "");
                                if (tblOTP.OTPId > 0)
                                {
                                    dbContext.Sp_Send_OTP_USB_Email(tblOTP.EmailId, sixDigitNumber, Location.Trim(), tblRequestFormsdata.RequestNo).FirstOrDefault();
                                    _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                                    _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                                }
                            }
                        }
                        else
                        {
                            if (paramObjNewITRequisitionFormVMFormsVM.requestFomsVM.PendingWith == "Department Head")
                            {
                                tblRequestFormsdata.PendingWith = "IT Department Head";
                                tblRequestFormsdata.Status = "Pending IT Department Head Approval";
                            }
                            if (paramObjNewITRequisitionFormVMFormsVM.requestFomsVM.PendingWith == "IT Department Head")
                            {
                                tblRequestFormsdata.PendingWith = "IT Helpdesk Admin";
                                tblRequestFormsdata.Status = "Pending IT Helpdesk Admin Approval";
                            }

                            if (paramObjNewITRequisitionFormVMFormsVM.requestFomsVM.PendingWith == "IT Helpdesk Admin")
                            {
                                tblRequestFormsdata.PendingWith = "IT Helpdesk";
                                tblRequestFormsdata.Status = "Pending IT Helpdesk Approval";
                            }
                            //if (paramObjNewITRequisitionFormVMFormsVM.requestFomsVM.PendingWith == "IT Helpdesk" && paramObjUSBRequestFormsVM.requestFomsVM.Status == "Pending IT Helpdesk Approval")
                            //{
                            //    tblRequestFormsdata.PendingWith = "IT Security";
                            //    tblRequestFormsdata.Status = "Pending IT Security";
                            //}



                            tblRequestFormsdata.UpdateBy = paramPsNo;
                            tblRequestFormsdata.UpdateOn = DateTime.Now;
                            tblRequestFormsdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            //tblUSBRequestForm tblUSBRequestFormdata = new tblUSBRequestForm();
                            tblNewITRequisition tblNewITRequisition = new tblNewITRequisition();
                            //tblUSBRequestFormdata = dbContext.tblUSBRequestForm.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                            tblNewITRequisition = dbContext.tblNewITRequisition.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVMFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                            if (tblNewITRequisition != null)
                            {
                                if (paramObjNewITRequisitionFormVMFormsVM.requestFomsVM.PendingWith == "Department Head")
                                {
                                    tblNewITRequisition.DHRemarks = paramObjNewITRequisitionFormVMFormsVM.DHRemarks;
                                }
                                if (paramObjNewITRequisitionFormVMFormsVM.requestFomsVM.PendingWith == "IT Department Head")
                                {
                                    tblNewITRequisition.ITDHRemark = paramObjNewITRequisitionFormVMFormsVM.ITDHRemark;
                                }
                                if (paramObjNewITRequisitionFormVMFormsVM.requestFomsVM.PendingWith == "IT Helpdesk Admin")
                                {
                                    tblNewITRequisition.ITHDAdminRemark = paramObjNewITRequisitionFormVMFormsVM.ITHDAdminRemark;
                                }


                                tblNewITRequisition.UpdateBy = paramPsNo;
                                tblNewITRequisition.UpdateOn = DateTime.Now;
                                tblNewITRequisition.UpdateBy = paramUserName;
                                dbContext.Entry(tblNewITRequisition).State = EntityState.Modified;
                                dbContext.SaveChanges();

                                //tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                                tblNewITRequisitionStatusHistory tblNewITRequisitionStatusHistory = new tblNewITRequisitionStatusHistory();
                                tblNewITRequisitionStatusHistory.NewITRequisitionID = tblNewITRequisition.NewITRequisitionID;
                                if (paramObjNewITRequisitionFormVMFormsVM.requestFomsVM.PendingWith == "Department Head")
                                {
                                    tblNewITRequisitionStatusHistory.StatusName = "Department Head Approve";
                                }
                                if (paramObjNewITRequisitionFormVMFormsVM.requestFomsVM.PendingWith == "IT Department Head")
                                {
                                    tblNewITRequisitionStatusHistory.StatusName = "IT Department Head Approve";
                                }
                                if (paramObjNewITRequisitionFormVMFormsVM.requestFomsVM.PendingWith == "IT Helpdesk Admin")
                                {
                                    tblNewITRequisitionStatusHistory.StatusName = "IT Helpdesk Admin Approve";
                                }


                                tblNewITRequisitionStatusHistory.IsDeleted = false;
                                tblNewITRequisitionStatusHistory.CreateBy = paramPsNo;
                                tblNewITRequisitionStatusHistory.CreatedOn = DateTime.Now;
                                tblNewITRequisitionStatusHistory.CreateByName = paramUserName;
                                dbContext.tblNewITRequisitionStatusHistory.Add(tblNewITRequisitionStatusHistory);
                                dbContext.SaveChanges();

                                _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                                _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                            }

                        }
                        _objResponseModel.PrimeryKeyId = paramObjNewITRequisitionFormVMFormsVM.RequestFormId;
                        #endregion
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("NewItRequisition", "ApproveNewItRequisitionForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel UnderProcessNewItRequisitionForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVMFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjNewITRequisitionFormVMFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVMFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {

                        #region new Code

                        tblRequestFormsdata.PendingWith = "IT Helpdesk";
                        tblRequestFormsdata.Status = "Under Processing";
                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        //tblUSBRequestForm tblUSBRequestFormdata = new tblUSBRequestForm();
                        tblNewITRequisition tblNewITRequisition = new tblNewITRequisition();
                        //tblUSBRequestFormdata = dbContext.tblUSBRequestForm.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        tblNewITRequisition = dbContext.tblNewITRequisition.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVMFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblNewITRequisition != null)
                        {

                            tblNewITRequisition.AssentID = paramObjNewITRequisitionFormVMFormsVM.AssentID;
                            tblNewITRequisition.Mark = paramObjNewITRequisitionFormVMFormsVM.Mark;
                            tblNewITRequisition.Model = paramObjNewITRequisitionFormVMFormsVM.Model;
                            tblNewITRequisition.Family = paramObjNewITRequisitionFormVMFormsVM.Family;
                            tblNewITRequisition.OperationSystem = paramObjNewITRequisitionFormVMFormsVM.OperationSystem;
                            tblNewITRequisition.MonitorSize = paramObjNewITRequisitionFormVMFormsVM.MonitorSize;
                            tblNewITRequisition.SerialNO = paramObjNewITRequisitionFormVMFormsVM.SerialNO;
                            tblNewITRequisition.MACAddress = paramObjNewITRequisitionFormVMFormsVM.MACAddress;
                            tblNewITRequisition.InstallationRemark = paramObjNewITRequisitionFormVMFormsVM.InstallationRemark;
                            tblNewITRequisition.HDD = paramObjNewITRequisitionFormVMFormsVM.HDD;
                            tblNewITRequisition.RAM = paramObjNewITRequisitionFormVMFormsVM.RAM;
                            tblNewITRequisition.DeliveryBY = paramObjNewITRequisitionFormVMFormsVM.DeliveryBY;
                            tblNewITRequisition.AntivirusInstalled = paramObjNewITRequisitionFormVMFormsVM.AntivirusInstalled;
                            tblNewITRequisition.SoftwareInstalled = paramObjNewITRequisitionFormVMFormsVM.SoftwareInstalled;
                            tblNewITRequisition.DomainRegistion = paramObjNewITRequisitionFormVMFormsVM.DomainRegistion;
                            tblNewITRequisition.OneDriveConfigured = paramObjNewITRequisitionFormVMFormsVM.OneDriveConfigured;
                            tblNewITRequisition.BitlockerConfigured = paramObjNewITRequisitionFormVMFormsVM.BitlockerConfigured;

                            tblNewITRequisition.UpdateBy = paramPsNo;
                            tblNewITRequisition.UpdateOn = DateTime.Now;
                            tblNewITRequisition.UpdateBy = paramUserName;
                            dbContext.Entry(tblNewITRequisition).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            //tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                            tblNewITRequisitionStatusHistory tblNewITRequisitionStatusHistory = new tblNewITRequisitionStatusHistory();
                            tblNewITRequisitionStatusHistory.NewITRequisitionID = tblNewITRequisition.NewITRequisitionID;


                            tblNewITRequisitionStatusHistory.StatusName = "Under Processing";
                            tblNewITRequisitionStatusHistory.IsDeleted = false;
                            tblNewITRequisitionStatusHistory.CreateBy = paramPsNo;
                            tblNewITRequisitionStatusHistory.CreatedOn = DateTime.Now;
                            tblNewITRequisitionStatusHistory.CreateByName = paramUserName;
                            dbContext.tblNewITRequisitionStatusHistory.Add(tblNewITRequisitionStatusHistory);
                            dbContext.SaveChanges();

                            _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                        }


                        _objResponseModel.PrimeryKeyId = paramObjNewITRequisitionFormVMFormsVM.RequestFormId;
                        #endregion
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("NewItRequisition", "UnderProcessNewItRequisitionForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel CompleteNewITForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjNewITRequisitionFormVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        if (paramObjNewITRequisitionFormVM.requestFomsVM.PendingWith == "IT Helpdesk Delivery")
                        {
                            var otpData = dbContext.tblOTP.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVM.RequestFormId && x.IsDeleted == false).OrderByDescending(x => x.OTPId).FirstOrDefault();
                            if (otpData != null)
                            {
                                if (otpData.OTP == paramObjNewITRequisitionFormVM.OTP)
                                {
                                    tblRequestFormsdata.PendingWith = "";
                                    tblRequestFormsdata.Status = "Complete";
                                    tblRequestFormsdata.UpdateBy = paramPsNo;
                                    tblRequestFormsdata.UpdateOn = DateTime.Now;
                                    tblRequestFormsdata.UpdateBy = paramUserName;
                                    dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                                    dbContext.SaveChanges();
                                    //tblUSBRequestForm tblUSBRequestFormdata = new tblUSBRequestForm();
                                    //tblUSBRequestFormdata = dbContext.tblUSBRequestForm.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                                    tblNewITRequisition tblNewITRequisition = new tblNewITRequisition();
                                    tblNewITRequisition = dbContext.tblNewITRequisition.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                                    if (tblNewITRequisition != null)
                                    {
                                        tblNewITRequisition.LocationByDelivery = paramObjNewITRequisitionFormVM.LocationByDelivery;
                                        tblNewITRequisition.Area = paramObjNewITRequisitionFormVM.Area;
                                        tblNewITRequisition.UpdateBy = paramPsNo;
                                        tblNewITRequisition.UpdateOn = DateTime.Now;
                                        tblNewITRequisition.UpdateBy = paramUserName;
                                        dbContext.Entry(tblNewITRequisition).State = EntityState.Modified;
                                        dbContext.SaveChanges();

                                        //tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                                        tblNewITRequisitionStatusHistory tblNewITRequisitionStatusHistory = new tblNewITRequisitionStatusHistory();
                                        tblNewITRequisitionStatusHistory.NewITRequisitionID = tblNewITRequisition.NewITRequisitionID;
                                        tblNewITRequisitionStatusHistory.StatusName = "Compelte";
                                        tblNewITRequisitionStatusHistory.IsDeleted = false;
                                        tblNewITRequisitionStatusHistory.CreateBy = paramPsNo;
                                        tblNewITRequisitionStatusHistory.CreatedOn = DateTime.Now;
                                        tblNewITRequisitionStatusHistory.CreateByName = paramUserName;
                                        dbContext.tblNewITRequisitionStatusHistory.Add(tblNewITRequisitionStatusHistory);
                                        dbContext.SaveChanges();

                                        _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                                        _objResponseModel.Message = HelperMessage.SaveSuccessfully;

                                    }
                                }
                                else
                                {
                                    _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                                    _objResponseModel.Message = HelperMessage.PasswordMatch;
                                }

                            }
                        }

                        _objResponseModel.PrimeryKeyId = paramObjNewITRequisitionFormVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("USBRequest", "CloseUSBRequestForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel ReturnNewITForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjNewITRequisitionFormVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        tblRequestFormsdata.PendingWith = "Initiator";
                        tblRequestFormsdata.Status = "Returned";

                        tblRequestFormsdata.ReturnRejectWith = paramObjNewITRequisitionFormVM.requestFomsVM.PendingWith;
                        tblRequestFormsdata.ReturnRejectBy = paramPsNo;
                        tblRequestFormsdata.ReturnRejectName = paramUserName;
                        tblRequestFormsdata.ReturnRejectDate = DateTime.Now;
                        tblRequestFormsdata.LastPendingWith = paramObjNewITRequisitionFormVM.requestFomsVM.Status;
                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        tblNewITRequisition tblNewITRequisition = new tblNewITRequisition();
                        tblNewITRequisition = dbContext.tblNewITRequisition.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblNewITRequisition != null)
                        {

                            tblNewITRequisition.DHRemarks = paramObjNewITRequisitionFormVM.DHRemarks;
                            tblNewITRequisition.ITDHRemark = paramObjNewITRequisitionFormVM.ITDHRemark;
                            tblNewITRequisition.ITHDAdminRemark = paramObjNewITRequisitionFormVM.ITHDAdminRemark;
                            tblNewITRequisition.AssentID = paramObjNewITRequisitionFormVM.AssentID;
                            tblNewITRequisition.Mark = paramObjNewITRequisitionFormVM.Mark;
                            tblNewITRequisition.Model = paramObjNewITRequisitionFormVM.Model;
                            tblNewITRequisition.Family = paramObjNewITRequisitionFormVM.Family;
                            tblNewITRequisition.OperationSystem = paramObjNewITRequisitionFormVM.OperationSystem;
                            tblNewITRequisition.MonitorSize = paramObjNewITRequisitionFormVM.MonitorSize;
                            tblNewITRequisition.SerialNO = paramObjNewITRequisitionFormVM.SerialNO;
                            tblNewITRequisition.MACAddress = paramObjNewITRequisitionFormVM.MACAddress;
                            tblNewITRequisition.InstallationRemark = paramObjNewITRequisitionFormVM.InstallationRemark;
                            tblNewITRequisition.HDD = paramObjNewITRequisitionFormVM.HDD;
                            tblNewITRequisition.RAM = paramObjNewITRequisitionFormVM.RAM;
                            tblNewITRequisition.DeliveryBY = paramObjNewITRequisitionFormVM.DeliveryBY;
                            tblNewITRequisition.AntivirusInstalled = paramObjNewITRequisitionFormVM.AntivirusInstalled;
                            tblNewITRequisition.SoftwareInstalled = paramObjNewITRequisitionFormVM.SoftwareInstalled;
                            tblNewITRequisition.DomainRegistion = paramObjNewITRequisitionFormVM.DomainRegistion;
                            tblNewITRequisition.OneDriveConfigured = paramObjNewITRequisitionFormVM.OneDriveConfigured;
                            tblNewITRequisition.BitlockerConfigured = paramObjNewITRequisitionFormVM.BitlockerConfigured;

                            tblNewITRequisition.UpdateBy = paramPsNo;
                            tblNewITRequisition.UpdateOn = DateTime.Now;
                            tblNewITRequisition.UpdateBy = paramUserName;
                            dbContext.Entry(tblNewITRequisition).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblNewITRequisitionStatusHistory tblNewITRequisitionStatusHistory = new tblNewITRequisitionStatusHistory();
                            tblNewITRequisitionStatusHistory.NewITRequisitionID = tblNewITRequisition.NewITRequisitionID;
                            tblNewITRequisitionStatusHistory.StatusName = "Returned";
                            tblNewITRequisitionStatusHistory.IsDeleted = false;
                            tblNewITRequisitionStatusHistory.CreateBy = paramPsNo;
                            tblNewITRequisitionStatusHistory.CreatedOn = DateTime.Now;
                            tblNewITRequisitionStatusHistory.CreateByName = paramUserName;
                            dbContext.tblNewITRequisitionStatusHistory.Add(tblNewITRequisitionStatusHistory);
                            dbContext.SaveChanges();


                            var tblNewITRequisitionStatusHistoryList = dbContext.tblNewITRequisitionStatusHistory.Where(x => x.NewITRequisitionID == paramObjNewITRequisitionFormVM.NewITRequisitionID && x.IsDeleted == false).ToList();

                            foreach (var item in tblNewITRequisitionStatusHistoryList)
                            {
                                var HistoryData = dbContext.tblNewITRequisitionStatusHistory.Where(x => x.NewITRequisitionStatusHistoryId == item.NewITRequisitionStatusHistoryId).FirstOrDefault();
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
                        _objResponseModel.PrimeryKeyId = paramObjNewITRequisitionFormVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("newit", "Returnnewit", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel RejectNewITForm(NewITRequisitionFormVM paramObjNewITRequisitionFormVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjNewITRequisitionFormVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        tblRequestFormsdata.PendingWith = "";
                        tblRequestFormsdata.Status = "Reject";
                        tblRequestFormsdata.ReturnRejectWith = paramObjNewITRequisitionFormVM.requestFomsVM.PendingWith;
                        tblRequestFormsdata.ReturnRejectBy = paramPsNo;
                        tblRequestFormsdata.ReturnRejectName = paramUserName;
                        tblRequestFormsdata.ReturnRejectDate = DateTime.Now;
                        tblRequestFormsdata.LastPendingWith = paramObjNewITRequisitionFormVM.requestFomsVM.Status;
                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();


                        tblNewITRequisition tblNewITRequisition = new tblNewITRequisition();
                        tblNewITRequisition = dbContext.tblNewITRequisition.Where(x => x.RequestFormId == paramObjNewITRequisitionFormVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblNewITRequisition != null)
                        {
                            tblNewITRequisition.DHRemarks = paramObjNewITRequisitionFormVM.DHRemarks;
                            tblNewITRequisition.ITDHRemark = paramObjNewITRequisitionFormVM.ITDHRemark;
                            tblNewITRequisition.ITHDAdminRemark = paramObjNewITRequisitionFormVM.ITHDAdminRemark;
                            tblNewITRequisition.AssentID = paramObjNewITRequisitionFormVM.AssentID;
                            tblNewITRequisition.Mark = paramObjNewITRequisitionFormVM.Mark;
                            tblNewITRequisition.Model = paramObjNewITRequisitionFormVM.Model;
                            tblNewITRequisition.Family = paramObjNewITRequisitionFormVM.Family;
                            tblNewITRequisition.OperationSystem = paramObjNewITRequisitionFormVM.OperationSystem;
                            tblNewITRequisition.MonitorSize = paramObjNewITRequisitionFormVM.MonitorSize;
                            tblNewITRequisition.SerialNO = paramObjNewITRequisitionFormVM.SerialNO;
                            tblNewITRequisition.MACAddress = paramObjNewITRequisitionFormVM.MACAddress;
                            tblNewITRequisition.InstallationRemark = paramObjNewITRequisitionFormVM.InstallationRemark;
                            tblNewITRequisition.HDD = paramObjNewITRequisitionFormVM.HDD;
                            tblNewITRequisition.RAM = paramObjNewITRequisitionFormVM.RAM;
                            tblNewITRequisition.DeliveryBY = paramObjNewITRequisitionFormVM.DeliveryBY;
                            tblNewITRequisition.AntivirusInstalled = paramObjNewITRequisitionFormVM.AntivirusInstalled;
                            tblNewITRequisition.SoftwareInstalled = paramObjNewITRequisitionFormVM.SoftwareInstalled;
                            tblNewITRequisition.DomainRegistion = paramObjNewITRequisitionFormVM.DomainRegistion;
                            tblNewITRequisition.OneDriveConfigured = paramObjNewITRequisitionFormVM.OneDriveConfigured;
                            tblNewITRequisition.BitlockerConfigured = paramObjNewITRequisitionFormVM.BitlockerConfigured;

                            tblNewITRequisition.IsReject = true;
                            tblNewITRequisition.UpdateBy = paramPsNo;
                            tblNewITRequisition.UpdateOn = DateTime.Now;
                            tblNewITRequisition.UpdateBy = paramUserName;
                            dbContext.Entry(tblNewITRequisition).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            //tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                            //tblUSBRequestFormStatusHistory.USBRequestFormId = tblUSBRequestFormdata.USBRequestFormId;
                            //tblUSBRequestFormStatusHistory.StatusName = "Reject";
                            //tblUSBRequestFormStatusHistory.IsDeleted = false;
                            //tblUSBRequestFormStatusHistory.CreateBy = paramPsNo;
                            //tblUSBRequestFormStatusHistory.CreatedOn = DateTime.Now;
                            //tblUSBRequestFormStatusHistory.CreateByName = paramUserName;
                            //dbContext.tblUSBRequestFormStatusHistory.Add(tblUSBRequestFormStatusHistory);
                            //dbContext.SaveChanges();
                            tblNewITRequisitionStatusHistory tblNewITRequisitionStatusHistory = new tblNewITRequisitionStatusHistory();
                            tblNewITRequisitionStatusHistory.NewITRequisitionID = tblNewITRequisition.NewITRequisitionID;
                            tblNewITRequisitionStatusHistory.StatusName = "Reject";
                            tblNewITRequisitionStatusHistory.IsDeleted = false;
                            tblNewITRequisitionStatusHistory.CreateBy = paramPsNo;
                            tblNewITRequisitionStatusHistory.CreatedOn = DateTime.Now;
                            tblNewITRequisitionStatusHistory.CreateByName = paramUserName;
                            dbContext.tblNewITRequisitionStatusHistory.Add(tblNewITRequisitionStatusHistory);
                            dbContext.SaveChanges();


                            var tblNewITRequisitionStatusHistoryList = dbContext.tblNewITRequisitionStatusHistory.Where(x => x.NewITRequisitionID == paramObjNewITRequisitionFormVM.NewITRequisitionID && x.IsDeleted == false).ToList();

                            foreach (var item in tblNewITRequisitionStatusHistoryList)
                            {
                                var HistoryData = dbContext.tblNewITRequisitionStatusHistory.Where(x => x.NewITRequisitionStatusHistoryId == item.NewITRequisitionStatusHistoryId).FirstOrDefault();
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
                        _objResponseModel.PrimeryKeyId = paramObjNewITRequisitionFormVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("USBRequest", "RejectUSBRequestForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel GetDeliveryByData(string paramPsNo)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                var datas = dbContext.SP_getDeliveryByData(paramPsNo);
                if (datas != null)
                {
                    _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                    _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                    _objResponseModel.OtherParameter = datas;
                }
            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("NewItRequisitionRepository", "GetDeliveryByData", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }
    }
    #endregion
}
