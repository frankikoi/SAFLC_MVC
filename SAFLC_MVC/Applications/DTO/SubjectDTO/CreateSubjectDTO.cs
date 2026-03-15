using SAFLC_MVC.Applications.Model;
using System.ComponentModel.DataAnnotations;

namespace SAFLC_MVC.Applications.DTO.SubjectDTO
{
    public class CreateSubjectDTO
    {
        [Required(ErrorMessage = "Subject Name is required")]
        public string? SubjectName { get; set; }
    }
}
