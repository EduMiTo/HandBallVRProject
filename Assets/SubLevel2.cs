using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubLevel2 : MonoBehaviour
{

    public GameObject ballPrefab;
    private GameObject ball;

    public List<Color> colorList = new List<Color>();

    private GameObject leftHand;
    private GameObject rightHand;

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

        this.GetComponent<BallSpawner>().numberOfColors = 3;

        ballPrefab = Resources.Load<GameObject>("GameBall");

        leftHand = (Resources.Load<GameObject>("XR Origin") as GameObject).transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;
        rightHand = (Resources.Load<GameObject>("XR Origin") as GameObject).transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;

        leftHand.GetComponent<Renderer>().sharedMaterial.color = Color.red;
        rightHand.GetComponent<Renderer>().sharedMaterial.color = Color.blue;


        ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft = new List<Color>(new Color[1]);
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight = new List<Color>(new Color[1]);

        changeBadBall();
        colorToDefendGlovesSubLevel2();

        GetComponent<BallSpawner>().SetAllFalse();
      //  GetComponent<BallSpawner>().startSpawn();
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
                        //Debug.Log("activateReactionTime = true");
                    }
                    touched = false;
                }
                if (numOfThrows == numberOfThrowsBeforeChange)
                {
                    changeBadBall();
                    colorToDefendGlovesSubLevel2();
                    numOfThrows = 0;
                }
            }
        }
    }

    void colorToDefendGlovesSubLevel2(){

        if(ballPrefab.GetComponent<GoalCollider>().badColor == Color.blue){
            //RED
            //WHITE
            
            chooseColorBetween2(Color.red, Color.white);
        }else if(ballPrefab.GetComponent<GoalCollider>().badColor == Color.red){
            //BLUE
            //WHITE

            chooseColorBetween2(Color.blue, Color.white);
        }else{
            //RED
            //BLUE

            chooseColorBetween2(Color.red, Color.blue);
        }
    }

    void chooseColorBetween2(Color color1, Color color2){

        System.Random rand = new System.Random();
        
        var num = rand.Next(0,2);

        if(num==0){
            ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft[0] = color1;
            ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight[0] = color2;
            //colors[0] = color1;
            //colors[1] = color2;
        }else{
            ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft[0] = color2;
            ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight[0] = color1;
            //colors[0] = color2;
            //colors[1] = color1;
        }
        
        string colorLeft = colorToString(ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft[0]);
        string colorRight = colorToString(ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight[0]);

        GetComponent<FeedbackIntruction>().feedbackText.text += ". Defend the "+colorLeft+" balls with the red glove and the "+colorRight+" balls with the blue glove!";

    }

    void changeBadBall(){
        
        System.Random rand = new System.Random();
        var num = rand.Next(0,3);
        if(num==0) {
            ballPrefab.GetComponent<GoalCollider>().badColor=Color.blue;
          //  color = Color.blue;
            Debug.Log("BAD BALL: Azul");
            GetComponent<FeedbackIntruction>().feedbackText.text = "Ignore the <color=blue>Blue</color> ball!";

        }
        else if(num==1) {
           // color = Color.red;
            ballPrefab.GetComponent<GoalCollider>().badColor=Color.red;

            Debug.Log("BAD BALL: Vermelho");
            GetComponent<FeedbackIntruction>().feedbackText.text = "Ignore the <color=red>Red</color> ball";

        }
        else if(num==2) {
            //color = Color.white;
            ballPrefab.GetComponent<GoalCollider>().badColor=Color.white;

            Debug.Log("BAD BALL: Branco");
            Debug.Log("Branco");
            GetComponent<FeedbackIntruction>().feedbackText.text = "Ignore the <color=white>White</color> ball";
        }

    }

    string colorToString(Color color) {
        string badColor;

        if(color == Color.red) {
            badColor = "<color=red>Red</color>";
        } else {
            if (color == Color.blue) {
                badColor = "<color=blue>Blue</color>";
            } else {
                badColor = "<color=white>White</color>";    
            }
        }

        return badColor;
    }
}
