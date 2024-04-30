using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorDapperCrud8.Data;

namespace BlazorDapperCrud8.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration _configuration;
        public CategoryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> AddCategory(Category category)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var paramter = new DynamicParameters();
                paramter.Add("Name", category.Name, DbType.String);
                const string Query = @"INSERT INTO Categories(Name) VALUES(@Name)";
                await conn.ExecuteAsync(Query, new { category.Name }, commandType: CommandType.Text);
            }
            return true;
        }
        public async Task<IEnumerable<Category>> categoryList()
        {
            IEnumerable<Category> categories;

            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                categories = await conn.QueryAsync<Category>("SELECT * FROM Categories",commandType:CommandType.Text);
            }
            return categories;
        }
        ///details
        public List<Details> detalList()
        {
            List<Details> categories;

            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                categories = new(conn.Query<Details>("SELECT * FROM Details", commandType: CommandType.Text));
            }
            return categories;
        }
    }
}
