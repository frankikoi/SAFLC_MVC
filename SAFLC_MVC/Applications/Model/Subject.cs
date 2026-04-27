using SAFLC_MVC.Applications.Model;

namespace SAFLC_MVC.Application.Model
{
    public class Subject : BaseEntity
    {
        public int Id { get; set; }

        public string? SubjectName { get; set; }
    }
}
