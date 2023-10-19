using System;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Program.Run();
    }
    public static void Run()
    {
        
        //створення гравця №1
        GameAccount Gamer1 = new GameAccount();
        Console.WriteLine("Введіть ім'я Гравця №1: ");
        Gamer1.UserName = Console.ReadLine();
        Gamer1.CurrentRating = 100;
        Gamer1.GamesCount = 0;

        //створення гравця №2
        GameAccount Gamer2 = new GameAccount();
        Console.WriteLine("Введіть ім'я Гравця №2: ");
        Gamer2.UserName = Console.ReadLine();
        Gamer2.CurrentRating = 100;
        Gamer2.GamesCount = 0;

        //вивід новостворенних об'єктів
        Gamer1.OutGamers();
        Gamer2.OutGamers();

        int rating;
        Game gameplay = new Game();//створення об'єкту класу 'Game, для подальшого виклику функції класу Game
        Console.WriteLine("Введіть рейтинг, на який гратимуть:");

        rating = int.Parse(Console.ReadLine());//ввід інтової змінної

        //умова вводу райтингу на кяий грають
        while (rating <= 0 || rating >= 100)
        {
            Console.WriteLine("Невірно введенно рейтинг, введіть ще раз: ");
            rating = int.Parse(Console.ReadLine());
        }
            int i;
            for (i = 0; i < 3; i++)
            {
                gameplay.PlayGame(Gamer1, Gamer2, rating);//виклик об'єкту класу Game, що має в СОБІ ФУНКЦІЮ 
            }

        Console.WriteLine("\nСтатистика 3 раундів:");
        Gamer1.GetStats(Gamer1);
    }
}
//класс 
class GameAccount
{
    public string UserName { get; set; }//нік
    public int CurrentRating { get; set; }//рейтинг гравця
    public int GamesCount { get; set; }//кількість ігор
    public List<GameResult> GameResults { get; set; } = new List<GameResult>();// список, в який записується історія ігр, для виводу історії.
    public int GameIndex { get; set; } = 1;//індекс гри//зробити статичними

    //статистика гравця. Вивід.
    public void OutGamers()
    {
        Console.WriteLine($"\nІм'я: {UserName}\nРейтинг: {CurrentRating} \nІгор зіграно: {GamesCount}\n");
    }
    //функція, що викликається на випадок виграшу гравця
    public void WinGame(int rating)
    {
        GamesCount++;
        CurrentRating += rating;
    }
    //функція, що викликається на випадок програшу гравцяю. ДОДАТКОВО ПЕРЕДИВИТИСЯ
    public void LoseGame(int rating)
    {
        GamesCount++;
        if (CurrentRating > 1)
        {
            CurrentRating -= rating;
        }
    }
    //функція, що викликається на випадок нічії
    public void DrawGame()
    {
        GamesCount++;
    }
    //вивід статистики
    public void GetStats(GameAccount Gamer1)
    {
        Console.WriteLine("\nСтатистика гри:");
        foreach (GameResult result in GameResults)
        {
            Console.WriteLine($"Гра проти {result.Opponent}, {result.IsWin}, Рейтинг на який грали: {result.Rating}, Рейтинг Гравця {result.Opponent}: {result.CurrentRating} Гра #{result.GameIndex} \nВаш рейтинг: {result.CurrentRating2}");
        }
    }
}
//клас, відповідає за процес гри
class Game
{
    string isWin;//підсумок раунду
    //функція гри
    public void PlayGame(GameAccount Gamer1, GameAccount Gamer2, int rating)
    {
        Random random = new Random();//ОБ'ЄКТ класу random для генерації рандомних чисел
        Console.WriteLine("\nВведіть число від 1 до 3\n1.Камінь\n2.Ножиці\n3.Папір \n");
        int a = int.Parse(Console.ReadLine());//відповіді Гравця№1
        Console.WriteLine($"\nГравець {Gamer1.UserName} обрав: " + a);
        int b = random.Next(1, 4);//відповіді Гравця№2
        Console.WriteLine($"\nГравець {Gamer2.UserName} обрав: " + b);
        //умови виграшу першого гравця
        if ((a == 1 && b == 2) || (a == 2 && b == 3) || (a == 3 && b == 1))
        {
            isWin = "перемога";
            Console.WriteLine($"\nГравець {Gamer1.UserName} виграв");
            Gamer1.WinGame(rating);
            Console.WriteLine($"\nГравець {Gamer2.UserName} програв");
            Gamer2.LoseGame(rating);
        }//умови виграшу другого гравця
        else if ((a == 1 && b == 3) || (a == 2 && b == 1) || (a == 3 && b == 2))
        {
            isWin = "поразка";
            Console.WriteLine($"\nГравець {Gamer2.UserName} виграв");
            Gamer1.LoseGame(rating);
            Console.WriteLine($"\nГравець {Gamer1.UserName} програв");
            Gamer2.WinGame(rating);
        }
        else//умови нічії
        {
            isWin = "нічия";
            Console.WriteLine("\nНічия");
            Gamer1.DrawGame();
            Gamer2.DrawGame();
        }//створює новий об'єкт GameResult, який представляє результат гри і додає цей об'єкт до списку GameResults для гравця Gamer1. ЗРОБИТИ ПІД ЧАС ВИКЛИКУ ПЕРЕМОГИ(ПОРАЗКи)
        Gamer1.GameResults.Add(new GameResult(Gamer2.UserName, isWin, rating, Gamer1.GameIndex, Gamer2.CurrentRating, Gamer1.CurrentRating));
        Gamer1.GameIndex++;

    }
}

//клас виводу статистики пройденої гри
class GameResult
{
    public string Opponent { get; }
    public string IsWin { get; }
    public int Rating { get; }
    public int GameIndex { get; }
    public int CurrentRating { get; }
    public int CurrentRating2 { get; }

    public GameResult(string opponent, string isWin, int rating, int gameIndex, int currentRating, int currentRating2)
    {
        Opponent = opponent;
        IsWin = isWin;
        Rating = rating;
        GameIndex = gameIndex;
        CurrentRating = currentRating;
        CurrentRating2 = currentRating2;
    }
}
