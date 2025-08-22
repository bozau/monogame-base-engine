using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.Settings;

namespace BrickBreaker.Components.Settings;

public class GameSettings : ISettingsObject {
    public GameVideoSettings VideoSettings { get; set; }
    public GameSoundSettings SoundSettings { get; set; }
    public GameInputSettings InputSettings { get; set; }
    public GameLanguageSettings LanguageSettings { get; set; }

    public void CreateAndLoadDefaultSettings() {
        Load( new XDocument( new XElement( "Settings" ) ) ); // Creates empty settings object which should allow for proper parsing and default settings.
    }

    public void Load( XDocument settingsInput ) {
        // Load settings from the settings file provided by the settings manager
        if( settingsInput?.Root is null ) {
            throw new ArgumentNullException( nameof( settingsInput ) );
        }

        VideoSettings = new GameVideoSettings {
            Width = int.Parse( settingsInput.Root.Element( "Video" )?.Element( "Width" )?.Value ?? "800" ),
            Height = int.Parse( settingsInput.Root.Element( "Video" )?.Element( "Height" )?.Value ?? "600" ),
            FullScreen = bool.Parse( settingsInput.Root.Element( "Video" )?.Element( "FullScreen" )?.Value ?? "false" ),
            Borderless = bool.Parse( settingsInput.Root.Element( "Video" )?.Element( "Borderless" )?.Value ?? "false" ),
            MultiSample = bool.Parse( settingsInput.Root.Element( "Video" )?.Element( "MultiSample" )?.Value ?? "false" ),
            MultiSampleCount =
                int.Parse( settingsInput.Root.Element( "Video" )?.Element( "MultiSampleCount" )?.Value ?? "0" ),
            VSync = bool.Parse( settingsInput.Root.Element( "Video" )?.Element( "VSync" )?.Value ?? "true" ),
            TargetFrameRate = int.Parse( settingsInput.Root.Element( "Video" )?.Element( "TargetFrameRate" )?.Value ?? "60" )
        };

        SoundSettings = new GameSoundSettings {
            MasterVolume = float.Parse( settingsInput.Root.Element( "Audio" )?.Element( "MasterVolume" )?.Value ?? "0.3" ),
            MasterMuted = bool.Parse( settingsInput.Root.Element( "Audio" )?.Element( "MasterMuted" )?.Value ?? "false" ),
            SongVolume = float.Parse( settingsInput.Root.Element( "Audio" )?.Element( "SongVolume" )?.Value ?? "0.3" ),
            SongMuted = bool.Parse( settingsInput.Root.Element( "Audio" )?.Element( "SongMuted" )?.Value ?? "false" ),
            SoundEffectsVolume = float.Parse( settingsInput.Root.Element( "Audio" )?.Element( "SoundEffectsVolume" )?.Value ?? "0.3" ),
            SoundEffectsMuted = bool.Parse( settingsInput.Root.Element( "Audio" )?.Element( "SoundEffectsMuted" )?.Value ?? "false" ),
            //InterfaceVolume = float.Parse( settingsInput.Root.Element( "Audio" )?.Element( "InterfaceVolume" )?.Value ?? "0.3" ),
            //InterfaceMuted = bool.Parse( settingsInput.Root.Element( "Audio" )?.Element( "InterfaceMuted" )?.Value ?? "false" )
        };

        LanguageSettings = new GameLanguageSettings {
            Language = settingsInput.Root.Element( "Language" )?.Element( "ISO" )?.Value ?? "en-US"
        };

        InputSettings = new GameInputSettings {
            MoveLeft = Enum.TryParse< Keys >(
                settingsInput.Root.Element( "Input" )?.Element( "MoveLeft" )?.Value,
                true,
                out var moveLeft
            )
                ? moveLeft
                : Keys.Left,
            MoveRight = Enum.TryParse< Keys >(
                settingsInput.Root.Element( "Input" )?.Element( "MoveRight" )?.Value,
                true,
                out var moveRight
            )
                ? moveRight
                : Keys.Right
        };
    }

    public XDocument Save() {
        // Build settings file to be written by the settings manager
        var settingsOutput = new XDocument(
            new XElement(
                "Settings",
                new XElement(
                    "Video",
                    new XElement( "Width", VideoSettings.Width ),
                    new XElement( "Height", VideoSettings.Height ),
                    new XElement( "FullScreen", VideoSettings.FullScreen ),
                    new XElement( "Borderless", VideoSettings.Borderless ),
                    new XElement( "MultiSample", VideoSettings.MultiSample ),
                    new XElement( "MultiSampleCount", VideoSettings.MultiSampleCount ),
                    new XElement( "VSync", VideoSettings.VSync ),
                    new XElement( "TargetFrameRate", VideoSettings.TargetFrameRate )
                ),
                new XElement(
                    "Audio",
                    new XElement( "MasterVolume", SoundSettings.MasterVolume ),
                    new XElement( "MasterMuted", SoundSettings.MasterMuted ),
                    new XElement( "SongVolume", SoundSettings.SongVolume ),
                    new XElement( "SongMuted", SoundSettings.SongMuted ),
                    new XElement( "SoundEffectsVolume", SoundSettings.SoundEffectsVolume ),
                    new XElement( "SoundEffectsMuted", SoundSettings.SoundEffectsMuted )
                    //new XElement( "InterfaceVolume", SoundSettings.InterfaceVolume ),
                    //new XElement( "InterfaceMuted", SoundSettings.InterfaceMuted )
                ),
                new XElement(
                    "Language",
                    new XElement( "ISO", LanguageSettings.Language )
                ),
                new XElement(
                    "Input",
                    new XElement( "MoveLeft", InputSettings.MoveLeft ),
                    new XElement( "MoveRight", InputSettings.MoveRight )
                )
            )
        );

        return settingsOutput;
    }
}

public sealed class GameVideoSettings {
    public int Width { get; set; }
    public int Height { get; set; }
    public bool FullScreen { get; set; }
    public bool Borderless { get; set; }
    public bool MultiSample { get; set; }
    public int MultiSampleCount { get; set; }
    public bool VSync { get; set; }
    public int TargetFrameRate { get; set; }
}

public sealed class GameSoundSettings {
    public float MasterVolume { get; set; }
    public bool MasterMuted { get; set; }
    public float SongVolume { get; set; }
    public bool SongMuted { get; set; }
    public float SoundEffectsVolume { get; set; }
    public bool SoundEffectsMuted { get; set; }
    //public float InterfaceVolume { get; set; }
    //public bool InterfaceMuted { get; set; }
}

public sealed class GameLanguageSettings {
    public string Language { get; set; }
}

public sealed class GameInputSettings {
    public Keys MoveLeft { get; set; }
    public Keys MoveRight { get; set; }
}
