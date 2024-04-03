using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurnType : MonoBehaviour
{
    public ActionBasedSnapTurnProvider snapTurnProvider;
    public ActionBasedContinuousTurnProvider continuousTurnProvider;

    public void SetTurnTypeCamera(int index)
    {
        if (index == 0)
        {
            continuousTurnProvider.enabled = true;
            snapTurnProvider.enabled = false;
        }
        else if (index == 1)
        {
            continuousTurnProvider.enabled = false;
            snapTurnProvider.enabled = true;
        }
    }

}
