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
    public ObservableCollection<ImageCardModel> Cards { get; set; } = new ObservableCollection<ImageCardModel>();
    string boor = "AIzaSyA8LXtz0zgcYWJSJJY_iJMXmaCUpRlnyi4";
    string channelId = "UCACxqsL_FA-gMD2fwil7ZXA";
    string searchQuery = "Alphabet";

	public MainPage()
	{
        InitializeComponent();
        ImageCards.BindingContext = this;
        SearchField.Text = searchQuery;
        LoadCollection();
        LoadCardSource();
	}
	public void LoadCardSource()
	{
		ImageCards.ItemsSource = Cards;
	}
	private void LoadCollection()
	{
        Cards.Clear();
        searchQuery = SearchField.Text;
        //ImageCardModel card = new ImageCardModel("Running", "L-4a6BcpZL8", "running.png", 270, false);
        //for (int i = 0; i < 10; i++) { Cards.Add(card); }

        List<YouTubeInfoModel> videoList = GetVideoIds(boor, channelId, searchQuery);
        foreach (YouTubeInfoModel video in videoList)
        {
            ImageCardModel card = new ImageCardModel(video.YTID_Name, video.YTID,video.Image_URL, 270, false);
            Cards.Add(card);
        }
    }
    private void Image_Clicked(object sender, EventArgs e)
    { 
        Refresh.IsRefreshing = true;
        LoadCollection();
        LoadCardSource();
        Refresh.IsRefreshing = false;
    }
	private void ImageButton_Clicked(object sender, EventArgs e)
	{
		ImageButton button = (ImageButton)sender;
		var context = button.BindingContext as ImageCardModel;
        context.IsImage = false;
        LoadCardSource();
	}
    public static List<YouTubeInfoModel> GetVideoIds(string apiKey, string channelId, string searchQuery = "")
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
        List<YouTubeInfoModel> videoIds = new List<YouTubeInfoModel>();
        foreach (var searchResult in searchListResponse.Items)
        {
            videoIds.Add(new YouTubeInfoModel( searchResult.Snippet.Title, searchResult.Snippet.Thumbnails.Medium.Url, searchResult.Id.VideoId));
           // searchResult.Snippet.Thumbnails.Default__
        }   

        return videoIds;
    }
    private void Submit_Clicked(object sender, EventArgs e)
    {
        LoadCollection();
        LoadCardSource();
    }
    private void SearchIcon_Clicked(object sender, EventArgs e)
    {
        Cards.Clear();
        ImageCards.ItemsSource = Cards;
        
    }
    private void Refresh_Refreshing(object sender, EventArgs e)
    {
        LoadCollection();
        LoadCardSource();
        Refresh.IsRefreshing = false;
    }
}

