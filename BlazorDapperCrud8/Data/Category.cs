using System.ComponentModel.DataAnnotations;

namespace BlazorDapperCrud8.Data
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Details> details { get; set;}
    }
}
