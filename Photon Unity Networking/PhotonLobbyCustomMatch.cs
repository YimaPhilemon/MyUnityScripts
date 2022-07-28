using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonLobbyCustomMatch : MonoBehaviourPunCallbacks	, ILobbyCallbacks
{
    public static PhotonLobbyCustomMatch lobby;

	public Text status;
	public string roomName;
	public int roomSize;
	public GameObject roomLisingPrefab;
	public Transform roomsPanel;
	public GameObject offlinebt;
	public GameObject lobbypanel;

	public List<RoomInfo> roomListings;

	private void Awake()
	{
        lobby = this;
	}
	// Start is called before the first frame update
	void Start()
    {
        PhotonNetwork.ConnectUsingSettings();//connects to master photon server
		roomListings = new List<RoomInfo>();
    }

	public override void OnConnectedToMaster()
	{
		status.text = "Connected";
		status.color = Color.green;

		PhotonNetwork.AutomaticallySyncScene = true;

		offlinebt.SetActive(false);
		lobbypanel.SetActive(true);
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		base.OnRoomListUpdate(roomList);
		//RemoveRoomListings();
		int tempIndex;
		foreach(RoomInfo room in roomList)
		{
			if(roomListings != null)
			{
				tempIndex = roomListings.FindIndex(ByName(room.Name));
			}
			else
			{
				tempIndex = -1;
			}

			if(tempIndex != -1)
			{
				roomListings.RemoveAt(tempIndex);
				Destroy(roomsPanel.GetChild(tempIndex).gameObject);
			}
			else
			{
				roomListings.Add(room);
				ListRoom(room);
			}
		}
	}

	static System.Predicate<RoomInfo> ByName(string name)
	{
		return delegate (RoomInfo room)
		{
			return room.Name == name;
		};
	}

	void RemoveRoomListings()
	{
		int i = 0;
		while(roomsPanel.childCount != 0)
		{
			Destroy(roomsPanel.GetChild(i).gameObject);
			i++;
		}
	}

	void ListRoom(RoomInfo room)
	{
		if(room.IsOpen && room.IsVisible)
		{
			GameObject tempListing = Instantiate(roomLisingPrefab, roomsPanel);
			RoomButton tempButton = tempListing.GetComponent<RoomButton>();
			tempButton.roomName = room.Name;
			tempButton.roomSize = room.MaxPlayers;
			tempButton.SetRoom();
		}
	}

	


	public void CreateRoom()
	{
		status.text = "Room Created";
		RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize};
		PhotonNetwork.CreateRoom(roomName, roomOps);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		status.text = "Change Room Name";
	}

	public void OnRoomNameChanged(string nameIn)
	{
		roomName = nameIn;
	}

	public void OnRoomSizeeChanged(string sizeIn)
	{
		roomSize = int.Parse(sizeIn);
	}

	public void JoinlobbyOnClick()
	{
		if(!PhotonNetwork.InLobby)
		{
			PhotonNetwork.JoinLobby();
		}
	}
}
