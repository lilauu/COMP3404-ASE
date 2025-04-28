using COMP3404_Client.ViewModels;

namespace COMP3404_Client;

/// <summary>
/// View representing the landing page, also referred to as the home or main page
/// </summary>
public partial class MainPage : ContentPage
{
    /// <summary>
    /// The ViewModel of the page.
    /// </summary>
    public MainPageViewModel ViewModel { get; private set; }

    /// <summary>
    /// Constructor for <see cref="MainPage"/>. Typically uses Dependency Injection to resolve the required parameters.
    /// </summary>
    public MainPage(MainPageViewModel viewModel)
    {
        ViewModel = viewModel;
        BindingContext = ViewModel;
        InitializeComponent();
    }
}
