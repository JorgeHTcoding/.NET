using System.ComponentModel.DataAnnotations;


namespace University1.Models.DataModels
{
    public enum Nivel { Básico, Intermedio, Avanzado };
    public class Course : BaseEntity
    {
        [Required, StringLength(25)]
        public string Nombre { get; set; } = string.Empty;
        [Required, StringLength(280)]
        public string DescripcionCorta { get; set; } = string.Empty;
        [Required]
        public string DescripcionLarga { get; set; } = string.Empty;
        [Required]
        public string PublicoObjetivo { get; set; } = string.Empty;
        [Required]
        public string Objetivos { get; set; } = string.Empty;
        [Required]
        public string Requisitos { get; set; } = string.Empty;
        [Required]
        public Nivel Nivel { get; set; }
        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        [Required]
        public Chapters Chapter{ get; set; } = new Chapters();
        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();


    }
}
