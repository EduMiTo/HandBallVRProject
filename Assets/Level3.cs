using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3 : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameObject ball;

    private GameObject leftHand;
    private GameObject rightHand;


    public List<Color> colorList = new List<Color>();

    private GameObject disapearThrower;
    private int numOfThrows=0;
    private int numberOfThrowsBeforeChange = 5;

    private int numberOfThrowsBeforeCallingReaction;

    public bool activateReactionTime;

    public static bool subLevel = false;

    // Start is called before the first frame update
    void Start()
    {

        numberOfThrowsBeforeCallingReaction = numberOfThrowsBeforeChange;
        PlayerPrefs.SetString("SceneNumber", SceneManager.GetActiveScene().name);
        colorList.Add(Color.red);
        colorList.Add(Color.blue);
        colorList.Add(Color.white);
        ballPrefab = Resources.Load<GameObject>("GameBall");
        leftHand = (Resources.Load<GameObject>("XR Origin") as GameObject).transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;
        rightHand = (Resources.Load<GameObject>("XR Origin") as GameObject).transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;

        leftHand.GetComponent<Renderer>().sharedMaterial.color = Color.black;
        rightHand.GetComponent<Renderer>().sharedMaterial.color = Color.black;


        ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft = new List<Color>(new Color[2]);
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight = new List<Color>(new Color[2]);

        GetComponent<BallSpawner>().numberOfColors = 3;



        changeBallsToDefend();

        GetComponent<BallSpawner>().SetAllFalse();

       // GetComponent<BallSpawner>().startSpawn();
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
                    if (numOfThrows != numberOfThrowsBeforeCallingReaction)
                    {
                        GetComponent<ReactionTime>().reactionTimeToMake = false;

                        GetComponent<BallSpawner>().startSpawn();

                    }
                    else
                    {
                        activateReactionTime = true;
                        //Debug.Log("activateReactionTime = true");
                    }
                    touched = false;
                }
                if (numOfThrows == numberOfThrowsBeforeChange)
                {
                    changeBallsToDefend();
                    numOfThrows = 0;
                }
            }
        }
    }

    void changeBallsToDefend(){

        System.Random rand = new System.Random();
        var randomInd = rand.Next(0,3);

        Color colorTemp = colorList[randomInd];

        ballPrefab.GetComponent<GoalCollider>().badColor = colorTemp;
        Debug.Log(""+randomInd);

        colorList.RemoveAt(randomInd);

        ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft[0] = colorList[0];
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft[1] = colorList[1];
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight[0] = colorList[0];
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight[1] = colorList[1];

        colorList.Add(colorTemp);
        
        string badColor="";

        if(subLevel) {
            Color color;
            var num = rand.Next(0,2);

            if(colorTemp==Color.red) {
                if(num==0) {
                    badColor = "<color=blue>Red</color>";
                } else {
                    badColor = "<color=white>Red</color>";  
                }
            } else if (colorTemp==Color.blue) {
                if(num==0) {
                    badColor = "<color=red>Blue</color>";
                } else {
                    badColor = "<color=white>Blue</color>";  
                }
            } else {
                if(num==0) {
                    badColor = "<color=red>White</color>";
                } else {
                    badColor = "<color=blue>White</color>";  
                }
            }
        } else {
            if(colorTemp == Color.red) {
                badColor = "<color=red>Red</color>";
            } else {
                if (colorTemp == Color.blue) {
                    badColor = "<color=blue>Blue</color>";
                } else {
                    badColor = "<color=white>White</color>";    
                }
            }
        }

        

        GetComponent<FeedbackIntruction>().feedbackText.text = "Do not defend the "+badColor+" balls!";
    }
}