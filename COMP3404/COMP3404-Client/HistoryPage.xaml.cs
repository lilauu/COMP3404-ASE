using COMP3404_Client.ViewModels;

namespace COMP3404_Client;

/// <summary>
/// View representing the history page, where saved chats are viewed.
/// </summary>
public partial class HistoryPage : ContentPage
{
	/// <summary>
	/// The ViewModel of the page.
	/// </summary>
	public HistoryPageViewModel ViewModel { get; private set; }

    /// <summary>
    /// Constructor for <see cref="HistoryPage"/>. Typically uses Dependency Injection to resolve the required parameters.
    /// </summary>
    public HistoryPage(HistoryPageViewModel viewModel)
	{
		ViewModel = viewModel;
		BindingContext = ViewModel;
		InitializeComponent();
	}
}