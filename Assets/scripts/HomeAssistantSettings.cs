using System.IO;
using UnityEngine;

public static class HomeAssistantSettings
{
    public static string Address;
    public static string Token;

    private static bool loaded = false;

    public static void Load()
    {
        if (loaded) return;

        string path = Path.Combine(Application.streamingAssetsPath, ".env");

        if (!File.Exists(path))
        {
            Debug.LogError(".env file not found at: " + path);
            return;
        }

        foreach (var line in File.ReadAllLines(path))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.StartsWith("#")) continue;

            var parts = line.Split('=');
            if (parts.Length != 2) continue;

            string key = parts[0].Trim();
            string value = parts[1].Trim();

            switch (key)
            {
                case "HOME_ASSISTANT_URL":
                    Address = value;
                    break;

                case "HOME_ASSISTANT_TOKEN":
                    Token = value;
                    break;
            }
        }

        loaded = true;
    }
}