using System;
using Gum.DataTypes;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGameGum;

namespace MonoGameLibrary.UserInterface;

public class UIManager {
    private GumService GumUI => GumService.Default;

    private GumProjectSave CurrentGumInterface { get; set; }

    private ScreenSave CurrentScreen { get; set; }

    private GraphicalUiElement CurrentScreenRuntime { get; set; }

    public bool IsDisposed { get; private set; }

    ~UIManager() {
        Dispose( false );
    }

    public void LoadUI( string gumxFile ) {
        CurrentGumInterface = GumUI.Initialize( Core.Instance, gumxFile );
    }

    public void Update( GameTime gameTime ) {
        GumUI.Update( gameTime );
    }

    public void Draw() {
        GumUI.Draw();
    }

    public void SetActiveScreen( string screenName ) {
        GumService.Default.Root.Children.Clear();

        CurrentScreen = CurrentGumInterface.Screens.Find( item => item.Name == screenName );
        CurrentScreenRuntime = CurrentScreen.ToGraphicalUiElement();
        CurrentScreenRuntime.AddToRoot();
    }

    protected void Dispose( bool disposing ) {
        if( IsDisposed ) {
            return;
        }

        if( disposing ) {}

        IsDisposed = true;
    }

    public void Dispose() {
        Dispose( true );
        GC.SuppressFinalize( this );
    }
}
