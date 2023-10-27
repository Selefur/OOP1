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
        int rating;
        int choose;
        string answer;
        Game gameplay = new Game();//створення об'єкту класу 'Game, для подальшого виклику функції класу Game

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
        Console.WriteLine("Виберіть статистику якого гравця ви хочете побачити: \n1. Гравець 1 \n2. Гравець 2");
        choose = int.Parse(Console.ReadLine());
        while (choose < 1 || choose > 2)
        {
            Console.WriteLine("Невірно введенно число, введіть ще раз: ");
            choose = int.Parse(Console.ReadLine());
        }
        if (choose == 1)
        {
            Console.WriteLine("\nСтатистика 3 раундів:");
            Gamer1.GetStats();
        }
        if (choose == 2)
        {
            Console.WriteLine("\nСтатистика 3 раундів:");
            Gamer2.GetStats();
        }
        Console.WriteLine("Чи хочете ви вивести статистику іншого гравця ? Так/Ні");
        answer = Console.ReadLine();
        if (answer == "Так" || answer == "так")
        {
            if (choose == 1)
            {
                Console.WriteLine("\nСтатистика 3 раундів:");
                Gamer2.GetStats();
            }
            if (choose == 2)
            {
                Console.WriteLine("\nСтатистика 3 раундів:");
                Gamer1.GetStats();
            }
        }
    }
}
class GameAccount
{
    public string UserName { get; set; }//нік
    public int CurrentRating { get; set; }//рейтинг гравця
    public int GamesCount { get; set; }//кількість ігор
    public List<GameResult> GameResults { get; set; } = new List<GameResult>();// список, в який записується історія ігр, для виводу історії.
    public int GameIndex { get; set; } = 1;//індекс гри
    //статистика гравця. Вивід.
    public void OutGamers()
    {
        Console.WriteLine($"\nІм'я: {UserName}\nРейтинг: {CurrentRating} \nІгор зіграно: {GamesCount}\n");
    }
    //функція, що викликається на випадок виграшу гравця
    public void WinGame(int rating, string player, int gameIndex, int currentRating)
    {
        GamesCount++;
        CurrentRating += rating;
        currentRating = CurrentRating;
        string winner = "виграв";
        //створює новий об'єкт GameResult, який представляє результат гри і додає цей об'єкт до списку GameResults. 
        GameResults.Add(new GameResult(player, winner, rating, gameIndex, currentRating));
    }
    //функція, що викликається на випадок програшу гравцяю.
    public void LoseGame(int rating, string player, int gameIndex, int currentRating)
    {
        GamesCount++;
        if (CurrentRating > 1)
        {
            CurrentRating -= rating;
            if (CurrentRating < 1)
            {
                CurrentRating = 1;
            }
        }
        string winner = "програв";
        currentRating = CurrentRating;
        GameResults.Add(new GameResult(player, winner, rating, gameIndex, currentRating));
    }
    //функція, що викликається на випадок нічії
    public void DrawGame(int rating, string player, int gameIndex, int currentRating)
    {
        GamesCount++;
        string winner = "нічия";
        GameResults.Add(new GameResult(player, winner, rating, gameIndex, currentRating));
    }
    //вивід статистики
    public void GetStats()
    {
        Console.WriteLine("\nСтатистика гри:");
        foreach (GameResult result in GameResults)
        {
            Console.WriteLine($"Гра проти {result.Opponent}, {result.IsWin}, Рейтинг на який грали: {result.Rating}, Ваш рейтинг: {result.CurrentRating}, Гра #{result.GameIndex}");
        }
    }
}
//клас, відповідає за процес гри
class Game
{
    //функція гри
    public void PlayGame(GameAccount Gamer1, GameAccount Gamer2, int rating)
    {
        Random random = new Random();//ОБ'ЄКТ класу random для генерації рандомних чисел
        Console.WriteLine("\nВведіть число від 1 до 3\n1.Камінь\n2.Ножиці\n3.Папір \n");
        int a = int.Parse(Console.ReadLine());//відповіді Гравця№1
        Console.WriteLine($"\nГравець {Gamer1.UserName} обрав: " + a);
        int b = random.Next(1, 4);//відповіді Гравця№2
        Console.WriteLine($"\nГравець {Gamer2.UserName} обрав: " + b);
        int gameIndex = Gamer1.GamesCount;
        //умови виграшу першого гравця
        if ((a == 1 && b == 2) || (a == 2 && b == 3) || (a == 3 && b == 1))
        {
            Console.WriteLine($"\nГравець {Gamer1.UserName} виграв");
            Gamer1.WinGame(rating, Gamer2.UserName, gameIndex, Gamer1.CurrentRating);
            Console.WriteLine($"\nГравець {Gamer2.UserName} програв");
            Gamer2.LoseGame(rating, Gamer1.UserName, gameIndex, Gamer2.CurrentRating);
        }//умови виграшу другого гравця
        else if ((a == 1 && b == 3) || (a == 2 && b == 1) || (a == 3 && b == 2))
        {
            Console.WriteLine($"\nГравець {Gamer2.UserName} виграв");
            Gamer2.WinGame(rating, Gamer1.UserName, gameIndex, Gamer1.CurrentRating);
            Console.WriteLine($"\nГравець {Gamer1.UserName} програв");
            Gamer1.LoseGame(rating, Gamer2.UserName, gameIndex, Gamer2.CurrentRating);
        }
        else//умови нічії
        {
            Console.WriteLine("\nНічия");
            Gamer1.DrawGame(rating, Gamer2.UserName, gameIndex, Gamer1.CurrentRating);
            Gamer2.DrawGame(rating, Gamer1.UserName, gameIndex, Gamer2.CurrentRating);
        }
        Gamer1.GameIndex++;
    }
}
//клас виводу статистики пройденої гри
class GameResult
{
    public string Opponent { get;}
    public string IsWin { get;}
    public int Rating { get;}
    public int GameIndex { get;}
    public int CurrentRating { get;}

    public GameResult(string opponent, string isWin, int rating, int gameIndex, int currentRating)
    {
        Opponent = opponent;
        IsWin = isWin;
        Rating = rating;
        GameIndex = gameIndex;
        CurrentRating = currentRating;
    }
}
