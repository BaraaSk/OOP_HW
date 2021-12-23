using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOP_HW
{

    class Tester
    {
        static List<Student> students = new List<Student>();
        static List<Subject> subjects = new List<Subject>();
        static List<Subject_Student> subject_Student = new List<Subject_Student>();
        static List<Teacher> teachers = new List<Teacher>();
        

        static void Main(string[] args)
        {

            /*  //Regular_Student x = new Regular_Student { Grade = 3};
              x.calculateFees();
             
              Console.WriteLine(students[0].ID);
              Console.WriteLine(students[1].ID);
              Console.WriteLine(students[2].ID);
              PritScholarshipStudents();*/
            populateData();
            
            MainMenue();
            Console.ReadKey();

        }

        //Application Interface
        static void MainMenue()
        {
            Console.Clear();
            ConsoleKey input;
            Print("Welcom To School Manegment System", "g");
            Print("1- Print the number of registered students.\n2- Print the number of students who get scholarship.\n3- Print the Passed Syllabuses for a student.\n4- Print top 10 students in second grade.\n5- Register a new student.\n6- Add a new teacher.\n7- Print a teacher salary.\n8- Exit.");
            input = Console.ReadKey(true).Key;
            switch (input)
            {
                case ConsoleKey.D1:
                    PrintCountOfRegisterdStudents();
                    break;
                case ConsoleKey.D2:
                    PrintCountOfScholarshipStudents();
                    break;
                case ConsoleKey.D3:
                    PrintPassedSyllabuses();
                    break;
                case ConsoleKey.D4:
                    PrintTopTen(); 
                    break;
                case ConsoleKey.D5:
                    AddNewStudent();
                    break;
                case ConsoleKey.D6:
                    AddNewTeacher();
                    break;
                case ConsoleKey.D7:
                    PrintSalary();
                    break;
                case ConsoleKey.D8:
                    Environment.Exit(0);
                    break;
                default:
                    Print("Wrong choice", "r");
                    MainMenue();
                    break;

            }
        }

        //Method to print the count of registered students
        static void PrintCountOfRegisterdStudents()
        {
            Console.Clear();
            Print(String.Concat("The number of Registered Students: ", students.Count().ToString()), "b");
            ReturnToMain();


        }
       //Method to print the count of scholarship students
        static void PrintCountOfScholarshipStudents()
        {
            Console.Clear();
            Print(String.Concat("the number of scholarship students:", students.Count(a => a is ScholarshipStudent).ToString()), "b");
            ReturnToMain();

        }
        //Print the passed Syllabuses for a certain student
        static void PrintPassedSyllabuses()
        {
            Console.Clear();
            string fName; string lName;int std_id; string output;
            Print("Enter Student's first name:","b");
            fName = Console.ReadLine();
            Print("Enter Student's Last name:","b");
            lName = Console.ReadLine();
            try
            {
                std_id = students.Where(c => c.fName == fName && c.lName == lName).Select(s => s.ID).First();
                output = String.Join(",", subject_Student.Where(ss => ss.std_ID == std_id && ss.isPassed).Select(s => subjects.First(subj => subj.ID == s.Subject_ID).Name));
                Print(output, "b");
            }
            catch (Exception e)
            {
                Print("The name is incorrect","r");
            }
            ReturnToMain();


        }
        //print the top ten students in the second grade
        static void PrintTopTen()
        {
            Console.Clear();
            //linq method to get the top ten students in second grade and order them by their Avarage
            var top = students.Where(s => s.Grade == 2)
                .OrderByDescending(s => s.CalcAverage(subject_Student))
                .Select(ss => new { ss.fName, ss.lName, avg = ss.CalcAverage(subject_Student),/*number of subjects for every student*/ subjects = subject_Student.Count(a=> a.std_ID == ss.ID) })
                .Take(10).ToList();
            //check if the number of subjects is not the same for all students
            if ((top.Select(c => c.subjects).Distinct().ToList()).Count() > 1)
            {
                Print("The registered Subjects are not the same for all students", "r");
                ReturnToMain();
            }
;            foreach (var x in top)
            {
                
                Print(String.Format("{0} {1}: {2}",x.fName,x.lName, x.avg),"g");
            }
            ReturnToMain();
        }
        //Add a new student
        static void AddNewStudent()
        {
            Console.Clear();
            string fName, lName; DateTime BirthDate; int Grade; ConsoleKey key;

            Print("Enter first Name","b");
            fName = checkName();
            Print("Enter last Name:","b");
            lName = checkName();
            Print("Enter Birth Date: (dd-mm-yyyy)","b");

            //loop until a proper value is given
            while(! DateTime.TryParse(Console.ReadLine(), out BirthDate))
            {
                Print("incorrect date format", "r");
                Print("Enter Birth Date: (dd-mm-yyyy)","b");                
            }
            Print("Enter student's Grade:","b");
            //loop until a proper value is given
            while (!int.TryParse(Console.ReadLine(), out Grade))
            {
                Print("Please enter a proper value","r");
                Print("Enter student's Grade:","g");               
            }
            Print("Select studnt's type:\n1- Regular.\n2- Scholarship.","b");
            while (true)
            {
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.D1)
                {
                    students.Add(new Regular_Student { ID = students.Count + 1, fName = fName, lName = lName, birthDate = BirthDate, Grade = Grade }); ;
                    break;
                }
                else if (key == ConsoleKey.D2)
                {
                    students.Add(new ScholarshipStudent { ID = students.Count + 1,fName = fName, lName = lName, birthDate = BirthDate, Grade = Grade });
                    break;
                }
                Print("Please enter a proper value","r");
                Print("Select studnt's type:\n1- Regular.\n2- Scholarship.","b");
            }
            Print(string.Format("Student {0} with ID: {1} was added",fName,students.Count),"g");
            ReturnToMain();
        }
        // Add a new Teacher 
        static void AddNewTeacher()
        {
            Console.Clear();
            string fName, lName; decimal Salary;
            Print("Enter first name:","b");
            fName = checkName();
            Print("Enter last Name","b");
            lName = checkName();
            Print("Enter Type:\n1- Houerly Paid.\n2- Monthly Paid.","b");
            while (true)
            {
                ConsoleKey input = Console.ReadKey(true).Key;
                if (input == ConsoleKey.D1)
                {
                    teachers.Add(new Houerly_Paid_Teacher { ID = teachers.Count+1, fName = fName, lName = lName});
                    break;

                }
                else if(input == ConsoleKey.D2)
                {
                    Print("Enter the base salary:","b");
                    while(! decimal.TryParse(Console.ReadLine(),out Salary))
                    {
                        Print("Please Enter a proper value","r");
                        Print("Enter the base salary:", "b");
                    }
                    teachers.Add(new Monthly_Paid_Teacher { ID = teachers.Count + 1, fName = fName,lName = lName, base_Salary = Salary }) ; 
                    break;
                }
                Print("Wrong Choice","r");
                Print("Enter Type:\n1- Houerly Paid.\n2- Monthly Paid.", "b");
            }
            Print(string.Format("Added teacher {0} {1}\nID: {2}",fName,lName,teachers.Count),"g") ;
            ReturnToMain();


        }

        //print the salary for a certain teacher
        static void PrintSalary()
        {
            string fName, lName; int teacher_ID; decimal salary;

            Console.Clear();

            Print("Enter teacher's first Name","b");
            fName = Console.ReadLine();
            Print("Enter Last Name", "b");
            lName = Console.ReadLine();
            try
            {
                var tech = teachers.First(t=> t.fName == fName && t.lName == lName);
                Print(tech.calcSalary().ToString(),"g");
                
            }
            catch(Exception e)
            {
                Print("Wrong Name","r");
                Console.ReadKey();
                AddNewTeacher();
            }
            ReturnToMain();
        }
        //get input from user and validate it
        static string checkName()
        {
            string Name;
            Name = Console.ReadLine();
            //check the input the name must contain only alphabets and at least three letters
            while (true)
            {
                if (Name.Length < 3)
                {
                    Print("Name must be at least 3 letters long", "r");
                    Name = Console.ReadLine();
                }
                //regular expresion to assure that input value contains only alphabets
                if (!Regex.IsMatch(Name, @"^[a-zA-Z]+$"))
                {
                    Print("Only Alphabets are accepted", "r");
                    Name = Console.ReadLine();
                }
                if (Regex.IsMatch(Name, @"^[a-zA-Z]+$") && Name.Length >= 3) break;

            }
            return Name;
        }
        static void ReturnToMain()
        {
            Print("Press any Key to return to MainMenue...."); Console.ReadKey(); MainMenue();
        }
        static void populateData()
        {
            //Add students to list
            students.Add(new Regular_Student {ID = students.Count+1 , fName = "baraa", lName = "skheta", birthDate = DateTime.Parse("2001-01-01"), RegisterationDate = DateTime.Parse("2008-09-01"),Grade = 3}) ;
            students.Add(new Regular_Student { ID = students.Count + 1, fName = "c", lName = "", birthDate = DateTime.Parse("2001-01-01"), RegisterationDate = DateTime.Parse("2008-09-01"), Grade = 2 });
            students.Add(new Regular_Student { ID = students.Count + 1, fName = "c", lName = "", birthDate = DateTime.Parse("2001-01-01"), RegisterationDate = DateTime.Parse("2008-09-01"), Grade = 2 });
            students.Add(new Regular_Student { ID = students.Count + 1, fName = "c", lName = "", birthDate = DateTime.Parse("2001-01-01"), RegisterationDate = DateTime.Parse("2008-09-01"), Grade = 2});
            students.Add(new ScholarshipStudent { ID = students.Count+1, fName = "a", lName = "", birthDate = DateTime.Parse("2001-01-01"), RegisterationDate = DateTime.Parse("2008-09-01"), Grade = 2 });
            students.Add(new ScholarshipStudent { ID = students.Count + 1, fName = "b", lName = "", birthDate = DateTime.Parse("2001-01-01"), RegisterationDate = DateTime.Parse("2008-09-01"), Grade = 3 });
            students.Add(new ScholarshipStudent { ID = students.Count + 1, fName = "c", lName = "", birthDate = DateTime.Parse("2001-01-01"), RegisterationDate = DateTime.Parse("2008-09-01"), Grade = 3 });

            //Add Teachers to List
            teachers.Add(new Monthly_Paid_Teacher { ID = teachers.Count+1,fName = "Zain", lName="Kadi",base_Salary = 500  }) ;
            teachers.Add(new Houerly_Paid_Teacher { ID = teachers.Count + 1, fName = "Mohammad", lName = "Ganem", WorksHouers = 160});
            teachers.Add(new Monthly_Paid_Teacher { ID = teachers.Count + 1, fName = "Ziad", lName = "Makki", base_Salary = 700, Additional_Work_Houers = 20 });
            teachers.Add(new Houerly_Paid_Teacher { ID = teachers.Count + 1, fName = "Zaki", lName = "chan", WorksHouers =120 });
            teachers.Add(new Monthly_Paid_Teacher { ID = teachers.Count + 1, fName = "Fatima", lName = "Morgan", base_Salary = 800, Additional_Work_Houers = 10 });

            //Add subjects to List
            subjects.Add(new Subject {ID = subjects.Count + 1, Name = "Math101", Teacher_ID = 1}); subjects.Add(new Subject { ID = subjects.Count + 1, Name = "Math102", Teacher_ID = 1 }); subjects.Add(new Subject { ID = subjects.Count + 1, Name = "Math201", Teacher_ID = 3 }); subjects.Add(new Subject { ID = subjects.Count + 1, Name = "Math202" , Teacher_ID = 3}); subjects.Add(new Subject { ID = subjects.Count, Name = "Math203", Teacher_ID = 3 });
            subjects.Add(new Subject { ID = subjects.Count + 1, Name = "Chemistry101", Teacher_ID = 2 }); subjects.Add(new Subject { ID = subjects.Count+1, Name = "Chemistry201", Teacher_ID = 2 }); subjects.Add(new Subject { ID = subjects.Count + 1, Name = "Chemistry301", Teacher_ID = 2 });
            subjects.Add(new Subject { ID = subjects.Count + 1, Name = "Physics101", Teacher_ID = 4 }); subjects.Add(new Subject { ID = subjects.Count+1, Name = "Physics102", Teacher_ID = 4 }); subjects.Add(new Subject { ID = subjects.Count, Name = "Physics201", Teacher_ID = 4 }); subjects.Add(new Subject { ID = subjects.Count, Name = "Physics202", Teacher_ID = 4 });
            subjects.Add(new Subject { ID = subjects.Count + 1, Name = "Biology101", Teacher_ID = 5 });

            //Add data to the break relation list
                //data for student 1
                    subject_Student.Add(new Subject_Student {ID = subject_Student.Count+1,std_ID =1, Subject_ID = 1, isPassed = true , Mark =75});
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 1, Subject_ID = 6, isPassed = true, Mark = 80});
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 1, Subject_ID = 9, isPassed = true, Mark = 65});
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 1, Subject_ID = 13, isPassed = false, Mark = 40 });
                //data for student 2
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 2, Subject_ID = 2, isPassed = true, Mark = 60 });
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 2, Subject_ID = 2, isPassed = true, Mark = 70  });
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 2, Subject_ID = 2, isPassed = true, Mark = 60 });
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 2, Subject_ID = 2, isPassed = true, Mark = 60 });
                //data for student 3
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 3, Subject_ID = 2, isPassed = true, Mark = 60 });
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 3, Subject_ID = 2, isPassed = true, Mark = 60 });
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 3, Subject_ID = 2, isPassed = true, Mark = 60 });
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 3, Subject_ID = 2, isPassed = true, Mark = 60 });
            //data for student 4
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 4, Subject_ID = 2, isPassed = true, Mark = 60 });
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 4, Subject_ID = 2, isPassed = true, Mark = 60 });
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 4, Subject_ID = 2, isPassed = true, Mark = 60 });
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 4, Subject_ID = 2, isPassed = true, Mark = 60 });
            //data for student 5
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 5, Subject_ID = 2, isPassed = true, Mark = 60 });
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 5, Subject_ID = 2, isPassed = true, Mark = 60 });
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 5, Subject_ID = 2, isPassed = true, Mark = 60 });
                    subject_Student.Add(new Subject_Student { ID = subject_Student.Count + 1, std_ID = 5, Subject_ID = 2, isPassed = true, Mark = 60 });
        }
        static void Print(string Message, string Color = "")
        {
            switch (Color)
            {
                case "r":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "g":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "b":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }
            Console.WriteLine(Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
