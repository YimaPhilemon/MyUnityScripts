using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

	public Text status;
	public GameObject battleButtom;
	public GameObject cancleButton;

	private void Awake()
	{
        lobby = this;
	}
	// Start is called before the first frame update
	void Start()
    {
        PhotonNetwork.ConnectUsingSettings();//connects to master photon server
    }

	public override void OnConnectedToMaster()
	{
		status.text = "Connected";
		status.color = Color.green;

		
		PhotonNetwork.JoinLobby();
		battleButtom.SetActive(true);
	}

	public override void OnJoinedLobby()
	{
		status.text = "Lobby joined";
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public void OnBattleButtonClick()
	{
		PhotonNetwork.JoinRandomRoom();
		status.text = "Searching for Room";
		battleButtom.SetActive(false);
		cancleButton.SetActive(true);
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		status.text = "Tried to join Room but failed";
		CreateRoom();

	}

	void CreateRoom()
	{
		status.text = "Room Created";
		int randomRoomNumber = Random.Range(1, 10000);
		RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSettings.multiplayerSettings.maxPlayers };
		PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		status.text = "Tried Creating Room but failed";
		CreateRoom();
	}
	public void OnCancledButtonClicked()
	{
		battleButtom.SetActive(true);
		cancleButton.SetActive(false);
		PhotonNetwork.LeaveRoom();

	}

	
}
