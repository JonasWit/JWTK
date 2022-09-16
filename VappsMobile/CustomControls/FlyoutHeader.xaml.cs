namespace VappsMobile.CustomControls;

public partial class FlyoutHeader : ContentView
{
    public FlyoutHeader(string email)
    {
        InitializeComponent();
        lblUserName.Text = email;
    }
}