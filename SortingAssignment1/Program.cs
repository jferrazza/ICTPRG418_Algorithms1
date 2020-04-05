using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SortingAssignment1
{
    class Program
    {

        #region Main
        public static void Main(string[] args)
        {

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine();
            }
            _.PrintLogo();


            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Press ENTER to continue, or insert args (testing only)...");
                    var arg = Console.ReadLine().Trim().ToLower();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    var defaultTests = " insertion shell analysis"; // the tests to run by default or on mode arg
                    var allTests = "test insertion shell linear linear2 binary analysis noexit testanalysis analysis2".Split(' '); // all args to syntax-check the args field
                    if (arg == "" || arg == "test" || arg == "noexit") arg += defaultTests;
                    arg = arg.Trim();
                    var nums = new int[] { 575154, 182339, 17132, 773788, 296934, 991395, 303270, 45231, 580, 629822 };

                    var argList = arg.Split(' ');

                    if (!ListContainsList(argList, allTests, out string discrep)) throw new Exception($"Unknown argument '{discrep}'.");

                    if (argList.Contains("test"))
                    {

                        Console.WriteLine("Type an ID 1 to 4 or type 0 for default data...");
                        Console.WriteLine("ID will be reset to 0 on done.");
                        var b = int.Parse(Console.ReadLine());
                        ChangeData(b);

                    }
                    else
                    {
                        ChangeData(0);
                    }
                    if (argList.Contains("testanalysis"))
                    {
                        // nums existence-testing macro
                        foreach (var item in nums)
                        {
                            LinearSearch(lines.ToList(), out TimeSpan time, item, false);
                        }
                    }

                    //Important stuff here
                    if (argList.Contains("insertion"))
                    {
                        InsertionSort();                                          //Insertion sort
                    }
                    if (argList.Contains("shell"))
                    {
                        ShellSort();                                              //Shell sort
                    }
                    if (argList.Contains("linear"))
                    {
                        //Test only
                        LinearSearch(linesSorted, out TimeSpan time);            //Test linear search
                    }
                    if (argList.Contains("linear2"))
                    {
                        //Test only
                        LinearSearch(lines, out TimeSpan time);                  //Test linear search general
                    }
                    if (argList.Contains("binary"))
                    {
                        //Test only
                        BinarySearch(out TimeSpan time);                         //Test binary search
                    }

                    if (argList.Contains("olinear"))
                    {
                        //Test only
                        var o = BigO_For.LinearSearch(linesSorted.Count());       //Test linear Big-O function

                    }
                    if (argList.Contains("obinary"))
                    {
                        //Test only
                        var o = BigO_For.BinarySearch(linesSorted.Count());       //Test linear Big-O function
                    }

                    if (argList.Contains("analysis"))
                    {
                        Analysis(nums);

                    }
                    if (argList.Contains("analysis2"))
                    {
                        //Extra only
                        Analysis(linesSorted.ToArray());

                    }
                    //

                    if (!argList.Contains("noexit"))
                    {
                        Environment.Exit(0);
                    }

                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

            }



        }

        private static void Analysis(int[] nums)
        {
            List<string> str = new List<string>();

            List<TimeSpan> timesToLinearSearch = new List<TimeSpan>();
            List<TimeSpan> timesToBinarySearch = new List<TimeSpan>();

            Console.WriteLine("Please wait while the searches are underway... Will NOT be dumped!");

            //Find times taken
            foreach (var item in nums)
            {
                LinearSearch(linesSorted, out TimeSpan time, item, false);
                timesToLinearSearch.Add(time);
            }
            Thread.Sleep(1000);

            foreach (var item in nums)
            {
                BinarySearch(out TimeSpan time, item, false);
                timesToBinarySearch.Add(time);

            }
            Thread.Sleep(1000);

            //Find average
            long averageLinearSearchTicks = 0;
            foreach (var item in timesToLinearSearch)
            {
                averageLinearSearchTicks += item.Ticks;
            }
            averageLinearSearchTicks /= timesToLinearSearch.Count;
            TimeSpan averageLinearSearch = new TimeSpan(averageLinearSearchTicks);
            long averageBinarySearchTicks = 0;
            foreach (var item in timesToBinarySearch)
            {
                averageBinarySearchTicks += item.Ticks;
            }
            averageBinarySearchTicks /= timesToLinearSearch.Count;
            TimeSpan averageBinarySearch = new TimeSpan(averageBinarySearchTicks);


            Console.WriteLine();
            Console.WriteLine();
            str.Add("Numbers searched are as follows:");
            foreach (var item in nums)
            {
                str.Add(item.ToString());
            }
            str.Add("Times taken to Linear Seach are as follows:");
            foreach (var item in timesToLinearSearch)
            {
                str.Add(item.ToString());
            }
            str.Add("");
            str.Add($"Average time is {averageLinearSearch}");
            str.Add("");

            str.Add("Times taken to Binary Seach are as follows:");
            foreach (var item in timesToBinarySearch)
            {
                str.Add(item.ToString());
            }
            str.Add("");
            str.Add($"Average time is {averageBinarySearch}");
            str.Add("");
            str.Add("");
            str.Add("Big O notation for linear search returns:");
            str.Add(BigO_For.LinearSearch(linesSorted.Count()).ToString());
            str.Add("");
            str.Add("Big O notation for binary search returns:");
            str.Add(BigO_For.BinarySearch(linesSorted.Count()).ToString());
            str.Add("");
            str.Add("");
            str.Add("The quotient (division) between the two average search times respectively returns: (|)");
            var divide1 = (double)averageLinearSearch.Ticks / (double)averageBinarySearch.Ticks;
            str.Add(divide1.ToString());
            str.Add("");
            str.Add("The quotient of the Big O functions respectively returns: (|)");
            var divide2 = BigO_For.LinearSearch(linesSorted.Count()) / BigO_For.BinarySearch(linesSorted.Count());
            str.Add(divide2.ToString());
            str.Add("");
            str.Add("");
            str.Add("The quotient of the linear time vs notation: (--)");
            var divide3 = averageLinearSearch.Ticks / BigO_For.LinearSearch(linesSorted.Count());
            str.Add(divide3.ToString());
            str.Add("");
            str.Add("The quotient of the binary time vs notation: (--)");
            var divide4 = averageBinarySearch.Ticks / BigO_For.BinarySearch(linesSorted.Count());
            str.Add(divide4.ToString());
            str.Add("");
            str.Add("Log10 of the last four respective numbers:");
            var log1 = (int)Math.Log(divide1);
            var log2 = (int)Math.Log(divide2);
            var log3 = (int)Math.Log(divide3);
            var log4 = (int)Math.Log(divide4);
            str.Add("Search time to search time - " + log1.ToString());
            str.Add("Big O to Big O - " + log2.ToString());
            if (log1 == log2) str.Add("PROPORTIONAL!!!");
            else str.Add("---- NOT PROPORTIONAL!!!----");
            str.Add("Search time to Big O L - " + log3.ToString());
            str.Add("Search time to Big O B - " + log4.ToString());
            if (log3 == log4) str.Add("PROPORTIONAL!!!");
            else str.Add("---- NOT PROPORTIONAL!!!----");
            if ((log3 == log4) || (log1 == log2)) str.Add("PASS!!!");
            else str.Add("----FAIL!!!----");

            WriteLines(str, "output_analysis.txt");
            WriteLines2(str);
            Console.WriteLine("Done *");
            Console.ReadKey(true);
        }

        private static int _ChangeData = -1;
        /// <summary>
        /// Change to actual or test data, where 0 is the actual data.
        /// </summary>
        /// <param name="f"></param>
        private static void ChangeData(int f)
        {
            if (_ChangeData == f) return;
            _ChangeData = f;

            if (f == 0)
            {
                ReadLines();

            }
            else if (f == 1)
            {
                lines = new int[] { 5, 3, 1, 2, 4 }.ToList();
            }
            else if (f == 2)
            {
                lines = new int[] { 5, 3, 1, 2, 4, 3, 1, 4, 2, 5 }.ToList();
            }
            else if (f == 3)
            {
                lines = new int[] { 6, 2, 9, 8, 3, 1, 7, 4, 10, 5 }.ToList();
            }
            else if (f == 4)
            {
                lines = new int[] { 15, 14, 5, 4, 12, 20, 16, 3, 10, 1, 7, 6, 17, 8, 11, 19, 9, 13, 2, 18 }.ToList();
            }
            else throw new ArgumentException("Invalid index " + f + ".");
            Console.WriteLine("List changed to a " + ListName(lines));
        }
        private static bool ListContainsList(string[] v1, string[] v2, out string discrep)
        {
            discrep = "";
            foreach (var item in v1)
            {
                if (!v2.Contains(item))
                {
                    discrep = item;
                    return false;
                }
            }
            return true;
        }

        #endregion
        //
        //
        //
        //
        #region Sorts
        /// <summary>
        /// Do an insertion sort
        /// </summary>
        private static void InsertionSort()
        {
            List<int> lines2 = new List<int>();
            for (int i = 0; i < lines.Count; i++)
            {
                lines2.Add(lines[i]);
            }
            // based off https://www.w3resource.com/csharp-exercises/searching-and-sorting-algorithm/searching-and-sorting-algorithm-exercise-6.php
            Console.WriteLine($"Doing Insertion Sort with {ListName(lines2)} ...");
            Console.WriteLine("Please Wait...");
            var sw = new Stopwatch();
            sw.Start();
            for (int i1 = 0; i1 < lines2.Count - 1; i1++)
            {
                for (int i2 = i1 + 1; i2 > 0; i2--)
                {

                    if (lines2[i2 - 1] > lines2[i2])
                    {
                        var temp = lines2[i2 - 1];
                        lines2[i2 - 1] = lines2[i2];
                        lines2[i2] = temp;



                    }
                }


            }
            Console.WriteLine("Done Insertion Sort ...");



            linesSorted = lines2;

            WriteLines(lines2, "output_insert.csv");
            sw.Stop();
            Console.WriteLine($"{new TimeSpan(sw.ElapsedTicks).TotalSeconds} seconds");
        }
        /// <summary>
        /// Do a shell sort
        /// </summary>
        private static void ShellSort()
        {
            List<int> lines2 = new List<int>();
            for (int i = 0; i < lines.Count; i++)
            {
                lines2.Add(lines[i]);
            }
            // based off https://www.w3resource.com/csharp-exercises/searching-and-sorting-algorithm/searching-and-sorting-algorithm-exercise-1.php
            Console.WriteLine($"Doing Shell Sort with {ListName(lines2)} ...");
            Console.WriteLine("Please Wait...");
            var sw = new Stopwatch();
            sw.Start();
            int i1, i2, inc, temp;
            inc = 3;

            while (inc > 0)
            {
                for (i1 = 0; i1 < lines2.Count; i1++)
                {
                    i2 = i1;
                    temp = lines2[i1];
                    while (i2 >= inc && lines2[i2 - inc] > temp)
                    {
                        lines2[i2] = lines2[i2 - inc];
                        i2 -= inc;
                    }
                    lines2[i2] = temp;
                }
                if (inc / 2 != 0)
                {
                    inc /= 2;
                }
                else if (inc == 1)
                {
                    inc = 0;
                }
                else
                {
                    inc = 1;
                }


            }
            Console.WriteLine("Done Shell Sort...");


            linesSorted = lines2;

            WriteLines(lines2, "output_shell.csv");
            sw.Stop();
            Console.WriteLine($"{new TimeSpan(sw.ElapsedTicks).TotalSeconds} seconds");

        }
        /// <summary>
        /// Does a linear search.
        /// </summary>
        /// <param name="list">List to search. Specify a sorted or unsorted list.</param>
        /// <param name="num">Item to search for. Type -1 for prompt instead of hard code.</param>
        /// <param name="getfile">Specify whether to stream to a file or just within app.</param>
        private static void LinearSearch(List<int> list, out TimeSpan time, int num = -1, bool getfile = true)
        {
            if (list.Count() == 0)
            {
                throw new ArgumentException("List is empty. Make sure the list has been edited before the method was called.");
            }
            if (num == -1)
            {
                Console.WriteLine($"Type a number to search for...");
                num = int.Parse(Console.ReadLine());

            }
            Console.WriteLine($"Doing Linear Search with {ListName(list)} ...");
            Console.WriteLine("Please Wait...");
            Console.WriteLine($"Searching for '{num}'");

            var sw = new Stopwatch();
            sw.Start();
            List<string> str = new List<string>();
            str.Add($"Done Linear Search with {ListName(list)} ...");
            str.Add($"Results are as follows:---");
            var found = false;
            var i = 1;
            foreach (var item in list)
            {
                if (item == num)
                {
                    str.Add($"Found '{num}' at line {i}!!");
                    found = true;
                }
                i++;
            }
            if (!found) str.Add($"----COULD NOT FIND {num}----");

            str.Add($"Done.---");




            sw.Stop();
            if (getfile)
            {
                WriteLines(str, num + ".output_linearsearch.text");

            }
            WriteLines2(str);
            Console.WriteLine($"{new TimeSpan(sw.ElapsedTicks).TotalSeconds} seconds");
            time = new TimeSpan(sw.ElapsedTicks);
        }
        /// <summary>
        /// Does a linear search. Must be the ordered list.
        /// </summary>
        /// <param name="num">Item to search for. Type -1 for prompt instead of hard code.</param>
        /// <param name="getfile">Specify whether to stream to a file or just within app.</param>

        private static void BinarySearch(out TimeSpan time, int num = -1, bool getfile = true)
        {
            var list = linesSorted;
            //errors
            if (list.Count() == 0)
            {
                throw new ArgumentException("List is empty. Make sure the list has been edited before the method was called.");
            }
            //var e1 = 0;
            //var e2 = 0;
            //foreach (var item1 in list)
            //{
            //    foreach (var item2 in list)
            //    {
            //        if (item1 == item2 && e1 != e2)
            //        {
            //            throw new ArgumentException($"All items of that list must be unique. Please use a different array.\nFound {item1}\nExpected #{e1}\nGot #{e2}");
            //        }
            //        e2++;
            //    }
            //    e1++;
            //    e2 = 0;
            //}
            // end errors

            if (num == -1)
            {
                Console.WriteLine($"Type a number to search for...");
                num = int.Parse(Console.ReadLine());

            }
            Console.WriteLine($"Doing Linear Search with {ListName(list)} ...");
            Console.WriteLine("Please Wait...");
            Console.WriteLine($"Searching for '{num}'");

            var sw = new Stopwatch();
            sw.Start();
            List<string> str = new List<string>();
            str.Add($"Done Linear Search with {ListName(list)} ...");
            str.Add($"Result is as follows:---");

            var markMin = 0;
            var markMax = list.Count() - 1;
            var markMiddle = 0;
            var markLength = list.Count();
            while (markLength >= 1)
            {
                markLength = markMax - markMin;
                markMiddle = (markMax + markMin) / 2;
                str.Add($"Got marker {markMiddle} by beginning at {markMin}, with selection length {markLength}...");

                if (list[markMiddle] < num)
                {
                    str.Add($"Gone right because {list[markMiddle]} < {num} ...");

                    markMin = markMiddle;
                }
                else
                if (list[markMiddle] > num)
                {
                    str.Add($"Gone left because {list[markMiddle]} > {num} ...");

                    markMax = markMiddle;

                }
                else break;

            }
            sw.Stop();

            str.Add($"Stopped either because ran out of cells to cut or middle item matched.");

            str.Add($"=====Result=====");

            if (list[markMiddle] == num)
                str.Add($"!!!Found instance of '{num}' at line {markMiddle} of last selection!!!");
            else
                str.Add($"----COULD NOT FIND {num}----");
            str.Add($"Finished with marker {markMiddle} by beginning at {markMin}, with selection length {markLength}.");


            str.Add($"Done.---");





            WriteLines2(str);
            if (getfile)
                WriteLines(str, num + ".output_linearsearch.text");
            Console.WriteLine($"{new TimeSpan(sw.ElapsedTicks).TotalSeconds} seconds");
            time = new TimeSpan(sw.ElapsedTicks);

        }

        static class BigO_For
        {
            public static double LinearSearch(int length)
            {

                return length;
            }
            public static double BinarySearch(int length)
            {

                return (double)(length * Math.Log(length));
            }
        }

        #endregion
        //
        //
        //
        //
        #region Read/Write
        public static List<int> lines = new List<int>();
        public static List<int> linesSorted = new List<int>();

        /// <summary>
        /// Return a name for the data
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListName(List<int> list)
        {
            return $"list  with {list.Count} items including {{{list[0]}, {list[1]}, {list[2]}...}}";
        }
        /// <summary>
        /// Output the results
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="filename"></param>
        public static void WriteLines(List<int> lines, string filename)
        {

            //placeholder code
            var d = new StreamWriter(filename);
            foreach (var item in lines)
            {
                d.WriteLine(item);
            }
            d.Close();
            Process.Start("NOTEPAD", filename);

        }
        /// <summary>
        /// Output the results
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="filename"></param>
        public static void WriteLines(List<string> lines, string filename)
        {

            //placeholder code
            var d = new StreamWriter(filename);
            foreach (var item in lines)
            {
                d.WriteLine(item);
            }
            d.Close();
            Process.Start("NOTEPAD", filename);

        }
        /// <summary>
        /// Output the lines in-app
        /// </summary>
        /// <param name="lines"></param>
        public static void WriteLines2(List<int> lines)
        {
            //placeholder code
            foreach (var item in lines)
            {
                Console.WriteLine(item);
            }

        }        /// <summary>
                 /// Output the lines in-app
                 /// </summary>
                 /// <param name="lines"></param>
        public static void WriteLines2(List<string> lines)
        {
            //placeholder code
            foreach (var item in lines)
            {
                if (item.EndsWith("!!"))
                    Console.ForegroundColor = ConsoleColor.White;
                else if (item.StartsWith("----"))
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine(item);
            }

        }
        /// <summary>
        /// Input the data. Use with ChangeData().
        /// </summary>
        public static void ReadLines()
        {


            Console.WriteLine($"|><| Loading numbers...");

            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-a-text-file-one-line-at-a-time

            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            System.IO.StringReader file =
                new StringReader(Properties.Resources.unsorted_numbers);

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(int.Parse(line));
                counter++;


            }


            Console.WriteLine($":) Got the {counter} numbers.");
            file.Close();
        }
        #endregion
    }
}
