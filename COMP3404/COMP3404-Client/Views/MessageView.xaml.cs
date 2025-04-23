namespace COMP3404_Client.Views;

public partial class MessageView : ContentView
{
    public static readonly BindableProperty MessageTextProperty =
        BindableProperty.Create(nameof(MessageText), typeof(string), typeof(MessageView), string.Empty);
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