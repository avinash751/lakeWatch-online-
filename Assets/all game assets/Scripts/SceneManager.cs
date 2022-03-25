using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    
    [Header("All Raycast Information")]
    public Ray CameraScreenRaycast;
    public string gameObjectName;
    public RaycastHit gameObjectClickInfo;

    [Header("Game Object & componants  References")]
    public Camera cameraObject;
   
    [Header("Camera  Lerping Location Offset information")]
    public GameObject offset_1_Location;
    public GameObject offset_2_Location;
    public GameObject offset_3_Location;
    public GameObject offset_4_Location;
    public GameObject NormalViewLocation;

    public bool cameraMoving = false;
    public bool offset_1Bool = false;
    public bool offset_2Bool = false;
    public bool offset_3Bool = false;
    public bool offset_4Bool = false;
    public bool NormalViewBool = false;

    [Range(0,1)]public float lerpValue;


    [Header("Lantern functionality variables")]
    public Light light1;
    public Light light2;
    private bool lightactive = false;

    [Header("BoatPaddleFuctionality")]
    public Animator PaddleAnim;
    public bool OnBoat = true;
    public bool animPlaying = false;

    [Header("Audio properties")]
    public AudioSource WhooshAudio;
    public bool isAudioplaying = false;

    [Header("spotlight info")]
    public Light spotlight_1;
    public Light spotlight_2;
    public float spotlightDelayTimer;
    void Start()
    {
        PaddleAnim.enabled = false;
       
    }

    // Update is called once per frame
    void Update()
    {

        StartRaycasting();
        SwitchCameraOffset();
        SwitchbackToNormalView();
        
        // checking if left mouse button is clicked. if clicked it calls StartRaycasting(); fun

        if (Input.GetMouseButtonDown(0))
        {
            BoatPaddleRightClickPlayAnim();
            LanternRightClickLightFuctionality();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            BoatClicked();
            BoatPaddlesLeftClickMove();
            LanternLeftClickMove();
            DialogueTrigger();
        }
    }

  

    // all rayacasting interactivity happens here.
    void StartRaycasting()
    {
        // creates a raycast from the camera screen, the mouse position or click is the direction
        CameraScreenRaycast = cameraObject.ScreenPointToRay(Input.mousePosition);

        // checks whether raycast has passed or collided with anyhting
        if(Physics.Raycast(CameraScreenRaycast,out gameObjectClickInfo))
        {
            // gets the object name from whatver the raycast has collided with, can be seen in the inspector
            gameObjectName = gameObjectClickInfo.collider.name;

            // can see what the rayscast hitt, can viewed in the console.
           // Debug.Log(gameObjectName);
        }
    }
    void DialogueTrigger()
    {
        if (gameObjectClickInfo.collider.tag == "Box")
        {
            Debug.Log("Trigger Clicked ");
        }
    }

    void BoatClicked()
   {
        if (gameObjectClickInfo.collider.tag  == "Boat")
        {
            Debug.Log("BoatClicked clicked");
            if(transform.position != offset_1_Location.transform.position && offset_1Bool == true)
            {
                DisableOrEnableSound(WhooshAudio, true, isAudioplaying = false);
                if (isAudioplaying == false)
                {
                    WhooshAudio.pitch = Random.Range(0.9f, 1.2f);
                    WhooshAudio.Play();
                    isAudioplaying = true;
                }

                offset_1Bool = false;
                SettingCameraOffsetvariables(offset_2Bool = true, offset_3Bool = true, offset_4Bool = true, NormalViewBool = true);
                cameraMoving = true;
            }
        }
   }

    void BoatPaddlesLeftClickMove()
    {
        if(gameObjectClickInfo.collider.tag == "Paddle_1" )
        {
            if (transform.position != offset_2_Location.transform.position && offset_2Bool == true)
            {
                
                DisableOrEnableSound(WhooshAudio, true, isAudioplaying = false);
                if (isAudioplaying == false)
                {
                    WhooshAudio.pitch = Random.Range(0.9f, 1.2f);
                    WhooshAudio.Play();
                    isAudioplaying = true;
                }

                offset_2Bool = false;
                SettingCameraOffsetvariables(offset_1Bool = true, offset_3Bool = true, offset_4Bool = true, NormalViewBool = true);
                
                cameraMoving = true;
            }
        }
        else if(gameObjectClickInfo.collider.tag == "Paddle_2" && offset_3Bool == true)
        {
            if (transform.position != offset_3_Location.transform.position)
            {
                DisableOrEnableSound(WhooshAudio, true, isAudioplaying = false);
                if (isAudioplaying == false)
                {
                    WhooshAudio.pitch = Random.Range(0.9f, 1.2f);
                    WhooshAudio.Play();
                    isAudioplaying = true;
                }

                offset_3Bool = false;
                SettingCameraOffsetvariables(offset_1Bool = true, offset_2Bool = true, offset_4Bool = true, NormalViewBool = true);
                cameraMoving = true;
            }
        }
    }

    void BoatPaddleRightClickPlayAnim()
    {
        if (gameObjectClickInfo.collider.tag == "Paddle_1" || gameObjectClickInfo.collider.tag == "Paddle_2")
        {
            if (OnBoat == true && animPlaying == false)
            {
                PaddleAnim.enabled = true;
                PaddleAnim.Play("paddleOnWater");
                OnBoat = false;
                animPlaying = true;
                StartCoroutine(disablePaddleAnimator());
            }
            else if (OnBoat == false && animPlaying == false)
            {
                PaddleAnim.enabled = true;
                PaddleAnim.Play("paddleOnBoat");
                OnBoat = true;
                animPlaying = true;
                StartCoroutine(disablePaddleAnimator());

            }
        }
    }

    IEnumerator disablePaddleAnimator()
    {
        yield return new WaitForSeconds(3.5f);
        PaddleAnim.enabled = false;
        animPlaying = false;

    }

    void LanternRightClickLightFuctionality()
    {
        if (gameObjectClickInfo.collider.tag == "Lantern")
        {
            Debug.Log("LanternClicked clicked");
            if (lightactive == false)
            {
                //cameraMoving = true;
                light1.enabled = true;
                light2.enabled = true;
                lightactive = true;
            }
            else if (lightactive == true)
            {
                Debug.Log("HELLO");
                light1.enabled = false;
                light2.enabled = false;
                lightactive = false;
            }
        }
    }

    void LanternLeftClickMove()
    {
        if (gameObjectClickInfo.collider.tag == "Lantern")
        {
            if (transform.position != offset_4_Location.transform.position && offset_4Bool == true)
            {
                DisableOrEnableSound(WhooshAudio, true, isAudioplaying = false);
                if (isAudioplaying == false)
                {
                    WhooshAudio.pitch = Random.Range(0.8f, 1);
                    WhooshAudio.Play();
                    isAudioplaying = true;
                }

                offset_4Bool = false;
                SettingCameraOffsetvariables(offset_1Bool = true, offset_2Bool = true, offset_3Bool = true, NormalViewBool = true);
                cameraMoving = true;
            }
        }
    }
    void SwitchbackToNormalView()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (transform.position != NormalViewLocation.transform.position && NormalViewBool == true)
            {
                DisableOrEnableSound(WhooshAudio, true, isAudioplaying = false);
                if (isAudioplaying == false)
                {
                    WhooshAudio.pitch = Random.Range(0.9f, 1.2f);
                    WhooshAudio.Play();
                    isAudioplaying = true;
                }

                NormalViewBool = false;
                SettingCameraOffsetvariables(offset_1Bool = true, offset_2Bool = true, offset_3Bool = true, offset_4Bool = true);

                cameraMoving = true;
            }
        }
    }

    void SwitchCameraOffset()
    {
         // lerping to offset 1 near boat
        LerpCameraLocation(offset_1Bool,cameraMoving, offset_1_Location);
        // lerping to offset 2 near paddle 1
        LerpCameraLocation(offset_2Bool, cameraMoving, offset_2_Location);
        // lerping to offset 3 near paddle 2
        LerpCameraLocation(offset_3Bool, cameraMoving, offset_3_Location);
        // lerping to offset 4 near Lantern
        LerpCameraLocation(offset_4Bool, cameraMoving, offset_4_Location);
        // lerping to default view
        LerpCameraLocation(NormalViewBool, cameraMoving, NormalViewLocation);
    }

    void LerpCameraLocation(bool currentOffsetReachedBool, bool cameraMovingBool,GameObject offsetLocation)
    {
        if (currentOffsetReachedBool == false && cameraMovingBool == true)
        {
            transform.position = Vector3.Lerp(transform.position, offsetLocation.transform.position, lerpValue * Mathf.SmoothStep(0, 1, lerpValue));
            transform.rotation = Quaternion.Lerp(transform.rotation, offsetLocation.transform.rotation, lerpValue * Mathf.SmoothStep(0, 1, lerpValue));

           /* if (transform.position == offsetLocation.transform.position)
            {
               cameraMovingBool = false;
               currentOffsetReachedBool = true;
              // DisableOrEnableSound(WhooshAudio, false, isAudioplaying=false);--
            }*/
        }
    }

    void DisableOrEnableSound(AudioSource audioAsset,bool activated,bool isPlaying)
    {
        audioAsset.enabled = activated;
    }

    void SettingCameraOffsetvariables(bool offsetReachedBool_1, bool offsetReachedBool_2, bool offsetReachedBool_3,bool normalView)
    {

    }

  

}

