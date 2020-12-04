using Lesson7.Game;
using Lesson7.Game.Equipment.Concrete;
using System;
using System.IO;

namespace Lesson7
{
    public class Program
    {
        static bool flag = true;
        static bool flagPause = true;
        static string[] modes;
        static string[] modesPause;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a name of map:");
            string path = Console.ReadLine();
            RenderMenu(path);
            Console.ReadKey();
        }
        //static char[,] RandomMap()
        //{
        //    var rand = new Random();
        //    for (int i = 0; i < Map.GetLength(0); i++)
        //    {
        //        for (int k = 0; k < Map.GetLength(1); k++)
        //        {
        //            Map[i, k] = new Cell(i, k, rand);
        //        }
        //    }
        //}
        static void DrawModes(string[] modes)
        {
            Console.Clear();
            int x = Console.WindowHeight / 2 - 1;
            int y = Console.WindowWidth / 2;
            for (int i = 0; i < modes.Length; i++)
            {
                Console.SetCursorPosition(y, x++);
                Console.WriteLine(modes[i]);
                Console.WriteLine();
            }
        }
        static void RenderMenu(string path)
        {
            int x = Console.WindowHeight / 2;
            int y = Console.WindowWidth / 2;
            Console.SetCursorPosition(y, x);
            
            if (flag)
            {
                modes = new string[4];
                modes[0] = "Start game<";
                modes[1] = "Developers";
                modes[2] = "Controls";
                modes[3] = "Rules";
                flag = false;
            }
            else
            {
                DrawModes(modes);
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.W)
                {
                    for(int i = 0; i < modes.Length; i++)
                    {
                        if(modes[i].Contains("<") && i - 1 >= 0)
                        {
                            modes[i] = modes[i].TrimEnd('<');
                            modes[i - 1] += '<';
                            break;
                        }
                    }
                    RenderMenu(path);
                }
                else if (key == ConsoleKey.S)
                {
                    for (int i = 0; i < modes.Length; i++)
                    {
                        if (modes[i].Contains("<") && i + 1 < modes.Length)
                        {
                            modes[i] = modes[i].TrimEnd('<');
                            modes[i + 1] += '<';
                            break;
                        }
                    }
                }
                else if (key == ConsoleKey.Enter || key == ConsoleKey.Spacebar)
                {
                    foreach (var line in modes)
                    {
                        if (line.Contains("<"))
                        {
                            if (line == "Start game<")
                            {
                                if (path.Contains(".txt"))
                                {
                                    Start(path);
                                }
                                else Start(path + ".txt");
                                Start(path);
                            }
                            else if(line == "Controls<")
                            {
                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine("W,A,S,D to move, I - inventory, 1 - take/hide a weapon (if you have it in the invontory)");
                                }
                                while (Console.ReadKey().Key != ConsoleKey.Escape);
                                RenderMenu(path);
                            }
                            else if(line == "Developers<")
                            {
                                do
                                { 
                                    Console.Clear();
                                    Console.WriteLine("Hideo Kojima, Hideo Kojima, Hideo Kojima, Hideo Kojima, Hideo Kojima, Hideo Kojima, Hideo Kojima and Hideo Kojima.");
                                }
                                while (Console.ReadKey().Key != ConsoleKey.Escape);
                                RenderMenu(path);
                            }
                            else if(line == "Rules<")
                            {
                                do
                                { 
                                    Console.Clear();
                                    Console.WriteLine("You have to reach F point. You can heal yourself when standing on \"F\". You can avoid enemies or defeat them with your bare hands or with a SWORD (\"W\")");
                                }
                                while (Console.ReadKey().Key != ConsoleKey.Escape);
                                RenderMenu(path);
                            }
                        }
                    }
                }
            }
            RenderMenu(path);
        }
        public static void RenderPause()
        {
            int x = Console.WindowHeight / 2;
            int y = Console.WindowWidth / 2;
            Console.SetCursorPosition(y, x);

            if (flagPause)
            {
                modesPause = new string[4];
                modesPause[0] = "Resume<";
                modesPause[1] = "Developers";
                modesPause[2] = "Controls";
                modesPause[3] = "Rules";
                flagPause = false;
            }
            else
            {
                DrawModes(modesPause);
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.W)
                {
                    for (int i = 0; i < modesPause.Length; i++)
                    {
                        if (modesPause[i].Contains("<") && i - 1 >= 0)
                        {
                            modesPause[i] = modesPause[i].TrimEnd('<');
                            modesPause[i - 1] += '<';
                            break;
                        }
                    }
                    //RenderPause();
                }
                else if (key == ConsoleKey.S)
                {
                    for (int i = 0; i < modesPause.Length; i++)
                    {
                        if (modesPause[i].Contains("<") && i + 1 < modesPause.Length)
                        {
                            modesPause[i] = modesPause[i].TrimEnd('<');
                            modesPause[i + 1] += '<';
                            break;
                        }
                    }
                }
                else if (key == ConsoleKey.Enter || key == ConsoleKey.Spacebar)
                {
                    foreach (var line in modesPause)
                    {
                        if (line.Contains("<"))
                        {
                            if (line == "Resume<")
                            {
                                return;
                            }
                            else if (line == "Controls<")
                            {
                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine("W,A,S,D to move, I - inventory, 1 - take/hide a weapon (if you have it in the invontory)");
                                }
                                while (Console.ReadKey().Key != ConsoleKey.Escape);
                                //RenderPause();
                            }
                            else if (line == "Developers<")
                            {
                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine("Hideo Kojima, Hideo Kojima and Hideo Kojima.");
                                }
                                while (Console.ReadKey().Key != ConsoleKey.Escape);
                                //RenderPause();
                            }
                            else if (line == "Rules<")
                            {
                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine("You have to reach F point. You can heal yourself when standing on \"F\". You can avoid enemies or defeat them with your bare hands or with a SWORD (\"W\")");
                                }
                                while (Console.ReadKey().Key != ConsoleKey.Escape);
                                //RenderPause();
                            }
                        }
                    }
                }
            }
            RenderPause();
        }
        static char[,] LoadMap(string path)
        {
            string[] array = null;
            try
            {
                array = File.ReadAllLines(path);
            }
            catch(Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                array = File.ReadAllLines(path);
            }
            int size_i = array.GetLength(0);
            int size_j = array[0].Length;
            char[,] map = new char[size_i, size_j];
            char[] temp = new char[size_i];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                temp = array[i].ToCharArray();
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = temp[j];
                }
            }
            return map;
        }
        static void Start(string path)
        {
            char[,] map = LoadMap(path);
            Player player = new Player(1, 1, 100, 25);
            Enemy[] enemies = new Enemy[2];
            ICarriable[] items = new ICarriable[2];
            items[0] = new LightSword('S', "Sword");
            items[1] = new LightSword('S', "Sword");
            enemies[0] = new Enemy(4, 1, 100, 15, null);
            enemies[1] = new Enemy(8, 8, 100, 15, null);
            Map level = null;
            player.Set(map, player, enemies, ref level, items);
            level.Move();
        }



        /// <summary>
        /// Use before calling SetPlayerToPosition
        /// </summary>
        /// <param name="mDelta"></param>
        /// <param name="nDelta"></param>
        /// <returns></returns>

        /// <summary>
        /// Only for move actions, Exception if !IsPositionAvailable
        /// </summary>
        /// <param name="mDelta"></param>
        /// <param name="nDelta"></param>



        static void Task()
        {
            int size = 0;
            bool success = false;
            do
            {
                Console.Write("Enter size: ");
                string input = Console.ReadLine();
                if (!double.TryParse(input, out double number))
                {
                    Console.WriteLine("Not a number!");
                    continue;
                }

                if (number <= 0)
                {
                    Console.WriteLine("Size must be more than 0");
                    continue;
                }

                if (number - (int)number > 0)
                {
                    Console.WriteLine("Only integer!");
                    continue;
                }

                success = true;
                size = (int)number;
                Console.WriteLine();
            }
            while (!success);

            Console.WriteLine("Task 1\n");
            int[,] vs = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int k = 0; k < size; k++)
                {
                    vs[i, k] = i >= k ? i + 1 : 0;
                    Console.Write($"{vs[i, k]}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("Task 2\n");
            int[,] vs1 = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int k = 0; k < size; k++)
                {
                    vs[i, k] = i >= k ? (i + 1) * (k + 1) : 0;
                    Console.Write($"{vs[i, k]}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }



        static void Recursion(params int[] array)
        {
            if (array.Length > 1)
            {
                int[] newLine = new int[array.Length - 1];
                for (int i = 0; i < newLine.Length; i++)
                {
                    newLine[i] = array[i] + array[i + 1];
                }
                Recursion(newLine);
                foreach (int item in array)
                {
                    Console.Write($"{item}\t");
                }
                Console.WriteLine();
            }
            else if (array.Length == 1)
            {
                Console.WriteLine($"{array[0]}");
            }
            else
            {
                Console.WriteLine("Empty array");
            }
        }
    }
}
