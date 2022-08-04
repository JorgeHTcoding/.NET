using System.ComponentModel.DataAnnotations;

namespace univeristy_api_backend.Models.DataModels
{
    public enum Nivel { Básico, Intermedio, Avanzado };
    public class Curso : BaseEntity
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

    }
}
