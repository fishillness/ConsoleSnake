using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    // Параметры еды
    static int food_x;
    static int food_y;

    // Параметры змейки
    static int snakeLen = 3;
    static int[] body_x = new int[100];
    static int[] body_y = new int[100];

    static void SpawnFood()
    {
        Random rnd = new Random();

        food_x = rnd.Next(5, 115);
        if (food_x % 2 != 0) food_x += 1;
        food_y = rnd.Next(5, 35);
        
        // Проверка совпадают ли координаты еды с координатами змеи
        for (int i = 0; i < snakeLen; i++)
        {
            if (food_x == body_x[i] && food_y == body_y[i])
            {
                SpawnFood();
                break;
            }
        }
    }

    static void Main(string[] args)
    {
        // Параметры программы 
        Console.SetWindowSize(120, 40);
        Console.SetBufferSize(120, 40);
        Console.CursorVisible = false;

        int score = 0;
        int record = 0;
        bool isWin = false;
        bool isLose = false;
        

        // Параметры змейки
        int head_x = 20;
        int head_y = 10;
        int dir = 0;

        // Стартовое значение змейки
        for (int i = 0; i < snakeLen; i++)
        {
            body_x[i] = head_x - (i * 2);
            body_y[i] = 10;
        }

        // Стартовое значение еды
        SpawnFood();

        // Игровой цикл
        while (true)
        {
            // 1. Очистка
            for (int i = 0; i < snakeLen; i++)
            {
                Console.SetCursorPosition(body_x[i], body_y[i]);
                Console.Write("  ");
            }

            Console.SetCursorPosition(head_x, head_y);
            Console.Write("  ");

            Console.SetCursorPosition(food_x, food_y);
            Console.Write("  ");

            Console.SetCursorPosition(0, 0);
            Console.Write("                                    ");

            if (isLose == false && isWin == false)
            {
                // 2. Расчет

                // Движение змейки
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo key;
                    Console.SetCursorPosition(0, 0);
                    key = Console.ReadKey();
                    Console.SetCursorPosition(0, 0);
                    Console.Write(" ");

                    if ((key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow) && dir != 2) dir = 0;
                    if ((key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow) && dir != 3) dir = 1;
                    if ((key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow) && dir != 0) dir = 2;
                    if ((key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow) && dir != 1) dir = 3;

                }

                if (dir == 0) head_x += 2;
                if (dir == 1) head_y += 1;
                if (dir == 2) head_x -= 2;
                if (dir == 3) head_y -= 1;

                if (head_x < 0) head_x = 118;
                if (head_x > 118) head_x = 0;
                if (head_y < 0) head_y = 39;
                if (head_y > 39) head_y = 0;


                for (int i = snakeLen; i > 0; i--)
                {
                    body_x[i] = body_x[i - 1];
                    body_y[i] = body_y[i - 1];
                }
                body_x[0] = head_x;
                body_y[0] = head_y;

                for (int i = 1; i < snakeLen; i++)
                {
                    if (body_x[i] == head_x && body_y[i] == head_y)
                    {
                        isLose = true;
                    }
                }

                // Бесконечное поле

                // Еда
                if (head_x == food_x && head_y == food_y)
                {
                    SpawnFood();
                    snakeLen++;                                
                    score++;
                    if (record < score)
                        record = score;
                    if (snakeLen == 100)
                        isWin = true;
                }

            }
            if (isLose == true || isWin == true)
            {
                if (isLose == true)
                {
                    Console.SetCursorPosition(40, 20);
                    Console.Write("Ты проиграл. Нажми \"R\", чтобы начать заново.");
                }
                else
                //Я понимаю, что игра должна быть бесконечной, но я хотела избежать ошибки, когда длина змеи станет больше максимальной длины массива
                {
                    Console.SetCursorPosition(40, 20);
                    Console.Write("Ты выиграл! Нажми \"R\", чтобы начать заново.");
                }

                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo key;
                    Console.SetCursorPosition(0, 0);
                    key = Console.ReadKey();
                    Console.SetCursorPosition(0, 0);
                    Console.Write(" ");
                    if (key.Key == ConsoleKey.R == true)
                    {
                        isLose = false;
                        isWin = false;
                        score = 0;
                        snakeLen = 3;

                        Console.SetCursorPosition(40, 20);
                        Console.Write("                                            ");

                        SpawnFood();
                    }
                }

            }

            // 3. Отрисовка
            for (int i = 0; i < snakeLen; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(body_x[i], body_y[i]);
                Console.Write("██");
            }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(head_x, head_y);
            Console.Write("██");


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(food_x, food_y);
            Console.Write("██");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.Write($"Score: {score}. Record: {record}");

            // 4. Ожидание
            System.Threading.Thread.Sleep(55);
        }
       
        Console.ReadLine();
    }
}

