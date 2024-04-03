using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class VRAnimatorController : MonoBehaviour
{
    public float speedTreshold = 0.1f;
    [Range(0,1)]
    public float smoothing = 1;
    private Animator animator;
    private Vector3 previousPos;
    private VRScript vrScript;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vrScript = GetComponent<VRScript>();
        previousPos = vrScript.head.vrTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Compute speed
        Vector3 headsetSpeed = (vrScript.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;
        //Local Speed
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = vrScript.head.vrTarget.position;

        //Set Animator Values
        float previousDirectionX = animator.GetFloat("DirectionX");
        float previousDirectionY = animator.GetFloat("DirectionY");

        animator.SetBool("isMoving", headsetLocalSpeed.magnitude > speedTreshold);
        animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x,-1,1), smoothing));
        animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY,Mathf.Clamp(headsetLocalSpeed.z,-1,1), smoothing));
    }
}
