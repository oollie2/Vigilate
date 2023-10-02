using System.IO;
using System.Text.Json;
using NLog;

namespace Vigilate.Core;

/// <summary>
/// This class configures the serialization and de-serialization of any settings defined within the class IntegrateSettings.
/// Reading and writing are possible to and from any JSON file.
/// </summary>
public class Settings<T> where T : new()
{
    private readonly static ILogger _logger = LogManager.GetCurrentClassLogger();
    /// <summary>
    /// Our main reference to the individual settings
    /// </summary>
    public static T Main { get; set; }
    /// <summary>
    /// The file used to read / write to.
    /// </summary>
    private static string _settingsFile { get; set; }
    /// <summary>
    /// Read from the existing settings file, if it does not exist a new one with default values is created.
    /// </summary>
    /// <param name="settingsFile">The full file path for the JSON file with extension.</param>
    public async static Task<bool> Read(string settingsFile)
    {
        _settingsFile = settingsFile;
        Main = new();
        _logger.Info($"Reading {typeof(T)} from the file {settingsFile}");
        await Deserialize();
        return await Serialize();
    }
    /// <summary>
    /// Reads in the JSON file set at SettingsFile, de-serializes this into Main. 
    /// </summary>
    private async static Task<bool> Deserialize()
    {
        if (string.IsNullOrEmpty(_settingsFile))
            return false;
        if (File.Exists(_settingsFile))
        {
            using FileStream fileStream = new(_settingsFile, FileMode.Open,
                          FileAccess.Read, FileShare.ReadWrite, 4096, useAsync: true);
            try
            {
                Main = await JsonSerializer.DeserializeAsync<T>(fileStream);
            }
            catch (JsonException ex)
            {
                if (BackupFile(out string backupFile))
                    _logger.Warn($"Unable to read settings file: {ex.Message} - A backup has been created at {backupFile}");
                else
                    _logger.Warn($"Unable to read settings file: {ex.Message} - A backup could not be created.");
                return await Serialize();
            }
            catch (FileNotFoundException ex)
            {
                if (BackupFile(out string backupFile))
                    _logger.Warn($"Unable to read settings file: {ex.Message} - A backup has been created at {backupFile}");
                else
                    _logger.Warn($"Unable to read settings file: {ex.Message} - A backup could not be created.");
                return await Serialize();
            }
            fileStream.Flush();
            fileStream.Close();
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Serializes Main and writes all into the file defined with SettingsFile.
    /// </summary>
    private async static Task<bool> Serialize()
    {
        if (_settingsFile == null)
            return false;
        string directory = Path.GetDirectoryName(_settingsFile);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);
        FileMode mode = FileMode.Create;
        if (File.Exists(_settingsFile))
            mode = FileMode.Truncate;
        using FileStream fileStream = new(_settingsFile, mode,
                               FileAccess.Write, FileShare.ReadWrite, 4096, useAsync: true);
        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };
        await JsonSerializer.SerializeAsync(fileStream, Main, options);
        fileStream.Flush();
        fileStream.Close();
        return true;
    }
    private static bool BackupFile(out string backupFile)
    {
        backupFile = "";
        if (string.IsNullOrEmpty(_settingsFile))
            return false;
        backupFile = string.Format("{0}-{1}",
            _settingsFile, DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"));
        File.Copy(_settingsFile, backupFile);
        return true;
    }
    /// <summary>
    /// Save the current settings to the file.
    /// </summary>
    public async static Task<bool> Save()
    {
        return await Serialize();
    }
}