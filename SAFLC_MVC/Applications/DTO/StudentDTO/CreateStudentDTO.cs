using SAFLC_MVC.Applications.Model;
using System.ComponentModel.DataAnnotations;

namespace SAFLC_MVC.Applications.DTO.StudentDTO
{
    public class CreateStudentDTO : BaseEntity
    {
        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please select a birth date")]
        [DataType(DataType.Date)]
        public DateOnly? BirthDate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only numbers are allowed")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Number must be exactly 11 digits")]
        public string? ContactNumber { get; set; }

        [Required(ErrorMessage = "Guardian is required")]
        public string? Guardian { get; set; }

        [Required(ErrorMessage = "Father's Name is required")]
        public string? FatherName { get; set; }

        [Required(ErrorMessage = "Mother's Name is required")]
        public string? MotherName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }

        public bool Status { get; set; }

    }
}
