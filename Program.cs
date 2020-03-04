using System;
using System.Collections.Generic;
using System.IO;

namespace MockPracticalExam
{
    class Program
    {
        const char UNDERGRADUATE = 'U';
        const char GRADUATE = 'G';
        const char ALL_STUDENTS = 'A';
        const char ALL_STUDENTS_NAME = 'N';
        const char DELIMITER = ',';
        const string FILE_NAME = "student.txt";
        static string[] menu =
        {
            "Add student",
            "Remove student",
            "Display undergraduate data",
            "Display graduate data",
            "Display all student data",
            "Save student data to a file",
            "Exit"
        };

        static List<Student> students;
        
        static void Main(string[] args)
        {
            students = new List<Student>();

            try
            {
                Read();
            }
            catch(IOException ioe)
            {
                Console.WriteLine($"Error: {ioe.Message}");
            }

            string input;
            while(true)
            {
                DisplayMenu(menu);
                Console.Write("Select one of the options: ");
                input = Console.ReadLine();
                Console.WriteLine();
                switch(input)
                {
                    case "1":
                        try
                        {
                             AddStudent();
                        }
                        catch(InvalidInputException iie)
                        {
                            Console.WriteLine(iie.Message);
                        }
                       
                        break;
                    case "2":
                        try
                        {
                             RemoveStudent();
                        }
                        catch(InvalidInputException iie)
                        {
                            Console.WriteLine(iie.Message);
                        }
                        break;
                    case "3":
                        DisplayData(UNDERGRADUATE);
                        break;
                    case "4":
                        DisplayData(GRADUATE);
                        break;
                    case "5":
                        DisplayData(ALL_STUDENTS);
                        break;
                    case "6":
                        try
                        {
                            Save();
                        }
                        catch(IOException ioe)
                        {
                            Console.WriteLine($"Error: {ioe.Message}");
                        }
                        break;
                    case "7":
                        Console.WriteLine("Exiting from the program...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine($"The input, {input}, that you entered is not valid or not in the range");
                        break;
                }
            }
        }

        public static void AddStudent()
        {
            Console.Write("Enter the name of the student: ");
            string name = Console.ReadLine();

            Console.Write($"Enter the degree level ('{UNDERGRADUATE}' for Undergraduate, '{GRADUATE}' for Graduate): ");
            char degreeLevel = Console.ReadLine().ToUpper()[0];

            Console.Write("Enter the abbreviation of the state residency: ");
            string stateResidence = Console.ReadLine();

            Console.Write("Enter the number of credits: ");
            int credits = int.Parse(Console.ReadLine());

            if(degreeLevel == UNDERGRADUATE)
            {
                Console.Write("Enter the amount of the scholarship for the student: ");
                int scholarship = int.Parse(Console.ReadLine());
                
                students.Add(new Undergraduate(degreeLevel, name, stateResidence, credits, scholarship));
            }
            else if(degreeLevel == GRADUATE)
            {
                Console.Write("Enter the name of the alma mater: ");
                string almaMater = Console.ReadLine();
                
                students.Add(new Graduate(degreeLevel, name,stateResidence, credits, almaMater));
            }
            else
            {
                throw new InvalidInputException("Must be either (U)ndergraduate or (G)raduate");
            }
        }

        public static void RemoveStudent()
        {
            DisplayData(ALL_STUDENTS_NAME);
            Console.Write("Select the student to remove: ");
            int selection = int.Parse(Console.ReadLine());

            if(selection <= 0 || selection > students.Count)
                throw new InvalidInputException("Out of range");

            Console.WriteLine($"Removing {students[selection -1].Name}");
            students.RemoveAt(selection - 1);
        }

        public static void DisplayMenu(string[] menu)
        {
            for(int index = 0; index < menu.Length; index++)
            {
                Console.WriteLine($"{index + 1} -- {menu[index]}");
            }
        }

        public static void DisplayData(char type)
        {
            switch(type)
            {
                case UNDERGRADUATE:
                    for(int index = 0; index < students.Count; index++)
                    {
                        if(students[index].Type == UNDERGRADUATE)
                            Console.WriteLine(students[index].ToString());
                    }
                    break;
                case GRADUATE:
                    for(int index = 0; index < students.Count; index++)
                    {
                       if(students[index].Type == GRADUATE)
                            Console.WriteLine(students[index].ToString() + "\n");
                    }
                    break;
                case ALL_STUDENTS:
                    for(int index = 0; index < students.Count; index++)
                    {
                        Console.WriteLine(students[index].ToString() + "\n");
                    }
                    break;
                case ALL_STUDENTS_NAME:
                    for(int index = 0; index < students.Count; index++)
                    {
                        Console.WriteLine($"{index +1}. {students[index].Name}\n");
                    }
                    break;
            }
        }

        public static void Read()
        {
            if(!File.Exists(FILE_NAME))
            {
                Console.WriteLine("File does not exist");
                return;
            }

            using(StreamReader sr = new StreamReader(FILE_NAME))
            {
                // loop through the file and at the same time split
                string[] studentInfo;
                char degreeLevel;
                while(!sr.EndOfStream)
                {
                    studentInfo = sr.ReadLine().Split(DELIMITER);
                    degreeLevel = studentInfo[0].ToUpper()[0];

                    if(degreeLevel == UNDERGRADUATE)
                        students.Add(new Undergraduate(degreeLevel, studentInfo[1], studentInfo[2], 
                            int.Parse(studentInfo[3]), int.Parse(studentInfo[4])));
                    else
                        students.Add(new Graduate(degreeLevel, studentInfo[1], studentInfo[2], 
                            int.Parse(studentInfo[3]), studentInfo[4]));
                }
            }

        }

        public static void Save()
        {

            using(StreamWriter sw = new StreamWriter(FILE_NAME))
            {
                foreach(Student student in students)
                {
                    sw.WriteLine($"{student.Type},{student.Name},{student.StateResidence},{student.Credits},"
                        + $"{(student.Type == UNDERGRADUATE ? $"{((Undergraduate)student).ScholarshipAmount}" : $"{((Graduate)student).AlmaMater}")}");
                }
            }
      
        }
    }
}
