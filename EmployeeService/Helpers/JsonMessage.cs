namespace EmployeeService.Helpers
{
    public class JsonMessage<T>
    {
        public List<T> Results { get; set; }
        public T Result { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
