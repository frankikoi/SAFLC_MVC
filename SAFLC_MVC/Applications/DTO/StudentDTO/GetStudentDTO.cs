using System.ComponentModel.DataAnnotations;

namespace SAFLC_MVC.Applications.DTO.StudentDTO
{
    public class GetStudentDTO
    {
        public int Id { get; set; }

        public string? StudentNo { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? Gender { get; set; }

        public string? ContactNumber { get; set; }

        public string? Guardian { get; set; }

        public string? FatherName { get; set; }

        public string? MotherName { get; set; }

        public string? Address { get; set; }

        public bool Status { get; set; } = true;

    }
}
