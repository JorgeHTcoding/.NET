using University1.Models.DataModels;

namespace University1.Services
{
    public class StudentService : IStudentServices
    {
       
        public IEnumerable<Student> GetStudentCourses()
        {
            var studentList = new List<Student>();

            var studentCourses = from student in studentList where student.FirstName.Equals("Martín") select student.Courses;

            return (IEnumerable<Student>)studentCourses;

        }

        public IEnumerable<Student> GetStudentsWithCourses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudentsWithNoCourses()
        {
            var studentList = new List<Student>();

            var studentsWithNoCourses = studentList.Select(student => student.Courses.Any() == false);

            return (IEnumerable<Student>)studentsWithNoCourses;
        }
    }
}
