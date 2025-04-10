using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LogWrapper
{
    public List<string> logs;
}

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
        Debug.Log("--- Game History ---");
        foreach (var log in logs)
        {
            Debug.Log(log);
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
        LogWrapper wrapper = new LogWrapper { logs = logs };
        string json = JsonUtility.ToJson(wrapper);
        System.IO.File.WriteAllText(logFilePath, json);
    }

    private void LoadLogsFromFile()
    {
        if (System.IO.File.Exists(logFilePath))
        {
            string json = System.IO.File.ReadAllText(logFilePath);
            LogWrapper wrapper = JsonUtility.FromJson<LogWrapper>(json);
            logs = wrapper.logs ?? new List<string>();
        }
    }
}
