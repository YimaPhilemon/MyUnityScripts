using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 100;
    public bool rotate = true;
    void FixedUpdate()
    {
        if (!rotate)
            return;

        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
