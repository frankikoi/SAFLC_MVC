namespace SAFLC_MVC.Applications.DTO.ActivityDTO
{
    public class CreateActivityDTO
    {
        public int ClassId { get; set; }

        public int SubjectId { get; set; }

        public int Quarter { get; set; }

        public string? Title { get; set; }

        public int TotalScore { get; set; }
    }
}
