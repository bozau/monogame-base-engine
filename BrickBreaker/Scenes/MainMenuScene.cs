using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;

namespace BrickBreaker.Scenes;

public class MainMenuScene : Scene {
    private Song _mainMenuSong;

    public override void Initialize() {
        base.Initialize();

        Core.UI.SetActiveScreen( "MainMenu" );

        Core.Audio.PlaySong( _mainMenuSong );
    }

    public override void LoadContent() {
        base.LoadContent();

        _mainMenuSong = Content.Load< Song >( "sounds/music/Epic Somewhere Main" );
    }

    public override void Update( GameTime gameTime ) {
        if( Core.Input.Keyboard.WasKeyJustPressed( Keys.Enter ) ) {
            Core.ChangeScene( new SplashScreen() );
        }
    }

    public override void Draw( GameTime gameTime ) {
        Core.GraphicsDevice.Clear( Color.ForestGreen );
    }
}
