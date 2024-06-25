namespace EmployeeService.Enums
{
    public class EnumsImplementation
    {
        public static string GetGender(Gender gender)
        {
            return gender switch
            {
                Gender.Male => "Male",
                Gender.Female => "Female",
                _ => "Invalid data detected",
            };
        }

        public static string ConfirmationMessage(ConnectionEnum message)
        {
            return message switch
            {
                ConnectionEnum.Accepted => "Accepted",
                ConnectionEnum.AddedtoConnection => "Added to connection",
                ConnectionEnum.AlreadyInYourConnectionList => "Already in your connection list",
                ConnectionEnum.CannotAdd => "Connection Request Not Found. Add To Connection First.",
                ConnectionEnum.CannotDelete => "Cannot delete. Add to connection first",
                ConnectionEnum.CouldNotBeFound => "Connection Not Found.",
                ConnectionEnum.EmployeeDeleted => "Employee successfully deleted from your connections.",
                _ => "Try Again",
            };
        }

        public static string ConfirmationMessage(RequestEnum connectionRequestMessages)
        {
            return connectionRequestMessages switch
            {
                RequestEnum.Pending => "Pending",
                RequestEnum.RequestAccepted => "Request Accepted Successfully ",
                RequestEnum.RequestSent => "Request Sent Successfully.",
                _ => "Invalid data detected",
            };
        }
    }
}
