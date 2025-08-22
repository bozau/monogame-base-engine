using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;

namespace BrickBreaker.Scenes;

public class TestScreen : Scene {
    public override void Initialize() {
        base.Initialize();

        Core.UI.SetActiveScreen( "TestScreen" );
    }

    public override void LoadContent() {}

    public override void Update( GameTime gameTime ) {
        if( Core.Input.Keyboard.WasKeyJustPressed( Keys.Enter ) ) {
            Core.ChangeScene( new MainMenuScene() );
        }
    }

    public override void Draw( GameTime gameTime ) {
        Core.GraphicsDevice.Clear( Color.Crimson );
    }
}
