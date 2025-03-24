namespace COMP3404_Client
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void ProfileButton_Clicked(object sender, EventArgs e)
        {
            // todo: static page?
            Navigation.PushAsync(new NewPage1());
        }
    }

}
