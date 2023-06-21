namespace GbAviationTicketApi.Extentions
{
    public static class StringExtensions
    {
        public static string ToCapitalize(this string value)
        {

            string[] str_array = value.Trim().ToLower().Split(' ');
            string newString = "";
            foreach (string str in str_array)
            {
                if (str.Length == 1)
                {
                    newString += str + " ";
                }
                else if (str.Length > 1)
                {
                    string ns = string.Concat(str[0].ToString().ToUpper(), str.AsSpan(1));
                    newString += ns + " ";
                }
            }

            return newString.Trim();
        }
    }
}
