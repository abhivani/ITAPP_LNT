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
    public interface IRoleMasterRepository
    {
        #region RoleMaster
        List<tblRoleMaster> getRoles();
        tblRoleMaster getRoleById(long paramRoleId);

        ResponseModel getRoleByPsNO(string paramPsNo);

        ResponseModel saveRole(tblRoleMaster paramObjtblRoleMaster, string paramPsNo, string paramUserName);

        ResponseModel deleteRecord(long paramPkRoleId, string paramPsNo);
        ResponseModel GetRoleMasterNameByPsNo(string paramPsNo);
        #endregion

        #region Email Configuration
        //List<tblEmailConfiguration> getEmailConfiguration();

        //ResponseModel getEmailConfigurationByEmailConfigurationId(long paramPkEmailConfigurationId);



        //ResponseModel deleteEmailConfigurationRecord(long paramPkEmailConfigurationId, string paramPsNo);
        #endregion

        #region Check UserDeatils

        vwEmpInfo getViewUserRolesByUserPsNo(string paramUserPsNo);
        vwEmpInfo LoginDetailsData(string paramUserPsNo);
        Task<vwUserImage> getViewUserImage(string paramUserPsNo);



        #endregion
    }
    #endregion

    #region Implementation
    public class RoleMsterRepository : IRoleMasterRepository
    {


        private readonly ITAPPFORMSDbContext dbContext;
        public RoleMsterRepository(ITAPPFORMSDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        #region RollMaster

        #region Fecth Role Data

        public List<tblRoleMaster> getRoles()
        {

            return dbContext.tblRoleMaster.Where(s => s.IsDeleted == false).ToList();
        }
        public tblRoleMaster getRoleById(long paramRoleId)
        {
            return dbContext.tblRoleMaster.FirstOrDefault(x => x.RoleId == paramRoleId && x.IsDeleted == false);
        }

        public ResponseModel getRoleByPsNO(string paramPsNo)
        {
            // return dbContext.tblRoleMaster.FirstOrDefault(x => x.PsNo == paramPsNo);

            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
               
                var objvwEmpInfoList = dbContext.vwEmpInfo.Where(x => x.t_psno.ToLower().Contains(paramPsNo.ToLower())).Select(x => new { Name = x.t_psno }).Distinct().Take(10).ToList();
                if (objvwEmpInfoList != null)
                {
                    _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                    _objResponseModel.OtherParameter = new { PsnoData = objvwEmpInfoList };
                }
                else
                {
                    _objResponseModel.Result = HelperMessage.ResponceResult.WARNING;
                    _objResponseModel.Message = string.Format("User PS NO is not Match");
                    //_objResponseModel.OtherParameter = new { PsnoData = objvwEmpInfoList };

                }


            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("RoleMaster", "getRoleByPsNO", "", ex.Message);
            }
            return _objResponseModel;
        }

        public ResponseModel GetRoleMasterNameByPsNo(string paramPsNo)
        {
            // return dbContext.tblRoleMaster.FirstOrDefault(x => x.PsNo == paramPsNo);

            ResponseModel _objResponseModel = new ResponseModel();
            try
            {

                var objvwEmpInfoList = dbContext.vwEmpInfo.FirstOrDefault(x => x.t_psno == paramPsNo);
                if (objvwEmpInfoList != null)
                {
                    _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                    _objResponseModel.OtherParameter = new { PsnoData = objvwEmpInfoList };
                }
                else
                {
                    _objResponseModel.Result = HelperMessage.ResponceResult.WARNING;
                    _objResponseModel.Message = string.Format("User PS NO is not found");
                    //_objResponseModel.OtherParameter = new { PsnoData = objvwEmpInfoList };

                }


            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("RoleMaster", "GetRoleMasterNameByPsNo", "", ex.Message);
            }
            return _objResponseModel;
        }


        public vwEmpInfo getViewUserRolesByUserPsNo(string paramUserPsNo)
        {
            return dbContext.vwEmpInfo.FirstOrDefault(x => x.t_psno == paramUserPsNo);
        }
        #endregion

        public vwEmpInfo LoginDetailsData(string paramUserPsNo)
        {
            var objRoloMaster = dbContext.tblRoleMaster.FirstOrDefault(s => s.PsNo == paramUserPsNo);
            vwEmpInfo objEmpInfo = dbContext.vwEmpInfo.FirstOrDefault(x => x.t_psno == paramUserPsNo);

            //Session[Helper.SessionKey.UserImgstring] = await getUserImage(paramobjvwUserRole.Psno);
            if (objRoloMaster != null)
            {
                objEmpInfo.ROWID = 1;
            }
            else
            {
                objEmpInfo.ROWID = 2;
            }
            return objEmpInfo;
        }

        #region Maintain RoleMaster Data

        public ResponseModel saveRole(tblRoleMaster paramObjtblRoleMaster, string paramPsNo, string paramUserName)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                List<string> objValidPsNoList = new List<string>();
                List<string> objInvPsNoList = new List<string>();
                if (paramObjtblRoleMaster.RoleId == 0)
                {
                    //string[] _strArray = paramObjtblRoleMaster.Psno.Split(',');
                    bool _blDuplicatePsNo = checkPsNoDuplicate(paramObjtblRoleMaster.PsNo, paramObjtblRoleMaster.Role);
                    if (_blDuplicatePsNo)
                    {
                        objInvPsNoList.Add(paramObjtblRoleMaster.PsNo);
                    }
                    else
                    {
                        paramObjtblRoleMaster.PsNo = paramObjtblRoleMaster.PsNo;
                        paramObjtblRoleMaster.Name = paramObjtblRoleMaster.Name;
                        paramObjtblRoleMaster.Role = paramObjtblRoleMaster.Role.Trim();
                        paramObjtblRoleMaster.Location = paramObjtblRoleMaster.Location;
                        var Email = dbContext.vwEmpInfo.Where(x => x.t_psno == paramObjtblRoleMaster.PsNo).Select(s => s.t_mail).FirstOrDefault();
                        if (Email != null && Email != string.Empty)
                        {
                            paramObjtblRoleMaster.Email = dbContext.vwEmpInfo.Where(x => x.t_psno == paramObjtblRoleMaster.PsNo).Select(s => s.t_mail).FirstOrDefault();
                        }
                        paramObjtblRoleMaster.IsDeleted = false;
                        paramObjtblRoleMaster.CreateBy = paramPsNo;
                        paramObjtblRoleMaster.CreatedOn = DateTime.Now;
                        paramObjtblRoleMaster.CreateByName = paramUserName;
                        dbContext.tblRoleMaster.Add(paramObjtblRoleMaster);
                        dbContext.SaveChanges();
                        objValidPsNoList.Add(paramObjtblRoleMaster.PsNo);

                    }

                    string _strInvalidPsNo = string.Empty;
                    if (objInvPsNoList != null && objInvPsNoList.Count() > 0)
                    {
                        _strInvalidPsNo = string.Join(",", objInvPsNoList);
                    }
                    if (objValidPsNoList != null && objValidPsNoList.Count() > 0)
                    {
                        if (!string.IsNullOrEmpty(_strInvalidPsNo))
                        {
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully + "& Psno already exists is " + _strInvalidPsNo;
                            _objResponseModel.Result = HelperMessage.ResponceResult.WARNING;
                            _objResponseModel.OtherParameter = new { AlertFieldName = "Psno", AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message) };
                        }
                        else
                        {
                            _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                            _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                        }

                        _objResponseModel.PrimeryKeyId = paramObjtblRoleMaster.RoleId;
                    }
                    else
                    {
                        _objResponseModel.Result = HelperMessage.ResponceResult.DUPLICATE;
                        _objResponseModel.Message = string.Format(HelperMessage.DuplicateByObject, "User PS NO");
                        _objResponseModel.OtherParameter = new { AlertFieldName = "Psno", AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message) };
                    }
                }
                else
                {
                    tblRoleMaster objtblUserRole = getRoleById(paramObjtblRoleMaster.RoleId);
                    if (objtblUserRole != null)
                    {
                        objtblUserRole.PsNo = paramObjtblRoleMaster.PsNo;
                        objtblUserRole.Name = paramObjtblRoleMaster.Name;
                        objtblUserRole.Role = paramObjtblRoleMaster.Role.Trim();
                        objtblUserRole.Location = paramObjtblRoleMaster.Location;

                        objtblUserRole.IsDeleted = false;

                        objtblUserRole.UpdateBy = paramPsNo;
                        objtblUserRole.UpdateOn = DateTime.Now;
                        objtblUserRole.UpdateByName = paramUserName;
                        dbContext.SaveChanges();
                        dbContext.Entry(paramObjtblRoleMaster).State = EntityState.Detached;
                        _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                        _objResponseModel.Message = HelperMessage.SaveSuccessfully;
                    }
                    else
                    {
                        _objResponseModel.Result = HelperMessage.ResponceResult.WARNING;
                        _objResponseModel.Message = HelperMessage.NoRecordFound;
                        _objResponseModel.OtherParameter = new { AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message) };
                    }
                    _objResponseModel.PrimeryKeyId = paramObjtblRoleMaster.RoleId;
                }
            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("RoleMaster", "saveRoleMAster", "", ex.Message);
                _objResponseModel.OtherParameter = new { AlertMessage = HelperMessage.alertMessage(_objResponseModel.Result, _objResponseModel.Message) };
            }
            return _objResponseModel;
        }

        private bool checkPsNoDuplicate(string paramPSNO, string Role)
        {
            return dbContext.tblRoleMaster.Any(x => x.PsNo == paramPSNO && x.Role==Role && x.IsDeleted == false);
        }

        public ResponseModel deleteRecord(long paramPkRoleId, string paramPsNo)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {

                var objRoloMaster = dbContext.tblRoleMaster.FirstOrDefault(s => s.RoleId == paramPkRoleId);

                if (objRoloMaster != null)
                {



                    objRoloMaster.IsDeleted = true;
                    objRoloMaster.UpdateBy = paramPsNo;
                    objRoloMaster.UpdateOn = DateTime.Now;
                    dbContext.SaveChanges();
                    dbContext.Entry(objRoloMaster).State = EntityState.Detached;

                    _objResponseModel.Result = HelperMessage.ResponceResult.OK;
                    _objResponseModel.Message = HelperMessage.DeleteSuccessfully;

                }
                else
                {
                    _objResponseModel.Result = HelperMessage.ResponceResult.WARNING;
                    _objResponseModel.Message = string.Format(HelperMessage.NotFoundByObject, "Record");
                    //_objResponseModel.OtherParameter = new { PsnoData = objvwEmpInfoList };

                }





            }
            catch (Exception ex)
            {
                _objResponseModel.Result = HelperMessage.ResponceResult.ERROR;
                _objResponseModel.Message = HelperMessage.Error("RoleMaster", "deleteRecord", "", ex.Message);
            }
            return _objResponseModel;
        }
        #endregion

        #region Other Supporting Methods
        public async Task<vwUserImage> getViewUserImage(string paramUserPsNo)
        {
            //byte[] bt= await dbContext.vwUserImage.FirstOrDefaultAsync(x => x.PSNo == paramUserPsNo).Result.ImageData;
            return await dbContext.vwUserImage.FirstOrDefaultAsync(x => x.PSNo == paramUserPsNo);
        }
        #endregion

        #endregion
                     
    }

    #endregion
}
