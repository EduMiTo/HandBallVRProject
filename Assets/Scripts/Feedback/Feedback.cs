using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Feedback : MonoBehaviour
{

    public GameObject canvas;
    public GameObject feedBackPrefab;
    public TMPro.TMP_Text rightAnswers;
    public TMPro.TMP_Text wrongAnswers;
    public TMPro.TMP_Text timeText;
    



    public float spawnDistance = 0.20f;

    public Transform head;


    public int rightAnswersCount=0;
    public int wrongAnswersCount=0;
    public int totalAnswers = 0;

    public GameObject reactionTime;

    private float time;

    public static GameObject audioSource;
    public static AudioClip soundClipRight;
    public static AudioClip soundClipWrong;
    public static AudioClip soundClipNetWrong;
    public static AudioClip soundClipNetRight;

    public GameObject audioSourceDummy;
    public AudioClip soundClipRightDummy;
    public AudioClip soundClipWrongDummy;
    public AudioClip soundClipNetWrongDummy;
    public AudioClip soundClipNetRightDummy;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = audioSourceDummy;
        soundClipRight = soundClipRightDummy;
        soundClipWrong = soundClipWrongDummy;
        soundClipNetWrong = soundClipNetWrongDummy;
        soundClipNetRight = soundClipNetRightDummy;
        
        time = MainMenuManager.time;
        
        rightAnswersCount = 0;
        wrongAnswersCount = 0;
        totalAnswers = 0;
        feedBackPrefab.GetComponent<Feedback>().rightAnswersCount = 0;
        feedBackPrefab.GetComponent<Feedback>().wrongAnswersCount = 0;
        //time = 7;
    }
    

    // Update is called once per frame
    void Update()
    {
        rightAnswers.text = "R: "+ feedBackPrefab.GetComponent<Feedback>().rightAnswersCount.ToString();
        wrongAnswers.text = "W: "+feedBackPrefab.GetComponent<Feedback>().wrongAnswersCount.ToString();

        feedBackPrefab.GetComponent<Feedback>().totalAnswers = feedBackPrefab.GetComponent<Feedback>().rightAnswersCount + feedBackPrefab.GetComponent<Feedback>().wrongAnswersCount;
        totalAnswers = feedBackPrefab.GetComponent<Feedback>().totalAnswers;
        canvas.transform.position = head.position + new Vector3(head.forward.x, head.forward.y+0.25f, head.forward.z).normalized * spawnDistance;

        canvas.transform.LookAt(new Vector3(head.position.x, head.transform.position.y, head.position.z));

        canvas.transform.forward *= -1;
        if (reactionTime.GetComponent<ReactionTime>().startTimer)
        {
            if (time > 0)
            {
                var minutes = Mathf.FloorToInt(time / 60);
                var seconds = Mathf.FloorToInt(time % 60);
                timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                time -= Time.deltaTime;

            }

            if (time <= 0)
            {
                SceneManager.LoadScene("Results");
                Debug.Log("Time is up");
                SceneManager.LoadScene("Results");
            }
        }
    }

    public static void PlayRightSound()
    {
        // Define o novo clip de áudio para reprodução
        audioSource.GetComponent<AudioSource>().clip = soundClipRight;

        // Reproduz o som
        audioSource.GetComponent<AudioSource>().Play();
    }

    public static void PlayWrongSound()
    {
        // Define o novo clip de áudio para reprodução
        audioSource.GetComponent<AudioSource>().clip = soundClipWrong;

        // Reproduz o som
        audioSource.GetComponent<AudioSource>().Play();
    }

    public static void PlayNetRight()
    {
        // Define o novo clip de áudio para reprodução
        audioSource.GetComponent<AudioSource>().clip = soundClipNetRight;

        // Reproduz o som
        audioSource.GetComponent<AudioSource>().Play();
    }

    public static void PlayNetWrong()
    {
        // Define o novo clip de áudio para reprodução
        audioSource.GetComponent<AudioSource>().clip = soundClipNetWrong;

        // Reproduz o som
        audioSource.GetComponent<AudioSource>().Play();
    }

}