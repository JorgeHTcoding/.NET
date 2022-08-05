using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Categoria : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
