using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Utility
{
    public class Enums
    {
        public enum paramTypeEnum
        {
            String,
            Integer,
            Description
        }
        public enum TermsAnswerType
        {
            [Description("Text (single line limited)")]
            Textsinglelinelimited = 1,
            [Description("Text (single line)")]
            Textsingleline = 2,
            [Description("Text (multiple lines)")]
            Textmultipleline = 3,
            [Description("Whole Number")]
            WholeNumber = 4,
            [Description("Decimal Number")]
            DecimalNumber = 5,
            [Description("Date")]
            Date = 6,
            [Description("Money")]
            Money = 7,
            [Description("Yes / No")]
            YesOrNo = 8,
            [Description("Attachment")]
            Attachment = 9,
            [Description("Certificate")]
            Certificate = 10,
            [Description("Address")]
            Address = 11,
            [Description("Percentage")]
            Percentage = 12,
            [Description("Quantity")]
            Quantity = 13
        }
        public enum TermsAcceptableValue
        {
            [Description("Any Value")]
            AnyValue = 1,
            [Description("List of Choices")]
            ListofChoices = 2,
            [Description("Limitted Range")]
            LimittedRange = 3
        }
        public enum YesOrNo
        {
            Yes,
            No
        }
        public enum TableName
        {

        }
        public enum RoleType
        {
            Admin,
            Other
        }
        public enum ActionFlg
        {
            Add,
            Edit,
            Delete,
            Cut,
            Copy,
            Paste
        }
    }
}
