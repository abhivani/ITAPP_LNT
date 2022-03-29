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
    public interface IUSBRequestRepository
    {
        USBRequestFormsVM getUSBRequestFormId(long paramUSBRequestFormId, string paramPsNo, string paramUserName, bool IsPending);
        ResponseModel sumbitUSBRequestForm(USBRequestFormsVM paramObjUSBRequestFormsVM, string paramPsNo, string paramUserName);
        ResponseModel ApproveUSBRequestForm(USBRequestFormsVM paramObjUSBRequestFormsVM, string paramPsNo, string paramUserName);
        ResponseModel CloseUSBRequestForm(USBRequestFormsVM paramObjUSBRequestFormsVM, string paramPsNo, string paramUserName);
        ResponseModel ReturnUSBRequestForm(USBRequestFormsVM paramObjUSBRequestFormsVM, string paramPsNo, string paramUserName);
        ResponseModel RejectUSBRequestForm(USBRequestFormsVM paramObjUSBRequestFormsVM, string paramPsNo, string paramUserName);
        List<tblUSBRequestFormStatusHistory> getStatuOfRequest(long paramRequestFormId);
    }
    #endregion

    public class USBRequestRepository : IUSBRequestRepository
    {


        private readonly ITAPPFORMSDbContext dbContext;
        public USBRequestRepository(ITAPPFORMSDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        #region USB Request Form        
        public USBRequestFormsVM getUSBRequestFormId(long paramUSBRequestFormId, string paramPsNo, string paramUserName, bool IsPending)
        {
            USBRequestFormsVM objUSBRequestFormsVMData = new USBRequestFormsVM();
            if (paramUSBRequestFormId > 0)
            {
                var tblUSBRequestFormtblData = dbContext.tblUSBRequestForm.Where(x => x.USBRequestFormId == paramUSBRequestFormId && x.IsDeleted == false).FirstOrDefault();
                if (tblUSBRequestFormtblData != null)
                {
                    var RequestFormData = dbContext.tblRequestForms.Where(x => x.RequestFormId == tblUSBRequestFormtblData.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (RequestFormData != null)
                    {
                        objUSBRequestFormsVMData.requestFomsVM.RequestFormId = RequestFormData.RequestFormId;
                        objUSBRequestFormsVMData.requestFomsVM.RequestNo = RequestFormData.RequestNo;
                        objUSBRequestFormsVMData.requestFomsVM.RequestDate = RequestFormData.RequestDate;
                        objUSBRequestFormsVMData.requestFomsVM.PsNo = RequestFormData.PsNo;
                        objUSBRequestFormsVMData.requestFomsVM.RequestFor = RequestFormData.RequestFor;
                        objUSBRequestFormsVMData.requestFomsVM.PendingWith = RequestFormData.PendingWith;
                        objUSBRequestFormsVMData.requestFomsVM.Status = RequestFormData.Status;
                        objUSBRequestFormsVMData.requestFomsVM.ReturnRejectWith = RequestFormData.ReturnRejectWith;
                        objUSBRequestFormsVMData.requestFomsVM.LastPendingWith = RequestFormData.LastPendingWith;
                    }
                    objUSBRequestFormsVMData.USBRequestFormId = tblUSBRequestFormtblData.USBRequestFormId;
                    objUSBRequestFormsVMData.RequestFormId = tblUSBRequestFormtblData.RequestFormId;
                    objUSBRequestFormsVMData.Name = tblUSBRequestFormtblData.Name;
                    objUSBRequestFormsVMData.DepartmentName = tblUSBRequestFormtblData.DepartmentName;
                    objUSBRequestFormsVMData.DepartmentHead = tblUSBRequestFormtblData.DepartmentHead;
                    objUSBRequestFormsVMData.Reason = tblUSBRequestFormtblData.Reason;
                    objUSBRequestFormsVMData.Location = tblUSBRequestFormtblData.Location;
                    objUSBRequestFormsVMData.Notes = tblUSBRequestFormtblData.Notes;
                    objUSBRequestFormsVMData.IsAccept = tblUSBRequestFormtblData.IsAccept;
                    objUSBRequestFormsVMData.DHRemarks = tblUSBRequestFormtblData.DHRemarks;
                    objUSBRequestFormsVMData.ITDHRemarks = tblUSBRequestFormtblData.ITDHRemarks;
                    objUSBRequestFormsVMData.SerialNo = tblUSBRequestFormtblData.SerialNo;
                    objUSBRequestFormsVMData.Model = tblUSBRequestFormtblData.Model;
                    objUSBRequestFormsVMData.Vendor = tblUSBRequestFormtblData.Vendor;
                    objUSBRequestFormsVMData.Description = tblUSBRequestFormtblData.Description;
                    objUSBRequestFormsVMData.OTP = "";
                    objUSBRequestFormsVMData.Remarks = tblUSBRequestFormtblData.Remarks;

                    var USBRequestformHistoryData = dbContext.tblUSBRequestFormStatusHistory.Where(x => x.USBRequestFormId == paramUSBRequestFormId && x.IsDeleted == false).ToList();

                    foreach (var item in USBRequestformHistoryData)
                    {
                        if(item.StatusName == "Initiator Submit")
                        {
                            objUSBRequestFormsVMData.uSBRequestFormsProcessBarVM.Initiate = "Initiate By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if(item.StatusName== "Department Head Approve")
                        {
                            objUSBRequestFormsVMData.uSBRequestFormsProcessBarVM.DepartmentHeadApproval = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if(item.StatusName== "IT Department Head Approve")
                        {
                            objUSBRequestFormsVMData.uSBRequestFormsProcessBarVM.ITDepartmentHeadApproval = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if(item.StatusName== "IT Helpdesk Approve")
                        {
                            objUSBRequestFormsVMData.uSBRequestFormsProcessBarVM.ITHelpdeskApproval = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "IT Security Approve")
                        {
                            objUSBRequestFormsVMData.uSBRequestFormsProcessBarVM.ITSecurity = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
                        }
                        else if (item.StatusName == "Close")
                        {
                            objUSBRequestFormsVMData.uSBRequestFormsProcessBarVM.ITHelpdeskApprovalLast = "Approved By " + item.CreateBy + " on " + item.CreatedOn.ToShortDateString();
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
                        lastRequestNumber = Month + "/" +Year + "-" + appliNo;
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
                    objUSBRequestFormsVMData.requestFomsVM.RequestNo = lastRequestNumber;
                    objUSBRequestFormsVMData.requestFomsVM.RequestDate = DateTime.Now;
                    objUSBRequestFormsVMData.requestFomsVM.PsNo = paramPsNo;
                    objUSBRequestFormsVMData.Name = paramUserName;
                    //objUSBRequestFormsVMData.DepartmentName = "Test Department";
                    var vwEmpdata = dbContext.vwEmpInfo.Where(x => x.t_psno == paramPsNo).FirstOrDefault();
                    if(vwEmpdata !=null)
                    {
                        objUSBRequestFormsVMData.DepartmentName = vwEmpdata.t_dsca;//vwEmpdata.t_depc;
                        if (!string.IsNullOrEmpty(vwEmpdata.t_dhps))
                        {
                            var headempdata = dbContext.vwEmpInfo.Where(x => x.t_psno == vwEmpdata.t_dhps).FirstOrDefault();
                            if(headempdata !=null)
                            {
                                objUSBRequestFormsVMData.DepartmentHead = vwEmpdata.t_dhps + "-" + headempdata.t_name;
                            }

                        }
                    }
                    //objUSBRequestFormsVMData.DepartmentHead = "Test Department Head";
                    objUSBRequestFormsVMData.Notes = "Note 1\nNote 2";
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
                    if(alldata[0].Length !=4)
                    {                        
                        var splitYear= alldata[0].Split('/');

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
                objUSBRequestFormsVMData.requestFomsVM.RequestNo = lastRequestNumber;
                objUSBRequestFormsVMData.requestFomsVM.RequestDate = DateTime.Now;
                objUSBRequestFormsVMData.requestFomsVM.PsNo = paramPsNo;
                objUSBRequestFormsVMData.Name = paramUserName;
               // objUSBRequestFormsVMData.DepartmentName = "Test Department";
                var vwEmpdata = dbContext.vwEmpInfo.Where(x => x.t_psno == paramPsNo).FirstOrDefault();
                if (vwEmpdata != null)
                {
                    objUSBRequestFormsVMData.DepartmentName = vwEmpdata.t_dsca; //vwEmpdata.t_depc;
                    if (!string.IsNullOrEmpty(vwEmpdata.t_dhps))
                    {
                        var headempdata = dbContext.vwEmpInfo.Where(x => x.t_psno == vwEmpdata.t_dhps).FirstOrDefault();
                        if (headempdata != null)
                        {
                            objUSBRequestFormsVMData.DepartmentHead = vwEmpdata.t_dhps + "-" + headempdata.t_name;
                        }
                    }
                }
                //objUSBRequestFormsVMData.DepartmentHead = "Test Department Head";
                objUSBRequestFormsVMData.Notes = "Note 1\nNote 2";
            }
            if (IsPending == true)
            {

                objUSBRequestFormsVMData.requestFomsVM.IsPending = true;
            }
            else
            {
                objUSBRequestFormsVMData.requestFomsVM.IsPending = false;
            }
            return objUSBRequestFormsVMData;
        }

        public ResponseModel sumbitUSBRequestForm(USBRequestFormsVM paramObjUSBRequestFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjUSBRequestFormsVM.RequestFormId == 0)
                {
                    tblRequestForms tblRequestForms = new tblRequestForms();
                    tblRequestForms.PsNo = paramObjUSBRequestFormsVM.requestFomsVM.PsNo;
                    tblRequestForms.RequestNo = paramObjUSBRequestFormsVM.requestFomsVM.RequestNo;
                    tblRequestForms.RequestDate = paramObjUSBRequestFormsVM.requestFomsVM.RequestDate;
                    tblRequestForms.RequestFor = "USB";
                    tblRequestForms.PendingWith = "Department Head";
                    tblRequestForms.Status = "Pending Department Head Approval";
                    if(!string.IsNullOrEmpty(paramObjUSBRequestFormsVM.DepartmentHead))
                    {
                        var listdata = paramObjUSBRequestFormsVM.DepartmentHead.Split('-');
                        if(!string.IsNullOrEmpty(listdata[0]))
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
                        tblUSBRequestForm tblUSBRequestForm = new tblUSBRequestForm();
                        tblUSBRequestForm.RequestFormId = tblRequestForms.RequestFormId;
                        tblUSBRequestForm.Name = paramObjUSBRequestFormsVM.Name;
                        tblUSBRequestForm.DepartmentName = paramObjUSBRequestFormsVM.DepartmentName;
                        tblUSBRequestForm.DepartmentHead = paramObjUSBRequestFormsVM.DepartmentHead;
                        tblUSBRequestForm.Reason = paramObjUSBRequestFormsVM.Reason;
                        tblUSBRequestForm.Location = paramObjUSBRequestFormsVM.Location;
                        tblUSBRequestForm.Notes = paramObjUSBRequestFormsVM.Notes;
                        tblUSBRequestForm.IsAccept = paramObjUSBRequestFormsVM.IsAccept;
                        tblUSBRequestForm.IsReject = false;
                        tblUSBRequestForm.IsDeleted = false;
                        tblUSBRequestForm.CreateBy = paramPsNo;
                        tblUSBRequestForm.CreatedOn = DateTime.Now;
                        dbContext.tblUSBRequestForm.Add(tblUSBRequestForm);
                        dbContext.SaveChanges();
                        if (tblUSBRequestForm.USBRequestFormId > 0)
                        {
                            tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                            tblUSBRequestFormStatusHistory.USBRequestFormId = tblUSBRequestForm.USBRequestFormId;
                            tblUSBRequestFormStatusHistory.StatusName = "Initiator Submit";
                            tblUSBRequestFormStatusHistory.IsDeleted = false;
                            tblUSBRequestFormStatusHistory.CreateBy = paramPsNo;
                            tblUSBRequestFormStatusHistory.CreatedOn = DateTime.Now;
                            tblUSBRequestFormStatusHistory.CreateByName = paramUserName;
                            dbContext.tblUSBRequestFormStatusHistory.Add(tblUSBRequestFormStatusHistory);
                            dbContext.SaveChanges();

                            _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully + "|" + "Request No : " +  paramObjUSBRequestFormsVM.requestFomsVM.RequestNo;
                            _objResponseModel.OtherParameter = paramObjUSBRequestFormsVM.requestFomsVM.RequestNo;
                        }
                        _objResponseModel.PrimeryKeyId = paramObjUSBRequestFormsVM.RequestFormId;
                    }
                }
                else
                {
                    tblRequestForms tblRequestFormsExistingdata = new tblRequestForms();
                    tblRequestFormsExistingdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsExistingdata != null)
                    {
                        tblRequestFormsExistingdata.PsNo = paramObjUSBRequestFormsVM.requestFomsVM.PsNo;
                        tblRequestFormsExistingdata.RequestNo = paramObjUSBRequestFormsVM.requestFomsVM.RequestNo;
                        tblRequestFormsExistingdata.RequestDate = paramObjUSBRequestFormsVM.requestFomsVM.RequestDate;
                        tblRequestFormsExistingdata.RequestFor = "USB";
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
                            tblUSBRequestForm tblUSBRequestFormExistingdata = new tblUSBRequestForm();
                            tblUSBRequestFormExistingdata = dbContext.tblUSBRequestForm.Where(x => x.USBRequestFormId == paramObjUSBRequestFormsVM.USBRequestFormId && x.IsDeleted == false && x.IsReject == false).FirstOrDefault();
                            if (tblUSBRequestFormExistingdata != null)
                            {
                                tblUSBRequestFormExistingdata.RequestFormId = tblRequestFormsExistingdata.RequestFormId;
                                tblUSBRequestFormExistingdata.Name = paramObjUSBRequestFormsVM.Name;
                                tblUSBRequestFormExistingdata.DepartmentName = paramObjUSBRequestFormsVM.DepartmentName;
                                tblUSBRequestFormExistingdata.DepartmentHead = paramObjUSBRequestFormsVM.DepartmentHead;
                                tblUSBRequestFormExistingdata.Reason = paramObjUSBRequestFormsVM.Reason;
                                tblUSBRequestFormExistingdata.Location = paramObjUSBRequestFormsVM.Location;
                                tblUSBRequestFormExistingdata.Notes = paramObjUSBRequestFormsVM.Notes;
                              //  tblUSBRequestFormExistingdata.IsAccept = paramObjUSBRequestFormsVM.IsAccept;
                                tblUSBRequestFormExistingdata.IsReject = false;
                                tblUSBRequestFormExistingdata.IsDeleted = false;
                                tblUSBRequestFormExistingdata.UpdateBy = paramPsNo;
                                tblUSBRequestFormExistingdata.UpdateOn = DateTime.Now;
                                dbContext.Entry(tblUSBRequestFormExistingdata).State = EntityState.Modified;
                                dbContext.SaveChanges();
                                if (tblUSBRequestFormExistingdata.USBRequestFormId > 0)
                                {
                                    tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                                    tblUSBRequestFormStatusHistory.USBRequestFormId = tblUSBRequestFormExistingdata.USBRequestFormId;
                                    tblUSBRequestFormStatusHistory.StatusName = "Initiator Submit";
                                    tblUSBRequestFormStatusHistory.IsDeleted = false;
                                    tblUSBRequestFormStatusHistory.CreateBy = paramPsNo;
                                    tblUSBRequestFormStatusHistory.CreatedOn = DateTime.Now;
                                    tblUSBRequestFormStatusHistory.CreateByName = paramUserName;
                                    dbContext.tblUSBRequestFormStatusHistory.Add(tblUSBRequestFormStatusHistory);
                                    dbContext.SaveChanges();

                                    _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                                    _objResponseModel.Message = HelperMessage.SaveSuccessfully + "|" + "Request No : " + paramObjUSBRequestFormsVM.requestFomsVM.RequestNo;
                                    _objResponseModel.OtherParameter = paramObjUSBRequestFormsVM.requestFomsVM.RequestNo;
                                }
                                _objResponseModel.PrimeryKeyId = paramObjUSBRequestFormsVM.RequestFormId;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("USBRequest", "sumbitUSBRequestForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel ApproveUSBRequestForm(USBRequestFormsVM paramObjUSBRequestFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjUSBRequestFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        if (paramObjUSBRequestFormsVM.requestFomsVM.PendingWith == "IT Security")
                        {
                            tblRequestFormsdata.PendingWith = "IT Helpdesk";
                            tblRequestFormsdata.Status = "Collection Pending";
                            tblRequestFormsdata.UpdateBy = paramPsNo;
                            tblRequestFormsdata.UpdateOn = DateTime.Now;
                            tblRequestFormsdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                            dbContext.SaveChanges();
                            tblUSBRequestForm tblUSBRequestFormdata = new tblUSBRequestForm();
                            tblUSBRequestFormdata = dbContext.tblUSBRequestForm.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                            if (tblUSBRequestFormdata != null)
                            {
                                tblUSBRequestFormdata.UpdateBy = paramPsNo;
                                tblUSBRequestFormdata.UpdateOn = DateTime.Now;
                                tblUSBRequestFormdata.UpdateBy = paramUserName;
                                dbContext.Entry(tblUSBRequestFormdata).State = EntityState.Modified;
                                dbContext.SaveChanges();

                                tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                                tblUSBRequestFormStatusHistory.USBRequestFormId = tblUSBRequestFormdata.USBRequestFormId;
                                if (paramObjUSBRequestFormsVM.requestFomsVM.PendingWith == "IT Security")
                                {
                                    tblUSBRequestFormStatusHistory.StatusName = "IT Security Approve";
                                }

                                tblUSBRequestFormStatusHistory.IsDeleted = false;
                                tblUSBRequestFormStatusHistory.CreateBy = paramPsNo;
                                tblUSBRequestFormStatusHistory.CreatedOn = DateTime.Now;
                                tblUSBRequestFormStatusHistory.CreateByName = paramUserName;
                                dbContext.tblUSBRequestFormStatusHistory.Add(tblUSBRequestFormStatusHistory);
                                dbContext.SaveChanges();

                                // OTP Send Code
                                var tblotpoldData = dbContext.tblOTP.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.RequestFor == "USB" && x.IsDeleted == false).ToList();
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
                                tblOTP.RequestFormId = paramObjUSBRequestFormsVM.RequestFormId;
                                tblOTP.RequestFor = "USB";
                                tblOTP.OTP = sixDigitNumber;
                                tblOTP.EmailId = dbContext.vwEmpInfo.Where(x => x.t_psno == tblRequestFormsdata.PsNo).Select(x => x.t_mail).FirstOrDefault();
                                tblOTP.PsNo = tblRequestFormsdata.PsNo;
                                tblOTP.IsDeleted = false;
                                tblOTP.CreatedOn = DateTime.Now;
                                tblOTP.CreateBy = paramPsNo;
                                dbContext.tblOTP.Add(tblOTP);
                                dbContext.SaveChanges();

                                string Location = tblUSBRequestFormdata.Location.Replace("L&T", "");
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
                            if (paramObjUSBRequestFormsVM.requestFomsVM.PendingWith == "Department Head")
                            {
                                tblRequestFormsdata.PendingWith = "IT Department Head";
                                tblRequestFormsdata.Status = "Pending IT Department Head Approval";
                            }
                            if (paramObjUSBRequestFormsVM.requestFomsVM.PendingWith == "IT Department Head")
                            {
                                tblRequestFormsdata.PendingWith = "IT Helpdesk";
                                tblRequestFormsdata.Status = "Pending IT Helpdesk Approval";
                            }
                            if (paramObjUSBRequestFormsVM.requestFomsVM.PendingWith == "IT Helpdesk" && paramObjUSBRequestFormsVM.requestFomsVM.Status == "Pending IT Helpdesk Approval")
                            {
                                tblRequestFormsdata.PendingWith = "IT Security";
                                tblRequestFormsdata.Status = "Pending IT Security Approval";
                            }
                            
                            tblRequestFormsdata.UpdateBy = paramPsNo;
                            tblRequestFormsdata.UpdateOn = DateTime.Now;
                            tblRequestFormsdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblUSBRequestForm tblUSBRequestFormdata = new tblUSBRequestForm();
                            tblUSBRequestFormdata = dbContext.tblUSBRequestForm.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                            if (tblUSBRequestFormdata != null)
                            {
                                if (paramObjUSBRequestFormsVM.requestFomsVM.PendingWith == "Department Head")
                                {
                                    tblUSBRequestFormdata.DHRemarks = paramObjUSBRequestFormsVM.DHRemarks;
                                }
                                if (paramObjUSBRequestFormsVM.requestFomsVM.PendingWith == "IT Department Head")
                                {
                                    tblUSBRequestFormdata.ITDHRemarks = paramObjUSBRequestFormsVM.ITDHRemarks;
                                }
                                if (paramObjUSBRequestFormsVM.requestFomsVM.PendingWith == "IT Helpdesk" && paramObjUSBRequestFormsVM.requestFomsVM.Status == "Pending IT Helpdesk Approval")
                                {
                                    tblUSBRequestFormdata.SerialNo = paramObjUSBRequestFormsVM.SerialNo;
                                    tblUSBRequestFormdata.Model = paramObjUSBRequestFormsVM.Model;
                                    tblUSBRequestFormdata.Vendor = paramObjUSBRequestFormsVM.Vendor;
                                    tblUSBRequestFormdata.Description = paramObjUSBRequestFormsVM.Description;
                                }
                               
                                tblUSBRequestFormdata.UpdateBy = paramPsNo;
                                tblUSBRequestFormdata.UpdateOn = DateTime.Now;
                                tblUSBRequestFormdata.UpdateBy = paramUserName;
                                dbContext.Entry(tblUSBRequestFormdata).State = EntityState.Modified;
                                dbContext.SaveChanges();

                                tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                                tblUSBRequestFormStatusHistory.USBRequestFormId = tblUSBRequestFormdata.USBRequestFormId;
                                if (paramObjUSBRequestFormsVM.requestFomsVM.PendingWith == "Department Head")
                                {
                                    tblUSBRequestFormStatusHistory.StatusName = "Department Head Approve";
                                }
                                if (paramObjUSBRequestFormsVM.requestFomsVM.PendingWith == "IT Department Head")
                                {
                                    tblUSBRequestFormStatusHistory.StatusName = "IT Department Head Approve";
                                }
                                if (paramObjUSBRequestFormsVM.requestFomsVM.PendingWith == "IT Helpdesk" && paramObjUSBRequestFormsVM.requestFomsVM.Status == "Pending IT Helpdesk Approval")
                                {
                                    tblUSBRequestFormStatusHistory.StatusName = "IT Helpdesk Approve";
                                }
                                
                                tblUSBRequestFormStatusHistory.IsDeleted = false;
                                tblUSBRequestFormStatusHistory.CreateBy = paramPsNo;
                                tblUSBRequestFormStatusHistory.CreatedOn = DateTime.Now;
                                tblUSBRequestFormStatusHistory.CreateByName = paramUserName;
                                dbContext.tblUSBRequestFormStatusHistory.Add(tblUSBRequestFormStatusHistory);
                                dbContext.SaveChanges();

                                _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                                _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                            }

                        }
                        _objResponseModel.PrimeryKeyId = paramObjUSBRequestFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("USBRequest", "ApproveUSBRequestForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel CloseUSBRequestForm(USBRequestFormsVM paramObjUSBRequestFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjUSBRequestFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        if (paramObjUSBRequestFormsVM.requestFomsVM.PendingWith == "IT Helpdesk" && paramObjUSBRequestFormsVM.requestFomsVM.Status == "Collection Pending")
                        {
                            var otpData = dbContext.tblOTP.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).OrderByDescending(x => x.OTPId).FirstOrDefault();
                            if (otpData != null)
                            {
                                if (otpData.OTP == paramObjUSBRequestFormsVM.OTP)
                                {
                                    tblRequestFormsdata.PendingWith = "";
                                    tblRequestFormsdata.Status = "Close";
                                    tblRequestFormsdata.UpdateBy = paramPsNo;
                                    tblRequestFormsdata.UpdateOn = DateTime.Now;
                                    tblRequestFormsdata.UpdateBy = paramUserName;
                                    dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                                    dbContext.SaveChanges();
                                    tblUSBRequestForm tblUSBRequestFormdata = new tblUSBRequestForm();
                                    tblUSBRequestFormdata = dbContext.tblUSBRequestForm.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                                    if (tblUSBRequestFormdata != null)
                                    {
                                        tblUSBRequestFormdata.Remarks = paramObjUSBRequestFormsVM.Remarks;
                                        tblUSBRequestFormdata.UpdateBy = paramPsNo;
                                        tblUSBRequestFormdata.UpdateOn = DateTime.Now;
                                        tblUSBRequestFormdata.UpdateBy = paramUserName;
                                        dbContext.Entry(tblUSBRequestFormdata).State = EntityState.Modified;
                                        dbContext.SaveChanges();

                                        tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                                        tblUSBRequestFormStatusHistory.USBRequestFormId = tblUSBRequestFormdata.USBRequestFormId;
                                        tblUSBRequestFormStatusHistory.StatusName = "Close";
                                        tblUSBRequestFormStatusHistory.IsDeleted = false;
                                        tblUSBRequestFormStatusHistory.CreateBy = paramPsNo;
                                        tblUSBRequestFormStatusHistory.CreatedOn = DateTime.Now;
                                        tblUSBRequestFormStatusHistory.CreateByName = paramUserName;
                                        dbContext.tblUSBRequestFormStatusHistory.Add(tblUSBRequestFormStatusHistory);
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

                        _objResponseModel.PrimeryKeyId = paramObjUSBRequestFormsVM.RequestFormId;
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

        public ResponseModel ReturnUSBRequestForm(USBRequestFormsVM paramObjUSBRequestFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjUSBRequestFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        tblRequestFormsdata.PendingWith = "Initiator";
                        tblRequestFormsdata.Status = "Returned";

                        tblRequestFormsdata.ReturnRejectWith = paramObjUSBRequestFormsVM.requestFomsVM.PendingWith;
                        tblRequestFormsdata.ReturnRejectBy = paramPsNo;
                        tblRequestFormsdata.ReturnRejectName = paramUserName;
                        tblRequestFormsdata.ReturnRejectDate = DateTime.Now;                        
                        tblRequestFormsdata.LastPendingWith = paramObjUSBRequestFormsVM.requestFomsVM.Status;                        
                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        tblUSBRequestForm tblUSBRequestFormdata = new tblUSBRequestForm();
                        tblUSBRequestFormdata = dbContext.tblUSBRequestForm.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblUSBRequestFormdata != null)
                        {
                            tblUSBRequestFormdata.DHRemarks = paramObjUSBRequestFormsVM.DHRemarks;
                            tblUSBRequestFormdata.ITDHRemarks = paramObjUSBRequestFormsVM.ITDHRemarks;
                            tblUSBRequestFormdata.SerialNo = paramObjUSBRequestFormsVM.SerialNo;
                            tblUSBRequestFormdata.Model = paramObjUSBRequestFormsVM.Model;
                            tblUSBRequestFormdata.Vendor = paramObjUSBRequestFormsVM.Vendor;
                            tblUSBRequestFormdata.Description = paramObjUSBRequestFormsVM.Description;
                            tblUSBRequestFormdata.Remarks = paramObjUSBRequestFormsVM.Remarks;
                            tblUSBRequestFormdata.UpdateBy = paramPsNo;
                            tblUSBRequestFormdata.UpdateOn = DateTime.Now;
                            tblUSBRequestFormdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblUSBRequestFormdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                            tblUSBRequestFormStatusHistory.USBRequestFormId = tblUSBRequestFormdata.USBRequestFormId;
                            tblUSBRequestFormStatusHistory.StatusName = "Returned";
                            tblUSBRequestFormStatusHistory.IsDeleted = false;
                            tblUSBRequestFormStatusHistory.CreateBy = paramPsNo;
                            tblUSBRequestFormStatusHistory.CreatedOn = DateTime.Now;
                            tblUSBRequestFormStatusHistory.CreateByName = paramUserName;
                            dbContext.tblUSBRequestFormStatusHistory.Add(tblUSBRequestFormStatusHistory);
                            dbContext.SaveChanges();


                            var tblUSBRequestFormStatusHistoryList = dbContext.tblUSBRequestFormStatusHistory.Where(x => x.USBRequestFormId == paramObjUSBRequestFormsVM.USBRequestFormId && x.IsDeleted == false).ToList();

                            foreach (var item in tblUSBRequestFormStatusHistoryList)
                            {
                                var HistoryData = dbContext.tblUSBRequestFormStatusHistory.Where(x => x.USBFormStatusHistoryId == item.USBFormStatusHistoryId).FirstOrDefault();
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
                        _objResponseModel.PrimeryKeyId = paramObjUSBRequestFormsVM.RequestFormId;
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("USBRequest", "ReturnUSBRequestForm", "", ex.Message);
                _objResponseModel.OtherParameter = new
                {
                    AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message)
                };
            }
            return _objResponseModel;
        }

        public ResponseModel RejectUSBRequestForm(USBRequestFormsVM paramObjUSBRequestFormsVM, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                // Data Save 
                if (paramObjUSBRequestFormsVM.RequestFormId > 0)
                {
                    tblRequestForms tblRequestFormsdata = new tblRequestForms();

                    tblRequestFormsdata = dbContext.tblRequestForms.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                    if (tblRequestFormsdata != null)
                    {
                        tblRequestFormsdata.PendingWith = "";
                        tblRequestFormsdata.Status = "Reject";
                        tblRequestFormsdata.ReturnRejectWith = paramObjUSBRequestFormsVM.requestFomsVM.PendingWith;
                        tblRequestFormsdata.ReturnRejectBy = paramPsNo;
                        tblRequestFormsdata.ReturnRejectName = paramUserName;
                        tblRequestFormsdata.ReturnRejectDate = DateTime.Now;
                        tblRequestFormsdata.LastPendingWith = paramObjUSBRequestFormsVM.requestFomsVM.Status;
                        tblRequestFormsdata.UpdateBy = paramPsNo;
                        tblRequestFormsdata.UpdateOn = DateTime.Now;
                        tblRequestFormsdata.UpdateBy = paramUserName;
                        dbContext.Entry(tblRequestFormsdata).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        tblUSBRequestForm tblUSBRequestFormdata = new tblUSBRequestForm();
                        tblUSBRequestFormdata = dbContext.tblUSBRequestForm.Where(x => x.RequestFormId == paramObjUSBRequestFormsVM.RequestFormId && x.IsDeleted == false).FirstOrDefault();
                        if (tblUSBRequestFormdata != null)
                        {
                            tblUSBRequestFormdata.DHRemarks = paramObjUSBRequestFormsVM.DHRemarks;
                            tblUSBRequestFormdata.ITDHRemarks = paramObjUSBRequestFormsVM.ITDHRemarks;
                            tblUSBRequestFormdata.SerialNo = paramObjUSBRequestFormsVM.SerialNo;
                            tblUSBRequestFormdata.Model = paramObjUSBRequestFormsVM.Model;
                            tblUSBRequestFormdata.Vendor = paramObjUSBRequestFormsVM.Vendor;
                            tblUSBRequestFormdata.Description = paramObjUSBRequestFormsVM.Description;
                            tblUSBRequestFormdata.Remarks = paramObjUSBRequestFormsVM.Remarks;
                            tblUSBRequestFormdata.IsReject = true;
                            tblUSBRequestFormdata.UpdateBy = paramPsNo;
                            tblUSBRequestFormdata.UpdateOn = DateTime.Now;
                            tblUSBRequestFormdata.UpdateBy = paramUserName;
                            dbContext.Entry(tblUSBRequestFormdata).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            tblUSBRequestFormStatusHistory tblUSBRequestFormStatusHistory = new tblUSBRequestFormStatusHistory();
                            tblUSBRequestFormStatusHistory.USBRequestFormId = tblUSBRequestFormdata.USBRequestFormId;
                            tblUSBRequestFormStatusHistory.StatusName = "Reject";
                            tblUSBRequestFormStatusHistory.IsDeleted = false;
                            tblUSBRequestFormStatusHistory.CreateBy = paramPsNo;
                            tblUSBRequestFormStatusHistory.CreatedOn = DateTime.Now;
                            tblUSBRequestFormStatusHistory.CreateByName = paramUserName;
                            dbContext.tblUSBRequestFormStatusHistory.Add(tblUSBRequestFormStatusHistory);
                            dbContext.SaveChanges();


                            var tblUSBRequestFormStatusHistoryList = dbContext.tblUSBRequestFormStatusHistory.Where(x => x.USBRequestFormId == paramObjUSBRequestFormsVM.USBRequestFormId && x.IsDeleted == false).ToList();

                            foreach (var item in tblUSBRequestFormStatusHistoryList)
                            {
                                var HistoryData = dbContext.tblUSBRequestFormStatusHistory.Where(x => x.USBFormStatusHistoryId == item.USBFormStatusHistoryId).FirstOrDefault();
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
                        _objResponseModel.PrimeryKeyId = paramObjUSBRequestFormsVM.RequestFormId;
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

        public List<tblUSBRequestFormStatusHistory> getStatuOfRequest(long paramUSBRequestFormId) {
            //RequestFomsVM RequestFomsVM = new RequestFomsVM();
            //var Response = dbContext.tblRequestForms.Where(s => s.RequestFormId == paramRequestFormId).FirstOrDefault();
            //RequestFomsVM.PendingWith = Response.PendingWith;
            //RequestFomsVM.Status = Response.Status;
            //return RequestFomsVM;
            return dbContext.tblUSBRequestFormStatusHistory.Where(s => s.USBRequestFormId == paramUSBRequestFormId && s.IsDeleted == false).ToList();
        }


        #endregion
    }
}
