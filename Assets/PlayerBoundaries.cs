using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundaries : MonoBehaviour
{
    // Set the min and max boundaries
    Vector3 minBoundary = new Vector3(-58.544f, -0.2f, -113.462f);
    Vector3 maxBoundary = new Vector3(-54.544f, 3.8f, -109.462f);

    void Update()
    {
        Vector3 pos = transform.position;
        
        pos.x = Mathf.Clamp(pos.x, minBoundary.x, maxBoundary.x);
        pos.y = Mathf.Clamp(pos.y, minBoundary.y, maxBoundary.y);
        pos.z = Mathf.Clamp(pos.z, minBoundary.z, maxBoundary.z);
        
        transform.position = pos;
    }
}
