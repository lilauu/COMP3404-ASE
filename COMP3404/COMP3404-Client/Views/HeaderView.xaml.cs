namespace COMP3404_Client.Views;

public partial class HeaderView : ContentView
{
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

	public HeaderView()
	{
		InitializeComponent();
	}
}