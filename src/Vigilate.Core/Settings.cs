using System;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NLog;

namespace Vigilate.Core;

public class VigilateSettings
{
    #region PreviousState
    internal bool _previousState = false;
    public bool State
    {
        get { return _previousState; }
        set { _previousState = value; }
    }
    #endregion
    #region PreviousState
    internal int _pollPeriodMs = 30000;
    public int PollPeriodMs
    {
        get { return _pollPeriodMs; }
        set { _pollPeriodMs = value; }
    }
    #endregion
}
/// <summary>
/// This class configures the serialization and de-serialization of any settings defined within the class IntegrateSettings.
/// Reading and writing are possible to and from any JSON file.
/// </summary>
public class Settings<T> where T : new()
{
    internal static ILogger _logger = LogManager.GetCurrentClassLogger();
    /// <summary>
    /// Our main reference to the individual settings
    /// </summary>
    public static T Main { get; set; }
    /// <summary>
    /// The file used to read / write to.
    /// </summary>
    internal static string SettingsFile { get; set; }
    /// <summary>
    /// Initiate a new settings object, to read and write user settings to.
    /// </summary>
    /// <param name="settingsFile">The file path for the JSON file with extension.</param>
    public Settings(string settingsFile)
    {
        SettingsFile = settingsFile;
        Main = new();
        if (File.Exists(SettingsFile))
        {
            // The file has already been created so 
            // there is no need to write defaults.
            Read();
        }
        else
        {
            // No file exists so lets use the default values
            Write();
        }
    }
    /// <summary>
    /// Reads in the JSON file set at SettingsFile, de-serializes this into Main. Settings file will be recreated if unable to de serialize.
    /// </summary>
    private static void Read()
    {
        try
        {
            Main = JsonConvert.DeserializeObject<T>(File.ReadAllText(SettingsFile), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            });
        }
        catch (SerializationException ex)
        {
            string backupFile = SettingsFile + "-" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
            File.Copy(SettingsFile, backupFile);
            _logger.Warn("Unable to read settings file: " + ex.Message + "\r\nA backup has been created at " + backupFile);
            Write();
        }
    }
    /// <summary>
    /// Serializes Main and writes all into the file defined with SettingsFile.
    /// </summary>
    private static void Write()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(SettingsFile));
        string contents = JsonConvert.SerializeObject(Main, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        });
        File.WriteAllText(SettingsFile, contents);
    }
    /// <summary>
    /// Save the current settings to the file.
    /// </summary>
    public static void Save()
    {
        Write();
    }
}