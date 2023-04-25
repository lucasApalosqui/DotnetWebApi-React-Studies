using System.ComponentModel.DataAnnotations;

namespace AlunosApi.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "The number of characters cannot exceed 80")]
        [MinLength(3, ErrorMessage = "The number of characters cannot be less than 3")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(80, ErrorMessage = "The number of characters cannot exceed 80")]
        [MinLength(5, ErrorMessage = "The number of characters cannot be less than 3")]
        public string Email { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
