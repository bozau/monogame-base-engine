using System.Xml.Linq;

namespace MonoGameLibrary.Settings;

/// <summary>
/// Provides required methods for working with settings files in order to be used with the SettingsManager.
/// </summary>
public interface ISettingsObject {
    /// <summary>
    /// Creates a new settings file recommend this to call your Load and Save methods to fill it and reduce code duplication.
    /// </summary>
    /// <returns>Settings file to be written.</returns>
    void CreateAndLoadDefaultSettings();

    /// <summary>
    /// Loads the settings from the settings file using custom logic.
    /// </summary>
    /// <param name="settingsInput">Settings file to be read to be parsed.</param>
    void Load( XDocument settingsInput );

    /// <summary>
    /// Saves the settings to the settings file using custom logic.
    /// </summary>
    /// <returns>Settings file to be written</returns>
    XDocument Save();
}
