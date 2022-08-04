using System.ComponentModel.DataAnnotations;

namespace univeristy_api_backend.Models.DataModels
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UpdateddBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; }
        public string DeleteddBy { get; set; } = string.Empty;
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
