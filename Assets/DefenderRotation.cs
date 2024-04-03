using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderRotation : MonoBehaviour
{

    public Transform follow;


    

    public GameObject target;
    private float rotationSpeed = 1.0f;

    private float speed = 1.0f;

    public float maxRotationAngle = -45f;

    private Quaternion initialRotation;

    private Vector3 initialPosition;

    public float moveSpeed = 5f; // Speed of moving towards the target object

    public float maxDistance = 2f; // Maximum distance the object can move towards the target



    // Start is called before the first frame update
    void Start()
    {
        //startingAngle = transform.rotation.eulerAngles.y;
        //Debug.Log("starting angle: " + startingAngle);

        initialRotation = transform.rotation;
        initialPosition = transform.position;


    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, follow.position, moveSpeed * Time.deltaTime);


        if (target.GetComponent<BallSpawner>().getBall() != null)
        {
            // Calculate the direction to the target
            Vector3 direction = target.GetComponent<BallSpawner>().getBall().transform.position - transform.position;
            direction.y = 0f; // Optional: Lock rotation to the horizontal plane

            // Calculate the rotation needed to face the target
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Calculate the angle between the initial rotation and the target rotation
            float angle = Quaternion.Angle(initialRotation, targetRotation);

            // Apply limited rotation if necessary
            if (angle <= maxRotationAngle)
            {
                // Apply the rotation
                transform.rotation = targetRotation;
            }
            else
            {
                // Calculate the limited rotation
                Quaternion limitedRotation = Quaternion.RotateTowards(initialRotation, targetRotation, maxRotationAngle);

                // Apply the limited rotation
                transform.rotation = limitedRotation;
            }




        }
    }
}
