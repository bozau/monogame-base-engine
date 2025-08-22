using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Audio;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;
using MonoGameLibrary.Settings;
using MonoGameLibrary.UserInterface;

namespace MonoGameLibrary;

public class Core : Game {
    private static Core _instance;
    private static Scene _activeScene;
    private static Scene _nextScene;

    public Core( string windowTitle, int windowWidth, int windowHeight, bool fullScreen = false ) {
        if( _instance != null ) {
            throw new InvalidOperationException( $"Only one instance of the {nameof( Core )} is allowed." );
        }

        _instance = this;

        Graphics = new GraphicsDeviceManager( this );

        Graphics.PreferredBackBufferWidth = windowWidth;
        Graphics.PreferredBackBufferHeight = windowHeight;
        Graphics.IsFullScreen = fullScreen;
        Graphics.ApplyChanges();

        Window.Title = windowTitle;

        Content = base.Content;

        Content.RootDirectory = "Content";

        IsMouseVisible = true;
    }

    public static Core Instance => _instance;

    public static GraphicsDeviceManager Graphics { get; private set; }

    public new static GraphicsDevice GraphicsDevice { get; private set; }

    public static SpriteBatch SpriteBatch { get; private set; }

    public new static ContentManager Content { get; private set; }

    public static AudioController Audio { get; private set; }

    public static InputManager Input { get; private set; }

    public static UIManager UI { get; private set; }

    public static SettingsManager Settings { get; set; }

    public static bool ExitOnEscape { get; set; }

    protected override void Initialize() {
        base.Initialize();

        Settings = new SettingsManager();

        GraphicsDevice = base.GraphicsDevice;

        SpriteBatch = new SpriteBatch( GraphicsDevice );

        Input = new InputManager();

        Audio = new AudioController();

        UI = new UIManager();
    }

    protected override void UnloadContent() {
        Audio.Dispose();

        UI.Dispose();

        base.UnloadContent();
    }

    protected override void Update( GameTime gameTime ) {
        Input.Update( gameTime );

        if( ExitOnEscape && Input.Keyboard.IsKeyDown( Keys.Escape ) ) {
            Exit();
        }

        Audio.Update();

        UI.Update( gameTime );

        if( _nextScene != null ) {
            TransitionScene();
        }

        if( _activeScene != null ) {
            _activeScene.Update( gameTime );
        }

        base.Update( gameTime );
    }

    protected override void Draw( GameTime gameTime ) {
        if( _activeScene != null ) {
            _activeScene.Draw( gameTime );
        }

        UI.Draw();

        base.Draw( gameTime );
    }

    public static void ChangeScene( Scene nextScene ) {
        if( _activeScene != nextScene ) {
            _nextScene = nextScene;
        }
    }

    private static void TransitionScene() {
        if( _activeScene != null ) {
            _activeScene.Dispose();
        }

        GC.Collect();

        _activeScene = _nextScene;
        _nextScene = null;

        if( _activeScene != null ) {
            _activeScene.Initialize();
        }
    }
}
