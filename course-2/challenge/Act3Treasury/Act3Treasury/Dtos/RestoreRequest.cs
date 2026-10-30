using System.ComponentModel.DataAnnotations;

namespace Act3Treasury.Dtos
{
    public class RestoreRequest
    {
        [Required]
        public string Reference { get; set; }
        [Required]
        public string Damaged { get; set; }
    }
}
