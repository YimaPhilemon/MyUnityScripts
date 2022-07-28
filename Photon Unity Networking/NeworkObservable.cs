using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NeworkObservable : MonoBehaviourPun, IPunObservable
{
	public NetworkWeaponManager NWM;
	public NetworkShooting[] NS;
	WeaponState state;

	private void Start()
	{
		NWM = GetComponent<NetworkWeaponManager>();
		state = NWM.state;
	}
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.IsWriting)
		{
			stream.SendNext(NWM.state);
		}
		else
		{
			state = (WeaponState)stream.ReceiveNext();
			NWM.state = state;
		}
	}

	
}
