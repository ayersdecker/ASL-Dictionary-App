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
    // The Flip Card collection to populate
    public ObservableCollection<ImageCardModel> Cards { get; } = new();
    // YouTube Data V3 BOOR ( Don't Adjust )
    string boor = "AIzaSyA8LXtz0zgcYWJSJJY_iJMXmaCUpRlnyi4";
    // YouTube Channel ID ( Currently = ASL Dictionary )
    string channelId = "UCACxqsL_FA-gMD2fwil7ZXA";
    // Loading Search Query
    string searchQuery = "";

	public MainPage()
	{
        InitializeComponent();
        ImageCards.BindingContext = this;   // Binding XAML Elements in CollectionView
        //LoadCollection();                   // Fills Cards with Img/Videos form Channel
	}
    /// <summary>
    /// Rebind method for the CollectionView Source
    /// </summary>
	public void LoadCardSource()
	{
		ImageCards.ItemsSource = Cards;
	}
    /// <summary>
    /// Loads 'Cards' based on current form properties
    /// </summary>
	private void LoadCollection()
	{
        Cards.Clear();
        searchQuery = SearchField.Text;

        List<YouTubeInfoModel> videoList = GetVideoIds(boor, channelId, searchQuery);
        foreach (YouTubeInfoModel video in videoList)
        {
            if(Cards.Count < 10)
            {
                ImageCardModel card = new ImageCardModel(video.YTID_Name, video.YTID,video.Image_URL, 270, true);
                Cards.Add(card);
            }
        }
    }
    /// <summary>
    /// Click action to flip the image to a video
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	private void ImageButton_Clicked(object sender, EventArgs e)
	{
		ImageButton button = (ImageButton)sender;
		var context = button.BindingContext as ImageCardModel;
        context.IsImage = false;
        //LoadCardSource();
	}
    /// <summary>
    /// Method to gather video ids from the selected channel via a search query
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="channelId"></param>
    /// <param name="searchQuery"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Submit action tied to the forward arrow icon
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Submit_Clicked(object sender, EventArgs e)
    {
        LoadCollection();
        LoadCardSource();
    }
    /// <summary>
    /// Offers the ability for the user to refresh the screen (not nessesary)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Refresh_Refreshing(object sender, EventArgs e)
    {
        LoadCollection();
        LoadCardSource();
        Refresh.IsRefreshing = false;
    }
}

