using System.ComponentModel.DataAnnotations;


namespace University1.Models.DataModels
{
    public class Category : BaseEntity
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
