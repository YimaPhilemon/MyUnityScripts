using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    public static PoolSystem instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
		public int size;
	}

	public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
	private Dictionary<GameObject, string> activeObjects = new Dictionary<GameObject, string>();

	#region Singleton/Pool Init
	private void Awake()
	{
		if(instance == null)
        {
            instance = this;
        }

		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();
			for (int i = 0; i < pool.size; i++)
			{
				GameObject obj = Instantiate(pool.prefab, transform);
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}

			poolDictionary.Add(pool.tag, objectPool);
		}
	}
	#endregion

	public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
            return null;

		// Check if there are any inactive objects in the pool
		if (poolDictionary[tag].Count == 0)
			return null;

		GameObject objectToSpawn = poolDictionary[tag].Dequeue();


		// Add the object to the active objects dictionary
		activeObjects.Add(objectToSpawn, tag);

		// Enable the object and call the OnSpawn method
		objectToSpawn.SetActive(true);
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

	public void DeactivateToPool(string tag, GameObject objectToDeactivate)
	{
		if (!poolDictionary.ContainsKey(tag))
			return;

		// Check if the object is active and in the active objects dictionary
		if (objectToDeactivate.activeSelf && activeObjects.ContainsKey(objectToDeactivate))
		{

			// Deactivate the object, remove it from the active objects dictionary, and return it to the pool
			objectToDeactivate.SetActive(false);
			objectToDeactivate.transform.parent = transform;
			activeObjects.Remove(objectToDeactivate);
			poolDictionary[tag].Enqueue(objectToDeactivate);
		}
	}
}
