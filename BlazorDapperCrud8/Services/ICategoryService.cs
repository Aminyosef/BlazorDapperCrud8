using BlazorDapperCrud8.Data;

namespace BlazorDapperCrud8.Services
{
    public interface ICategoryService
    {
        Task<bool> AddCategory(Category category);
        Task<IEnumerable<Category>> categoryList();
        List<Details> detalList();
    }
}