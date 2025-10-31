using System;

namespace TicTacToeApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Крестики‑нолики!");
            Console.WriteLine("1 — играть против компьютера");
            Console.WriteLine("2 — играть против другого игрока");

            int mode = 0;
            while (mode != 1 && mode != 2)
            {
                Console.Write("Выберите режим (1 или 2): ");
                if (int.TryParse(Console.ReadLine(), out mode) && (mode == 1 || mode == 2))
                    break;
                Console.WriteLine("Введите 1 или 2.");
            }

            PlayGame(mode);
            Console.WriteLine("Игра окончена.");
        }

        static void PlayGame(int mode)
        {
            char[,] board = new char[3, 3];
            InitializeBoard(board);
            char currentPlayer = GetFirstPlayer();
            bool gameOver = false;

            while (!gameOver)
            {
                DisplayBoard(board);
                Console.WriteLine($"Ход игрока: {currentPlayer}");

                if (currentPlayer == 'X')
                {
                    if (mode == 1)
                        GetUserMove(board);        // Ход пользователя (против компа)
                    else
                        GetPlayerMove(board, 'X'); // Ход первого игрока (PvP)
                }
                else // currentPlayer == 'O'
                {
                    if (mode == 1)
                        GetComputerMove(board);  // Ход компьютера
                    else
                        GetPlayerMove(board, 'O'); // Ход второго игрока (PvP)
                }

                if (CheckWinner(board, currentPlayer))
                {
                    DisplayBoard(board);
                    Console.WriteLine($"Игрок {currentPlayer} победил!");
                    gameOver = true;
                }
                else if (IsBoardFull(board))
                {
                    DisplayBoard(board);
                    Console.WriteLine("Ничья!");
                    gameOver = true;
                }
                else
                {
                    currentPlayer = SwitchPlayer(currentPlayer);
                }
            }
        }

        static void InitializeBoard(char[,] board)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = ' ';
        }

        static char GetFirstPlayer()
        {
            Random random = new Random();
            return random.Next(2) == 0 ? 'X' : 'O';
        }

        static void DisplayBoard(char[,] board)
        {
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                Console.Write(" ");
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                        Console.Write(i * 3 + j + 1);
                    else
                        Console.Write(board[i, j]);
                    if (j < 2) Console.Write(" | ");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("-----------");
            }
        }

        static void GetUserMove(char[,] board)
        {
            int pos;
            bool valid = false;

            while (!valid)
            {
                Console.Write("Ваш ход (1–9): ");
                if (!int.TryParse(Console.ReadLine(), out pos) || pos < 1 || pos > 9)
                {
                    Console.WriteLine("Введите число от 1 до 9.");
                    continue;
                }

                int row = (pos - 1) / 3;
                int col = (pos - 1) % 3;

                if (board[row, col] == ' ')
                {
                    board[row, col] = 'X';
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Клетка занята! Попробуйте ещё раз.");
                }
            }
        }

        static void GetComputerMove(char[,] board)
        {
            Random random = new Random();
            int row, col;

            do
            {
                row = random.Next(3);
                col = random.Next(3);
            } while (board[row, col] != ' ');

            board[row, col] = 'O';
            Console.WriteLine($"Компьютер поставил O в позицию {row * 3 + col + 1}");
        }

        static void GetPlayerMove(char[,] board, char player)
        {
            int pos;
            bool valid = false;

            while (!valid)
            {
                Console.Write($"Ход игрока {player} (1–9): ");
                if (!int.TryParse(Console.ReadLine(), out pos) || pos < 1 || pos > 9)
                {
                    Console.WriteLine("Введите число от 1 до 9.");
                    continue;
                }

                int row = (pos - 1) / 3;
                int col = (pos - 1) % 3;

                if (board[row, col] == ' ')
                {
                    board[row, col] = player;
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Клетка занята! Попробуйте ещё раз.");
                }
            }
        }

        static bool CheckWinner(char[,] board, char player)
        {
            // Строки
            for (int i = 0; i < 3; i++)
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                    return true;

            // Столбцы
            for (int j = 0; j < 3; j++)
                if (board[0, j] == player && board[1, j] == player && board[2, j] == player)
                    return true;

            // Диагонали
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) return true;
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player) return true;

            return false;
        }

        static bool IsBoardFull(char[,] board)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == ' ') return false;
            return true;
        }

        static char SwitchPlayer(char player)
        {
            return player == 'X' ? 'O' : 'X';
        }
    }
}
