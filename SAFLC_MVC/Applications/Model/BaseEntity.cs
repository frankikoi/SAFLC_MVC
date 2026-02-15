using System.ComponentModel.DataAnnotations;

namespace SAFLC_MVC.Applications.Model
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? CreatedBy { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public string? LastModifiedBy { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
