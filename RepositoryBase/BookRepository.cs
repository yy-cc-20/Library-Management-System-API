using Contracts;
using Entities;
using Entities.Helper;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Reflection;
using System.Text;

namespace Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        private ISortHelper<Book> _sortHelper;

        public BookRepository(RepositoryContext repositoryContext, ISortHelper<Book> sortHelper)
            : base(repositoryContext)
        {
            _sortHelper = sortHelper;
        }

        public PagedList<Book> GetBooksWithFilteringAndSorting(BookParameters bookParameters)
        {
            IQueryable<Book> books = FindAllAsIQueryable();

            ApplySearch(ref books, bookParameters);
            ApplyFilter(ref books, bookParameters);
            var sortedBooks = _sortHelper.ApplySort(books, bookParameters.OrderBy);

            // Paging
            return PagedList<Book>.ToPagedList(books
                .Include(book => book.Category),
                bookParameters.PageNumber,
                bookParameters.PageSize);

            //return PagedList<Book>.ToPagedList(FindAll(),
            //    bookParameters.PageNumber,
            //    bookParameters.PageSize);
        }

        // Search by Name / Author
        private void ApplySearch(ref IQueryable<Book> books, BookParameters bookParameters)
        {
            if (!books.Any())
                return;

            if (!string.IsNullOrEmpty(bookParameters.SearchTerm))
            {
                books = books.Where(book =>
                    book.Author.Contains(bookParameters.SearchTerm.ToLower()) ||
                    book.Name.Contains(bookParameters.SearchTerm.ToLower()));
            }
        }

        private void ApplyFilter(ref IQueryable<Book> books, BookParameters bookParameters)
        {
            if (!books.Any())
                return;

            // Filter by Category
            if (bookParameters.Category_Id != null)
            {
                books = books.Where(book =>
                    book.Category.Id.Equals(bookParameters.Category_Id));
            }

            // Filter by Author
            if (!string.IsNullOrEmpty(bookParameters.Author))
            {
                books = books.Where(book => string.Equals(book.Author, bookParameters.Author.Trim()));
            }
        }

        public Book GetBookById(Guid id)
        {
            var book = FindByCondition(book => book.Id.Equals(id))
                .Include(book => book.Category)
                .FirstOrDefault();

            if (book == null)
            {
                // catch the exception in controller
                throw new KeyNotFoundException($"Book with id: {id} not found.");
            }

            return book;
        }

        public void CreateBook(Book book)
        {
            Create(book);
        }

        public void UpdateBook(Book book)
        {
            Update(book);
        }

        public void DeleteBook(Book book)
        {
            Delete(book);
        }
    }
}