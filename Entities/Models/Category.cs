using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("category")]
    public class Category
    {
        [Column("Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
        public string? Name { get; set; }

        //public ICollection<Book>? Books { get; set; }
    }
}