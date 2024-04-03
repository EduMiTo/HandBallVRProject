// Import necessary namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Define public variables
    public Transform head;
    public float spawnDistance = 0.99f;
    public GameObject menu;
    public GameObject GameBall;
    public GameObject ballPrefab;
    public Vector3 smallBallSize = new Vector3(0.2f, 0.2f, 0.2f);
    public Vector3 largeBallSize = new Vector3(0.3f, 0.3f, 0.3f);

    public InputActionProperty showButton;
    public TMP_Dropdown spawnTimeDropdown;
    public TMP_Dropdown ballSpeedDropdown;
    

    public GameObject LevelController;
    public GameObject feedbackScript;

    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI ageText;
    public TextMeshProUGUI noAreaSelected;

    public Button activated;

    public Button smallSizeBallButton;
    public Button largeSizeBallButton;
    public Button amateurButton;
    public Button professionalButton;

    public static bool[] goalAreas;
    public Button bttMid;
    public GameObject bttTopL1;
    public GameObject bttTopL2;
    public GameObject bttTopR1;
    public GameObject bttTopR2;
    public GameObject bttLowL1;
    public GameObject bttLowL2;
    public GameObject bttLowR1;
    public GameObject bttLowR2;

    public Toggle togglePenalty;

    private Canvas confirmMenu;
    public Canvas noNameMenu;
    public Canvas repeatedNameMenu;
    public Canvas selectGoalArea;
    public Canvas userMenu;
    public Canvas mainMenu;
    public Canvas numpad;
    public Canvas keyboard;

    private bool compLvl = false;

    public static float time;

    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider audienceSlider;

    //public InputField usernameText;
    //public InputField ageText;



    // Start is called before the first frame update
    void Start()
    {
        smallSizeBallButton.GetComponent<Image>().color = new Color(255,196,0);
        ballPrefab.transform.localScale = smallBallSize;
        time = 30;
        goalAreas = new bool[] {true, true, true, false, false};
        noAreaSelected.gameObject.SetActive(false);

        Debug.Log(PlayerPrefs.GetFloat("musicVolume"));

        if(PlayerPrefs.GetFloat("musicVolume") != null){
            Debug.Log("Siga siga meu menino");
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }else{
            Debug.Log("Vim aqui outra vez");
            musicSlider.value = 0.2f;
        }

        if(PlayerPrefs.GetFloat("sfxVolume") != null){
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        }else{
            sfxSlider.value = 0.55f;
        }

        if(PlayerPrefs.GetFloat("audienceVolume") != null){
            audienceSlider.value = PlayerPrefs.GetFloat("audienceVolume");
        }else{
            audienceSlider.value = 0.2f;
        }

    
        
        //PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
        //PlayerPrefs.SetFloat("audienceVolume", audienceSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Method to load Level 1
    public void LoadLevel1(Button but)
    {
        LevelController.GetComponent<ReactionTime>().setLevel(1);
        ActivateColorLevelButton(but);
    }

    // Method to load SubLevel 1
    public void LoadSubLevel1(Button but)
    {
        LevelController.GetComponent<ReactionTime>().setLevel(2);
        ActivateColorLevelButton(but);
    }

    // Method to load Level 2
    public void LoadLevel2(Button but)
    {
        LevelController.GetComponent<ReactionTime>().setLevel(3);
        ActivateColorLevelButton(but);
    }

    // Method to load SubLevel 2
    public void LoadSubLevel2(Button but)
    {
        LevelController.GetComponent<ReactionTime>().setLevel(4);
        ActivateColorLevelButton(but);
    }

    // Method to load Level 3
    public void LoadLevel3(Button but)
    {
        LevelController.GetComponent<ReactionTime>().setLevel(5);
        ActivateColorLevelButton(but);
    }

    // Method to load SubLevel 3
    public void LoadSubLevel3(Button but)
    {
        LevelController.GetComponent<ReactionTime>().setLevel(6);
        Level3.subLevel = true;
        ActivateColorLevelButton(but);
    }

    // Method to load Competition Level
    public void LoadCompetitionLevel(Button but)
    {
        LevelController.GetComponent<ReactionTime>().setLevel(7);
        compLvl=true;
        BallSpawner.competitionLevel2=false;
        BallSpawner.competitionLevel2SubLevel=false;
        BallSpawner.activatePointer=false;
        ActivateColorLevelButton(but);
    }

    public void LoadCompetitionLevel2(Button but)
    {
        LevelController.GetComponent<ReactionTime>().setLevel(7);
        compLvl=true;
        BallSpawner.competitionLevel2=true;
        BallSpawner.competitionLevel2SubLevel=false;
        BallSpawner.activatePointer=true;
        ActivateColorLevelButton(but);
    }

    public void LoadCompetitionLevel2SubLevel(Button but)
    {
        LevelController.GetComponent<ReactionTime>().setLevel(7);
        compLvl=true;
        BallSpawner.competitionLevel2SubLevel=true;
        BallSpawner.competitionLevel2=false;
        BallSpawner.activatePointer=true;
        ActivateColorLevelButton(but);
    }

    public void StartGame()
    {
        if(!goalAreas[0] && !goalAreas[1] && !goalAreas[2] && !goalAreas[3] && !goalAreas[4]) {
            noAreaSelected.gameObject.SetActive(true);
        } else if(activated!=null) {
            PlayerAttributes();
        }
    }

    public void PlayerAttributes()
    {
        string filename = "";
        if(usernameText.text.Length<=1 && PlayerPrefs.GetString("Username").Length<=1){
            userMenu.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(false);
            numpad.gameObject.SetActive(false);
            keyboard.gameObject.SetActive(false);
            selectGoalArea.gameObject.SetActive(false);
            noNameMenu.gameObject.SetActive(true);
            confirmMenu = noNameMenu;
        } else {
            filename = Application.dataPath + "/Scores/" + usernameText.text + ".csv";

            if(File.Exists(filename)) {
                userMenu.gameObject.SetActive(false);
                mainMenu.gameObject.SetActive(false);
                numpad.gameObject.SetActive(false);
                keyboard.gameObject.SetActive(false);
                selectGoalArea.gameObject.SetActive(false);
                repeatedNameMenu.gameObject.SetActive(true);
                confirmMenu = repeatedNameMenu;
            } else {
                FillPlayerPrefs();
            }
        } 
    }

    public void ConfirmButton() {
        confirmMenu.gameObject.SetActive(false);
        FillPlayerPrefs();
    }

    public void CloseButton() {
        confirmMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        userMenu.gameObject.SetActive(true);
        selectGoalArea.gameObject.SetActive(true);
    }

    public void FillPlayerPrefs() {
        PlayerPrefs.SetString("Username", usernameText.text);
        PlayerPrefs.SetString("Age", ageText.text);
        PlayerPrefs.Save();
        LoadScene();
    }

    public void LoadScene() {
        if(compLvl) {
            SceneManager.LoadScene("CompetitionLevel");
        } else {
            SceneManager.LoadScene("Level1 1");
        }
    }

    // Method to update spawn time based on the selected dropdown value
    public void UpdateSpawnTime(int value)
    {
        switch (value)
        {
            case 0:
                BallSpawner.chainSpeed = 1;
                break;
            case 1:
                BallSpawner.chainSpeed = 2;
                break;
            case 2:
                BallSpawner.chainSpeed = 3;
                break;
            case 3:
                BallSpawner.chainSpeed = 4;
                break;
            case 4:
                BallSpawner.chainSpeed = 5;
                break;
            case 5:
                BallSpawner.chainSpeed = 6;
                break;
            case 6:
                BallSpawner.chainSpeed = 7;
                break;
            case 7:
                BallSpawner.chainSpeed = 8;
                break;
            case 8:
                BallSpawner.chainSpeed = 9;
                break;
            case 9:
                BallSpawner.chainSpeed = 10;
                break;
            default:
                break;
        }
    }

    // Method to disable ball throwers
    public void DisableThrowers()
    {
        BallSpawner.appearShooter = false;
    }

    // Method to enable ball throwers
    public void EnableThrowers()
    {
        BallSpawner.appearShooter = true;
    }

    // Method to start the game with small balls :( o o
    public void StartSmallBallGame()
    {   
        smallSizeBallButton.GetComponent<Image>().color = new Color(255,196,0);
        largeSizeBallButton.GetComponent<Image>().color = new Color(255,255,255);

        if (ballPrefab.transform.localScale == largeBallSize)
        {
            ballPrefab.transform.localScale = smallBallSize;
        }
        
    }

    // Method to start the game with large balls :) O O
    public void StartLargeBallGame()
    {
        smallSizeBallButton.GetComponent<Image>().color = new Color(255,255,255);
        largeSizeBallButton.GetComponent<Image>().color = new Color(255,196,0);

        if (ballPrefab.transform.localScale == smallBallSize)
        {
            ballPrefab.transform.localScale = largeBallSize;
        }
        
    }

    // Method to set time session duration
    public void SetSessionDuration(int value)
    {
        switch (value)
        {
            case 0:
                time = 30;
                break;
            case 1:
                time = 60;
                break;
            case 2:
                time = 90;
                break;
            case 3:
                time = 120;
                break;
            default:
                time = 30;
                break;
        }
        
    }
     
    public void UpdateBallSpeed(int value)
    {
        switch (value)
        {
            case 0:  // Random speed
                BallSpawner.isRandomSpeed  = true;
                CompetitionLevel.isRandomSpeed  = true;

                break;

            case 1:  
                BallSpawner.isRandomSpeed  = false;
                BallSpawner.ballSpeed = value;
                CompetitionLevel.ballSpeed = value;
                break;

            case 2:  
                BallSpawner.isRandomSpeed  = false;
                BallSpawner.ballSpeed = value;
                CompetitionLevel.ballSpeed = value;

                break;

            case 3:  
                BallSpawner.isRandomSpeed  = false;
                BallSpawner.ballSpeed = value;
                CompetitionLevel.ballSpeed = value;

                break;

            case 4:  
                BallSpawner.isRandomSpeed  = false;
                BallSpawner.ballSpeed = value;
                CompetitionLevel.ballSpeed = value;

                break;

            case 5:  
                BallSpawner.isRandomSpeed  = false;
                BallSpawner.ballSpeed = value;
                CompetitionLevel.ballSpeed = value;

                break;

            case 6:  
                BallSpawner.isRandomSpeed  = false;
                BallSpawner.ballSpeed = value;
                CompetitionLevel.ballSpeed = value;
                break;

            case 7:  
                BallSpawner.isRandomSpeed  = false;
                BallSpawner.ballSpeed = value;
                CompetitionLevel.ballSpeed = value;
                break;

            case 8:  
                BallSpawner.isRandomSpeed  = false;
                BallSpawner.ballSpeed = value;
                CompetitionLevel.ballSpeed = value;
                break;

            case 9:  
                BallSpawner.isRandomSpeed  = false;
                BallSpawner.ballSpeed = value;
                CompetitionLevel.ballSpeed = value;
                break;

            case 10: 
                BallSpawner.isRandomSpeed  = false;
                BallSpawner.ballSpeed = value;
                CompetitionLevel.ballSpeed = value;
                break;
            case 12: 
                BallSpawner.isRandomSpeed  = false;
                BallSpawner.ballSpeed = value;
                CompetitionLevel.ballSpeed = value;
                break;
            default:
                break;
        }
    }

    public void PlayerSkillLevelAmateur()
    {
        amateurButton.GetComponent<Image>().color = new Color(255,196,0);
        professionalButton.GetComponent<Image>().color = new Color(255,255,255);

        PlayerPrefs.SetString("SkillLevel", "Amateur");
    }

    public void PlayerSkillLevelProfessional()
    {
        amateurButton.GetComponent<Image>().color = new Color(255,255,255);
        professionalButton.GetComponent<Image>().color = new Color(255,196,0);

        PlayerPrefs.SetString("SkillLevel", "Professional");
    }
    
    public void ActivateColorLevelButton(Button but){
        if(activated == null) {
            activated = but;
        } else {
            activated.GetComponent<Image>().color = new Color(255,255,255);
        }

        but.GetComponent<Image>().color = new Color(255,196,0);
        activated = but;
    }

    public void TogglePenaltyOnly()
    {
        if(togglePenalty.isOn){
            BallSpawner.penaltyOnly = true;

            Debug.Log("penaltyOnly -> true : MENU");
        } else {
            BallSpawner.penaltyOnly = false;
            Debug.Log("penaltyOnly -> false : MENU");
        }
    }

    public void ActivateMid() {
        if(bttMid.GetComponent<Image>().color==new Color(0,1,0,0.4f)) {
            bttMid.GetComponent<Image>().color=new Color(1,0,0,0.4f);
            goalAreas[0]=false;
        } else {
            bttMid.GetComponent<Image>().color=new Color(0,1,0,0.4f);
            goalAreas[0]=true;
            noAreaSelected.gameObject.SetActive(false);
        }
    }

    public void ActivateTopR() {
        GoalArea(1, bttTopR1, bttTopR2);
    }

    public void ActivateTopL() {
        GoalArea(2, bttTopL1, bttTopL2);
    }

    public void ActivateLowR() {
        GoalArea(3, bttLowR1, bttLowR2);
    }

    public void ActivateLowL() {
        GoalArea(4, bttLowL1, bttLowL2);
    }

    public void GoalArea(int pos, GameObject but1, GameObject but2) {
        Debug.Log(but1.GetComponent<Image>().color);

        if(but1.GetComponent<Image>().color==new Color(0,1,0,0.4f)) {
            but1.GetComponent<Image>().color=new Color(1,0,0,0.4f);
            but2.GetComponent<Image>().color=new Color(1,0,0,0.4f);
            goalAreas[pos]=false;
        } else {
            but1.GetComponent<Image>().color=new Color(0,1,0,0.4f);
            but2.GetComponent<Image>().color=new Color(0,1,0,0.4f);
            goalAreas[pos]=true;
            noAreaSelected.gameObject.SetActive(false);
        }
    }

    
}