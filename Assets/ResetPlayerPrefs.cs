using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class ResetPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
   
    private void Awake(){
        Debug.Log("Isto só faz uma vez");
        DontDestroyOnLoad(this.gameObject);
        Debug.Log(PlayerPrefs.GetString("Username"));
        PlayerPrefs.DeleteAll();
        Debug.Log(PlayerPrefs.GetString("Username"));
        PlayerPrefs.SetFloat("musicVolume", 0.2f);
        PlayerPrefs.SetFloat("sfxVolume", 0.55f);
        PlayerPrefs.SetFloat("audienceVolume", 0.05f);
    }
    
}
