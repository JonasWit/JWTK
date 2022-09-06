namespace VappsMobile.CustomControls;

public partial class FlyoutHeader : ContentView
{
    public FlyoutHeader()
    {
        InitializeComponent();
        if (App.UserInfo is not null)
        {
            lblUserName.Text = App.UserInfo.Email;
        }
    }
}