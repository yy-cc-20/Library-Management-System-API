using Entities.Models;

namespace Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(Guid id);
    }
}