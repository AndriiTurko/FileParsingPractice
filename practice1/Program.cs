using System;
using System.IO;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main()
    {
        int amountOfValidPasswords = 0;

        var content = ReadFile();

        amountOfValidPasswords = CountAmountOfValidPasswords(content);

        Console.WriteLine("Amount of valid passwords is " + amountOfValidPasswords);
    }

    public static string ReadFile()
    {
        string filePath, fileContent;

        // User should enter the file, which exists in the same directory as Program.cs
        do
        {
            Console.WriteLine("Enter the file path: ");

            filePath = "../../../" + Console.ReadLine();
        } 
        while (!File.Exists(filePath));

        fileContent = File.ReadAllText(filePath);
        //}
        //catch (IOException)
        //{
        //    Console.WriteLine("An error occurred while reading the file!");
        //}

        return fileContent;
    }

    public static int CountAmountOfValidPasswords(string content)
    {
        int count = 0;

        var lines = content.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var parsedLine = ParseLine(line);

            if (CheckPasswordValid(parsedLine.Item1, parsedLine.Item2, parsedLine.Item3, parsedLine.Item4))
            {
                count++;
            }
        }

        return count;
    }

    public static Tuple<char, int, int, string> ParseLine(string line)
    {
        var elements = line.Split(new[] { " " }, StringSplitOptions.None);

        var minMax = elements[1].Split(new[] { "-" }, StringSplitOptions.None);

        var result = Tuple.Create(elements[0].ToCharArray()[0], Int32.Parse(minMax[0]), Int32.Parse(minMax[1].Substring(0, minMax[1].Length - 1)), elements[2]);

        return result;
    }

    public static bool CheckPasswordValid(char symbol, int min, int max, string password)
    {
        int count = 0;

        foreach (char c in password)
        {
            if (c == symbol)
            {
                count++;
            }
        }

        var result = count >= min && count <= max;

        return result;
    }
}
