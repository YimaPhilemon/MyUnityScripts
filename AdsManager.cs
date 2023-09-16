using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
	public static AdsManager ads;
	private BannerView bannerView;
	private RewardedAd rewardedAd;

	[Header("AD ID")]
	[SerializeField]string adUnitId = "************************************";
	[SerializeField] string rewardedAdUnitId = "************************************";

	private Player player;
	private void Awake()
	{
		if(ads == null)
		{
			ads = this;
		}
	}
	// Start is called before the first frame update
	void Start()
    {
		player = Player.instance;
		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(initStatus => { });

		this.RequestBanner();
		this.rewardedAd = new RewardedAd(rewardedAdUnitId);
	}

	private void RequestBanner()
	{
		/*#if UNITY_ANDROID
		
		#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
		#else
            string adUnitId = "unexpected_platform";
		#endif	  */

		// Create a 320x50 banner at the top of the screen.
		//this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the banner with the request.
		this.bannerView.LoadAd(request);
	}

	public void LoadRewardedAds()
	{

		// Create an empty ad request.
		//AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded ad with the request.
		//this.rewardedAd = new RewardedAd(rewardedAdUnitId);

		// Called when an ad request has successfully loaded.
		this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		// Called when an ad is shown.
		this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded ad with the request.
		this.rewardedAd.LoadAd(request);
	}

	public void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdLoaded event received");
		UserChoseToWatchAd();
		player.CancleDied();
	}

	public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print(									 
			"HandleRewardedAdFailedToLoad event received with message: "
							 + args.LoadAdError.GetMessage());
		player.playerDied();
	}

	public void HandleRewardedAdOpening(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdOpening event received");
		player.CancleDied();
	}

	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToShow event received with message: "
							 + args.AdError.GetMessage());
		player.playerDied();
	}

	public void HandleRewardedAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdClosed event received");
	}

	public void HandleUserEarnedReward(object sender, Reward args)
	{
		player.RespawnSuccess();
	}

	private void UserChoseToWatchAd()
	{
		if (this.rewardedAd.IsLoaded())
		{
			this.rewardedAd.Show();
		}
	}
}
