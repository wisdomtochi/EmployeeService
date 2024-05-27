namespace EmployeeService.Enums
{
    public class EnumsImplementation
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

        public static string ConfirmationMessage(ConnectionMessagesEnum message)
        {
            return message switch
            {
                ConnectionMessagesEnum.Accepted => "Accepted",
                ConnectionMessagesEnum.AddedtoConnection => "Added to connection",
                ConnectionMessagesEnum.AlreadyInYourConnectionList => "Already in your connection list",
                ConnectionMessagesEnum.CannotDelete => "Cannot delete. Add to connection first",
                ConnectionMessagesEnum.CouldNotBeFound => "The Employee or Connection could not be found in the database",
                ConnectionMessagesEnum.EmployeeDeleted => "Employee successfully deleted from your connections",
                _ => "Try Again",
            };
        }

        public static string ConfirmationMessage(ConnectionRequestMessagesEnum connectionRequestMessages)
        {
            return connectionRequestMessages switch
            {
                ConnectionRequestMessagesEnum.Pending => "Pending",
                ConnectionRequestMessagesEnum.RequestRemoved => "Request successfully removed from list.",
                ConnectionRequestMessagesEnum.CannotDeleteRequest => "You can't delete an employee's request when you don't have a connection request message",
                ConnectionRequestMessagesEnum.CouldNotBeFound => "The request Id or employee Id cannot be found in the database. Check that your entries are correct.",
                _ => "Invalid data detected",
            };
        }
    }
}
