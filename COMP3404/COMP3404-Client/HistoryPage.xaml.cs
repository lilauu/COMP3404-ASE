
using COMP3404_Client.ViewModels;

namespace COMP3404_Client;

public partial class HistoryPage : ContentPage
{
	public HistoryPageViewModel ViewModel { get; private set; }
	public HistoryPage(HistoryPageViewModel viewModel)
	{
		ViewModel = viewModel;
		BindingContext = ViewModel;
		InitializeComponent();
	}
}