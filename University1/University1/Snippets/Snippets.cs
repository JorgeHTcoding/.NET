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
    }

       
}



    
