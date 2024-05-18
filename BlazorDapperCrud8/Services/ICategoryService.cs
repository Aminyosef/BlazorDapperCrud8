using BlazorDapperCrud8.Data;

namespace BlazorDapperCrud8.Services
{
    public interface ICategoryService
    {
        Task<bool> AddCategory(Category category);
        Task AddDetail(Details detail);
        Task<IEnumerable<Category>> categoryList();
        Task<bool> DeleteCategory(Category category);
        List<Details> detalList();
        Task<bool> RemoveDetails(Details detail);
        Task<bool> UpdateCategory(Category category);
        Task<bool> UpdateDetails(Details detail);
    }
}