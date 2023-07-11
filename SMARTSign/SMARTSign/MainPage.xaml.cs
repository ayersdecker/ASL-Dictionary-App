using SMARTSign.Models;
using System.Collections.ObjectModel;

namespace SMARTSign;

public partial class MainPage : ContentPage
{
	public ObservableCollection<ImageCardModel> Cards = new(); 
	public MainPage()
	{
        InitializeComponent();
		LoadCardTesting();
	}

	public void LoadCardTesting()
	{
		ImageCardModel card = new ImageCardModel("Running", "ytidhere", "running.png");
		for(int i = 0; i < 10; i++) { Cards.Add(card); }
		ImageCards.ItemsSource = Cards;
	}

    private void ImageCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void Image_Clicked(object sender, EventArgs e)
    {

    }
}

