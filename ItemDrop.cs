using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public health hp;
    public bool droped;
    public GameObject[] Dropable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp.dead && !droped)
		{
            Drop();
		}
    }

    public void Drop()
    {
        
        for(int i = 0; i < Dropable.Length; i++)
		{
            GameObject item = Instantiate(Dropable[i], transform.position, Quaternion.identity, null);
        }

        droped = true;
    }
}
