using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class NetwookPooling :MonoBehaviourPun, IPunObservable
{
    public static NetwookPooling Instance;

    PhotonView PV;

    [System.Serializable]
    public class Pool
	{
        public string tag;
        public string prefabName;
        public int size;
	}
    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

   

	// Start is called before the first frame update
	void Start()
    {
        PV = GetComponent<PhotonView>();

       
        if (PV.IsMine)
		{
            if (PhotonNetwork.IsMasterClient)
            {
                poolDictionary = new Dictionary<string, Queue<GameObject>>();

                 foreach (Pool pool in pools)
                 {
                    Queue<GameObject> objectpool = new Queue<GameObject>();
               
                    for (int i = 0; i < pool.size; i++)
                    {
                        GameObject obj = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", pool.prefabName), transform.position,
                        transform.rotation, 0);
                        obj.SetActive(false);
                        objectpool.Enqueue(obj);
                    }

                    poolDictionary.Add(pool.tag, objectpool);

                 }
				
            }
            
        }

    }

    [PunRPC]
    public void CreatePools()
	{
       
       poolDictionary = new Dictionary<string, Queue<GameObject>>();

       foreach (Pool pool in pools)
            {
                Queue<GameObject> objectpool = new Queue<GameObject>();
                if (PhotonNetwork.IsMasterClient)
                {
                    for (int i = 0; i < pool.size; i++)
                    {
                        GameObject obj = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", pool.prefabName), transform.position,
                        transform.rotation, 0);
                        obj.SetActive(false);
                        objectpool.Enqueue(obj);
                    }

                    

                }
            poolDictionary.Add(pool.tag, objectpool);
        }
        
    }
    public void InstantiateFromPool(string tag, Vector3 Position, Quaternion rotation, bool activate, Transform spawner, float force)
	{
        if (!poolDictionary.ContainsKey(tag))
		{
            Debug.Log("Pool with tag" + tag + " doesn't excist.");
            return;
        }
            

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = Position;
        objectToSpawn.transform.rotation = rotation;
        if(activate)
		{
            Rigidbody2D rb = objectToSpawn.GetComponent<Rigidbody2D>();
            rb.AddForce(spawner.up * force, ForceMode2D.Impulse);
        }

        //poolDictionary[tag].Enqueue(objectToSpawn);
	}

    public void DestroyToPool(string tag, GameObject objToDestroy)
	{
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag" + tag + " doesn't excist.");
            return;
        }


        poolDictionary[tag].Enqueue(objToDestroy);

        objToDestroy.SetActive(false);
        Rigidbody2D rb = objToDestroy.GetComponent<Rigidbody2D>();
        rb.Sleep();
        // objectToSpawn.transform.position = Position;
        // objectToSpawn.transform.rotation = rotation;

    }

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.IsWriting)
		{
            //stream.SendNext(poolDictionary);
		}
		else
		{
           // poolDictionary = (Dictionary<string, Queue<GameObject>>)stream.ReceiveNext();
		}
	}
}
