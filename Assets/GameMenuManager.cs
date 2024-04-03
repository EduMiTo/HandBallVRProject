using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 0.15f;
    public GameObject menu;
    public GameObject mainMenu;
    public GameObject soundMenu;
    public GameObject audience;
    public BallSpawner ballSpawner;
    public TextMeshProUGUI restartButton;
    public InputActionProperty showButton;
    public AudioSource audienceAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        menu = mainMenu;
        menu.SetActive(false);
        if(restartButton!=null){
            restartButton.text = "Restart";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            // Opens/Closes menu
            menu.SetActive(!menu.activeSelf);
            // Pauses/Resumes the game
            string sceneName = SceneManager.GetActiveScene().name;
            if(sceneName == "CompetitionLevel" || sceneName == "Level1 1"){
                ballSpawner.TogglePause();
            }

        }

        //Spawns the menu at the position of the players head
        menu.transform.position = head.position + new Vector3(head.forward.x, head.forward.y, head.forward.z).normalized * spawnDistance;
        // Update the menu to move every frame of the game and follow the player
        menu.transform.LookAt(new Vector3(head.position.x, head.transform.position.y, head.position.z));
        // Flip the menu to face the player
        menu.transform.forward *= -1;
    }

    public void LoadMainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level1 1");
    }

    public void LoadMainInGameMenu()
    {
        menu.SetActive(false);
        menu = mainMenu;
        menu.SetActive(true);
    }

    public void LoadSoundMenu()
    {
        menu.SetActive(false);
        menu = soundMenu;
        menu.SetActive(true);
    }

    public void LoadEnvironmentMenu()
    {
        Debug.Log("Environment Menu");
    }

    public void changeAudienceVisibility()
    {
        audience.SetActive(!audience.activeSelf);
        audienceAudioSource.mute = !audienceAudioSource.mute;
    }
}
