using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class BookForCreationDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "Category id is required")]
        public Guid Category_Id { get; set; }
    }
}
