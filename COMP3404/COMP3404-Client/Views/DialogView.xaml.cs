using System.Collections;

namespace COMP3404_Client.Views;

/// <summary>
/// View representing a collection of messages, aka a dialogue
/// </summary>
public partial class DialogView : ContentView
{
    /// <summary>
    /// A bindable property for <see cref="ItemsSource"/>
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(DialogView), null);
    /// <summary>
    /// The collection representing the message in the dialogue
    /// </summary>
    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Constructor for <see cref="DialogView"/>
    /// </summary>
    public DialogView()
    {
        InitializeComponent();
    }
}