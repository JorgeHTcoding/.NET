using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using University1.Models.DataModels;

namespace University1.Services
{
    public class Services
    {
        static public void practiceLinq()
        {
            var userList = new List<User>();
            var studentList = new List<Student>();
            var courseList = new List<Course>();
            var categoryList = new List<Category>();
            var levelCategoryList = new List<Course>();

            var usersByEmail = from user in userList where user.Email != null select user;

            var studentsOfAge = from student in studentList where student.Dob.Year < 2004 select student;

            var studentsCourse = from student in studentList where student.Courses.Count > 0 select student;

            var courseLevelWithAssinstence = from courses in courseList
                                             where courses.Nivel == Nivel.Básico &&
                                             courses.Students.Count >= 1
                                             select courses;

            var courseLevelFromCategory = courseList.Select(course => course.Nivel == Nivel.Avanzado
                                          && course.Categories.Any(category => category.Nombre == "Deluxe"));


            var emptyCourse = courseList.Select(course => course.Students.Any() == false);



        }
    }
}
