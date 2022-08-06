using System.ComponentModel.DataAnnotations;

namespace University1.Models.DataModels
{
    public class Chapters : BaseEntity
    {
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = new Course();
        [Required]
        public string List { get; set; } = string.Empty;
    }
}