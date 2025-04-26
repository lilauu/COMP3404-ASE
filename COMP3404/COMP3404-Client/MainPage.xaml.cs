using COMP3404_Client.ViewModels;

namespace COMP3404_Client;
public partial class MainPage : ContentPage
{
    public MainPageViewModel ViewModel { get; private set; }
    public MainPage(MainPageViewModel viewModel)
    {
        ViewModel = viewModel;
        BindingContext = ViewModel;
        InitializeComponent();
    }
}
