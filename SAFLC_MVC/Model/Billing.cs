namespace SAFLC_MVC.Model
{
    public class Billing
    {
        public int Id { get; set; }

        public int EnrollmentId { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Status { get; set; }
    }
}
