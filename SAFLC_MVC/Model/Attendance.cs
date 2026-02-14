namespace SAFLC_MVC.Model
{
    public class Attendance
    {
        public int Id { get; set; }

        public int EnrollmentId { get; set; }

        public DateOnly DateOnly { get; set; }

        public bool Status { get; set; }

        public string? Remarks { get; set; }
    }
}
