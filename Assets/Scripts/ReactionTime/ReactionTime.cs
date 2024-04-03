using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;



public class ReactionTime : MonoBehaviour
{

    public GameObject Golie;
    public GameObject feedback;

    public GameObject rightHand;
    public GameObject leftHand;

    public GameObject rightPosition;
    public GameObject leftPosition;

    public bool startTimer;
    public bool reactionTimeTimer;

    public GameObject GoliePrefabForLevels;

    private float time;

    public InputActionProperty pinchAnimationAction;

    public int level;

    private string levelname;

    public List<float> reactionTimeList;

    public bool reactionTimeToMake;



    private bool startButtonPressed;

    public float reactionTimeTemp;

    // Start is called before the first frame update
    void Start()
    {
        reactionTimeList = new List<float>();
        level = GoliePrefabForLevels.GetComponent<ReactionTime>().level;
        startTimer = false;
        startButtonPressed = false;
        reactionTimeTimer = false;

        GoliePrefabForLevels.GetComponent<ReactionTime>().reactionTimeList= new List<float>();

        switch (level)
                    {
                        case 1:
                            levelname = "Level1";
                            if ((GetComponent("Level1") as Level1) == null)
                            {
                                Golie.AddComponent<Level1>();
                            }
                            break;
                        case 2:
                            levelname = "SubLevel1";
                            if ((GetComponent("SubLevel1") as SubLevel1) == null)
                            {
                                Golie.AddComponent<SubLevel1>();
                            }
                            else
                            {
                                GetComponent<BallSpawner>().startSpawn();
                                GetComponent<SubLevel1>().activateReactionTime = false;

                            }
                            break;
                        case 3:
                            levelname = "Level2";
                            if ((GetComponent("Level2") as Level2) == null)
                            {
                                Golie.AddComponent<Level2>();
                            }
                            else
                            {
                                GetComponent<BallSpawner>().startSpawn();
                                GetComponent<Level2>().activateReactionTime = false;

                            }
                            break;
                        case 4:
                            levelname = "SubLevel2";
                            if ((GetComponent("SubLevel2") as SubLevel2) == null)
                            {
                                Golie.AddComponent<SubLevel2>();
                            }
                            else
                            {
                                GetComponent<BallSpawner>().startSpawn();
                                GetComponent<SubLevel2>().activateReactionTime = false;

                            }
                            break;
                        case 5:
                            levelname = "Level3";
                            if ((GetComponent("Level3") as Level3) == null)
                            {
                                Golie.AddComponent<Level3>();
                            }
                            else
                            {
                                GetComponent<BallSpawner>().startSpawn();
                                GetComponent<Level3>().activateReactionTime = false;

                            }
                            break;
                        case 6:
                            levelname = "Level3";
                            if ((GetComponent("Level3") as Level3) == null)
                            {
                                Golie.AddComponent<Level3>();
                            }
                            else
                            {
                                GetComponent<BallSpawner>().startSpawn();
                                GetComponent<Level3>().activateReactionTime = false;

                            }
                            break;
                        case 7:
                            levelname = "CompetitionLevel";
                            if ((GetComponent("CompetitionLevel") as CompetitionLevel) == null)
                            {
                                Golie.AddComponent<CompetitionLevel>();
                            }
                            else
                            {
                                //GetComponent<CompetitionLevel>().Start();
                                GetComponent<CompetitionLevel>().activateReactionTime = false;
                            }
                            break;
                        default:
                            break;
                    }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (reactionTimeTimer)
        {
            //Debug.Log("time: " + time);
            time += Time.deltaTime;
        }
        if (!startButtonPressed)
        {
            if (System.Math.Round(rightHand.transform.position.y, 1) == System.Math.Round(rightPosition.transform.position.y, 1) && System.Math.Round(rightHand.transform.position.x, 1) == System.Math.Round(rightPosition.transform.position.x, 1) && System.Math.Round(rightHand.transform.position.z, 1) == System.Math.Round(rightPosition.transform.position.z, 1))

            {

                if (System.Math.Round(leftHand.transform.position.y, 1) == System.Math.Round(leftPosition.transform.position.y, 1) && System.Math.Round(leftHand.transform.position.x, 1) == System.Math.Round(leftPosition.transform.position.x, 1) && System.Math.Round(leftHand.transform.position.z, 1) == System.Math.Round(leftPosition.transform.position.z, 1))
                {
                    //Start throwing ball
                    //start the timer



                    // Debug.Log("Hands in correct position");

                    rightPosition.SetActive(false);
                    leftPosition.SetActive(false);

                    float triggerValue = pinchAnimationAction.action.ReadValue<float>();
                    //    Debug.Log("primaryButtonValue: " + triggerValue);

                    startButtonPressed = true;
                    startTimer = true;

                    reactionTimeToMake = true;


                  //  if (triggerValue>0)
                    // {
                    switch (level)
                    {
                        case 1:
                            levelname = "Level1";
                            if ((GetComponent("Level1") as Level1) == null)
                            {
                                Golie.AddComponent<Level1>();
                            }
                            else
                            {
                                GetComponent<BallSpawner>().startSpawn();
                                GetComponent<Level1>().activateReactionTime = false;
                            }
                            break;
                        case 2:
                            levelname = "SubLevel1";
                            if ((GetComponent("SubLevel1") as SubLevel1) == null)
                            {
                                Golie.AddComponent<SubLevel1>();
                            }
                            else
                            {
                                GetComponent<BallSpawner>().startSpawn();
                                GetComponent<SubLevel1>().activateReactionTime = false;

                            }
                            break;
                        case 3:
                            levelname = "Level2";
                            if ((GetComponent("Level2") as Level2) == null)
                            {
                                Golie.AddComponent<Level2>();
                            }
                            else
                            {
                                GetComponent<BallSpawner>().startSpawn();
                                GetComponent<Level2>().activateReactionTime = false;

                            }
                            break;
                        case 4:
                            levelname = "SubLevel2";
                            if ((GetComponent("SubLevel2") as SubLevel2) == null)
                            {
                                Golie.AddComponent<SubLevel2>();
                            }
                            else
                            {
                                GetComponent<BallSpawner>().startSpawn();
                                GetComponent<SubLevel2>().activateReactionTime = false;

                            }
                            break;
                        case 5:
                            levelname = "Level3";
                            if ((GetComponent("Level3") as Level3) == null)
                            {
                                Golie.AddComponent<Level3>();
                            }
                            else
                            {
                                GetComponent<BallSpawner>().startSpawn();
                                GetComponent<Level3>().activateReactionTime = false;

                            }
                            break;
                        case 6:
                            levelname = "Level3";
                            if ((GetComponent("Level3") as Level3) == null)
                            {
                                Golie.AddComponent<Level3>();
                            }
                            else
                            {
                                GetComponent<BallSpawner>().startSpawn();
                                GetComponent<Level3>().activateReactionTime = false;

                            }
                            break;
                        case 7:
                            levelname = "CompetitionLevel";
                            if ((GetComponent("CompetitionLevel") as CompetitionLevel) == null)
                            {
                                Golie.AddComponent<CompetitionLevel>();
                            }
                            else
                            {
                                GetComponent<CompetitionLevel>().activateReactionTime = false;

                                GetComponent<CompetitionLevel>().StartSpawn();

                            }
                            break;
                        default:
                            break;
                        //    }



                            startButtonPressed = true;
                            startTimer = true;
                    }
                }
                else
                {
                    //  Debug.Log("missing left hand");
                    rightPosition.SetActive(false);
                    leftPosition.SetActive(true);

                    startTimer = false;
                }



            }
            else if (System.Math.Round(leftHand.transform.position.y, 1) == System.Math.Round(leftPosition.transform.position.y, 1) && System.Math.Round(leftHand.transform.position.x, 1) == System.Math.Round(leftPosition.transform.position.x, 1) && System.Math.Round(leftHand.transform.position.z, 1) == System.Math.Round(leftPosition.transform.position.z, 1))
            {
                if (System.Math.Round(rightHand.transform.position.y, 1) == System.Math.Round(rightPosition.transform.position.y, 1) && System.Math.Round(rightHand.transform.position.x, 1) == System.Math.Round(rightPosition.transform.position.x, 1) && System.Math.Round(rightHand.transform.position.z, 1) == System.Math.Round(rightPosition.transform.position.z, 1))
                {


                    //Debug.Log("Hands in correct position");
                    rightPosition.SetActive(false);
                    leftPosition.SetActive(false);

                   // float triggerValue = pinchAnimationAction.action.ReadValue<float>();
                    // Debug.Log("primaryButtonValue: " + triggerValue);

                  /*  if (triggerValue > 0)
                    {
                        if ((GetComponent("Level1") as Level1) == null)
                        {
                            Golie.AddComponent<Level1>();
                        }
                        else
                        {
                            GetComponent<BallSpawner>().startSpawn();
                        }
                        GetComponent<Level1>().activateReactionTime = false;


                        startButtonPressed = true;
                        startTimer = true;

                    }*/

                }
                else
                {
                    //Debug.Log("missing right hand");
                    rightPosition.SetActive(true);
                    leftPosition.SetActive(false);

                    startTimer = false;


                }
            }
            else
            {
                //Debug.Log("Else");
                leftPosition.SetActive(true);
                rightPosition.SetActive(true);

                startTimer = false;

            }



        }
        else
        {
            //Debug.Log("2 Else");

            // Debug.Log("total answers: " + feedback.GetComponent<Feedback>().totalAnswers);
            //if ((GetComponent(levelname) as Script).activateReactionTime)
            // {
            //Debug.Log("teh total total answers are : "+feedback.GetComponent<Feedback>().totalAnswers);
            //Destroy(Golie.GetComponent<Level1>());
            //  startButtonPressed = false;
            //GetComponent<Level1>().activateReactionTime = false;
            //}

            switch (level)
            {
                case 1:
                    if (GetComponent<Level1>().activateReactionTime)
                    {

                        startButtonPressed = false;
                    }
                    break;
                case 2:
                    if (GetComponent<SubLevel1>().activateReactionTime)
                    {

                        startButtonPressed = false;
                    }
                    break;
                case 3:

                    if (GetComponent<Level2>().activateReactionTime)
                    {

                        startButtonPressed = false;
                    }
                    break;
                case 4:
                    if (GetComponent<SubLevel2>().activateReactionTime)
                    {

                        startButtonPressed = false;
                    }
                    break;
                case 5:
                    if (GetComponent<Level3>().activateReactionTime)
                    {

                        startButtonPressed = false;
                    }
                    break;
                case 6:
                    if (GetComponent<Level3>().activateReactionTime)
                    {

                        startButtonPressed = false;
                    }
                    break;
                case 7:
                    if (GetComponent<CompetitionLevel>().activateReactionTime)
                    {

                        startButtonPressed = false;
                    }
                    break;
                default:
                    break;

            }

        }
    }

    public void startTime()
    {
        //Debug.Log("come�ou");
        //Debug.Log("Start time antes: " + reactionTimeTimer);

        reactionTimeTimer = true;
        //Debug.Log("Start time depois: " + reactionTimeTimer);
    }

    public void stopTimer()
    {
       // Debug.Log("acabou");
        Debug.Log("reaction time: " + time);
        if (time != 0)
        {
            GoliePrefabForLevels.GetComponent<ReactionTime>().reactionTimeTemp = time;
            Debug.Log("CARALHO: " + GoliePrefabForLevels.GetComponent<ReactionTime>().reactionTimeTemp);

            //reactionTimeList.Add(time);
        }
        //criar lista para reaction time
        // Debug.Log("stop time depois: " + reactionTimeTimer);

        time = 0;
        reactionTimeTimer = false;
        //Debug.Log("top time depois: " + reactionTimeTimer);

    }

    public void restartTimer()
    {
        //Debug.Log("restart time depois: " + reactionTimeTimer);

        time = 0;
        reactionTimeTimer = false;

       // Debug.Log("resetou");

        //Debug.Log("restart time depois: " + reactionTimeTimer);

    }

    public void setLevel(int level)
    {
        this.level = level;
    }

    public int getLevel()
    {
        return this.level;
    }

    public void checkIfReactionTimeIsValid(bool answer)
    {

        if (!answer)
        {
            reactionTimeList.Remove(reactionTimeList.Count - 1);
        }
    }

    public bool getStartButtonPressed()
    {
        return startButtonPressed;
    }

    public void goodDefenceReactionTime()
    {
        Debug.Log("Sera que vai entrar?");
        if (feedback.GetComponent<Feedback>().totalAnswers %5==0)
        {
            Debug.Log(this.reactionTimeTemp);
            Debug.Log("Entrou");
            reactionTimeList.Add(this.reactionTimeTemp);
        }
    }

    public void badDefenceReactionTime()
    {
        Debug.Log("Sera que vai entrar?2");

        if (feedback.GetComponent<Feedback>().totalAnswers % 5 == 0)
        {
            Debug.Log("Entrou2");

            reactionTimeTemp = -1;
            reactionTimeList.Add(reactionTimeTemp);

        }
    }


}
