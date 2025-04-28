namespace COMP3404_Client.Views;

/// <summary>
/// View representing the page header
/// </summary>
public partial class HeaderView : ContentView
{
    /// <summary>
    /// A bindable property for <see cref="Title"/>
    /// </summary>
    public static readonly BindableProperty TitleProperty =
		BindableProperty.Create(nameof(Title), typeof(string), typeof(HeaderView), string.Empty);
	/// <summary>
	/// The title of the page.
	/// </summary>
	public string Title
	{
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}

	/// <summary>
	/// Constructor for <see cref="HeaderView"/>
	/// </summary>
	public HeaderView()
	{
		InitializeComponent();
	}
}