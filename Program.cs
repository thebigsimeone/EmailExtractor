using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Inserisci il percorso del file:");
        string filePath = Console.ReadLine();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File non trovato!");
            return;
        }

        int emailCount = 0;
        int fileIndex = 1;
        List<string> emails = new List<string>();

        try
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fileStream))
            {
                string line;
                Regex emailRegex = new Regex(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b", RegexOptions.Compiled);

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    MatchCollection matches = emailRegex.Matches(line);
                    foreach (Match match in matches)
                    {
                        emails.Add(match.Value);
                        emailCount++;

                        if (emailCount % 2000000 == 0)
                        {
                            await SaveEmailsToFile(emails, $"emails_{fileIndex++}.txt");
                            emails.Clear();
                        }
                    }
                }

                // Save any remaining emails
                if (emails.Count > 0)
                {
                    await SaveEmailsToFile(emails, $"emails_{fileIndex}.txt");
                }
            }

            Console.WriteLine("Estrazione completata con successo.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore durante l'elaborazione: {ex.Message}");
        }
    }

    private static async Task SaveEmailsToFile(List<string> emails, string fileName)
    {
        using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        using (var writer = new StreamWriter(fileStream))
        {
            foreach (string email in emails)
            {
                await writer.WriteLineAsync(email);
            }
        }
    }
}
