using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderMovement : MonoBehaviour
{
    public GameObject target;

    public Transform leftPosition;
    public Transform rightPosition;

    public float moveSpeed = 5f; // Speed of moving the object towards the target
    public float maxDistance = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.GetComponent<BallSpawner>().getBall() != null)
        {

            //Debug.Log("ball position: " + target.GetComponent<BallSpawner>().getBall().transform.position.z);
            //Debug.Log(" position: " + transform.position.z);
            // Check target direction and move to the appropriate position
            if (target.GetComponent<BallSpawner>().getBall().transform.position.z > transform.position.z)
            {
                // Target goes right, move to the right position
                transform.position = Vector3.Lerp(transform.position, rightPosition.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                // Target goes right, move to the right position
                transform.position = Vector3.Lerp(transform.position, leftPosition.position, moveSpeed * Time.deltaTime);
            }
           
        }
    }
}
