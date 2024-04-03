using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubLevel1 : MonoBehaviour
{
    //public GameObject goalie;
    //public bool flag= this.GetComponent<BallSpawner>().flag;

    public GameObject ballPrefab;
    private GameObject ball;

    public List<Color> colorList = new List<Color>();

    public GameObject leftHand;
    public GameObject rightHand;

    private GameObject disapearThrower;
    private int numOfThrows=0;
    private int numberOfThrowsBeforeChange = 5;

    private int numberOfThrowsBeforeCallingReaction;

    public bool activateReactionTime;


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

        ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft = new List<Color>(new Color[1]);
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight = new List<Color>(new Color[1]);

        this.GetComponent<BallSpawner>().numberOfColors = 3;



        changeGlovesColor();

        GetComponent<BallSpawner>().SetAllFalse();

       // GetComponent<BallSpawner>().startSpawn();
    }

    // Update is called once per frame
    void Update(){
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
                    }
                    touched = false;
                }
                if (numOfThrows == numberOfThrowsBeforeChange)
                {
                    changeGlovesColor();
                    numOfThrows = 0;
                }
            }
        }
    }

    void changeGlovesColor(){

        Color colorTemp;
        Color colorTemp2;

        System.Random rand = new System.Random();
        var randomColor1 = rand.Next(0,3);

        colorTemp = colorList[randomColor1];

        ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft[0] = colorList[randomColor1];
        leftHand.GetComponent<Renderer>().sharedMaterial.color = colorList[randomColor1];

        colorList.RemoveAt(randomColor1);

        var randomColor2 = rand.Next(0,2);

        colorTemp2 = colorList[randomColor2];

        ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight[0] = colorList[randomColor2];
        rightHand.GetComponent<Renderer>().sharedMaterial.color = colorList[randomColor2];

        colorList.RemoveAt(randomColor2);
        
        ballPrefab.GetComponent<GoalCollider>().badColor = colorList[0];

        colorList.Add(colorTemp);
        colorList.Add(colorTemp2);

        string badColor;

        if(colorList[0] == Color.red) {
            badColor = "<color=red>Red</color>";
        } else {
            if (colorList[0] == Color.blue) {
                badColor = "<color=blue>Blue</color>";
            } else {
                badColor = "<color=white>White</color>";    
            }
        }
        
        GetComponent<FeedbackIntruction>().feedbackText.text = "Defend the ball with the glove which has the same color. Ignore the "+badColor+" balls!";
        
    }
}