namespace SMARTSign;

public partial class NoInternetPage : ContentPage
{
	public NoInternetPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            Navigation.PushAsync(new MainPage(), false);
        }
    }
}