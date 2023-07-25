using Newtonsoft.Json;
using SMARTSign.Models;
using System;
using System.Collections.ObjectModel;

namespace SMARTSign;

public partial class MainPage : ContentPage
{
	public ObservableCollection<ImageCardModel> Cards = new(); 
	public MainPage()
	{
        InitializeComponent();
		LoadCollection();
		LoadCardSource();
	}

	public void LoadCardSource()
	{
		ImageCards.ItemsSource = Cards;
	}
	private void LoadCollection()
	{
        ImageCardModel card = new ImageCardModel("Running", "_HZM0QiuUS8", "running.png", 270, false);
        for (int i = 0; i < 10; i++) { Cards.Add(card); }
    }

    private void ImageCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void Image_Clicked(object sender, EventArgs e)
    {

    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		ImageButton button = (ImageButton)sender;
		var context = button.BindingContext as ImageCardModel;
		foreach (ImageCardModel card in Cards) {if(card == context){ card.IsImage = false; }}
    }
}

