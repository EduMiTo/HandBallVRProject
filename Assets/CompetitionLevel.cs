using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompetitionLevel : MonoBehaviour
{

    public Transform spawnpoint;
    public float speed = 5f;
    public static float ballSpeed = 5f;

    public float minSpeed = 0.5f;
    public float maxSpeed = 10.0f;
    public static bool isRandomSpeed = false;


    public GameObject ballPrefab;

    private Vector3 direction;
    public int numOfThrows = 0;
    public int numberOfThrowsBeforeCallingReaction = 5;
    public bool activateReactionTime;
    public bool onlyOneTime;
    public bool shootingToTheGoal;

    // Start is called before the first frame update
    public void Start()
    {
        PlayerPrefs.SetString("SceneNumber", SceneManager.GetActiveScene().name);

        if (isRandomSpeed)
        {
            speed = Random.Range(minSpeed, maxSpeed);
        }
        else
        {
            speed = ballSpeed;
        }

        activateReactionTime = false;
        GetComponent<BallSpawner>().SetAllFalse();

        ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft = new List<Color>();
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendLeft.Add(Color.blue);
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight = new List<Color>();
        ballPrefab.GetComponent<GoalCollider>().colorsToDefendRight.Add(Color.blue);

        ballPrefab.GetComponent<GoalCollider>().badColor = Color.black;

        //GetComponent<BallSpawner>().startCompetitionMode();
        onlyOneTime = true;
        shootingToTheGoal = false;


    }

    public void StartSpawn(){
        GetComponent<BallSpawner>().competitionModeBallThrown = true;
        GetComponent<BallSpawner>().endCompetitionMode = false;

        GetComponent<BallSpawner>().flagToEnd = false;
        GetComponent<BallSpawner>().SetAllFalse();
        onlyOneTime = true;
        GetComponent<BallSpawner>().preStartCompetitionMode();
    }

    // Update is called once per frame
    void Update()
    {

        //spawnpoint.transform.position = Vector3.MoveTowards(GetComponent<BallSpawner>().getActualPos().position, GetComponent<BallSpawner>().getNextPos(), speed * Time.deltaTime);
        if (!activateReactionTime)
        {

            if (GetComponent<BallSpawner>().getBall() != null)
            {
                if (!GetComponent<BallSpawner>().competitionModeBallThrown)
                {
                    if (GetComponent<BallSpawner>().flagToEnd && onlyOneTime)
                    {

                        Rigidbody ballRigidbody = GetComponent<BallSpawner>().getBall().GetComponent<Rigidbody>();



                         direction = (GetComponent<BallSpawner>().getNextPos() - GetComponent<BallSpawner>().getBall().transform.position).normalized;

                        ballRigidbody.velocity = direction * speed;



                        onlyOneTime = false;

                       // GetComponent<ReactionTime>().startTime();

                        shootingToTheGoal = true;

                    }
                    else if (!GetComponent<BallSpawner>().flagToEnd)
                    {
                        GetComponent<BallSpawner>().getBall().transform.position = Vector3.MoveTowards(GetComponent<BallSpawner>().getBall().transform.position, GetComponent<BallSpawner>().getNextPos(), speed * Time.deltaTime);
                        shootingToTheGoal = false;
                    }



                    if ( GetComponent<BallSpawner>().getBall().GetComponent<GoalCollider>().touched)
                    {

                        
                    numOfThrows++;

                        

                       // if(!shootingToTheGoal){
                       //     GetComponent<ReactionTime>().restartTimer();    
                      //  }

                        if (numOfThrows != numberOfThrowsBeforeCallingReaction)
                        {
                            GetComponent<ReactionTime>().reactionTimeToMake = false;

                           // GetComponent<BallSpawner>().startSpawn();

                        }
                        else
                        {
                            activateReactionTime = true;
                            //Debug.Log("activateReactionTime = true");
                            numOfThrows = 0;
                        }

                        GetComponent<BallSpawner>().competitionModeBallThrown = true;
                    }

                    if (Vector3.Distance(GetComponent<BallSpawner>().getBall().transform.position, GetComponent<BallSpawner>().getNextPos()) < 0.001f){
                        GetComponent<BallSpawner>().competitionModeBallThrown = true;
                    }



                }
                if (shootingToTheGoal)
                {
                    Debug.Log("Vamos despachar que o jantar tá na mesa");
                    GetComponent<BallSpawner>().raycastMaker(direction);

                }
                

                
            }
            

            if (GetComponent<BallSpawner>().endCompetitionMode)
            {
                GetComponent<BallSpawner>().competitionModeBallThrown = true;
                GetComponent<BallSpawner>().endCompetitionMode = false;

                GetComponent<BallSpawner>().flagToEnd = false;
                onlyOneTime = true;
                GetComponent<BallSpawner>().startCompetitionMode();

            }
        }
    }
    
}
