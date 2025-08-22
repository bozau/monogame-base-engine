using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Input;

public class InputManager {
    public InputManager() {
        Keyboard = new KeyboardInfo();
        Mouse = new MouseInfo();

        GamePads = new GamePadInfo[4];
        for( var i = 0; i < 4; i++ ) {
            GamePads[ i ] = new GamePadInfo( ( PlayerIndex )i );
        }
    }

    public KeyboardInfo Keyboard { get; }

    public MouseInfo Mouse { get; }

    public GamePadInfo[] GamePads { get; }

    public void Update( GameTime gameTime ) {
        Keyboard.Update();
        Mouse.Update();

        for( var i = 0; i < 4; i++ ) {
            GamePads[ i ].Update( gameTime );
        }
    }
}
