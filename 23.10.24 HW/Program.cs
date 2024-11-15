using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Program
{
    static void Main()
    {
        Task1();

        Task2();

        Task3();

        Task4();

        Task5();
    }

    //Task 1
    static void Task1()
    {
        Random random = new Random();
        List<int> numbers = new List<int>();
        List<int> primes = new List<int>();
        List<int> fibonacci = new List<int>();

        for (int i = 0; i < 100; i++)
        {
            int number = random.Next(1, 101);
            numbers.Add(number);

            if (IsPrime(number))
            {
                primes.Add(number);
            }

            if (IsFibonacci(number))
            {
                fibonacci.Add(number);
            }
        }

        File.WriteAllText("primes.json", JsonSerializer.Serialize(primes, new JsonSerializerOptions { WriteIndented = true }));
        File.WriteAllText("fibonacci.json", JsonSerializer.Serialize(fibonacci, new JsonSerializerOptions { WriteIndented = true }));

        Console.WriteLine($"Generated Numbers: {string.Join(", ", numbers)}");
        Console.WriteLine($"Primes Count: {primes.Count}");
        Console.WriteLine($"Fibonacci Count: {fibonacci.Count}");
    }

    static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        for (int i = 2; i < number; i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }

    static bool IsFibonacci(int number)
    {
        int a = 0, b = 1, temp;
        while (b < number)
        {
            temp = a;
            a = b;
            b = temp + b;
        }
        return b == number || number == 0;
    }

    // Task 2
    static void Task2()
    {
        Console.Write("Enter the word to search: ");
        string searchWord = Console.ReadLine();

        Console.Write("Enter the word to replace: ");
        string replaceWord = Console.ReadLine();

        string text = File.ReadAllText("input.txt");
        string updatedText = text.Replace(searchWord, replaceWord);

        File.WriteAllText("input.txt", updatedText);

        var statistics = new
        {
            WordToSearch = searchWord,
            WordToReplace = replaceWord,
            TotalReplacements = (text.Length - updatedText.Length) / searchWord.Length
        };

        Console.WriteLine(JsonSerializer.Serialize(statistics, new JsonSerializerOptions { WriteIndented = true }));
    }

    // Task 3
    static void Task3()
    {
        Console.Write("Enter the path to the text file: ");
        string textFilePath = Console.ReadLine();

        Console.Write("Enter the path to the moderation words file: ");
        string moderationFilePath = Console.ReadLine();

        string[] moderationWords = File.ReadAllLines(moderationFilePath);
        string text = File.ReadAllText(textFilePath);

        foreach (var word in moderationWords)
        {
            text = text.Replace(word, new string('*', word.Length));
        }

        File.WriteAllText(textFilePath, text);
        Console.WriteLine("Moderation completed.");
    }

    // Task 4
    static void Task4()
    {
        Console.Write("Enter the path to the folder: ");
        string folderPath = Console.ReadLine();

        Console.Write("Enter the file mask (e.g., *.txt): ");
        string fileMask = Console.ReadLine();

        string[] files = Directory.GetFiles(folderPath, fileMask, SearchOption.AllDirectories);

        foreach (var file in files)
        {
            Console.WriteLine(file);
        }

        Console.WriteLine($"Total files found: {files.Length}");
    }

    // Task 5
    static void Task5()
    {
        string[] lines = File.ReadAllLines("numbers.txt");
        List<int> numbers = Array.ConvertAll(lines, int.Parse).ToList();

        int positiveCount = numbers.Count(n => n > 0);
        int negativeCount = numbers.Count(n => n < 0);
        int twoDigitCount = numbers.Count(n => n >= 10 && n < 100);
        int fiveDigitCount = numbers.Count(n => n >= 10000 && n < 100000);

        File.WriteAllText("positive_numbers.json", JsonSerializer.Serialize(numbers.Where(n => n > 0).ToList(), new JsonSerializerOptions { WriteIndented = true }));
        File.WriteAllText("negative_numbers.json", JsonSerializer.Serialize(numbers.Where(n => n < 0).ToList(), new JsonSerializerOptions { WriteIndented = true }));
        File.WriteAllText("two_digit_numbers.json", JsonSerializer.Serialize(numbers.Where(n => n >= 10 && n < 100).ToList(), new JsonSerializerOptions { WriteIndented = true }));
        File.WriteAllText("five_digit_numbers.json", JsonSerializer.Serialize(numbers.Where(n => n >= 10000 && n < 100000).ToList(), new JsonSerializerOptions { WriteIndented = true }));

        var statistics = new
        {
            PositiveCount = positiveCount,
            NegativeCount = negativeCount,
            TwoDigitCount = twoDigitCount,
            FiveDigitCount = fiveDigitCount
        };

        Console.WriteLine(JsonSerializer.Serialize(statistics, new JsonSerializerOptions { WriteIndented = true }));
    }
}

