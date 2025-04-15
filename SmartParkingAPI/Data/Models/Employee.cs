namespace SmartParking.API.Data.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public double Salary { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

        public int JobId { get; set; }
        public Job Job { get; set; }

        public int GarageId { get; set; }
        public Garage Garage { get; set; }
    }
}
