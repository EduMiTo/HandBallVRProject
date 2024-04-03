using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Globalization;

public class ResultsScript : MonoBehaviour
{
    public GameObject feedBack;
    public GameObject reactionTime;
    public GameObject mainMenu;

    public TextMeshProUGUI sessionTime;
    public TextMeshProUGUI rightAnswers;
    public TextMeshProUGUI wrongAnswers;
    public TextMeshProUGUI slowestReactionTimeText;
    public TextMeshProUGUI fastestReactionTimeText;
    public TextMeshProUGUI averageReactionTimeText;
    public TextMeshProUGUI savedResultsText;

    public Button exportCSVButton;

    public GameObject audioSource;
    public AudioClip soundClipHorray;

    string PrevScene;

    string filename = "";
    string username = "";
    string age = "";
    string skillLevel = "";
    string currentDate = "";
    string path = "";

    // Start is called before the first frame update
    void Start()
    {
        PlaySound(soundClipHorray);

        path = Application.dataPath + "/Scores";
        try{
            if (!Directory.Exists(path)){
                Directory.CreateDirectory(path);
            }
        }
        catch (IOException ex)
        {
            Debug.Log(ex.Message);
        }

        PrevScene = PlayerPrefs.GetString("SceneNumber");

        sessionTime.text = MainMenuManager.time.ToString();
        rightAnswers.text = feedBack.GetComponent<Feedback>().rightAnswersCount.ToString();
        wrongAnswers.text = feedBack.GetComponent<Feedback>().wrongAnswersCount.ToString();
        verifyReactionTimes(reactionTime.GetComponent<ReactionTime>().reactionTimeList);

        if(PlayerPrefs.GetString("Username").Length <= 1){
            username = "defaultfile";
        } else {
            username = PlayerPrefs.GetString("Username");
        }

        age = PlayerPrefs.GetString("Age");
        skillLevel = PlayerPrefs.GetString("SkillLevel");

        filename = Application.dataPath + "/Scores/" + username + ".csv";
    }

    // Method to load Level
    public void RestartLevel()
    {
        SceneManager.LoadScene(PrevScene);
    }

    // Method to load Level
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void verifyReactionTimes(List<float> list){
        float maxValue = 0;
        for(int i=0; i< list.Count; i++){
            if(list[i]>maxValue){
                maxValue = list[i];
            }
        }

        if(maxValue>0){
            slowestReactionTimeText.text= maxValue.ToString();

            float lowest = float.MaxValue;

            for(int i=0; i< list.Count; i++){
                if(list[i]<lowest && list[i]!=-1){
                    lowest = list[i];
                }
            }
            
            fastestReactionTimeText.text= lowest.ToString();
        
            float sum=0;
            int cont=0;
            for(int i=0; i< list.Count; i++){
                if(list[i]!=-1){
                    sum+=list[i];
                    cont++;
                }
            }
            float meanReactionTime=sum/cont;
            averageReactionTimeText.text=meanReactionTime.ToString();
              
        }else{
            slowestReactionTimeText.text="There was no valid reaction time";
        }
    }

    public void ExportDataIntoCSV()
    {
        exportCSVButton.interactable = false;
        savedResultsText.gameObject.SetActive(true);
        exportCSVButton.GetComponent<Image>().color = new Color(255,196,0);
        int currentLevel = reactionTime.GetComponent<ReactionTime>().getLevel();
        string currentLevelString;

        switch(currentLevel){
            case 1:
                currentLevelString = "Level 1";
                break;
            case 2:
                currentLevelString = "SubLevel 1";
                break;
            case 3:
                currentLevelString = "Level 2";
                break;
            case 4:
                currentLevelString = "SubLevel 2";
                break;
            case 5:
                currentLevelString = "Level 3";
                break;
            case 6:
                currentLevelString = "SubLevel 3";
                break;
            case 7:
                currentLevelString = "Competition Level";
                break;
            default:
                currentLevelString = "";
                break;
        }

        if(!File.Exists(filename)) {
            TextWriter tw = new StreamWriter(filename, false, System.Text.Encoding.UTF8);
            tw.WriteLine("Username;Age;Skill Level");
            tw.WriteLine(username + ";" + age + ";" + skillLevel);
            tw.WriteLine();
            tw.WriteLine("Timestamp;Session Level;Session Time;Right Answers;Wrong Answers;Slowest Reaction Time;Fastest Reaction Time;Average Reaction Time;ALL REACTION TIMES");
            tw.Close();

            tw = new StreamWriter(filename, true);

            writeOnFile(tw, currentLevelString);
        } else {
            using (StreamWriter tw = File.AppendText(filename)) {
                writeOnFile(tw, currentLevelString);
            }
        }
    }

    public void writeOnFile(TextWriter tw, string currentLevelString) {
        currentDate = getCurrentDateTime();
        List<float> reactionTimeList = reactionTime.GetComponent<ReactionTime>().reactionTimeList;
        tw.Write(currentDate + ";" + currentLevelString + ";" + sessionTime.text + ";" + rightAnswers.text + ";" + wrongAnswers.text + ";" + slowestReactionTimeText.text + ";" + fastestReactionTimeText.text + ";" + averageReactionTimeText.text);

        int i;

        for(i=0; i<reactionTimeList.Count-1; i++){
            tw.Write(";" +reactionTimeList[i]);
        }

        tw.WriteLine(";" +reactionTimeList[i]);
    
        tw.Close();
    }
    
    // Update is called once per frame
    public void SendUsernameToCSV(TextMeshProUGUI usernameText, TextMeshProUGUI ageText)
    {
        username = usernameText.text;
    }

    // Update is called once per frame
    public void SendAgeToCSV(TextMeshProUGUI usernameText, TextMeshProUGUI ageText)
    {
        age = ageText.text;
    }

    public string getCurrentDateTime(){
        System.DateTime dt = System.DateTime.Now;
        
        return dt.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
    }

    public void PlaySound(AudioClip clip)
    {
        // Define o novo clip de áudio para reprodução
        audioSource.GetComponent<AudioSource>().clip = clip;

        // Reproduz o som
        audioSource.GetComponent<AudioSource>().Play();
    }

}