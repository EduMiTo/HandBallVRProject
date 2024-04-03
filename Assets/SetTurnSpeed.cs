using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class SetTurnSpeed : MonoBehaviour
{
    public ActionBasedContinuousTurnProvider continuousTurnProvider;
    public Slider slider;

    //Getting the value of the slider and setting it to the ContinuousTurnProvider component;
    public void ChangeTurnSpeed()
    {
        continuousTurnProvider.turnSpeed = slider.value;
    }
}
