using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Utility
{
    public static class HelperMessage
    {
        #region Response Result
        public static class ResponceResult
        {
            public static string OK = "OK";
            public static string WARNING = "WARNING";
            public static string ERROR = "ERROR";
            public static string DUPLICATE = "DUPLICATE";

        }
        #endregion

        #region Error Message
        public static string Error(string paramRepositoryOrClassName,
                                   string paramRepositoryOrClassMethod,
                                   string paramErrorInRegion,
                                   string paramErrorMessage,
                                   string paramErrorDetail = "")
        {
            return string.Format("<b>Repository Name:</b> {0} <br/><b>Repository Method:</b> {1} <br/><b>Region:</b> {2} <br/><br/><p><b>Error Message:</b> {3} </p><p><b>Error Detail:</b> {4}</p>.",
                                     paramRepositoryOrClassName,
                                     paramRepositoryOrClassMethod,
                                     paramErrorInRegion,
                                     paramErrorMessage,
                                     paramErrorDetail);
        }
        #endregion

        #region By Object Message
        public static string DuplicateByObject = "{0} already exists.";
        public static string RequiredByObject = "{0} field is required.";
        public static string NotFoundByObject = "No {0} found.";
        #endregion

        #region Common Message
        public static string SaveSuccessfully = "Data Saved Successfully.";
        public static string DeleteSuccessfully = "Record Deleted Successfully.";
        public static string NoRecordFound = "No Record found.";
        public static string AjaxError = "something went wrong.";
        public static string PasswordMatch = "your passwords do not match. please try again.";
        #endregion

        #region Return Bootstrape Alert
        public static string alertMessage(string paramResult, string paramMessage)
        {
            try
            {
                if (!string.IsNullOrEmpty(paramResult))
                {
                    string _strReturnMessage = string.Empty;
                    if (paramResult == HelperMessage.ResponceResult.DUPLICATE || paramResult == HelperMessage.ResponceResult.WARNING)
                    {
                        _strReturnMessage = "<div class='alert alert-warning alert-dismissible' role='alert'><strong>" + paramMessage + "</strong>";
                    }
                    else if (paramResult == HelperMessage.ResponceResult.ERROR)
                    {
                        _strReturnMessage = "<div class='alert alert-danger alert-dismissible ' role='alert'><strong>" + paramMessage + "</strong>";
                    }
                    else if (paramResult == HelperMessage.ResponceResult.OK)
                    {
                        _strReturnMessage = "<div class='alert alert-primary alert-dismissible ' role='alert'><strong>" + paramMessage + "</strong>";
                    }
                    _strReturnMessage += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div>";
                    return _strReturnMessage;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
