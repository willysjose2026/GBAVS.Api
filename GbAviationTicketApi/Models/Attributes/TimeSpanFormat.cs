using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GbAviationTicketApi.Models.Attributes
{
    public partial class TimeSpanFormat : ValidationAttribute
    {

        public TimeSpanFormat()
        {
        }

        public override bool IsValid(object? value)
        {
            string strValue = value as string ?? "";
            if (!string.IsNullOrEmpty(strValue))
            {
                return MyRegex().IsMatch(strValue);
            }
            return base.IsValid(value);
        }

        [GeneratedRegex("^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\\d{1,9}))?)?$")]
        private static partial Regex MyRegex();
    }
}
