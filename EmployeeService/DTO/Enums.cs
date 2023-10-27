namespace EmployeeService.DTO
{
    public class Enums
    {
        public static string GetGender(GenderEnum gender)
        {
            return gender switch
            {
                GenderEnum.Male => "Male",
                GenderEnum.Female => "Female",
                _ => "Invalid data detected",
            };
        }

        public static string GetConfirmationMessage(ConfirmationMessagesEnum message)
        {
            return message switch
            {
                ConfirmationMessagesEnum.Accepted => "Accepted",
                ConfirmationMessagesEnum.AddedtoConnection => "Added to connection",
                ConfirmationMessagesEnum.AlreadyInYourConnectionList => "Already in your connection list",
                ConfirmationMessagesEnum.CannotDelete => "Cannot delete. Add to connection first",
                ConfirmationMessagesEnum.CouldNotBeFound => "The Employee or Connection could not be found in the database",
                ConfirmationMessagesEnum.EmployeeDeleted => "Employee successfully deleted from your connections",
                _ => "Try Again",
            };
        }
    }
}
