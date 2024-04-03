using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GoalkeeperController : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;

    void Update()
    {
        UpdateHand(leftHand, InputDevices.GetDeviceAtXRNode(XRNode.LeftHand));
        UpdateHand(rightHand, InputDevices.GetDeviceAtXRNode(XRNode.RightHand));
    }

    void UpdateHand(Transform hand, InputDevice device)
    {
        Vector3 position;
        Quaternion rotation;
        if (device.TryGetFeatureValue(CommonUsages.devicePosition, out position) &&
            device.TryGetFeatureValue(CommonUsages.deviceRotation, out rotation))
        {
            hand.position = position;
            hand.rotation = rotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Acertou");
            Destroy(collision.gameObject);
            // Add scoring or other game logic here
        }
    }
}