using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject[] characters;
    private int playerIndex;
    [SerializeField] Color[] PlayerColors;
	[SerializeField] MeshRenderer playerMesh;
	[SerializeField] Material playerMat;

	[SerializeField] GameObject homeUI;
	[SerializeField] GameObject buyCharacterUI;
	[SerializeField] GameObject[] charactersPanel;
	[SerializeField] GameObject[] lockUI;
	[SerializeField] int[] characterPrice;
	[SerializeField] Text[] characterPriceText;
	// Start is called before the first frame update
	void Start()
    {
		if(PlayerPrefs.HasKey("PlayerIndex"))
		{
			playerIndex = PlayerPrefs.GetInt("PlayerIndex");
			OnCharacterSelect(playerIndex);
			playerMesh.enabled = false;
		}
		else
		{
			playerMesh.enabled = true;
			playerMat.color = playerMesh.material.color;
		}

		for (int i = 0; i < characterPriceText.Length; i++)
		{
			characterPriceText[i].text = characterPrice[i].ToString();
		}
		OnlockUI();
	}

	void OnlockUI()
	{
		for(int i = 0; i < lockUI.Length; i++)
		{
			if(PlayerPrefs.HasKey("PlayerBought" + i))
			{
				lockUI[i].SetActive(false);
			}
		}
	}

	public void BuyCharacter(int index)
	{
		if (!PlayerPrefs.HasKey("PlayerBought" + index))
		{
			bool success = CoinManager.instance.UseCoin(characterPrice[index]);
			if (!success)
				return;
			PlayerPrefs.SetInt("PlayerBought" + index, 1);
			OnCharacterSelect(index);
			homeUI.SetActive(true);
			buyCharacterUI.SetActive(false);
		}
		OnlockUI();
	}

    public void OnCharacterSelect(int index)
    {
		if (PlayerPrefs.HasKey("PlayerBought" + index))
		{
			PlayerPrefs.SetInt("PlayerIndex", index);
			playerMesh.material.color = PlayerColors[index];
			playerMat.color = PlayerColors[index];
			playerMesh.enabled = false;
			for (int i = 0; i < characters.Length; i++)
			{
				if (i == index)
					characters[i].SetActive(true);
				else
					characters[i].SetActive(false);
			}
		}
		else
		{
			homeUI.SetActive(false);
			buyCharacterUI.SetActive(true);
			for (int i = 0; i < charactersPanel.Length; i++)
			{
				if (i == index)
					charactersPanel[i].SetActive(true);
				else
					charactersPanel[i].SetActive(false);
			}
		}
	}
}
