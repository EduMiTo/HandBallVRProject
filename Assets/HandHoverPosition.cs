using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHoverPosition : MonoBehaviour
{

    public Transform head;
    public float spawnDistance = 0.15f;


    // Start is called before the first frame update
    void Update()
    {

        float headHeight = head.position.y;

        Vector3 handPosition = transform.position;
        handPosition.y = headHeight;
        transform.position = handPosition;


    }

}
