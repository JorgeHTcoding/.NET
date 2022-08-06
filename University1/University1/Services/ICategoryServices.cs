using University1.Models.DataModels;

namespace University1.Services
{
    public interface ICategoryServices
    {
        IEnumerable<Category> GetCourseByCategory();
    }
}
