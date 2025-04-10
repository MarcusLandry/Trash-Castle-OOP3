using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Log
{
    private List<string> logs;
    private string logFilePath = "game_logs.json";

    public Log()
    {
        logs = new List<string>();
        LoadLogsFromFile();
    }

    public void LogMessage(string message)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        string logEntry = $"[{timestamp}] {message}";
        logs.Add(logEntry);
        SaveLogsToFile();
    }

    public void PrintAllLogs()
    {
        Console.WriteLine("--- Game History ---");
        foreach (var log in logs)
        {
            Console.WriteLine(log);
        }
    }

    public List<string> GetAllLogs()
    {
        return new List<string>(logs);
    }

    public void ClearLogs()
    {
        logs.Clear();
        SaveLogsToFile();
    }

    private void SaveLogsToFile()
    {
        File.WriteAllText(logFilePath, JsonSerializer.Serialize(logs));
    }

    private void LoadLogsFromFile()
    {
        if (File.Exists(logFilePath))
        {
            string json = File.ReadAllText(logFilePath);
            logs = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }
    }
} 
