using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string inputFile = @"C:\Users\enriq\Downloads\lab3.2-cypher\Encrypted.txt";
        string outputFile = @"C:\Users\enriq\Downloads\lab3.2-cypher\Decrypted.txt";

        Console.WriteLine($"Reading input file \"{Path.GetFileName(inputFile)}\".");

        string[] lines = File.ReadAllLines(inputFile);
        string encrypted = string.Join(Environment.NewLine, lines);

        Console.WriteLine($"Length of input file is {lines.Length} lines, and {encrypted.Length} characters.");

        var groups = encrypted.ToUpper().Where(char.IsLetter).GroupBy(c => c)
                        .OrderByDescending(g => g.Count()).Take(2).ToList();
        char top1 = groups[0].Key; int top1Count = groups[0].Count();
        char top2 = groups[1].Key; int top2Count = groups[1].Count();

        Console.WriteLine($"The two most occurring characters are {top1} and {top2}, occurring {top1Count} times and {top2Count} times.");

        Console.WriteLine($"Reading the encrypted file \"{Path.GetFileName(inputFile)}\".");
        Console.WriteLine($"The most occurring character is {top1}, occurring {top1Count} times.");

        char assumedMost = 'E';
        int shift = (top1 - assumedMost + 26) % 26;
        Console.WriteLine($"A shift factor of {shift} has been determined.");

        string decrypted = new string(encrypted.Select(c =>
        {
            if (!char.IsLetter(c)) return c;
            char offset = char.IsUpper(c) ? 'A' : 'a';
            return (char)((((c - offset) - shift + 26) % 26) + offset);
        }).ToArray());

        File.WriteAllText(outputFile, decrypted);
        Console.WriteLine($"Writing output file now to \"{Path.GetFileName(outputFile)}\".");

        Console.WriteLine("Display the file? (y/n).");
        string choice = Console.ReadLine()?.Trim().ToLower();

        if (choice == "y")
        {
            Console.WriteLine("Y (display the decrypted file to the screen)\n");
            Console.WriteLine("--- Decrypted Text ---");
            Console.WriteLine(decrypted);
        }
        else
        {
            Console.WriteLine("N (End the program).");
        }
    }
}