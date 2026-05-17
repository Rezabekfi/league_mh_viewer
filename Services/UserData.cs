using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace league_mh_viewer.Services;

public class UserDataService
{
    private readonly string _filePath;

    public UserDataService()
    {
        var folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "LeagueMHViewer");

        Directory.CreateDirectory(folder);
        _filePath = Path.Combine(folder, "userdata.json");
    }

    public async Task<UserData> LoadUserDataAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new UserData();
        }

        var json = await File.ReadAllTextAsync(_filePath);

        return JsonSerializer.Deserialize<UserData>(json) ?? new UserData();
    }

    public async Task SaveUserDataAsync(UserData userData)
    {
        var json = JsonSerializer.Serialize(userData, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(_filePath, json);
    }
}

public class UserData
{
    public List<SavedProfile> Profiles { get; set; } = new();
}

public class SavedProfile
{
    public string Name { get; set; } = "";
    public string Tag { get; set; } = "";
}
