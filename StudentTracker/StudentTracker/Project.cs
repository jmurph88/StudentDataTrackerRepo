//Jennifer Murphy
//December 6, 2019
//C# Class

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace StudentTracker
{
    class Program
    {
        // Creating StudentData list to be accessed by all functions in class
        public static List<StudentData> StudentDataList = new List<StudentData>();

        // Loading the Data from the File(Input)
        static void LoadData(string pFilePath)
        {
            StudentData s;
            string inline;
            string[] values;

            try
            {
                if (File.Exists(pFilePath))
                {
                    using (StreamReader sr = new StreamReader(pFilePath))
                    {
                        while ((inline = sr.ReadLine()) != null)
                        {
                            values = inline.Split(',');
                            //s = StudentData.FromData(inline);
                            s = new StudentData(values[0], values[1], values[2], values[3], values[4], values[5], values[6]); //index was outside bounds of array?
                            StudentDataList.Add(s);
                        }
                    }

                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            char choice;
            string name;
            bool isDone = false;

            // The following declaration will build a string of your current directory with the CSV filename
            string filePath = Path.Combine(Environment.CurrentDirectory, "StudentDataCSVFile.csv");

            try
            {
                // Loads data from csv file and readies if for processing
                LoadData(filePath);

                // While user does not QUIT, continually show the menu of options available
                while (!isDone)
                {
                    // Displays Menu for user
                    choice = StudentDataFunc.DisplayMenu();

                    if (choice == 'F') // Option: FIND
                    {
                        // Search List of Students
                        Console.WriteLine(" ");
                        Console.WriteLine("Please Enter \r\n [F] to search by student's first name, \r\n [L] to search by student's last or \r\n [T] to search teacher name:");
                        name = Console.ReadLine().ToUpper();

                        if (name == "F")
                        {
                            searchByFirstName();
                            break;
                        }

                        else if (name == "L")
                        {
                            searchByLastName();
                            break;
                        }

                        else if (name == "T")
                        {
                            searchByTeacherName();
                            break;
                        }

                        else
                        {
                            Console.WriteLine("No matching records found.");
                        }
                        Console.WriteLine(" ");
                    }
                    else if (choice == 'A') // Option: ADD
                    {
                        // Get data from the user to add to the student list
                        Console.WriteLine(" ");
                        Console.WriteLine("Enter first name of student to be added:");
                        string FNameInput = Console.ReadLine();

                        Console.WriteLine("Enter last name of student to be added:");
                        string LNameInput = Console.ReadLine();

                        Console.WriteLine("Enter the last name of the student's teacher:");
                        string TeacherNameInput = Console.ReadLine();

                        Console.WriteLine("Enter Quiz 1 score:");
                        int Quiz1Input = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter Quiz 2 score:");
                        int Quiz2Input = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter Quiz 3 score:");
                        int Quiz3Input = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter Quiz 4 score:");
                        int Quiz4Input = Convert.ToInt32(Console.ReadLine());

                        StudentDataList.Add(new StudentData(FNameInput, LNameInput, TeacherNameInput, Quiz1Input.ToString(),
                                        Quiz2Input.ToString(), Quiz3Input.ToString(), Quiz4Input.ToString()));

                        Console.WriteLine(" ");
                        Console.WriteLine("Your data has been added.");
                        Console.WriteLine(" ");
                    }

                    else if (choice == 'D') // Option: DELETE
                    {
                        DeleteStudent();
                        Console.WriteLine(" ");
                    }

                    else if (choice == 'P') // Option: PRINT
                    {
                        // Print the student data to the screen
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Student     \tTeacher\tQz 1\tQz 2\tQz 3\tQz 4");
                        Console.WriteLine("--------    \t-------\t----\t----\t----\t----");
                        foreach (StudentData item in StudentDataList)
                        {
                            Console.WriteLine(item.LName + ", " +
                                item.FName + "\t" +
                                item.TeacherName + "\t" +
                                item.Quiz1 + "\t" +
                                item.Quiz2 + "\t" +
                                item.Quiz3 + "\t" +
                                item.Quiz4);
                        }
                        Console.WriteLine();
                    }

                    else if (choice == 'Q') // Option: QUIT
                    {
                        isDone = true;
                    }
                }

                // Save the student data and then set the done flag to exit the while loop
                SaveData(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Your input is invalid.", innerException: ex);
            }
        }

        //////FileRead(): This method will pull the CSV file, reads it and returns all of the content in the file.
        ////public static List<StudentData> FileRead(StudentData item)
        ////{
        ////    List<StudentData> StudentDataList = new List<StudentData>();
        ////    string[] Line;

        ////    using (var reader = new StreamReader(@"C:\\Users\\jmrig\\source\\repos\\StudentTracker\\StudentTracker\\StudentDataCSVFile.csv"))
        ////    {
        ////        string dataRead;

        ////        while ((dataRead = reader.ReadLine()) != null)
        ////        {
        ////            Line = dataRead.Split(',');

        ////            // Setting value of items in list
        ////            var listItem = new StudentData();
        ////            listItem.FName = Line[0];
        ////            listItem.LName = Line[1];
        ////            listItem.TeacherName = Line[2];
        ////            listItem.Quiz1 = Convert.ToInt32(Line[3]); //input string not in correct format
        ////            listItem.Quiz2 = Int32.Parse(Line[4].ToString());
        ////            listItem.Quiz3 = Int32.Parse(Line[5]);
        ////            listItem.Quiz4 = Int32.Parse(Line[6]);

        ////            StudentDataList.Add(listItem);
        ////        }
        ////    }
        ////    return StudentDataList;
        ////}


        //Print List(What does this function do vs.the one above?)
        //public static void FileWrite(List<StudentData> StudentDataList)
        //{
        //    using (StreamWriter sr = new StreamWriter(@"C:\\Users\\jmrig\\source\\repos\\StudentTracker\\StudentTracker\\StudentDataCSVFile.csv"))
        //    {
        //        foreach (StudentData s in StudentDataList)
        //        {
        //            sr.WriteLine(s.FName + "," + s.LName + "," + s.TeacherName + "," + s.Quiz1.ToString() + "," + s.Quiz2.ToString() + "," +
        //                         s.Quiz3.ToString() + "," + s.Quiz4.ToString());
        //        }

        //        sr.Close();
        //    }
        //}

        // Save the student data to the file
        public static void SaveData(string pFilePath)
        {
            using (StreamWriter sw = new StreamWriter(pFilePath, false))
            {
                foreach (StudentData s in StudentDataList)
                {
                    sw.WriteLine(s.ToData());
                }
            }
        }

        //Searches student data List by Student First Name
        public static void searchByFirstName()
        {

            Console.WriteLine("Enter first name of student:");
            string input = Console.ReadLine();
            foreach (StudentData result in StudentDataList.Where(s => s.FName.Contains(input)))
            {
                Console.WriteLine(result);
            }
        }

        //Searches student data list by student last name
        public static void searchByLastName()
        {
            Console.WriteLine("Enter last name of student:");
            string input = Console.ReadLine();
            foreach (StudentData result in StudentDataList.Where(s => s.LName.Contains(input)))
            {
                Console.WriteLine(result);
            }
        }

        //Searches student data list by teacher name
        public static void searchByTeacherName()
        {
            Console.WriteLine("Enter teacher name:");
            string input = Console.ReadLine();
            foreach (StudentData result in StudentDataList.Where(s => s.TeacherName.Contains(input)))
            {
                Console.WriteLine(result);
            }
        }

        //Deleting a student from the student tracker or returns to main menu
        public static void DeleteStudent()
        {
            Console.WriteLine("Enter the first name of the student you wish to delete:");
            string deleteInput = Console.ReadLine();
            Console.WriteLine("You are deleting all instances of" + deleteInput + ". Is this correct? [Y]es or [N]o");
            char answerInput = Console.ReadKey().KeyChar;
            if (answerInput == 'Y')
            {
                foreach (StudentData deleteResult in StudentDataList.Where(s => s.FName.Contains(deleteInput)))
                {
                    StudentDataList.Remove(deleteResult);
                }

                Console.WriteLine("You have succesfully deleted a student.");

            }
            else if (answerInput == 'N')
            {
                StudentDataFunc.DisplayMenu();
            }
        }

    }
}



