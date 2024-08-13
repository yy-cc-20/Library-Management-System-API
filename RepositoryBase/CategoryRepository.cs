using Contracts;
using Entities;
using Entities.Helper;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private ISortHelper<Category> _sortHelper;

        public CategoryRepository(RepositoryContext repositoryContext, ISortHelper<Category> sortHelper)
            : base(repositoryContext)
        {
            _sortHelper = sortHelper;
        }

        public IEnumerable<Category> GetCategories()
        {
            return FindAll();
        }

        public Category GetCategoryById(Guid id)
        {
            var category = FindByCondition(category => category.Id.Equals(id))
                .FirstOrDefault();

            if (category == null)
            {
                // catch the exception in controller
                throw new KeyNotFoundException($"Category with id: {id} not found.");
            }

            return category;
        }
    }
}