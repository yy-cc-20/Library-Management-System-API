using Contracts;
using Entities;
using Entities.Helper;
using Entities.Models;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IBookRepository _book;
        private ICategoryRepository _category;
        private ISortHelper<Book> _bookSortHelper;
        private ISortHelper<Category> _categorySortHelper;

        public RepositoryWrapper(RepositoryContext repositoryContext,
        ISortHelper<Book> bookSortHelper,
        ISortHelper<Category> categorySortHelper)
        {
            _repoContext = repositoryContext;
            _bookSortHelper = bookSortHelper;
            _categorySortHelper = categorySortHelper;
        }

        public IBookRepository Book
        {
            get
            {
                if (_book == null)
                {
                    _book = new BookRepository(_repoContext, _bookSortHelper);
                }

                return _book;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_repoContext, _categorySortHelper);
                }

                return _category;
            }
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}