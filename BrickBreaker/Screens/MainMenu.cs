using System;
using BrickBreaker.Components.Settings;
using BrickBreaker.Scenes;
using MonoGameLibrary;

namespace BrickBreaker.Screens;

internal partial class MainMenu {
    partial void CustomInitialize() {
        // Set initial values for sliders based on settings
        OptionsMenuInstance.MasterVolumeSlider.SliderPercent = ( ( GameSettings )Core.Settings.GetSettingsObject() ).SoundSettings.MasterVolume * 100.0f;
        OptionsMenuInstance.SongVolumeSlider.SliderPercent = ( ( GameSettings )Core.Settings.GetSettingsObject() ).SoundSettings.SongVolume * 100.0f;
        OptionsMenuInstance.SoundEffectsVolumeSlider.SliderPercent = ( ( GameSettings )Core.Settings.GetSettingsObject() ).SoundSettings.SoundEffectsVolume * 100.0f;


        // Setup custom event handlers for UI Elements
        SwitchToSplashScreenButton.Click += ( _, _ ) => { Core.ChangeScene( new SplashScreen() ); };
        OpenOptionsMenuButton.Click += ( _, _ ) => { OptionsMenuInstance.IsVisible = true; };
        OptionsMenuInstance.CloseOptionsMenuButton.Click += ( _, _ ) => { OptionsMenuInstance.IsVisible = false; };
        
        OptionsMenuInstance.MasterVolumeSlider.ValueChangedByUi += ( _, _ ) => {
            // Will update both the audio settings live, and the settings file
            Core.Audio.MasterVolume = OptionsMenuInstance.MasterVolumeSlider.SliderPercent / 100.0f;
            ( ( GameSettings )Core.Settings.GetSettingsObject() ).SoundSettings.MasterVolume = ( float )OptionsMenuInstance.MasterVolumeSlider.Value / 100.0f;
            Core.Settings.SaveSettings();
        };
        
        OptionsMenuInstance.SongVolumeSlider.ValueChangedByUi += ( _, _ ) => {
            // Will update both the audio settings live, and the settings file
            Core.Audio.SongVolume = OptionsMenuInstance.SongVolumeSlider.SliderPercent / 100.0f;
            ( ( GameSettings )Core.Settings.GetSettingsObject() ).SoundSettings.SongVolume = ( float )OptionsMenuInstance.SongVolumeSlider.Value / 100.0f;
            Core.Settings.SaveSettings();
        };
        
        OptionsMenuInstance.SoundEffectsVolumeSlider.ValueChangedByUi += ( _, _ ) => {
            // Will update both the audio settings live, and the settings file
            Core.Audio.SoundEffectsVolume = OptionsMenuInstance.SoundEffectsVolumeSlider.SliderPercent / 100.0f;
            ( ( GameSettings )Core.Settings.GetSettingsObject() ).SoundSettings.SoundEffectsVolume = ( float )OptionsMenuInstance.SoundEffectsVolumeSlider.Value / 100.0f;
            Core.Settings.SaveSettings();
        };
    }
}
