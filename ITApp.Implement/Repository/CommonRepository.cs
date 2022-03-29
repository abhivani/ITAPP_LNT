using ITApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Implement.Repository
{

    #region interface

    public interface ICommonRepository
    {
        //tblRoleMaster getRoleMasterList(string paramUserPsNo);
        #region Check UserDeatils
        tblRoleMaster getViewUserRolesByUserPsNo(string paramUserPsNo);
        vwEmpInfo getLogingcheckPsNo(string paramUserPsNo);
        string getViewUserRolesByUserPsNoData(string paramUserPsNo);
        vwEmpInfo LoginDetailsData(string paramUserPsNo);
        Task<vwUserImage> getViewUserImage(string paramUserPsNo);
        List<RequestFomsVM> getRequestFoms(string UPSNO);
        List<RequestFomsVM> getPendingRequestFoms(string UPSNO, string UserLocation, string Results);

        List<RequestFomsVM> getAllRequestFoms(string UPSNO);
        List<RequestFomsVM> getPendingRequestFomsData(string UPSNO,string Role, string UserLocation, string Results);
        #endregion
    }

    #endregion

    #region Implementation
    public class CommonRepository : ICommonRepository
    {
        private readonly ITAPPFORMSDbContext dbContext;

        public CommonRepository(ITAPPFORMSDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        //public tblRoleMasterSSM getRoleMasterList(string paramUserPsNo)
        //{
        //    return dbContext.tblRoleMasterSSM.FirstOrDefault();
        //}

        #region Fecth UserData
        public string getViewUserRolesByUserPsNoData(string paramUserPsNo)
        {
            string roleName = string.Empty;
            var data= dbContext.tblRoleMaster.FirstOrDefault(x => x.PsNo == paramUserPsNo && x.IsDeleted == false);
            if(data !=null)
            {
                roleName = data.Role;
            }
            else
            {
                roleName = "Initiator";
            }
            return roleName;
        }
        public vwEmpInfo getLogingcheckPsNo(string paramUserPsNo)
        {           
           return dbContext.vwEmpInfo.FirstOrDefault(x => x.t_psno == paramUserPsNo);
            
        }
        public tblRoleMaster getViewUserRolesByUserPsNo(string paramUserPsNo)
        {
            return dbContext.tblRoleMaster.FirstOrDefault(x => x.PsNo == paramUserPsNo && x.IsDeleted == false);

        }
        public vwEmpInfo LoginDetailsData(string paramUserPsNo)
        {
            var objRoloMaster = dbContext.tblRoleMaster.FirstOrDefault(s => s.PsNo == paramUserPsNo && s.IsDeleted == false);
            vwEmpInfo objEmpInfo = dbContext.vwEmpInfo.FirstOrDefault(x => x.t_psno == paramUserPsNo);

            //Session[Helper.SessionKey.UserImgstring] = await getUserImage(paramobjvwUserRole.Psno);
            //if (objRoloMaster != null)
            //{
            //    if (objRoloMaster.Role == "Design")
            //    {
            //        objEmpInfo.ROWID = 1;
            //    }
            //    else
            //    {
            //        objEmpInfo.ROWID = 2;
            //    }
            //}
            return objEmpInfo;
        }

        #endregion
        #region Other Supporting Methods
        public async Task<vwUserImage> getViewUserImage(string paramUserPsNo)
        {
            //byte[] bt= await dbContext.vwUserImage.FirstOrDefaultAsync(x => x.PSNo == paramUserPsNo).Result.ImageData;
            return await dbContext.vwUserImage.FirstOrDefaultAsync(x => x.PSNo == paramUserPsNo);
        }

        public List<RequestFomsVM> getRequestFoms(string UPSNO) 
        {            
            return dbContext.SP_getRequestFoms(UPSNO);
        }
        public List<RequestFomsVM> getPendingRequestFoms(string UPSNO,string UserLocation, string Results)
        {


            return dbContext.SP_getPendingRequestFoms(UPSNO, UserLocation, Results);
        }  
        public List<RequestFomsVM> getPendingRequestFomsData(string UPSNO, string Role, string UserLocation, string Results)
        {
            List<RequestFomsVM> requestFomsVM = new List<RequestFomsVM>();
            string strinQuery = string.Empty;
            if (string.IsNullOrEmpty(UserLocation))
            {
                if (Role == "Initiator")
                {
                    var data = dbContext.tblRequestForms.Where(x => x.DepartmentHeadId == UPSNO).FirstOrDefault();
                    if (data != null)
                    {
                        strinQuery = "where RF.PendingWith Like 'Department Head' and RF.DepartmentHeadId='"+ UPSNO + "' OR (RF.Status = 'Returned' and RF.CreateBy = '" + UPSNO + "')";
                        return dbContext.SP_getPendingRequestFomsNew(UPSNO, strinQuery, UserLocation, Results);
                    }
                    else
                    {

                        return requestFomsVM;
                    }
                }
                else
                {                    
                        strinQuery = "where RF.PendingWith Like '" + Role + "'  OR (RF.Status = 'Returned' and RF.CreateBy = '" + UPSNO + "')";
                        return dbContext.SP_getPendingRequestFomsNew(UPSNO, strinQuery, UserLocation, Results);
                }
            }
            else
            {
                if (Role == "Initiator")
                {
                    var data = dbContext.tblRequestForms.Where(x => x.DepartmentHeadId == UPSNO).FirstOrDefault();
                    if (data != null)
                    {
                        strinQuery = "Where (RF.PendingWith Like 'Department Head' and RF.DepartmentHeadId='" + UPSNO + "' and Roles.PsNo = '" + UPSNO + "' )";
                        //or(RF.Status = 'Returned' and RF.CreateBy = '"+UPSNO+"')";
                        return dbContext.SP_getPendingRequestFomsNew(UPSNO, strinQuery, UserLocation, Results);
                    }
                    else
                    {

                        return requestFomsVM;
                    }
                }
                else if(Role== "IT Department Head")
                {
                    strinQuery = "where (RF.PendingWith Like 'Department Head' or RF.PendingWith='" + Role + "') and RF.DepartmentHeadId='" + UPSNO + "' OR (RF.Status = 'Returned' and RF.CreateBy = '" + UPSNO + "')";
                    return dbContext.SP_getPendingRequestFomsNew(UPSNO, strinQuery, UserLocation, Results);
                }
                else
                {
                    strinQuery = "Where (RF.PendingWith Like '"+ Role + "'  and Roles.PsNo = '" + UPSNO + "' ) or (RF.Status = 'Returned' and RF.CreateBy = '" + UPSNO + "')";
                    return dbContext.SP_getPendingRequestFomsNew(UPSNO, strinQuery, UserLocation, Results);
                }
            }           
           // return dbContext.SP_getPendingRequestFoms(UPSNO, UserLocation, Results);
        }

        public List<RequestFomsVM> getAllRequestFoms(string UPSNO)
        {
            return dbContext.SP_getAllRequestFoms(UPSNO);
        }
        #endregion
    }
    #endregion
}
