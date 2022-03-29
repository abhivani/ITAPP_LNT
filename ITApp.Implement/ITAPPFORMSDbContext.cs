using ITApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ITApp.Implement
{
    public class ITAPPFORMSDbContext : DbContext
    {
        public ITAPPFORMSDbContext()
         : base("name=ITAPPConnectionString")

        {
            //Disable initializer
            Database.SetInitializer<ITAPPFORMSDbContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("ITApp");
        }
        #region Tables
        public virtual DbSet<tblRoleMaster> tblRoleMaster { get; set; }
        public virtual DbSet<tblRequestForms> tblRequestForms { get; set; }
        public virtual DbSet<tblUSBRequestForm> tblUSBRequestForm { get; set; }
        public virtual DbSet<tblUSBRequestFormStatusHistory> tblUSBRequestFormStatusHistory { get; set; }
        public virtual DbSet<tblOTP> tblOTP { get; set; }
        public virtual DbSet<tblDriveAccessForm> tblDriveAccessForm { get; set; }
        public virtual DbSet<tblDriveAccessFormStatusHistory> tblDriveAccessFormStatusHistory { get; set; }
        public virtual DbSet<tblDamageDeviceForm> tblDamageDeviceForm { get; set; }
        public virtual DbSet<tblDamageDeviceFormStatusHistory> tblDamageDeviceFormStatusHistory { get; set; }
        public virtual DbSet<tblDeviceShiftingForm> tblDeviceShiftingForm { get; set; }
        public virtual DbSet<tblDeviceShiftingFormStatusHistory> tblDeviceShiftingFormStatusHistory { get; set; }

        public virtual DbSet<tblNewITRequisition> tblNewITRequisition { get; set; }
        public virtual DbSet<tblNewITRequisitionStatusHistory> tblNewITRequisitionStatusHistory { get; set; }


        #endregion


        #region Views    
        public virtual DbSet<vwEmpInfo> vwEmpInfo { get; set; }
        public virtual DbSet<vwUserImage> vwUserImage { get; set; }

        #endregion


        #region Store Procedure 
        public virtual ObjectResult<string> Sp_Send_OTP_USB_Email(string paramEmail, string paramOTP, string paramLocation, string paramRequestNo)
        {

            SqlParameter[] parameters = {
                                          new SqlParameter { ParameterName = "Email", Value = paramEmail },
                                          new SqlParameter { ParameterName = "OTP", Value = paramOTP },
                                          new SqlParameter { ParameterName = "Location", Value = paramLocation },
                                          new SqlParameter { ParameterName = "RequestNo", Value = paramRequestNo }
                                        };
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<string>("ITApp.Sp_Send_OTP_USB_Email @Email,@OTP,@Location,@RequestNo", parameters);
        }



        public virtual List<RequestFomsVM> SP_getRequestFoms(string paramUPSNO)
        {


            List<RequestFomsVM> RequestFoms = new List<RequestFomsVM>();
            try
            {
                SqlParameter[] parameters = {
                                          new SqlParameter { ParameterName = "UPSNO", Value = paramUPSNO }
                                        };
                RequestFoms = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<RequestFomsVM>("ITApp.SP_getRequestFoms @UPSNO", parameters).ToList();

            }
            catch (Exception exp)
            {

            }
            return RequestFoms;
        }

        public virtual List<RequestFomsVM> SP_getAllRequestFoms(string paramUPSNO)
        {


            List<RequestFomsVM> RequestFoms = new List<RequestFomsVM>();
            try
            {
                SqlParameter[] parameters = {
                                          new SqlParameter { ParameterName = "UPSNO", Value = paramUPSNO }
                                        };
                RequestFoms = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<RequestFomsVM>("ITApp.SP_getAllRequestFoms @UPSNO", parameters).ToList();

            }
            catch (Exception exp)
            {

            }
            return RequestFoms;
        }

        public virtual List<RequestFomsVM> SP_getPendingRequestFoms(string paramUPSNO, string paramUserLocation, string paramResults)
        {


            List<RequestFomsVM> RequestFoms = new List<RequestFomsVM>();
            try
            {
                SqlParameter[] parameters = {
                                          new SqlParameter { ParameterName = "UPSNO", Value = paramUPSNO },
                                          new SqlParameter { ParameterName = "Location", Value = paramUserLocation },
                                          new SqlParameter { ParameterName = "Results", Value = paramResults }
                                        };
                RequestFoms = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<RequestFomsVM>("ITApp.SP_getPendingRequestFoms @UPSNO,@Location,@Results", parameters).ToList();

            }
            catch (Exception exp)
            {

            }
            return RequestFoms;
        }

        public virtual ObjectResult<string> Sp_Send_Mail_Drive_Access(string paramEmail, string paramRequestNo)
        {

            SqlParameter[] parameters = {
                                          new SqlParameter { ParameterName = "Email", Value = paramEmail },

                                          new SqlParameter { ParameterName = "RequestNo", Value = paramRequestNo }
                                        };
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<string>("ITApp.Sp_Send_Mail_Drive_Access @Email,@RequestNo", parameters);


        }
        public virtual ObjectResult<string> Sp_Send_Mail_Damage_Device(string paramEmail, string paramRequestNo)
        {

            SqlParameter[] parameters = {
                                          new SqlParameter { ParameterName = "Email", Value = paramEmail },

                                          new SqlParameter { ParameterName = "RequestNo", Value = paramRequestNo }
                                        };
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<string>("ITApp.Sp_Send_Mail_Damage_Device @Email,@RequestNo", parameters);


        }

        public virtual ObjectResult<string> Sp_Send_Mail_Device_Shifting(string paramEmail, string paramRequestNo)
        {

            SqlParameter[] parameters = {
                                          new SqlParameter { ParameterName = "Email", Value = paramEmail },

                                          new SqlParameter { ParameterName = "RequestNo", Value = paramRequestNo }
                                        };
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<string>("ITApp.Sp_Send_Mail_Device_Shifting @Email,@RequestNo", parameters);


        }

        public virtual List<RequestFomsVM> SP_getPendingRequestFomsNew(string paramUPSNO, string paramstrinQuery, string paramUserLocation, string paramResults)
        {


            List<RequestFomsVM> RequestFoms = new List<RequestFomsVM>();
            try
            {
                SqlParameter[] parameters = {
                                          new SqlParameter { ParameterName = "UPSNO", Value = paramUPSNO },
                                          new SqlParameter { ParameterName = "StrinQuery", Value = paramstrinQuery },
                                          new SqlParameter { ParameterName = "Location", Value = paramUserLocation },
                                          new SqlParameter { ParameterName = "Results", Value = paramResults }
                                        };
                RequestFoms = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<RequestFomsVM>("ITApp.SP_getPendingRequestFomsNew @UPSNO,@StrinQuery,@Location,@Results", parameters).ToList();

            }
            catch (Exception exp)
            {

            }
            return RequestFoms;
        }


        public virtual List<DeliveryByVM> SP_getDeliveryByData(string paramUPSNO)
        {
            List<DeliveryByVM> RequestFoms = new List<DeliveryByVM>();
            try
            {
                SqlParameter[] parameters = { new SqlParameter { ParameterName = "UPSNO", Value = paramUPSNO } };
                RequestFoms = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<DeliveryByVM>("ITApp.SP_getDeliveryByData @UPSNO", parameters).ToList();
            }
            catch (Exception exp)
            {
            }
            return RequestFoms;
        }

        #endregion


    }
}
