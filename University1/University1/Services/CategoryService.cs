using University1.Models.DataModels;

namespace University1.Services
{
    public class CategoryService : ICategoryServices
    {
        public IEnumerable<Category> GetCourseByCategory()
        {
            var courseCategory = new List<Course>();
            IEnumerable<Course> courseByCategory = (IEnumerable<Course>)courseCategory.Select(category => category.Nombre == "Deluxe");

            return (IEnumerable<Category>)courseByCategory;
        }
    }
}
