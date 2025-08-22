using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MonoGameLibrary.Settings;

public class SettingsManager {
    private static readonly Lock _fileLock = new();
    private static readonly Lock _debounceLock = new();

    private CancellationTokenSource _savingToken = new();
    private string SettingsFilePath { get; set; }
    private const string TempSettingsFileExtension = ".tmp";

    // Abstracted settings object
    public ISettingsObject ActiveSettings { get; set; }

    public void SetSettingsFilePath( string settingsFilePath ) {
        // Set the settings file path
        SettingsFilePath = settingsFilePath;
    }

    public void SetSettingsObject( ISettingsObject settingsObject ) {
        // Set the settings object
        ActiveSettings = settingsObject;
    }


    /// <summary>
    /// Get the saved settings object.
    /// </summary>
    /// <example>
    /// In order to use it, you'll need to cast it to your type.
    /// <code>
    /// ((GameSettings)Core.Settings.GetSettingsObject()).VideoSettings.Borderless = false;
    /// </code>
    /// </example>
    /// <returns></returns>
    public ISettingsObject GetSettingsObject() {
        return ActiveSettings;
    }

    public void LoadSettings() {
        lock( _fileLock ) {
            if( SettingsFilePath is not null ) {
                // Load the settings file
                if( File.Exists( SettingsFilePath ) ) {
                    var document = XDocument.Load( SettingsFilePath );
                    ActiveSettings.Load( document );
                } else {
                    ActiveSettings.CreateAndLoadDefaultSettings();
                    SaveSettings( 0 );
                }
            }
        }
    }

    public void SaveSettings( int debounceMs = 250 ) {
        if( SettingsFilePath is not null ) {
            lock( _debounceLock ) {
                // Cancel any previous save task and create a new one
                _savingToken.Cancel();
                _savingToken.Dispose();
                _savingToken = new CancellationTokenSource();
                var token = _savingToken.Token;

                // Save the settings file in a separate thread
                Task.Run( async () => {
                        try {
                            await Task.Delay( debounceMs, token );

                            lock( _fileLock ) {
                                var document = ActiveSettings.Save();
                                document.Save( string.Concat( SettingsFilePath, TempSettingsFileExtension ) );

                                if( File.Exists( SettingsFilePath ) ) {
                                    File.Replace( string.Concat( SettingsFilePath, TempSettingsFileExtension ), SettingsFilePath, null, true );
                                } else {
                                    File.Move( string.Concat( SettingsFilePath, TempSettingsFileExtension ), SettingsFilePath );
                                }
                            }
                        } catch( TaskCanceledException ) {
                            // Save was cancelled due to a new change, do nothing
                            //Console.WriteLine( "Save was cancelled due to a new change" );
                        }
                    }
                );
            }
        }
    }
}
