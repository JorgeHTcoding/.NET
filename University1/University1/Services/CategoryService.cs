using University1.Models.DataModels;

namespace University1.Services
{
   
    public class CategoryService : ICategoryServices
    {
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(ILogger<CategoryService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Category> GetCourseByCategory()
        {
            _logger.LogWarning($"{nameof(CategoryService)} - {nameof(GetCourseByCategory)} Warning Level Log");
            _logger.LogError($"{nameof(CategoryService)} - {nameof(GetCourseByCategory)} Error Level Log");
            _logger.LogCritical($"{nameof(CategoryService)} - {nameof(GetCourseByCategory)} Critical Level Log");
            var courseCategory = new List<Course>();
            IEnumerable<Course> courseByCategory = (IEnumerable<Course>)courseCategory.Select(category => category.Nombre == "Deluxe");

            return (IEnumerable<Category>)courseByCategory;
        }
    }
}
