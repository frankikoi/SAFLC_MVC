namespace SAFLC_MVC.Model
{
    public class SchoolYear
    {
        public int Id { get; set; }

        public string? Year { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}
