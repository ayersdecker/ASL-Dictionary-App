using Newtonsoft.Json;
using SMARTSign.Models;
using System;
using System.Collections.ObjectModel;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace SMARTSign;

public partial class MainPage : ContentPage
{
	public ObservableCollection<ImageCardModel> Cards = new();

    // Replace 'YOUR_API_KEY' with your actual YouTube Data API key
    string apiKey = "AIzaSyA8LXtz0zgcYWJSJJY_iJMXmaCUpRlnyi4";
    // Replace 'CHANNEL_ID' with the ID of the specific channel you want to search within
    string channelId = "UCACxqsL_FA-gMD2fwil7ZXA";
    // Replace 'SEARCH_QUERY' with your search query (optional)
    string searchQuery = "Running";

    
    

	public MainPage()
	{
        InitializeComponent();
		
        
        
	}

	public void LoadCardSource()
	{
		ImageCards.ItemsSource = Cards;
	}
	private void LoadCollection()
	{
        Cards.Clear();
        searchQuery = Search.Text;
        //ImageCardModel card = new ImageCardModel("Running", "L-4a6BcpZL8", "running.png", 270, false);
        //for (int i = 0; i < 10; i++) { Cards.Add(card); }

        List<string> videoIds = GetVideoIds(apiKey, channelId, searchQuery);
        foreach (string videoId in videoIds)
        {
            ImageCardModel card = new ImageCardModel("Running", videoId, "running.png", 270, false);
            Cards.Add(card);
        }
    }

    private void ImageCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void Image_Clicked(object sender, EventArgs e)
    {
        LoadCollection();
        LoadCardSource();
    }

	private void ImageButton_Clicked(object sender, EventArgs e)
	{
		ImageButton button = (ImageButton)sender;
		var context = button.BindingContext as ImageCardModel;
		foreach (ImageCardModel card in Cards) { if (card == context) { card.IsImage = false; } }

	}

    public static List<string> GetVideoIds(string apiKey, string channelId, string searchQuery = "")
    {
        // Create a YouTube Data API service
        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        {
            ApiKey = apiKey
        });

        // Set the search parameters and perform the search
        var searchListRequest = youtubeService.Search.List("snippet");
        searchListRequest.Q = searchQuery; // The search query (e.g., keywords, etc.)
        searchListRequest.ChannelId = channelId; // The ID of the specific channel to search within
        searchListRequest.Type = "video"; // We only want video results
        searchListRequest.MaxResults = 50; // Maximum number of results per API call (default is 5, max is 50)

        // Execute the search and get the response
        SearchListResponse searchListResponse = searchListRequest.Execute();

        // Extract VideoIDs from the API response
        List<string> videoIds = new List<string>();
        foreach (var searchResult in searchListResponse.Items)
        {
            videoIds.Add(searchResult.Id.VideoId);
        }   

        return videoIds;
    }

    private void Submit_Clicked(object sender, EventArgs e)
    {
        LoadCollection();
        LoadCardSource();
    }
}

