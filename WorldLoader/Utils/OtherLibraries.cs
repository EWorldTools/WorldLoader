using System;
using System.IO;
using Newtonsoft.Json;

namespace WorldLoader.OtherLibraries; // Needs Newtonsoft

public class WorldConfig<T> where T : class
{
    private string FilePath { get; }
    public T Config { get; private set; }
    public event Action PreOnConfigUpdate;
    public event Action OnConfigUpdate;

    public WorldConfig(string Path)
    {
        FilePath = Path;
        CheckConfig();

        Config = JsonConvert.DeserializeObject<T>(File.ReadAllText(FilePath));
    }

    private void CheckConfig() {
        if (!File.Exists(FilePath))
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(Activator.CreateInstance(typeof(T)), Formatting.Indented, new JsonSerializerSettings()));
    }

    private void UpdateConfig(object obj, FileSystemEventArgs args)
    {
        try
        {
            var UpdatedObject = JsonConvert.DeserializeObject<T>(File.ReadAllText(FilePath));

            if (UpdatedObject != null)
                foreach (var Prop in UpdatedObject.GetType()?.GetProperties()) {
                    var Original = Config.GetType().GetProperty(Prop?.Name);

                    if (Original != null 
                        && Prop.GetValue(UpdatedObject) != Original.GetValue(Config)) 
                    {
                        PreOnConfigUpdate?.Invoke();
                        Config = UpdatedObject;

                        OnConfigUpdate?.Invoke();
                        break;
                    }
                }
        }
        catch { // Throw Error
        }
    }

    public void Save() =>
        File.WriteAllText(FilePath, JsonConvert.SerializeObject(Config, Formatting.Indented, new JsonSerializerSettings()));
}