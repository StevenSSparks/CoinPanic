using System;

namespace CoinPanic
{
    class Program
    {

       
        // Game Helper Methods
        public static string[ , ] SetCoins(int LevelWidth, int LevelHeight, int NumOfCoins, string[,] level)
        {

            Random rn = new();

            for (int c = 0; c < NumOfCoins; c++)
            {
                int coiny = rn.Next(0, LevelWidth - 1);
                int coinx = rn.Next(0, LevelHeight - 1);
                level[coiny, coinx] = "C";
            }

            return level;
        }

        public static int CoinCount(int LevelWidth, int LevelHeight, string[,] level) 
        {
            int c = 0;
            for (int y = 0; y <= LevelWidth - 1; y++)
                for (int x = 0; x <= LevelHeight - 1; x++)
                    if (level[y, x] == "C") c++;

            return c;

        }

        public static void WriteText(ConsoleColor Back, ConsoleColor Text, string text)
        {
            Console.BackgroundColor= Back;
            Console.ForegroundColor= Text;
            Console.Write(text);

        }

        public static void WritelnText(ConsoleColor Back, ConsoleColor Text, string text)
        {
            Console.BackgroundColor = Back;
            Console.ForegroundColor = Text;
            Console.Write(text);
            Console.WriteLine();

        }
    


        static void Main(string[] args)
        {
            Console.Title = "Coin Panic";

            ConsoleColor Back = ConsoleColor.Black;
            ConsoleColor Text = ConsoleColor.Green;

            string choice = "";
            while (choice == "")
            {
                
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                WritelnText(Back, ConsoleColor.Yellow, "Welcome to:");
                Console.WriteLine();
                WritelnText(Back, ConsoleColor.Red, " = = = = > C O I N  P A N I C  < = = = = ");
                Console.WriteLine();
                WriteText(Back, Text, $"Gather as many coins as you can in a set number of moves!");
                Console.WriteLine();
                WritelnText(Back, ConsoleColor.White, "How long of a game do you want to play?");
                Console.WriteLine();
                WritelnText(Back, ConsoleColor.Yellow, "1 = 100 Moves");
                WritelnText(Back, ConsoleColor.Yellow, "2 = 250 Moves");
                WritelnText(Back, ConsoleColor.Yellow, "3 = 500 Moves");
                WritelnText(Back, ConsoleColor.Yellow, "X to Exit");
                Console.WriteLine();
                WriteText(ConsoleColor.Black, ConsoleColor.White, "Type choice and press enter :");
                choice = Console.ReadLine();

              
            }

            int maxMoves = 100;
            if (choice == "2") maxMoves = 250;
            if (choice == "3") maxMoves = 500;
            if (choice.ToLower() == "x") return;


            int playerX = 1;
            int playerY = 1;

           

            int levelWidth = 15; // y
            int levelHeight = 30; // x
           
            int maxCoins = 10;
            int coinCount = 0;

            string[,] level = new string[levelWidth, levelHeight];


            for (int y = 0; y <= levelWidth-1; y++)
                for (int x = 0; x <= levelHeight-1; x++)
                    level[y, x] = ".";

            level = SetCoins(levelWidth, levelHeight,maxCoins,level);
            // Even though we set 10 coints we count them to see if some might have steped on them we randomly generated
            coinCount = CoinCount(levelWidth, levelHeight,level);

            int numCoins = 0;
            int moveCounter = 0;
            string input;


            bool gameOver = false;

            while (!gameOver)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                WritelnText(ConsoleColor.Black, ConsoleColor.Yellow, "Welcome to:");
                Console.WriteLine();
                WritelnText(ConsoleColor.Black, ConsoleColor.Red, " = = = = > C O I N  P A N I C  < = = = = ");
                Console.WriteLine();

                WriteText(Back, Text, $"Gather as many coins as you can in");
                WriteText(Back, ConsoleColor.White, $" {maxMoves} ");
                WritelnText(Back, Text,"moves!");

                WritelnText(Back,ConsoleColor.Blue,  "[W] = Up, [A] = Left, [S] = Right, [D] = Down to move.");
                WritelnText(Back, ConsoleColor.Cyan, "[X] to Exit the game.");
                Console.WriteLine();

                for (int y = 0; y <= levelWidth - 1; y++)
                {
                    for (int x = 0; x <= levelHeight - 1; x++)
                    {
                        if (playerX == x && playerY == y) WriteText(Back, ConsoleColor.Cyan, "P");
                        else
                        {
                           if (level[y,x] == ".") WriteText(Back, ConsoleColor.White, level[y, x]);
                           if (level[y, x] == "C") WriteText(Back, ConsoleColor.DarkYellow, level[y, x]);
                        }
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
                // Print game stats
                WriteText(Back, Text, "Coins Collected :");
                WritelnText(Back,ConsoleColor.DarkYellow,$" {numCoins}");

                WriteText(Back, Text, "Coins Remaining :");
                WritelnText(Back,ConsoleColor.White,$" {CoinCount(levelWidth,levelHeight,level)}");

                WriteText(Back,Text,  "Total Moves     :");
                WritelnText(Back,ConsoleColor.White,$" {moveCounter}");

                // Get player input
                while (!Console.KeyAvailable) { Task.Delay(1); }
                input = Console.ReadKey().Key.ToString().ToLower();
                // input = Console.ReadKey().KeyChar.ToString().ToLower();
                
               

                // Update player position
                if ((input == "w") || (input == "uparrow"))
                {
                    playerY--;
                    if (playerY< 0) playerY = levelWidth-1;
                    moveCounter++;

                }
                else if ((input == "a") || (input == "leftarrow"))
                {
                    playerX--;
                    if (playerX < 0) playerX = levelHeight-1;
                    moveCounter++;
                }
                else if ((input == "s") || (input == "downarrow"))
                {
                    playerY++;
                    if (playerY > levelWidth - 1) playerY = 0;
                    moveCounter++;
                }
                else if ((input == "d") || (input == "rightarrow"))
                {
                    playerX++;
                    if (playerX > levelHeight - 1) playerX = 0;
                    moveCounter++;
                }

                // Check for coin pick-up
                if (level[playerY, playerX].ToString() == "C")
                {
                    numCoins++;
                    level[playerY, playerX] = ".";
                    if (CoinCount(levelWidth, levelHeight, level) == 0) SetCoins(levelWidth, levelHeight,maxCoins,level);
                }

                // Check for game over conditions
                if ((input == "x"))
                {
                    gameOver = true;
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine();
                    WritelnText(ConsoleColor.Black, ConsoleColor.Red, " = = = = > C O I N  P A N I C  < = = = = ");
                    Console.WriteLine();
                    WritelnText(Back,ConsoleColor.Red,"You QUIT the game!");
                    WriteText(Back, Text, "You had");
                    WriteText(Back, ConsoleColor.Red, $" {maxMoves - moveCounter} ");
                    WritelnText(Back,Text,"remaining!");

                }

                if (moveCounter == maxMoves + 1)
                {
                    gameOver = true;
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine();
                    WritelnText(ConsoleColor.Black, ConsoleColor.Red, " = = = = > C O I N  P A N I C  < = = = = ");
                    Console.WriteLine();
                    WriteText(Back, Text, "You collected");
                    WriteText(Back, ConsoleColor.DarkYellow, $" {numCoins} ");
                    WritelnText(Back, Text,"coins!");
                    Console.WriteLine();
                    Console.WriteLine();
                }

            }

            WritelnText(ConsoleColor.Black, ConsoleColor.Red, "GAME OVER! - Thanks for Playing!");
            WritelnText(ConsoleColor.Black, ConsoleColor.Red, "Press X to close the game.");

            input= "";

            while (input.ToLower() != "x")
            {
                while (!Console.KeyAvailable) { Task.Delay(1); }
                input = Console.ReadKey().KeyChar.ToString().ToLower();

            }


        }
    }
}
