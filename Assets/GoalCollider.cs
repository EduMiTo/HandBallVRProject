using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class GoalCollider : MonoBehaviour
{
    public bool touched=false;
    public GameObject gameObject;
    public GameObject goalie;

    public float timeToDestroy=0f;

    public bool answer=false;

    private InputDevice leftController;
    private InputDevice rightController;

    public List<Color> colorsToDefendLeft;
    public List<Color> colorsToDefendRight;

    public float vibrateAmplitude = 0.1f; // Set the vibration strength to maximum (0.1)
    public ushort vibrateDuration = 1; // Set the vibration duration to 1 milliseconds
    
    private HapticCapabilities capabilities;

    public Color badColor;

    public GameObject audioSource;
    public AudioClip soundClipRight;
    public AudioClip soundClipWrong;


    //goalie.GetComponent<BallSpawner>().color;

    //public GameObject popUp;
    public GameObject feedBack;
    void OnCollisionEnter(Collision collision)
    {       
        
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        // Add scoring or other game logic here~

        Debug.Log(badColor);

        if (collision.gameObject.CompareTag("Goal") && badColor != gameObject.GetComponent<Renderer>().material.color)
        {
            Feedback.PlayNetWrong();
            Debug.Log("ERRADO COM BALIZAAAAAAA");
            goalie.GetComponent<ReactionTime>().badDefenceReactionTime();

            answer = false;

        }
        else if((!collision.gameObject.CompareTag("LeftHand") || !collision.gameObject.CompareTag("RightHand")) && badColor == gameObject.GetComponent<Renderer>().material.color)
        {
            Feedback.PlayNetRight();
            Debug.Log("CERTO COM BALIZAAAAAAA");
            goalie.GetComponent<ReactionTime>().badDefenceReactionTime();

            answer = true;
        }
            
            
        //Make the controller vibration when catch the ball
        if(collision.gameObject.CompareTag("LeftHand")){
            if(colorsToDefendLeft.Contains(gameObject.GetComponent<Renderer>().material.color)){
                goalie.GetComponent<ReactionTime>().goodDefenceReactionTime();
                Feedback.PlayRightSound();
                answer = true;

            }else{
                goalie.GetComponent<ReactionTime>().badDefenceReactionTime();
                Feedback.PlayWrongSound();
                answer = false;
                

                //feedBack.GetComponent<Feedback>().wrongAnswersCount++;
            }
            if (leftController.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    leftController.SendHapticImpulse(0, vibrateAmplitude, vibrateDuration);
                }
            }
        }
        if(collision.gameObject.CompareTag("RightHand")){
            if(colorsToDefendRight.Contains(gameObject.GetComponent<Renderer>().material.color)){
                goalie.GetComponent<ReactionTime>().goodDefenceReactionTime();
                Feedback.PlayRightSound();
                answer = true;

            }else{
                
                goalie.GetComponent<ReactionTime>().badDefenceReactionTime();
                Feedback.PlayWrongSound();
                answer = false;


                //feedBack.GetComponent<Feedback>().wrongAnswersCount++;
            }
            if (rightController.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    rightController.SendHapticImpulse(0, vibrateAmplitude, vibrateDuration);
                }
            }
        }
        touched = true;
    }

    void Start(){
        answer=false;
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }
    void Update(){
        if(touched){

            

            if(answer){
                //popUp.GetComponent<PopUpWindowScript>().AddToQueue("Good answer");
                //PlaySound(soundClipRight);
                
                feedBack.GetComponent<Feedback>().rightAnswersCount++;
                
                
                Destroy(gameObject, timeToDestroy);
                
               // goalie.GetComponent<ReactionTime>().goodDefenceReactionTime();
                Debug.Log("Good answer");
            }else{
                //PlaySound(soundClipWrong);
                
                feedBack.GetComponent<Feedback>().wrongAnswersCount++;
                
                Destroy(gameObject, timeToDestroy);
                //goalie.GetComponent<ReactionTime>().badDefenceReactionTime();

                Debug.Log("Bad answer");
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        // Define o novo clip de áudio para reprodução
        audioSource.GetComponent<AudioSource>().clip = clip;
        Debug.Log("THE CLIP IS: " + audioSource.GetComponent<AudioSource>().clip);

        // Reproduz o som
        audioSource.GetComponent<AudioSource>().Play();
    }

}