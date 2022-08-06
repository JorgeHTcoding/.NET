using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            // 1. SELECT * of cars (All cars)
            var carList = from car in cars select car;

            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            //2. SELECT WHERE car as Audi (audis)

            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }
        }
        // Number examples

        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Each number multiplied by 3
            //take all numbers, but 9
            //order numbers by ascending value

            var processedNumberList = numbers.Select(num => num * 3).Where(num => num != 9).OrderBy(num => num);

        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "b",
                "c",
                "d",
                "ce",
                "f",
                "g",
            };

            //1. First of all elements
            var first = textList.First();
            //2. First elemente that is "c"
            var cText = textList.First(text => text.Equals("c"));
            //3. First element that contains "j"
            var jText = textList.First(text => text.Contains("j"));
            //4. First element that contains Z or default
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z")); // returns "" or element that contains "z"
            //5. Last element that contains Z or default
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z")); // returns "" or element that contains "z"
            //6. Single values
            var uniqueTexts = textList.Single();
            var uniqueOrDefaultTexts = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            //Obtain {4,8}

            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers); // { 4,8 } las compara y elimina los valores repetidos

        }
        static public void MultipleSelects()
        {
            // Select many
            string[] myOpinions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3",
            };

            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));
            //Nos va a seleccionar todos los valores spliteados en la coma

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise1",
                    Employees = new[] {


                        new Employee
                        {
                            Id = 1,
                            Name = "Martin",
                            Email = "loquesa@gmail.com",
                            Salary = 3000,
                        },

                        new Employee
                        {
                            Id = 2,
                            Name = "Pepe",
                            Email = "loquesa2@gmail.com",
                            Salary = 1000,
                        },

                        new Employee
                        {
                            Id = 3,
                            Name = "Juano",
                            Email = "loquesa3@gmail.com",
                            Salary = 2000,
                        },

                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise2",
                    Employees = new[] {


                        new Employee
                        {
                            Id = 1,
                            Name = "Martina",
                            Email = "loquesa@gmail.com",
                            Salary = 3000,
                        },

                        new Employee
                        {
                            Id = 2,
                            Name = "Pepa",
                            Email = "loquesa2@gmail.com",
                            Salary = 1000,
                        },

                        new Employee
                        {
                            Id = 3,
                            Name = "Juana",
                            Email = "loquesa3@gmail.com",
                            Salary = 2000,
                        }
                    }
                }
            };
            // Obtain all Employees of al Enterprises

            var employeelist = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Know if any list is empty (nos devuelve un boolean si contiene elementos y un false si no)

            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // All enterprises have at least an employee who earns more than 1000€

            bool hasEmployeeWithSalaryMoreThan1000 = enterprises.Any(enterprise => enterprise.Employees.Any(employee => employee.Salary >= 1000));
        }
        static public void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            // Inner Join
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                secondList, element => element, secondElement,
                (element, secondElement) => new { element, secondElement }
                );

            // OUTER LEFT JOIN 

            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };
            // OUTER RIGHT JOIN

            var rightOuterJoin = from secondElement in firstList
                                 join element in secondList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            // Union

            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }
        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };


            //SKIP

            var skipTwoFirstValues = myList.Skip(2); // {3,4,5,6,7,8,9,10}

            var skipLastTwoValues = myList.SkipLast(2); //  { 1,2,3,4,5,6,7,8}

            var skipWhileSmallerThanFour = myList.SkipWhile(num => num < 4); // {5,6,7,8} it skips al numbers smaller than 4

            //TAKE

            var takeFirstTwoValues = myList.Take(2); // {1,2}

            var takeLastTwoValues = myList.TakeLast(2); // {9,10}

            var takeWhileSmallerThanFour = myList.TakeWhile(num => num < 4); // {1,2,3} it takes al numbers smaller than 4



        }
        // Paging with Skip & Take
        public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }

        // Variables
        static public void linqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboutAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average
                               select number;

            Console.WriteLine("Average {0}", numbers.Average());
            foreach (int number in aboutAverage)
            {
                Console.WriteLine("Query: {0} {1}", number, Math.Pow(number, 2));
            }
        }
        //ZIP Intercala un array con otro sin tener que ser del mismo tipo de dato

        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + " = " + word);
            // prints {"1 = one" , " 2 = two ", ...}
        }

        //REPEAT & RANGE generate a value collection 

        static public void repeatRangeLinq()
        {
            var first1000 = Enumerable.Range(1, 1000);

            // var aboveAverage = from number in first1000
            //                    select number;

            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5); //prints {"X","X","X","X","X"}
        }
        static public void studentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Martín",
                    Grade = 90,
                    Certified = true
                },  new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false
                }, new Student
                {
                    Id = 3,
                    Name = "Ana",
                    Grade = 96,
                    Certified = true
                },  new Student
                {
                    Id = 4,
                    Name = "Álvaro",
                    Grade = 10,
                    Certified = false
                },  new Student
                {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true
                }
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;
            var notCertifiedStudents = from student in classRoom
                                       where student.Certified == false
                                       select student;
            var passingStudents = from student in classRoom
                                  where student.Grade >= 40 && student.Certified == true
                                  select student.Grade;

            // Another example
            var passQuery = classRoom.GroupBy(student => student.Certified && student.Grade >= 50);

            foreach (var group in passQuery)
            {
                Console.WriteLine("------ {0}  {1} ------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }

            //we obtain two groups 
            // 1 - not certifed students
            // 2 - certified students
        }
        //ALL
        static public void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };

            bool allAreSmallerThan10 = numbers.All(x => x < 10);
            bool allAreSmallerOrEqualThan2 = numbers.All(x => x >= 2);

            var emptyList = new List<int>();

            bool allNumberAreGreaterThan0 = numbers.All(x => x >= 0);
            //true por defecto cuando una lista está vacia nos díra por defecto
            //cumplent todos el > 0 y lo que hace el ALL es parar la iteración cuando encuentra un valor que no cumple con la condición
            //a diferencia con el any que para cuando una cumple el all tienen que cumplir todos los valores

        }
        //Aggregate funciona como una secuencia acumulativa de funciones (una detrás de otra)
        //realizando operaciones cuya previa salida es la entrade de la siguiente operación

        static public void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Sum all numbers
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);
            // hace el proceso de -> 0,1 => 1 --- 1,2 =>3 --- 3,4 => 7 --- etc

            string[] words = { "hello,", "my", "name", "is", "SlimShady" };//Hello my name is slimShady
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current); //hace lo mismo que el otro ejemplo

        }
        //Distinct - devuelve los valores que son distintos

        static public void distinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 4, 3, 2, 9 };

            IEnumerable<int> distinctValues = numbers.Distinct();
        }
        //GroupBy

        static public void groupByExamples()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Obtain only even numbers and generate 2 groups
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            // We will have two groups:
            // 1. The group that doesn't fit the condition (odd numbors)
            // 2. The group that fits the condition (even numbers)

            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value); // 1,3,5,7,9... 2,4,6,8 (first the odds and then the even)
                }
            }
        }

        static public void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My content"

                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My second comment",
                            Content = "My other content"

                        }
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "My other comment",
                            Content = "My new content"

                        },
                        new Comment()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title = "My other comment",
                            Content = "My other new content"

                        }
                    }
                }
            };
        }
    }
}







