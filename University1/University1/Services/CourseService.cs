using University1.Models.DataModels;

namespace University1.Services
{
    public class CourseService : ICourseServices
    {
        public IEnumerable<Course> GetCourseNoSyllabus()
        {
            var courseNoSyllabus = new List<Course>();

            var courseWithNoSyllabus = from course in courseNoSyllabus where course.Chapter == null select course;
            return (IEnumerable<Course>)courseWithNoSyllabus;
        }

        public IEnumerable<Course> GetCourseSyllabus()
        {
            var courseSyllabus = new List<Course>();

            var courseSyllabusChapter = from course in courseSyllabus where course.Nombre == "C# Avanzado" select course.Chapter;
            return (IEnumerable<Course>)courseSyllabusChapter;
        }

        public IEnumerable<Course> GetCourseStudents()
        {
           var courseList = new List<Course>();

            var courseStudents = from course in courseList where course.Nombre == "C# Avanzado" select course.Students;
            return (IEnumerable<Course>)courseStudents;

            

            
        }
       
    }
}
