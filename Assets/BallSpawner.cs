using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;


public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject goliePrefab;

    public RigBuilder rigBuilder;
    public TwoBoneIKConstraint robotKylieTest;

    public float minSpeed = 0.5f;
    public float maxSpeed = 10.0f;
    public static float ballSpeed = 2.0f;
    public static bool isRandomSpeed = false;

    private bool flagBetweenThrows = false;

    public GameObject goal;
    private float offset = 0.05f;

    public bool topRightCorner;
    public bool topLeftCorner;
    public bool bottomRightCorner;
    public bool bottomLeftCorner;
    public bool middle;
    public bool allGoal;

    private GameObject ball;

    public static bool appearShooter = true;
    private GameObject disapearThrower;

    private int spawnPoint;
    public Transform spawnPointMiddleLH;
    public Transform spawnPointMiddleLeftLH;
    public Transform spawnPointMiddleRightLH;
    public Transform spawnPointLeftLH;
    public Transform spawnPointRightLH;
    public Transform spawnPointMiddleRH;
    public Transform spawnPointMiddleLeftRH;
    public Transform spawnPointMiddleRightRH;
    public Transform spawnPointLeftRH;
    public Transform spawnPointRightRH;
    private Transform[] spawnPoints = new Transform[5];

    public GameObject throwerMiddleLH;
    public GameObject throwerMiddleLeftLH;
    public GameObject throwerMiddleRightLH;
    public GameObject throwerLeftLH;
    public GameObject throwerRightLH;
    public GameObject throwerMiddleRH;
    public GameObject throwerMiddleLeftRH;
    public GameObject throwerMiddleRightRH;
    public GameObject throwerLeftRH;
    public GameObject throwerRightRH;


    private Color[] colors;
    private Color color;

    public static float chainSpeed=2;

    public GameObject leftHand;
    public GameObject rightHand;

    private float defaultRadius = 1;

    public int numberOfColors;

    private bool isPaused = false;

    private Vector3 direction;
    private RaycastHit hit;

    public bool competitionModeBallThrown = true;

    private Vector3 nextPos;
    public Transform actualPosition;

    private int maxPasses = 10;
    private int shotsBeforeChange = 5;

    public bool endCompetitionMode = false;

    public bool flagToEnd = false;

    public static bool penaltyOnly;

    public static bool competitionLevel2 = false;
    public static bool competitionLevel2SubLevel = false;
    
    public static bool activatePointer = false;
    public RectTransform pointer;

    public GameObject audioSource;
    public AudioClip soundClipThrower;

    private int firstShotBadBall;

    public void changefirstShotBadBall(int badBall){
        firstShotBadBall=badBall;
    }

    public Transform getActualPos()
    {
        return actualPosition;
    }
    
    public Vector3 getNextPos()
    {
        return nextPos;
        
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused ) 
        {
            GetComponent<ReactionTime>().startTimer=false;
            if(SceneManager.GetActiveScene().name == "Level1 1"){
                StopCoroutine("SpawnBalls");
            }
            else if(SceneManager.GetActiveScene().name == "CompetitionLevel"){
                StopCoroutine(SpawnBallsCompetitionMode());
            }
            if(ball != null){
                toogleFlagBetweenThrows();
                SetAllFalse();
                Destroy(ball);
            }
        }
        else if(GetComponent<ReactionTime>().getStartButtonPressed())
        {
            GetComponent<ReactionTime>().startTimer=true;
            if(SceneManager.GetActiveScene().name == "Level1 1"){
                GetComponent<ReactionTime>().restartTimer();
                StartCoroutine("SpawnBalls");
            }else if(SceneManager.GetActiveScene().name == "CompetitionLevel"){
                GetComponent<ReactionTime>().restartTimer();
                GetComponent<CompetitionLevel>().StartSpawn();
                
            }
        }
    }

    public IEnumerator SpawnBalls()
    {
        if (!isPaused)
        {
            yield return new WaitForSeconds(chainSpeed);

            PlaySound(soundClipThrower);

            Vector3 endPosition = GetRandomPositionWithinGoal();
            System.Random rand = new System.Random();
            
            int ballColor;
            //BALL COLOR THAT COMES OUT OF THE SHOOTER HAND      

            if(ballPrefab.GetComponent<GoalCollider>().badColor==Color.blue) {
                firstShotBadBall=0;
            } else if(ballPrefab.GetComponent<GoalCollider>().badColor==Color.red) {
                firstShotBadBall=1;
            } else if(ballPrefab.GetComponent<GoalCollider>().badColor==Color.white){
                firstShotBadBall=2;
            } else {
                firstShotBadBall=3;
            }

            do{
                ballColor = rand.Next(0, numberOfColors);
                if(ballColor == 0) {
                    ballPrefab.GetComponent<Renderer>().sharedMaterial.color = Color.blue;
                } else if(ballColor == 1){
                    ballPrefab.GetComponent<Renderer>().sharedMaterial.color = Color.red;
                } else if(ballColor == 2){
                    ballPrefab.GetComponent<Renderer>().sharedMaterial.color = Color.white;
                }

                Debug.Log("numero bola" + ballColor);
                Debug.Log("numero bola mal" + firstShotBadBall);

            }while(ballColor==firstShotBadBall && GetComponent<ReactionTime>().reactionTimeToMake);

            //flagBetweenThrows=true;

            System.Random randomBallSpawnPosition = new System.Random(); 

            if (penaltyOnly)
            {
                int tmpNum = randomBallSpawnPosition.Next(0, 2);

                if (tmpNum == 0)
                {
                    spawnPoint = 2;
                }
                else
                {
                    spawnPoint = 7;
                }
                
            }
            else
            {
                spawnPoint = randomBallSpawnPosition.Next(0, 10);

            }

            //0 - LEFT          Left Handed Thrower
            //1 - MIDDLE LEFT           "
            //2 - MIDDLE                "
            //3 - MIDDLE RIGHT          "
            //4 - RIGHT                 "
            //5 - LEFT          Right Handed Thrower  
            //6 - MIDDLE LEFT           "
            //7 - MIDDLE                "
            //8 - MIDDLE RIGHT          "
            //9 - RIGHT                 "

            switch(spawnPoint){
                case 0:
                    disapearThrower = InitiateThrow(throwerLeftLH, spawnPointLeftLH, endPosition);
                    break;
                case 1:
                    disapearThrower = InitiateThrow(throwerMiddleLeftLH, spawnPointMiddleLeftLH, endPosition);
                    break;
                case 2:
                    disapearThrower = InitiateThrow(throwerMiddleLH, spawnPointMiddleLH, endPosition);
                    break;
                case 3:
                    disapearThrower = InitiateThrow(throwerMiddleRightLH, spawnPointMiddleRightLH, endPosition);
                    break;
                case 4:
                    disapearThrower = InitiateThrow(throwerRightLH, spawnPointRightLH, endPosition);
                    break;
                case 5:
                    disapearThrower = InitiateThrow(throwerLeftRH, spawnPointLeftRH, endPosition);
                    break;
                case 6:
                    disapearThrower = InitiateThrow(throwerMiddleLeftRH, spawnPointMiddleLeftRH, endPosition);
                    break;
                case 7:
                    disapearThrower = InitiateThrow(throwerMiddleRH, spawnPointMiddleRH, endPosition);
                    break;
                case 8:
                    disapearThrower = InitiateThrow(throwerMiddleRightRH, spawnPointMiddleRightRH, endPosition);
                    break;
                case 9:
                    disapearThrower = InitiateThrow(throwerRightRH, spawnPointRightRH, endPosition);
                    break;
                default:
                    disapearThrower = null;
                    break;
            }
        }
    }

    public void startSpawn() {
        Debug.Log("penaltyOnly -> "+penaltyOnly+" :Spawn Balls");
        StartCoroutine("SpawnBalls");
    }

    public GameObject getBall(){
        return ball;
    }

    public GameObject getDisapearThrower()
    {
        return disapearThrower;
    }

    public bool getFlagBetweenThrows()
    {
        return flagBetweenThrows;
    }

    public void toogleFlagBetweenThrows()
    {
        flagBetweenThrows = !flagBetweenThrows;
    }

    void SetTrue(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void SetFalse(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void SetAllFalse()
    {
        SetFalse(throwerLeftLH);
        SetFalse(throwerMiddleLeftLH);
        SetFalse(throwerMiddleLH);
        SetFalse(throwerMiddleRightLH);
        SetFalse(throwerRightLH);
        SetFalse(throwerLeftRH);
        SetFalse(throwerMiddleLeftRH);
        SetFalse(throwerMiddleRH);
        SetFalse(throwerMiddleRightRH);
        SetFalse(throwerRightRH);
    }
    public void SetAllTrue()
    {
        SetTrue(throwerLeftLH);
        SetTrue(throwerMiddleLeftLH);
        SetTrue(throwerMiddleLH);
        SetTrue(throwerMiddleRightLH);
        SetTrue(throwerRightLH);
        SetTrue(throwerLeftRH);
        SetTrue(throwerMiddleLeftRH);
        SetTrue(throwerMiddleRH);
        SetTrue(throwerMiddleRightRH);
        SetTrue(throwerRightRH);
    }

    GameObject InitiateThrow(GameObject thrower, Transform iniPos, Vector3 endPos)
    {
        if (appearShooter)
        {
            SetTrue(thrower);
        }

        InstantiateBall(endPos, iniPos);
        return thrower;
    }

    Vector3 GetRandomPositionWithinGoal()
    {
        Renderer rend = goal.GetComponent<Renderer>();

        //Getting the Range of the goal, to define the maximum that the ball can be thrown ( offset so the ball isnt thrown at the posts)

        float maxZ;
        float minZ;
        float maxY;
        float minY;

        System.Random rand = new System.Random(); 
        int num;
        do{
            num = rand.Next(0,5);
        }while(!MainMenuManager.goalAreas[num]);

        switch(num) {
            case 1:
                maxZ = rend.bounds.max.z - offset;
                minZ = goal.transform.position.z;
                maxY = rend.bounds.max.y - offset;
                minY = goal.transform.position.y;
                break;
            case 2:
                maxZ = goal.transform.position.z;
                minZ = rend.bounds.min.z + offset;
                maxY = rend.bounds.max.y - offset;
                minY = goal.transform.position.y;
                break;
            case 3:
                maxZ = rend.bounds.max.z - offset;
                minZ = goal.transform.position.z;
                maxY = goal.transform.position.y;
                minY = rend.bounds.min.y + offset;
                break;
            case 4:
                maxZ = goal.transform.position.z;
                minZ = rend.bounds.min.z + offset;
                maxY = goal.transform.position.x;
                minY = rend.bounds.min.y + offset;
                break;
            default:
                maxZ = goal.transform.position.z ;
                minZ = goal.transform.position.z ;
                maxY = goal.transform.position.y ;
                minY = goal.transform.position.y ;
                break;
        }

        float z = Random.Range(minZ, maxZ);
        float y = Random.Range(minY, maxY);
        float x = rend.bounds.min.x;

        return new Vector3(x, y, z);
    }

    void InstantiateBall(Vector3 endPosition, Transform pos)
    {
        //Vector3 finalPosition = randomPositionInCircle(position);
        Vector3 finalPosition = new Vector3(pos.position.x, pos.position.y, pos.position.z);

        ball = Instantiate(ballPrefab, finalPosition, Quaternion.identity);

        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();

        float speed;
        if (isRandomSpeed)
        {
            speed = Random.Range(minSpeed, maxSpeed);
        }
        else
        {
            speed = ballSpeed;
        }

        direction = (endPosition - finalPosition).normalized;

        ballRigidbody.velocity = direction * speed;
        if (GetComponent<ReactionTime>().reactionTimeToMake)
        {
            GetComponent<ReactionTime>().startTime();
        }

        toogleFlagBetweenThrows();
    }

/*
    Vector3 randomPositionInCircle(Transform centerPosition)
    {

        float angle = Random.value * 360;

        float xDistance = Random.Range(0, defaultRadius);
        float yDistance = Random.Range(0, defaultRadius);

        Vector3 pos;

        pos.x = centerPosition.position.x + xDistance * Mathf.Sin(angle * Mathf.Deg2Rad);

        pos.y = centerPosition.position.y + yDistance * Mathf.Cos(angle * Mathf.Deg2Rad);

        pos.z = centerPosition.position.z;
        return pos;
    }
*/

    public void raycastMaker()
    {
        //Debug.Log("Ball position: " + ball.transform.position);



        Debug.DrawRay(ball.transform.position, direction * 100, Color.red, 10f);
        float actualradius = ball.GetComponent<SphereCollider>().radius * Mathf.Max(ball.transform.lossyScale.x, ball.transform.lossyScale.y, ball.transform.lossyScale.z);
        //Debug.Log(actualradius);
        if (Physics.SphereCast(ball.transform.position, actualradius, direction * 100, out hit, 100))
        {

            Debug.Log("hit tag: " + hit.transform.tag);


            if (hit.transform.tag == "LeftHand" || hit.transform.tag == "RightHand")
            {
                GetComponent<ReactionTime>().stopTimer();
            }

        }


        //Debug.DrawRay(ball.transform.position, direction * 100, Color.black, 10f);

        //verify which objects raycast touched


        //    print("There is something in front of the object!");



    }

    public void raycastMaker(Vector3 dir)
    {
        
        direction= dir;


        raycastMaker();

    }


    public void preStartCompetitionMode(){
        SetThrower(throwerLeftLH, throwerLeftRH, spawnPointLeftLH, spawnPointLeftRH, 0);
        SetThrower(throwerMiddleLeftLH, throwerMiddleLeftRH, spawnPointMiddleLeftLH, spawnPointMiddleLeftRH, 1);
        SetThrower(throwerMiddleLH, throwerMiddleRH, spawnPointMiddleLH, spawnPointMiddleRH, 2);
        SetThrower(throwerMiddleRightLH, throwerMiddleRightRH, spawnPointMiddleRightLH, spawnPointMiddleRightRH, 3);
        SetThrower(throwerRightLH, throwerRightRH, spawnPointRightLH, spawnPointRightRH, 4);

        if(activatePointer){
            pointer.gameObject.SetActive(true);
        } else {
            pointer.gameObject.SetActive(false);
        }

        startCompetitionMode();
    }


    public void startCompetitionMode() {
        
        
        actualPosition.position = spawnPoints[2].position;

        StartCoroutine(SpawnBallsCompetitionMode());
    }


            
    public IEnumerator SpawnBallsCompetitionMode()
    {
        
        ball = Instantiate(ballPrefab, actualPosition.position, Quaternion.identity);
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();

        yield return new WaitForSeconds(chainSpeed);

        int numOfPasses = 0;

       // Debug.Log("bola criada");

        /*Debug.Log(actualPosition.position);
        Debug.Log(spawnPointMiddleLH.position);
        Debug.Log(spawnPointMiddleRH.position);*/

        while (!endCompetitionMode)
        {
            if (!isPaused)
            {
                // yield return new WaitForSeconds(chainSpeed);

                //Vector3 endPosition = GetRandomPositionWithinGoal();


                //BALL COLOR THAT COMES OUT OF THE SHOOTER HAND

                ballPrefab.GetComponent<Renderer>().sharedMaterial.color = Color.blue;


                //0 - LEFT
                //1 - MIDDLE LEFT
                //2 - MIDDLE
                //3 - MIDDLE RIGHT
                //4 - RIGHT

               // SetTrue(throwerLeftRH);
                //SetTrue(throwerMiddleLeftRH);
                //SetTrue(throwerMiddleRH);
                //SetTrue(throwerMiddleRightRH);
                //SetTrue(throwerRightRH);
                
                int numberWhereItsThrown = DisccoverNumberWhereItsThrown(actualPosition.position);

                if(numOfPasses<maxPasses){
                    nextPos = RandomNextThrow(numberWhereItsThrown);
                    numOfPasses++;
                } else {
                    flagToEnd = true;
                    nextPos = GetRandomPositionWithinGoal();
                    numOfPasses=0;
                }

                if(flagToEnd){
                    if(competitionLevel2){
                        
                        Vector3 toPosition = nextPos;
                        toPosition.x=0;
                        Vector3 fromPosition=Camera.main.transform.position;

                        fromPosition.x=0f;
                        Debug.Log("fromPosition1: "+fromPosition);
                       // Debug.Log("targetPosition: "+toPosition);


                        Vector3 dir = (toPosition-fromPosition).normalized;
                        float angle = (Mathf.Atan2(dir.z, dir.y) * Mathf.Rad2Deg) % 360;

                       // Debug.Log(angle);
                        pointer.localEulerAngles = new Vector3(0,0,-angle);

                        yield return new WaitForSeconds(1f);

                    }

                    if(competitionLevel2SubLevel){
                        System.Random rand = new System.Random();
        
                        var randAngle = rand.Next(0,360);

                        pointer.localEulerAngles = new Vector3(0,0,randAngle);
                        yield return new WaitForSeconds(1f);

                    }
                }

                PlaySound(soundClipThrower);
                if (GetComponent<ReactionTime>().reactionTimeToMake && flagToEnd)
                {
                    Debug.Log("O TEMPO COMEÇOU");
                    GetComponent<ReactionTime>().startTime();
                }
                instatiateBallComprtitionMode(nextPos, actualPosition, ball);

                actualPosition.position = nextPos;

               
                

                yield return new WaitUntil(CheckCompetitionModeBallThrown);

              

                if (flagToEnd)
                {
                    
                    endCompetitionMode = true;
                    if(competitionLevel2 || competitionLevel2SubLevel){
                        pointer.localEulerAngles = new Vector3(0,0,180);
                    }
                }
            }
        }

    }

    
    Vector3 RandomNextThrow(int numberWhereItsThrown)
    {
        
        System.Random rand = new System.Random();

        var nextThrow =0;
        do {
            nextThrow = rand.Next(0, 6);
        }
        while (nextThrow == numberWhereItsThrown);

        Debug.Log(nextThrow);
        
        switch (nextThrow)
        {
            case 0:
                flagToEnd = true;
                return GetRandomPositionWithinGoal();
            case 1:
                return spawnPoints[0].position;
            case 2:
                return spawnPoints[1].position;
            case 3:
                return spawnPoints[2].position;
            case 4:
                return spawnPoints[3].position;
            case 5:
                return spawnPoints[4].position;
            default:

                flagToEnd = true;
                if (GetComponent<ReactionTime>().reactionTimeToMake)
                {
                    GetComponent<ReactionTime>().startTime();
                }
                return GetRandomPositionWithinGoal();
        }

    }

    int DisccoverNumberWhereItsThrown(Vector3 startPosition)
    {
        if (startPosition == spawnPoints[0].position)
        {
            return 1;

        } else if (startPosition == spawnPoints[1].position)
        {
            return 2;
        }
        else if (startPosition == spawnPoints[2].position)
        {
            return 3;
         }
        else if (startPosition == spawnPoints[3].position)
        {
            return 4;  
        }
        else
        {
            return 5;
        }
    }
    
    void instatiateBallComprtitionMode(Vector3 endPosition, Transform position, GameObject ball)
    {

        //Vector3 finalPosition = randomPositionInCircle(position);



        //  float speed = Random.Range(minSpeed, maxSpeed);
        // direction = (endPosition - finalPosition).normalized;

        // ball.transform.position = Vector3.MoveTowards(finalPosition, endPosition, speed * Time.deltaTime);

        //if (GetComponent<ReactionTime>().reactionTimeToMake)
        //{
        //    GetComponent<ReactionTime>().startTime();
       // }



        competitionModeBallThrown = false;


    }

    private bool CheckCompetitionModeBallThrown()
    {
        return competitionModeBallThrown;
    }
    
    private void SetThrower(GameObject left, GameObject right, Transform spawnPointL, Transform spawnPointR, int pos) {
        System.Random rand = new System.Random();

        int num = rand.Next(0,2);

        Transform spawnPoint;

        if(num==0) {
            SetTrue(left);
            spawnPoint = spawnPointL;
        } else {
            SetTrue(right);
            spawnPoint = spawnPointR;
        }

        spawnPoints[pos] = spawnPoint;
    }

    public void PlaySound(AudioClip clip)
    {
        // Define o novo clip de áudio para reprodução
        audioSource.GetComponent<AudioSource>().clip = clip;
        
        // Reproduz o som
        audioSource.GetComponent<AudioSource>().Play();
    }
}
