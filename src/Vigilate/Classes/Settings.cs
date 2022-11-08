using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Vigilate.Classes
{
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
    public class Settings : IDisposable
    {
        /// <summary>
        /// Our main reference to the individual settings
        /// </summary>
        public static VigilateSettings Main { get; set; }
        public bool ErrorState { get; set; }
        private XmlSerializer Serializer;
        private readonly XmlWriterSettings XmlWriterSettings;
        /// <summary>
        /// The file used to read / write to.
        /// </summary>
        internal string SettingsFile { get; set; }
        /// <summary>
        /// Initiate a new settings object, to read and write user settings to.
        /// </summary>
        /// <param name="settingsFile">The file path for the xml file</param>
        public Settings(string settingsFile)
        {
            ErrorState = false;
            SettingsFile = settingsFile;
            Main = new();
            XmlWriterSettings = new() { Indent = true };
            Serializer = new(typeof(VigilateSettings));
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
        private void Read()
        {
            using FileStream fs = new(SettingsFile, FileMode.Open);
            try
            {
                Main = (VigilateSettings)Serializer.Deserialize(fs);
            }
            catch (InvalidOperationException)
            {
                ErrorState = true;
                fs.Close();
                Write();
            }
        }
        private void Write()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SettingsFile));
            using XmlWriter write = XmlWriter.Create(SettingsFile, XmlWriterSettings);
            Serializer.Serialize(write, Main);
            write.Close();
        }
        public void Reset(VigilateSettings settings)
        {
            using XmlWriter write = XmlWriter.Create(SettingsFile, XmlWriterSettings);
            Serializer.Serialize(write, settings);
            write.Close();
        }
        public void Reset()
        {
            Main = new();
            using XmlWriter write = XmlWriter.Create(SettingsFile, XmlWriterSettings);
            Serializer.Serialize(write, Main);
            write.Close();
        }
        public void Save()
        {
            Write();
        }
        public void Dispose()
        {
            Write();
            Serializer = null;
            Main = null;
            GC.SuppressFinalize(this);
        }
    }
}
