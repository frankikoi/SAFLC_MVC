namespace SAFLC_MVC.Applications.DTO.SchoolYearDTO
{
    public class GetSchoolYearDTO
    {
        public int Id { get; set; }

        public string? Year { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }

}
