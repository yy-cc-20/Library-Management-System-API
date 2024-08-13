using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        PagedList<Book> GetBooksWithFilteringAndSorting(BookParameters bookParameters);
        Book GetBookById(Guid id);
        void CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
    }
}