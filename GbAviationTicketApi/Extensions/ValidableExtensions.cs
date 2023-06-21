using GbAviationTicketApi.Common;
using System.ComponentModel.DataAnnotations;

namespace GbAviationTicketApi.Extentions
{
    public static class ValidableExtension
    {
        public static bool IsValid(this IValidable validable)
        {
            return Validator.TryValidateObject(validable, new ValidationContext(validable), null, true);
        }

        public static bool IsValid(this IValidable validable, ICollection<ValidationResult> results)
        {
            return Validator.TryValidateObject(validable, new ValidationContext(validable), results, true);
        }

        public static bool IsValid(this IValidable validable, out List<string> resultResume)
        {
            resultResume = new List<string>();
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(validable, new ValidationContext(validable), results, true);
            foreach (var r in results)
                resultResume.Add(r.ErrorMessage ?? "Error no especificado");

            return isValid;
        }

        public static bool IsValid(this IValidable validable, out string resultResume)
        {
            resultResume = "";
            var isValid = IsValid(validable, out List<string> resultResumeList);

            foreach (var r in resultResumeList)
                resultResume += r + '\n';

            return isValid;
        }

    }
}
