using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
	private Text coinText;
	public int coin;
	void Awake()
	{
		coinText = GameObject.Find("CoinText").GetComponent<Text>();
		MakeSingleton();
	}
	// Start is called before the first frame update
	void Start()
    {
		coin = PlayerPrefs.GetInt("coin");
		coinText.text = coin.ToString();
	}

    // Update is called once per frame
    void Update()
    {
		if (coinText == null)
		{
			coinText = GameObject.Find("CoinText").GetComponent<Text>();
			coinText.text = coin.ToString();
		}
	}
	public void AddCoin(int amount)
	{
		coin += amount;
		coinText.text = coin.ToString();
		PlayerPrefs.SetInt("coin", coin);
	}

	public bool UseCoin(int amount)
	{
		bool success = false;
		if(amount <= coin)
		{
			coin -= amount;
			success = true;
		}
		coinText.text = coin.ToString();
		PlayerPrefs.SetInt("coin", coin);
		return success;
	}
	void MakeSingleton()
	{
		if (instance != null)
			Destroy(gameObject);
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

}
