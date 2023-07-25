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
	private async Task<dynamic> GetVideo()
	{
        string url = "https://www.googleapis.com/youtube/v3/ ";

        // Build and Connect to OpenWeather API
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (platform; rv:geckoversion) Gecko/geckotrail Firefox/firefoxversion");
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        // Get JSON Data
        string content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<dynamic>(content);
        
    }
}

