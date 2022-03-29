using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ITApp.Utility
{
    public static class Helper
    {
        #region Session Key
        public static class SessionKey
        {
            public static string init = "init";
            public static string UPSNO = "UPSNO";
            public static string UserName = "UserName";
            public static string UserEmailId = "UserEmailId";
            public static string Role = "Role";
            public static string AuthToken = "AuthToken";
            public static string UserImgstring = "UserImgstring";
            public static string IsRegisterSupplier = "IsRegisterSupplier";
            public static string Location = "Location";
        }

        public static class ViewDataKey
        {
            public static string AlertMessage = "AlertMessage";
        }
        #endregion

        #region Enums Description Read Value + EnumToSelectList
        public static string DescriptionAttr<T>(this T source)
        {
            try
            {
                FieldInfo fi = source.GetType().GetField(source.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0) return attributes[0].Description;
                else return source.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static SelectList EnumToSelectList<T>(object selectedValue, string paramTextType, string paramValueType)
        {
            try
            {
                return new SelectList((Enum.GetValues(typeof(T)).Cast<T>())
                    .Select(x => new SelectListItem
                    {

                        Text = paramTextType == Enums.paramTypeEnum.Description.ToString() ? x.DescriptionAttr() : Enum.GetName(typeof(T), x),
                        Value = paramValueType == Enums.paramTypeEnum.Description.ToString() ? x.DescriptionAttr() : paramValueType == Enums.paramTypeEnum.String.ToString() ? Enum.GetName(typeof(T), x) : (Convert.ToInt32(x)).ToString()
                    }), "Value", "Text", selectedValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Common strings
        public static string ddlSelect = "---Select---";
        public static string DateFormat = "dd/MM/yyyy";
        public static string BootstrapDateFormat = "dd/mm/yy";
        public static string DateTimeFormat = "dd/MM/yyyy hh:mm:ss tt";
        public static string DateTimehhmmFormat = "dd/MM/yyyy hh:mm tt";
        public static string AttachmentBasePathCountSplitChar = "¾";
        public static string StringSplitChar = "½";
        #endregion
        #region VAPT : Malicious file found - File Extension , File Size (50Mb) ,exe rename pdf(other extension) 
        //a) File extensions(Web.config)
        //c) Magic Numbers(IsExeFile)
        //d) File Size.50MB

        public static string GetCheckValidFile(HttpFileCollectionBase paramHttpFileCollectionBase)
        {
            try
            {
                bool _blValid = false;
                if (paramHttpFileCollectionBase != null && paramHttpFileCollectionBase.Count > 0)
                {
                    for (int i = 0; i < paramHttpFileCollectionBase.Count; i++)
                    {
                        HttpPostedFileBase objHttpPostedFileBase = paramHttpFileCollectionBase[i];
                        if (objHttpPostedFileBase != null && objHttpPostedFileBase.ContentLength > 0)
                        {
                            if (!string.IsNullOrEmpty(objHttpPostedFileBase.FileName))
                            {
                                string[] _strarrExt = objHttpPostedFileBase.FileName.Split('.');
                                if (_strarrExt != null && _strarrExt.Length == 2)
                                {
                                    string _strfileExtension = _strarrExt[1].ToLower();
                                    int _fileSizeValid = 50 * 1024 * 1024;//50MB

                                    if (!AppSetting.FileAllowedExtensions.Split(',').Contains(_strfileExtension))
                                    {
                                        return "Please select proper file.<br/>Allowed File Type: " + AppSetting.FileAllowedExtensions;
                                    }
                                    else if (objHttpPostedFileBase.ContentLength > _fileSizeValid)
                                    {
                                        return "File size too large! Please upload less than 50MB.";
                                    }
                                    else
                                    {
                                        byte[] _byfile;
                                        using (Stream inputStream = objHttpPostedFileBase.InputStream)
                                        {
                                            MemoryStream memoryStream = inputStream as MemoryStream;
                                            if (memoryStream == null)
                                            {
                                                memoryStream = new MemoryStream();
                                                inputStream.CopyTo(memoryStream);
                                            }
                                            _byfile = memoryStream.ToArray();
                                        }
                                        if (_byfile != null && _byfile.Length > 0)
                                        {
                                            bool _blExeFile = IsExeFile(_byfile);
                                            if (_blExeFile == false)
                                            {
                                                _blValid = true;
                                                continue;
                                            }
                                            else
                                                return "Malicious file found -please select proper file.";
                                        }
                                        return "Malicious file found -please select proper file.";
                                    }
                                }
                                else
                                    return "Malicious file found -please select proper file.";
                            }
                            else
                                return "No file name found.";
                        }
                    }
                    return _blValid == true ? Enums.YesOrNo.Yes.ToString() : "No file found.";
                }
                return "No file found.";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static bool IsExeFile(byte[] FileContent)
        {
            try
            {
                var twoBytes = SubByteArray(FileContent, 0, 2);
                return ((Encoding.UTF8.GetString(twoBytes) == "MZ") || (Encoding.UTF8.GetString(twoBytes) == "ZM"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static byte[] SubByteArray(byte[] data, int index, int length)
        {
            try
            {
                byte[] result = new byte[length];
                Array.Copy(data, index, result, 0, length);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}
