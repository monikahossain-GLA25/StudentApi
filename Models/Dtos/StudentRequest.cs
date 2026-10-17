using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models.Dtos
{
    public class StudentRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = "";

    }
}
