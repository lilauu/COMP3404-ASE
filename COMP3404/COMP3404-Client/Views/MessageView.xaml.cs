using COMP3404_Client.Services;
using COMP3404_Client.ViewModels;

namespace COMP3404_Client.Views;

/// <summary>
/// A View for a message, either sent by the user or received from the AI model.
/// </summary>
public partial class MessageView : ContentView
{
    /// <summary>
    /// A bindable property for <see cref="MessageText"/>
    /// </summary>
    public static readonly BindableProperty MessageTextProperty =
    BindableProperty.Create(nameof(MessageText), typeof(string), typeof(MessageView), string.Empty);
    /// <summary>
    /// The text to display.
    /// </summary>
    public string MessageText
    {
        get => (string)GetValue(MessageTextProperty);
        set => SetValue(MessageTextProperty, value);
    }

    /// <summary>
    /// A bindable property for <see cref="IsSender"/>
    /// </summary>
    public static readonly BindableProperty IsSenderProperty =
        BindableProperty.Create(nameof(IsSender), typeof(bool), typeof(MessageView), false,
            propertyChanging: (BindableObject bindable, object oldValue, object newValue) =>
            {
                // update the sender flow direction when this changes
                var view = (MessageView)bindable;

                view.SenderFlowDirection = view.GetFlowDirection((bool)newValue);
            });
    /// <summary>
    /// Whether or not the message was sent by the user.
    /// </summary>
    public bool IsSender
    {
        get => (bool)GetValue(IsSenderProperty);
        set => SetValue(IsSenderProperty, value);
    }

    /// <summary>
    /// A bindable property for <see cref="SenderFlowDirection"/>
    /// </summary>
    public static readonly BindableProperty SenderFlowDirectionProperty =
           BindableProperty.Create(nameof(SenderFlowDirection), typeof(FlowDirection), typeof(MessageView),
               defaultValueCreator: bindable =>
               {
                   var view = (MessageView)bindable;
                   return view.GetFlowDirection(view.IsSender);
               });
    /// <summary>
    /// The desired alignment of the text in the message box, controlled by <see cref="IsSender"/>
    /// </summary>
    public FlowDirection SenderFlowDirection
    {
        get => (FlowDirection)GetValue(SenderFlowDirectionProperty);
        private set => SetValue(SenderFlowDirectionProperty, value);
    }

    private FlowDirection GetFlowDirection(bool value) => value ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
    private TTSService m_ttsService;
    private MainPageViewModel m_mainPageViewModel;

    /// <summary>
    /// Default constructor for <see cref="MessageView"/>. Attempts to resolve service dependencies automatically.
    /// </summary>
    public MessageView()
        : this(MauiProgram.GetService<TTSService>(), MauiProgram.GetService<MainPageViewModel>())
    { }

    /// <summary>
    /// Constructor for <see cref="MessageView"/>. Typically uses Dependency Injection to resolve the required parameters.
    /// </summary>
    public MessageView(TTSService ttsService, MainPageViewModel mainPageViewModel)
    {
        m_ttsService = ttsService;
        m_mainPageViewModel = mainPageViewModel;
        InitializeComponent();
    }

    private void TTSButton_Clicked(object sender, EventArgs e)
    {
        m_ttsService.Speak(MessageText);
    }

    private async void LanguageButton_Clicked(object sender, EventArgs e)
    {
        //TODO: Translate the MessageText into a language passed in by the user
        string language = await Shell.Current.CurrentPage.DisplayPromptAsync("Question", "What language do you want to translate to?");

        m_mainPageViewModel.ActiveChat.TranslateMessage(MessageText, language);
    }
}