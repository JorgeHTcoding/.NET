using University1.Models.DataModels;

namespace University1.Services
{
    public interface ICourseServices
    {
       
        IEnumerable<Course> GetCourseNoSyllabus();
        IEnumerable<Course> GetCourseSyllabus();
        IEnumerable<Course> GetCourseStudents();


    }
}
