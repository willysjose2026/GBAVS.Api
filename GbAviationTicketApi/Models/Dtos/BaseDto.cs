namespace GbAviationTicketApi.Models.Dtos
{
    public abstract class BaseDto
    {
        protected const string DOMAIN_REGEX = "[a-zA-Z0-9\\-_\\.]+@gbgroup\\.com";
        protected const string PASS_REGEX = "^(?=.*[0-9])(?=.*[a-z])(?=.*[.@_])(?=.*[A-Z]).{8,10}$";
        protected const string PASSWORD_ERROR_MESSAGE = "Password must have: \nRange between 8 to 10 characters" +
            "\nAt least one upper case letter" +
            "\nAt least one lower case letter" +
            "\nAt least one number" +
            "\nAt least one of these symbols (.@_)";
        public BaseDto()
        {

        }

        public abstract void Normalize();
    }
}
