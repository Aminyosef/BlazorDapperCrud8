using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorDapperCrud8.Data;
using Microsoft.Extensions.Configuration;

namespace BlazorDapperCrud8.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration _configuration;
        public CategoryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> RemoveDetails(Details detail)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var parameter = new DynamicParameters();
                parameter.Add("id", detail.Id, DbType.Int32);
                const string Query = "DELETE FROM Details WHERE Id=@id";
                await conn.ExecuteAsync(Query, parameter);
            }
            return true;
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
        //add details
        public async Task AddDetail(Details detail)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var paramter = new DynamicParameters();
                paramter.Add("Description", detail.Description, DbType.String);
                paramter.Add("Date", detail.Date, DbType.DateTime2);
                paramter.Add("Amount", detail.Amount, DbType.Decimal);
                paramter.Add("CategoryId", detail.CategoryId, DbType.Int32);
                const string Query = @"INSERT INTO Details(Description, Date, Amount, CategoryId) VALUES(@Description, @Date, @Amount, @CategoryId)";
                await conn.ExecuteAsync(Query, paramter);
            }
        }
        public async Task<bool> UpdateCategory(Category category)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var paramter = new DynamicParameters();
                paramter.Add("Name", category.Name, DbType.String);
                paramter.Add("id", category.Id, DbType.Int32);

                const string Query = @"Update Categories set Name=@Name where Id=@id";
                await conn.ExecuteAsync(Query, paramter);
            }
            return true;
        }
        public async Task<bool> DeleteCategory(Category category)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var parameter = new DynamicParameters();
                parameter.Add("id", category.Id, DbType.Int32);
                const string Query = "DELETE FROM Categories WHERE Id=@id";
                await conn.ExecuteAsync(Query, parameter);
            }
            return true;
        }
        public async Task<bool> UpdateDetails(Details detail)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var parameter = new DynamicParameters();
                parameter.Add("id", detail.Id, DbType.Int32);
                parameter.Add("date", detail.Date, DbType.DateTime2);
                parameter.Add("amt", detail.Amount, DbType.Decimal);
                parameter.Add("desc", detail.Description, DbType.String);
                parameter.Add("catId", detail.CategoryId, DbType.Int32);
                await conn.ExecuteAsync("UPDATE Details SET Description=@desc,Date=@date,Amount=@amt,CategoryId=@catId WHERE Id=@id", parameter);

            }
            return true;

        }

        public async Task<IEnumerable<Category>> categoryList()
        {
            IEnumerable<Category> categories;

            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                categories = await conn.QueryAsync<Category>("SELECT * FROM Categories", commandType: CommandType.Text);
            }
            return categories;
        }
        //update table

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
