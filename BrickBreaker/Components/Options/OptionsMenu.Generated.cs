//Code for Options/OptionsMenu (Container)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using BrickBreaker.Components.Controls;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace BrickBreaker.Components.Options;
partial class OptionsMenu : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("Options/OptionsMenu");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new OptionsMenu(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(OptionsMenu)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("Options/OptionsMenu", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ButtonStandard CloseOptionsMenuButton { get; protected set; }
    public Slider MasterVolumeSlider { get; protected set; }
    public Slider SoundEffectsVolumeSlider { get; protected set; }
    public Slider SongVolumeSlider { get; protected set; }
    public Label MasterVolumeSliderLabel { get; protected set; }
    public Label SoundEffectsVolumeSliderLabel { get; protected set; }
    public Label SongVolumeSliderLabel { get; protected set; }
    public ContainerRuntime MasterVolumeContainer { get; protected set; }
    public ContainerRuntime SoundEffectsVolumeContainer { get; protected set; }
    public ContainerRuntime SongVolumeContainer { get; protected set; }

    public OptionsMenu(InteractiveGue visual) : base(visual)
    {
    }
    public OptionsMenu()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        CloseOptionsMenuButton = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"CloseOptionsMenuButton");
        MasterVolumeSlider = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Slider>(this.Visual,"MasterVolumeSlider");
        SoundEffectsVolumeSlider = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Slider>(this.Visual,"SoundEffectsVolumeSlider");
        SongVolumeSlider = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Slider>(this.Visual,"SongVolumeSlider");
        MasterVolumeSliderLabel = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Label>(this.Visual,"MasterVolumeSliderLabel");
        SoundEffectsVolumeSliderLabel = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Label>(this.Visual,"SoundEffectsVolumeSliderLabel");
        SongVolumeSliderLabel = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Label>(this.Visual,"SongVolumeSliderLabel");
        MasterVolumeContainer = this.Visual?.GetGraphicalUiElementByName("MasterVolumeContainer") as ContainerRuntime;
        SoundEffectsVolumeContainer = this.Visual?.GetGraphicalUiElementByName("SoundEffectsVolumeContainer") as ContainerRuntime;
        SongVolumeContainer = this.Visual?.GetGraphicalUiElementByName("SongVolumeContainer") as ContainerRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
