namespace SmartParking.API.Data.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string JobName { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
