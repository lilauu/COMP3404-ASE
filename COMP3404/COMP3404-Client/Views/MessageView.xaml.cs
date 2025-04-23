namespace COMP3404_Client.Views;

/// <summary>
/// A View for a message, either sent by the user or received from the AI model.
/// </summary>
public partial class MessageView : ContentView
{
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

    public static readonly BindableProperty IsSenderProperty =
        BindableProperty.Create(nameof(IsSender), typeof(bool), typeof(MessageView), false,
            propertyChanging: (BindableObject bindable, object oldValue, object newValue) =>
            {
                var view = (MessageView)bindable;

                view.HorizontalTextAlignment = view.GetAlignment((bool)newValue);
            });
    /// <summary>
    /// Whether or not the message was sent by the user.
    /// </summary>
    public bool IsSender
    {
        get => (bool)GetValue(IsSenderProperty);
        set => SetValue(IsSenderProperty, value);
    }

    public static readonly BindableProperty HorizontalTextAlignmentProperty =
           BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(MessageView),
               defaultValueCreator: bindable =>
               {
                   var view = (MessageView)bindable;
                   return view.GetAlignment(view.IsSender);
               });
    /// <summary>
    /// The desired alignment of the text in the message box, controlled by <see cref="IsSender"/>
    /// </summary>
    public TextAlignment HorizontalTextAlignment
    {
        get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
        private set => SetValue(HorizontalTextAlignmentProperty, value);
    }

    private TextAlignment GetAlignment(bool value) => value ? TextAlignment.End : TextAlignment.Start;

    public MessageView()
	{
		InitializeComponent();
	}
}