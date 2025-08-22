using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonoGameLibrary.Scenes;

public abstract class Scene : IDisposable {
    public Scene() {
        Content = new ContentManager( Core.Content.ServiceProvider );
        Content.RootDirectory = Core.Content.RootDirectory;
    }

    protected ContentManager Content { get; }
    public bool IsDisposed { get; private set; }

    public void Dispose() {
        Dispose( true );
        GC.SuppressFinalize( this );
    }

    ~Scene() {
        Dispose( false );
    }

    public virtual void Initialize() {
        LoadContent();
    }

    public virtual void LoadContent() {}

    public virtual void UnloadContent() {
        Content.Unload();
    }

    public virtual void Update( GameTime gameTime ) {}

    public virtual void Draw( GameTime gameTime ) {}

    protected virtual void Dispose( bool disposing ) {
        if( IsDisposed ) {
            return;
        }

        if( disposing ) {
            UnloadContent();
            Content.Dispose();
        }
    }
}
