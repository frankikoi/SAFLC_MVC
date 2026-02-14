namespace SAFLC_MVC.Model
{
    public class Payment
    {
        public int Id { get; set; }

        public int BillingId { get; set; }

        public DateOnly PaymentDate { get; set; }

        public decimal Amount { get; set; }
    }
}
