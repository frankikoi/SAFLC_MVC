namespace SAFLC_MVC.Applications.DTO.SchoolYearDTO
{
    public class CreateSchoolYearDTO
    {
        public string? Year { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}
