
using University1.Models.DataModels;

namespace University1.Services
{
    public interface IStudentServices
    {
        IEnumerable<Student> GetStudentCourses();
        IEnumerable<Student> GetStudentsWithCourses();
        IEnumerable<Student> GetStudentsWithNoCourses();
    }
}
