using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public GameObject test;
    public RectTransform pointer;
        public GameObject test2;

    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition= test.transform.position;
        Debug.Log("targetPosition: "+targetPosition);
        pointer=transform.Find("Pointer").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition= test.transform.position;
        //targetPosition.z=test2.transform.position.z;
       /* Vector3 diff = targetPosition - pointer.transform.position;
         diff.Normalize();
 
         float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
         pointer.transform.rotation = Quaternion.Euler(0f, 90f, rot_z );*/

        Vector3 toPosition = targetPosition;
        toPosition.x=0;
        Vector3 fromPosition=Camera.main.transform.position;

        fromPosition.x=0f;
        Debug.Log("fromPosition1: "+fromPosition);
        Debug.Log("targetPosition: "+toPosition);


        Vector3 dir = (toPosition-fromPosition).normalized;
        float angle = (Mathf.Atan2(dir.z, dir.y) * Mathf.Rad2Deg) % 360;

        Debug.Log(angle);
        pointer.localEulerAngles = new Vector3(0,0,-angle);


       //test2.transform.LookAt(new Vector3(targetPosition.x, targetPosition.y, targetPosition.z));

    }
}
