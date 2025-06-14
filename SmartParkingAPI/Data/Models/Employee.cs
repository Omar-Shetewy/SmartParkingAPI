namespace SmartParking.API.Data.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public double Salary { get; set; }

        public int JobId { get; set; }
        public Job Job { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
