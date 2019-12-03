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
        //Creating StudentData list to be accessed by all functions in class
        public static List<StudentData> StudentDataList = new List<StudentData>();
      
       //Loading the Data from the File(Input)
        public List<StudentData> LoadData()
        {
            StudentData s;
            string inline;
            string[] values = new string[7];
            string filePath = @"C:\\Users\\jmrig\\source\\repos\\StudentTracker\\StudentTracker\\StudentDataCSVFile.csv";


            try
            {
                if (File.Exists(filePath))
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        while ((inline = sr.ReadLine()) != null)
                        {
                        
                            values = inline.Split(',');
                            s = new StudentData(values[0], values[1], values[2], values[3], 
                                values[4], values[5], values[6]); //index was outside bounds of array?
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

            return StudentDataList;
        }

        static void Main(string[] args)
        {
            char choice;
            var p = new Program();
            string filePath = @"C:\\Users\\jmrig\\source\\repos\\StudentTracker\\StudentTracker\\StudentDataCSVFile.csv";


            bool isDone = false;

//try
            //{
                while (!isDone)
                {
                //Loads data from csv file
                p.LoadData();

                    //Displays Menu for user
                    choice = StudentDataFunc.DisplayMenu();

                    if (choice == 'F')
                    {
                        //Search List of Students
                        Console.WriteLine(" ");
                        Console.WriteLine("Please Enter \r\n [F] to search by student's first name, \r\n [L] to search by student's last or \r\n [T] to search teacher name:");
                        string name = Console.ReadLine().ToUpper();
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
                        StudentDataFunc.DisplayMenu();

                    }

                    else if (choice == 'A')
                    {
                        //Add Students to File
                        using (FileStream fs = new FileStream(@filePath, FileMode.Append))
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
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

                            string textAdd = (FNameInput + "," + LNameInput + "," + TeacherNameInput + "," + Quiz1Input + "," + Quiz2Input + "," + Quiz3Input + "," + Quiz4Input);
                            sw.WriteLine(filePath, textAdd + Environment.NewLine);
                            Console.ReadLine();

                            StudentDataList.Add(new StudentData(FNameInput, LNameInput, TeacherNameInput, Quiz1Input.ToString(),
                                            Quiz2Input.ToString(), Quiz3Input.ToString(), Quiz4Input.ToString()));
                            //SaveData(ToData());
                        }



                        Console.WriteLine("Your data has been added.");
                        Console.WriteLine(" ");
                        StudentDataFunc.DisplayMenu();
                    }

                    else if (choice == 'D')
                    {
                        DeleteStudent();
                        Console.WriteLine(" ");
                        StudentDataFunc.DisplayMenu();
                    }

                    else if (choice == 'P')
                    {
                        foreach (StudentData item in StudentDataList)
                        {
                            FileRead(item);
                        }
                        StudentDataFunc.DisplayMenu();

                    }

                    else if (choice == 'Q')
                    {
                        isDone = true;
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Your input is invalid.", innerException: ex);
            //}
        }


        //Decalring vairable for user input from menu
        private static char UserInput()
        {
            return Console.ReadKey().KeyChar;
        }


        //FileRead(): This method will pull the CSV file, reads it and returns all of the content in the file.
        public static List<StudentData> FileRead(StudentData item)
        {
            List<StudentData> StudentDataList = new List<StudentData>();

            using (var reader = new StreamReader(@"C:\\Users\\jmrig\\source\\repos\\StudentTracker\\StudentTracker\\StudentDataCSVFile.csv"))
            {
                string dataRead;

                while ((dataRead = reader.ReadLine()) != null)
                {
                    var Line = dataRead.Split(',');

                    //Setting value of items in list
                    var listItem = new StudentData();
                    listItem.FName = Line[0];
                    listItem.LName = Line[1];
                    listItem.TeacherName = Line[2];
                    listItem.Quiz1 = Convert.ToInt32(Line[3]); //input string not in correct format
                    listItem.Quiz2 = Int32.Parse(Line[4].ToString());
                    listItem.Quiz3 = Int32.Parse(Line[5]);
                    listItem.Quiz4 = Int32.Parse(Line[6]);

                    StudentDataList.Add(listItem);
                }

                reader.Close();
            }
            return StudentDataList;
        }


        //Print List (What does this function do vs. the one above?)
        public static void FileWrite(List<StudentData> StudentDataList)
        {

            using (StreamWriter sr = new StreamWriter(@"C:\\Users\\jmrig\\source\\repos\\StudentTracker\\StudentTracker\\StudentDataCSVFile.csv"))
            {
                foreach (StudentData s in StudentDataList)
                {
                    sr.WriteLine(s.FName + "," + s.LName + "," + s.TeacherName + "," + s.Quiz1.ToString() + "," + s.Quiz2.ToString() + "," +
                                 s.Quiz3.ToString() + "," + s.Quiz4.ToString());
                }

                sr.Close();
            }
        }
        //Save FName, LName, School, Quiz1, Quiz2, Quiz3, Quiz4
        public static void SaveData(StudentData item)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\\Users\\jmrig\\source\\repos\\StudentTracker\\StudentTracker\\StudentDataCSVFile.cs"))
            {
                foreach (StudentData s in StudentDataList)
                {
                    sw.WriteLine(s.ToData());
                }
                sw.Close();
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


