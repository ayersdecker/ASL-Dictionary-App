namespace SMARTSign;

public partial class NoInternetPage : ContentPage
{
	public NoInternetPage()
	{
		InitializeComponent();
	}

    // Checks if Internet has been Detected
    private void Button_Clicked(object sender, EventArgs e)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            Navigation.PushAsync(new MainPage(), false);
        }
    }
}