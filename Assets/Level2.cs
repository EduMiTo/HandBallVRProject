using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour
{

    public GameObject ballPrefab;
    private GameObject ball;
    private GameObject disapearThrower;


    private GameObject leftHand;
    private GameObject rightHand;

    private int numOfThrows = 0;

    private int numberOfThrowsBeforeCallingReaction = 5;

    public bool activateReactionTime;


    // Start is called before the first frame update
    void Start()
    {
        ballPrefab = Resources.Load<GameObject>("GameBall");

        PlayerPrefs.SetString("SceneNumber", SceneManager.GetActiveScene().name);
        leftHand = (Resources.Load<GameObject>("XR Origin") as GameObject).transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;
        rightHand = (Resources.Load<GameObject>("XR Origin") as GameObject).transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;

        leftHand.GetComponent<Renderer>().sharedMaterial.color = Color.red;
        rightHand.GetComponent<Renderer>().sharedMaterial.color = Color.blue;

        ballPrefab.GetComponent<GoalCollider>().badColor = Color.black;
        GetComponent<FeedbackIntruction>().feedbackText.text = "Defend the <color=blue>Blue</color> balls with the <color=red>Red</color> glove and the <color=red>Red</color> balls with the <color=blue>Blue</color> glove!";

        GetComponent<BallSpawner>().numberOfColors = 2;



        ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft = new List<Color>();
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft.Add(Color.blue);
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight = new List<Color>();
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight.Add(Color.red);

        GetComponent<BallSpawner>().SetAllFalse();


        //GetComponent<BallSpawner>().startSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activateReactionTime)
        {
            if (GetComponent<BallSpawner>().getFlagBetweenThrows())
            {
                GetComponent<BallSpawner>().raycastMaker();

                ball = GetComponent<BallSpawner>().getBall();

                var touched = ball.GetComponent<GoalCollider>().touched;

                if (touched)
                {
                    numOfThrows++;
                    GetComponent<ReactionTime>().restartTimer();

                    disapearThrower = GetComponent<BallSpawner>().getDisapearThrower();
                    GetComponent<BallSpawner>().SetFalse(disapearThrower);
                    GetComponent<BallSpawner>().toogleFlagBetweenThrows();
                    if (numOfThrows != numberOfThrowsBeforeCallingReaction) {
                        GetComponent<ReactionTime>().reactionTimeToMake = false;

                        GetComponent<BallSpawner>().startSpawn();
                    }
                    else
                    {
                        activateReactionTime = true;
                        numOfThrows = 0;
                    }
                    touched = false;
                }
            }
        }
    }
}