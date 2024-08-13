using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("book")]
    public class Book
    {
        [Column("Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
        public string? Author { get; set; }

        [ForeignKey(nameof(Category))]
        [Required(ErrorMessage = "Category is required")]
        public Guid Category_Id { get; set; } // Required foreign key property

        public Category Category { get; set; } = null!; // Required reference navigation to principal
    }
}