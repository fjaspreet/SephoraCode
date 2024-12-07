using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sephoraMsystem
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Category(int categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }

        public static List<Category> GetDefaultCategories()
        {
            return new List<Category>
        {
            new Category(1, "Skincare"),
            new Category(2, "Makeup"),
            new Category(3, "Fragrance"),
            new Category(4, "Hair Care"),
            new Category(5, "Nail Care")
        };
        }

        public override string ToString()
        {
            return CategoryName;
        }
    }
}

