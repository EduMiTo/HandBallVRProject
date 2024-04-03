/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubLevel3 : MonoBehaviour
{

    public GameObject ballPrefab;
    private GameObject ball;

    public List<Color> colorList = new List<Color>();

    private GameObject disapearThrower;
    private int numOfThrows=0;
    private int numberOfThrowsBeforeChange = 5;

    // Start is called before the first frame update
    void Start()
    {
        colorList.Add(Color.red);
        colorList.Add(Color.blue);
        colorList.Add(Color.white);

        ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft = new List<Color>(new Color[1]);
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight = new List<Color>(new Color[1]);

        changeBallsToDefend();

        GetComponent<BallSpawner>().SetAllFalse();
        GetComponent<BallSpawner>().startSpawn(Color.white);
        GetComponent<BallSpawner>().startSpawn(Color.red);
        GetComponent<BallSpawner>().startSpawn(Color.blue);
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<BallSpawner>().getFlagBetweenThrows()) {
            ball = GetComponent<BallSpawner>().getBall();

            var touched = ball.GetComponent<GoalCollider>().touched;

            if(touched){
                numOfThrows++;
                GetComponent<BallSpawner>().SetAllFalse();
                GetComponent<BallSpawner>().toogleFlagBetweenThrows();
                GetComponent<BallSpawner>().startSpawn(Color.white);
                GetComponent<BallSpawner>().startSpawn(Color.red);
                GetComponent<BallSpawner>().startSpawn(Color.blue);
                touched = false;
            }
            if(numOfThrows==numberOfThrowsBeforeChange){
                changeBallsToDefend();
                numOfThrows=0;
            }
        }
    }

    void changeBallsToDefend(){

        System.Random rand = new System.Random();
        var randomInd = rand.Next(0,3);

        ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft[0] = colorList[randomInd];
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight[0] = colorList[randomInd];
        
        GetComponent<FeedbackIntruction>().feedbackText.text = "Defend the "+colorList[randomInd]+" balls!";
    }
}*/
