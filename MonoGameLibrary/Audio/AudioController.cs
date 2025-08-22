using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoGameLibrary.Audio;

public class AudioController : IDisposable {
    private readonly List< SoundEffectInstance > _activeSoundEffectInstances;

    private float _masterVolume;
    private float _songVolume;
    private float _soundEffectsVolume;

    public AudioController() {
        _activeSoundEffectInstances = new List< SoundEffectInstance >();

        SongVolume = 0.2f;
        SoundEffectsVolume = 0.2f;
        MasterVolume = 0.2f;
    }

    public bool AreAllMuted { get; private set; }
    public bool AreSoundEffectsMuted { get; private set; }
    public bool AreSongsMuted { get; private set; }

    public float MasterVolume {
        get => _masterVolume;
        set {
            _masterVolume = Math.Clamp( value, 0.0f, 1.0f );
            MediaPlayer.Volume = _masterVolume * _songVolume;
            SoundEffect.MasterVolume = _masterVolume * _soundEffectsVolume;
        }
    }

    public float SongVolume {
        get => _songVolume;
        set {
            _songVolume = Math.Clamp( value, 0.0f, 1.0f );
            MediaPlayer.Volume = _songVolume * _masterVolume;
        }
    }

    public float SoundEffectsVolume {
        get => _soundEffectsVolume;
        set {
            _soundEffectsVolume = Math.Clamp( value, 0.0f, 1.0f );
            SoundEffect.MasterVolume = _soundEffectsVolume * _masterVolume;
        }
    }

    public bool IsDisposed { get; private set; }

    public void Dispose() {
        Dispose( true );
        GC.SuppressFinalize( this );
    }

    ~AudioController() {
        Dispose( false );
    }

    public void Update() {
        for( var i = _activeSoundEffectInstances.Count - 1; i >= 0; i-- ) {
            var instance = _activeSoundEffectInstances[ i ];

            if( instance.State == SoundState.Stopped ) {
                if( !instance.IsDisposed ) {
                    instance.Dispose();
                }

                _activeSoundEffectInstances.RemoveAt( i );
            }
        }
    }

    public SoundEffectInstance PlaySoundEffect( SoundEffect soundEffect ) {
        return PlaySoundEffect( soundEffect, 1.0f, 0.0f, 0.0f, false );
    }

    public SoundEffectInstance PlaySoundEffect(
        SoundEffect soundEffect,
        float volume,
        float pitch,
        float pan,
        bool isLooped
    ) {
        var soundEffectInstance = soundEffect.CreateInstance();

        soundEffectInstance.Volume = volume;
        soundEffectInstance.Pitch = pitch;
        soundEffectInstance.Pan = pan;
        soundEffectInstance.IsLooped = isLooped;

        soundEffectInstance.Play();

        _activeSoundEffectInstances.Add( soundEffectInstance );

        return soundEffectInstance;
    }

    public void PlaySong( Song song, bool isLooped = true ) {
        if( MediaPlayer.State == MediaState.Playing ) {
            MediaPlayer.Stop();
        }

        MediaPlayer.Play( song );
        MediaPlayer.IsRepeating = isLooped;
    }

    public void PauseAudio() {
        MediaPlayer.Pause();

        for( var i = _activeSoundEffectInstances.Count - 1; i >= 0; i-- ) {
            _activeSoundEffectInstances[ i ].Pause();
        }
    }

    public void ResumeAudio() {
        MediaPlayer.Resume();

        for( var i = _activeSoundEffectInstances.Count - 1; i >= 0; i-- ) {
            _activeSoundEffectInstances[ i ].Resume();
        }
    }

    private void MuteAllAudio() {
        MediaPlayer.Volume = 0.0f;
        SoundEffect.MasterVolume = 0.0f;

        AreAllMuted = true;
    }

    private void MuteSoundEffects() {
        AreSoundEffectsMuted = true;
    }

    private void MuteSongs() {
        AreSongsMuted = true;
    }

    private void UnmuteAllAudio() {
        MediaPlayer.Volume = _songVolume;
        SoundEffect.MasterVolume = _soundEffectsVolume;

        AreAllMuted = false;
    }

    private void UnmuteSoundEffects() {
        SoundEffect.MasterVolume = _soundEffectsVolume;

        AreSoundEffectsMuted = false;
    }

    private void UnmuteSongs() {
        MediaPlayer.Volume = _songVolume;

        AreSongsMuted = false;
    }

    public void ToggleAllAudioMute() {
        if( AreAllMuted ) {
            UnmuteAllAudio();
            return;
        }

        MuteAllAudio();
    }

    public void ToggleSoundEffectsMute() {
        if( AreSoundEffectsMuted ) {
            UnmuteSoundEffects();
            return;
        }

        MuteSoundEffects();
    }

    public void ToggleSongsMute() {
        if( AreSongsMuted ) {
            UnmuteSongs();
            return;
        }

        MuteSongs();
    }

    protected void Dispose( bool disposing ) {
        if( IsDisposed ) {
            return;
        }

        if( disposing ) {
            foreach( var soundEffectInstance in _activeSoundEffectInstances ) {
                soundEffectInstance.Dispose();
            }

            _activeSoundEffectInstances.Clear();
        }

        IsDisposed = true;
    }
}
