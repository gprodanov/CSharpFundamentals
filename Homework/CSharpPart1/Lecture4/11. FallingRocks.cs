using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.SetWindowSize(60, 21);
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;
        Console.CursorVisible = false;
        Random rnd = new Random();
        string[] screen = new string[18];
        string dwarf = "(0)";
        int counter = 0;
        bool gameOver = false;
        int speed = 15;
        int score = 0;

        for (int i = 0; i < screen.Length; i++) screen[i] = new string(' ', 20);

        //string[] colorNames = ConsoleColor.GetNames(typeof(ConsoleColor));
        char[] rocks = new char[] { '@', '^', '*', '&', '+', '%', '$', '#', '!', '.', ';' };

        int left = 10;

        Console.WriteLine("Use the Left and Right arrow keys to move sideways.\nUse '+' and '-' to adjust game speed.");
        Console.WriteLine("Press \"P\" to pause.");
        Console.WriteLine("\nPress any key to start game.");
        Console.ReadKey();

        while (true)
        {
            Console.Clear();
            if (counter % speed == 0)
            {
                for (int i = 0; i < screen[screen.Length - 2].Length; i++)
                {
                    if (screen[screen.Length - 1][i] != ' ' && (i >= left && i < left + 3))
                    {
                        gameOver = true;
                        break;
                    }
                }
                if (gameOver) break;

                for (int i = screen.Length - 1; i >= 1; i--)
                {
                    screen[i] = screen[i - 1];
                }

                foreach (var item in screen[screen.Length - 1])
                {
                    if (item != ' ') score += 10;
                }

                int indent = rnd.Next(0, 20);
                
                screen[0] = new string(' ', indent) + rocks[rnd.Next(rocks.Length)] + new string(' ', 19 - indent);
            }

            for (int i = 0; i < screen.Length; i++)
            {
                //ConsoleColor color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorNames[rnd.Next(colorNames.Length)]);
                //Console.ForegroundColor = color;
                Console.WriteLine(screen[i]);
            }

            Console.WriteLine(new string(' ', left) + dwarf);

            Console.WriteLine("Score: {0}", score);

            Console.ForegroundColor = ConsoleColor.Black;
            if (Console.KeyAvailable == true)
            {
                ConsoleKeyInfo command = Console.ReadKey();
                if (command.Key == ConsoleKey.LeftArrow)
                {
                    if (left != 0) left = left - 1;
                }
                else if (command.Key == ConsoleKey.RightArrow)
                {
                    if (left != 17) left = left + 1;
                }
                else if (command.Key == ConsoleKey.Add)
                {
                    if (speed > 5) speed -= 5;
                }
                else if (command.Key == ConsoleKey.Subtract)
                {
                    if (speed <= 35) speed += 5;
                }
                else if (command.Key == ConsoleKey.P)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game paused. Press any key to continue...");
                    Console.ResetColor();
                    while (!Console.KeyAvailable) { }
                }
            }

            Console.ResetColor();
            Thread.Sleep(50);
            counter++;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Game Over");
        Console.WriteLine("Your score is {0} points!", score);
        Console.ReadLine();
    }
}