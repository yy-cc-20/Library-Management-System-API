namespace Entities.Models
{
    public class BookParameters : QueryStringParameters
    {
        public BookParameters()
        {
            OrderBy = "Name";
        }
        public Guid? Category_Id { get; set; }
        public string? SearchTerm { get; set; }
        public string? Author { get; set; }
    }
}
