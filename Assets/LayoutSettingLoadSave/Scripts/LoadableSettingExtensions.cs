using sugi.cc.data;
using SFB;
using System.IO;

public static class LoadableSettingExtensions
{
    public static void LoadJson<T>(this LoadableSetting<T> setting)
    {
        var filePath = setting.FilePath;
        var directoryName = 0 < filePath.Length ? Path.GetDirectoryName(filePath) : "";
        var fileName = 0 < filePath.Length ? Path.GetFileName(filePath) : "";
        var extensions = new[]
        {
                new ExtensionFilter("Json File", "json"),
                new ExtensionFilter("All Files", "*")
            };
        StandaloneFileBrowser.OpenFilePanelAsync(
            $"Load {setting.Data.GetType().Name} JSON", directoryName, extensions, false, (paths) =>
            {
                if (0 < paths.Length)
                    setting.Load(paths[0]);
            });
    }

    public static void SaveJson<T>(this LoadableSetting<T> setting)
    {
        var filePath = setting.FilePath;
        var directoryName = 0 < filePath.Length ? Path.GetDirectoryName(filePath) : "";
        var fileName = 0 < filePath.Length ? Path.GetFileName(filePath) : "";
        var extensions = new[]
        {
                new ExtensionFilter("Json File", "json"),
                new ExtensionFilter("All Files", "*")
            };
        StandaloneFileBrowser.SaveFilePanelAsync(
            $"Load {setting.Data.GetType().Name} JSON", directoryName, fileName, extensions, (path) =>
            {
                setting.Save(path, setting.Data);
            });
    }

    public static void ShowInExplorer<T>(this LoadableSetting<T> setting)
    {
        var filePath = setting.FilePath;
        if (0 < filePath.Length)
        {
            var directoryName = Path.GetDirectoryName(filePath);
            System.Diagnostics.Process.Start(directoryName);
        }
    }
}
