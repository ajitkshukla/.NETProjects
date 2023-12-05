namespace StudentAPI.Model.Requests
{
    public class AddStudentRequest
    {       
        public string? Name { get; set; }
        public string? DawgTag { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Address? Address { get; set; }
    }
}
