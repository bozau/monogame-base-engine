using BrickBreaker.Components.Settings;
using BrickBreaker.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Settings;

namespace BrickBreaker;

public class BrickBreaker : Core {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public BrickBreaker() : base( "Brick Breaker", 1280, 720 ) {}

    protected override void Initialize() {
        base.Initialize();

        // Setup our settings
        Settings.SetSettingsObject( new GameSettings() );
        Settings.SetSettingsFilePath( "settings.xml" );
        Settings.LoadSettings();

        // Setup our graphics based upon our loaded settings
        Graphics.IsFullScreen = ( ( GameSettings )Settings.GetSettingsObject() ).VideoSettings.FullScreen;
        Graphics.PreferredBackBufferWidth = ( ( GameSettings )Settings.GetSettingsObject() ).VideoSettings.Width;
        Graphics.PreferredBackBufferHeight = ( ( GameSettings )Settings.GetSettingsObject() ).VideoSettings.Height;
        Graphics.PreferMultiSampling = ( ( GameSettings )Settings.GetSettingsObject() ).VideoSettings.MultiSample;
        Graphics.SynchronizeWithVerticalRetrace = ( ( GameSettings )Settings.GetSettingsObject() ).VideoSettings.VSync;
        Graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8; // See here for details on stencil's: https://learnopengl.com/Advanced-OpenGL/Stencil-testing

        // Subscribe to the PreparingDeviceSettings event so we can set our multisampling count and other required items
        Graphics.PreparingDeviceSettings += OnPreparingDeviceSettings;

        Graphics.ApplyChanges();

        // Setup our audio based upon our loaded settings
        Audio.MasterVolume = ( ( GameSettings )Settings.GetSettingsObject() ).SoundSettings.MasterVolume;
        if( Audio.AreAllMuted != ( ( GameSettings )Settings.GetSettingsObject() ).SoundSettings.MasterMuted ) {
            Audio.ToggleAllAudioMute();
        }
        
        Audio.SoundEffectsVolume = ( ( GameSettings )Settings.GetSettingsObject() ).SoundSettings.SoundEffectsVolume;
        if( Audio.AreSoundEffectsMuted != ( ( GameSettings )Settings.GetSettingsObject() ).SoundSettings.SoundEffectsMuted ) {
            Audio.ToggleSoundEffectsMute();
        }
        
        Audio.SongVolume = ( ( GameSettings )Settings.GetSettingsObject() ).SoundSettings.SongVolume;
        if( Audio.AreSongsMuted != ( ( GameSettings )Settings.GetSettingsObject() ).SoundSettings.SongMuted ) {
            Audio.ToggleSongsMute();
        }

        // Setup our input based upon our loaded settings


        // Set our initial scene
        ChangeScene( new TestScreen() );
        //ChangeScene( new SplashScreen() );

        // Load our UI
        UI.LoadUI( "GumInterface/Interface.gumx" );
    }

    protected override void LoadContent() {
        base.LoadContent();
    }

    protected override void Update( GameTime gameTime ) {
        base.Update( gameTime );
    }

    protected override void Draw( GameTime gameTime ) {
        GraphicsDevice.Clear( Color.CornflowerBlue );

        base.Draw( gameTime );
    }

    private void OnPreparingDeviceSettings( object sender, PreparingDeviceSettingsEventArgs e ) {
        e.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = ( ( GameSettings )Settings.GetSettingsObject() ).VideoSettings.MultiSampleCount;
    }
}
