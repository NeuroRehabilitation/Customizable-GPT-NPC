using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
public class WizardOfOz : MonoBehaviour
{
    public Animator anim;
    private int random;
    public AudioSource source;
    public AudioClip[] speeches;

    private Vector3 initialPos;
    private Quaternion initialRot;

    private AudioClip emotionSound;

    private int ChosenSpeech;
    public GameObject lookAtTarget;

    private bool isFirstkey = true;

    public float cooldownDuration = .3f; // duration of the cooldown in seconds
    private float nextUseTime; // time when the key will be able to be used again
    private bool onCooldown; // whether the key is currently on cooldown
    public float lastRotation = 17.5f;
    public bool therapistOverride = false;
    public bool agentTalking=false;


    // Start is called before the first frame update
    void Start()
    {
        anim.SetFloat("speed", 1.0f); 
        initialPos = transform.position;
        initialRot = transform.rotation;
        playIntro();
    }

    // Update is called once per frame
    void Update()
    {   
        //keep user in the same position
        transform.position=initialPos;
        transform.rotation=initialRot;

        // manually set the camera to the agent
        if (Input.GetKeyDown(KeyCode.J))
        {
            Transform xrOrigin = GameObject.Find("XR Origin").transform;
            lastRotation = lastRotation - Camera.main.transform.eulerAngles.y;
            xrOrigin.eulerAngles = new Vector3(xrOrigin.eulerAngles.x, lastRotation, xrOrigin.eulerAngles.z);
            lastRotation= lastRotation + 17f;
        }

        // set agent animations and speech depending on the button pressed
        if (Time.time > nextUseTime)
        {

            if (IsAnyKeyboardKeyPressed())
            {
                therapistOverride = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))//presenattion
            {
                ChooseSpeech(0,"Blend_softsmile");
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("Wave", .1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                anim.CrossFade("Blend_softsmile", 0.1f,6, 0.1f);
                //anim.CrossFade("Blend_smile", 0.1f,5, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))//initial silence
            {
                ChooseSpeech(1,"Blend_softsmile");
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                anim.CrossFade("Blend_softsmile", 0.1f,6, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))//interesting
            {
                ChooseSpeech(2,"Blend_softsmile");
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                anim.CrossFade("Blend_interjection", 0.1f,5, 0.1f);
                source.PlayOneShot(speeches[7]);
                anim.CrossFade("Blend_smile", 0.1f,6, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))//not understand
            {
                anim.CrossFade("Head_TiltRight", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                anim.CrossFade("Blend_idle", 0.1f,5, 0.1f);
                anim.CrossFade("Blend_mhm", 0.1f,5, 0.1f);
                source.PlayOneShot(speeches[3]);
                anim.CrossFade("Blend_softsmile", 0.1f,6, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))//understand agree
            {
                lookAtTarget.GetComponent<Animator>().CrossFade("nod", 0.1f,0, 0.1f);
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                anim.CrossFade("Blend_mhm", 0.1f,5, 0.1f);
                source.PlayOneShot(speeches[4]);
                anim.CrossFade("Blend_idle", 0.1f,6, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))//emotional response1
            {
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                int speechIndex = Random.Range(0, 2);
                ChooseSpeech(5,"Blend_softsmile");
                anim.CrossFade("Blend_softsmile", 0.1f,6, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.T))//emotional response1
            {
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                int speechIndex = Random.Range(0, 2);
                ChooseSpeech(6,"Blend_softsmile");
                anim.CrossFade("Blend_softsmile", 0.1f,6, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))//lomngbreak
            {
                lookAtTarget.GetComponent<Animator>().CrossFade("nod", 0.1f,0, 0.1f);
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                int speechIndex = Random.Range(0, 2);
                ChooseSpeech(7,"Blend_softsmile");
                
                anim.CrossFade("Blend_idle", 0.1f,6, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.E))//lomngbreak
            {
                lookAtTarget.GetComponent<Animator>().CrossFade("nod", 0.1f,0, 0.1f);
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                int speechIndex = Random.Range(0, 2);
                ChooseSpeech(8,"Blend_softsmile");
                
                anim.CrossFade("Blend_idle", 0.1f,6, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))//short break
            {
                lookAtTarget.GetComponent<Animator>().CrossFade("nod", 0.1f,0, 0.1f);
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                int speechIndex = Random.Range(0, 2);
                ChooseSpeech(9,"Blend_softsmile");
                anim.CrossFade("Blend_idle", 0.1f,6, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.W))//short break
            {
                lookAtTarget.GetComponent<Animator>().CrossFade("nod", 0.1f,0, 0.1f);
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                int speechIndex = Random.Range(0, 2);
                ChooseSpeech(10,"Blend_softsmile");
                anim.CrossFade("Blend_idle", 0.1f,6, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))//end warning
            {
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                int speechIndex = Random.Range(0, 2);
                ChooseSpeech(11,"Blend_softsmile");
                anim.CrossFade("Blend_idle", 0.1f,6, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.Q))//end warning
            {
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                ChooseSpeech(12,"Blend_softsmile");
                anim.CrossFade("Blend_idle", 0.1f,6, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))//conclusion
            {
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                ChooseSpeech(13,"Blend_softsmile");
                
                anim.CrossFade("Blend_smile", 0.1f,6, 0.1f);
                waitSecs(7f);
            }
            if (Input.GetKeyDown(KeyCode.R))//presentation
            {
                ChooseSpeech(20,"Blend_softsmile");
                anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
                anim.CrossFade("Straight", 0.1f,2, 0.1f);
                anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
                anim.CrossFade("Sitted", 0.1f,4, 0.1f);
                anim.CrossFade("Blend_softsmile", 0.1f,6, 0.1f);
            }

            // set cooldown for keys
            if (Input.anyKey)
            {
                if (Input.anyKey)
                {
                    for (char c = '0'; c <= '9'; c++)
                    {
                        if (Input.GetKey(c.ToString()))
                        {
                            onCooldown = true;
                            nextUseTime = Time.time + cooldownDuration;
                        }
                    }

                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        if (Input.GetKey(c.ToString()))
                        {
                            onCooldown = true;
                            nextUseTime = Time.time + cooldownDuration;
                        }
                    }
                }
            }
        }  
        
        // if on cooldown then cant play animations
        if (onCooldown)
        {
            if (Time.time > nextUseTime)
            {
                onCooldown = false;
            }
        }
    }

    // checks if any key is pressed
    bool IsAnyKeyboardKeyPressed()
    {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if ((int)keyCode >= (int)KeyCode.A && (int)keyCode <= (int)KeyCode.Z)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    return true;
                }
            }
            else if ((int)keyCode >= (int)KeyCode.Alpha0 && (int)keyCode <= (int)KeyCode.Alpha9)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    return true;
                }
            }
            else if ((int)keyCode >= (int)KeyCode.Keypad0 && (int)keyCode <= (int)KeyCode.Keypad9)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    return true;
                }
            }
            else if ((int)keyCode >= (int)KeyCode.F1 && (int)keyCode <= (int)KeyCode.F12)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // depending on the chosen speech, start the mechanism to play the speech audio and mouth animation
    public void ChooseSpeech(int chosenSpeech, string nextState)
    {

        anim.SetFloat("speed", 1.0f); 
        ChosenSpeech=chosenSpeech;
        anim.CrossFade("Blend_talk", 0.1f,5, 0.1f);
        StartCoroutine(stateSpeed("Blend_talk",nextState));
    }

    int count = 0;

    // This is used for both the WizardOfOz approach and the automatic (agent chat.cs if ChosenSpeech -1) approach.
    // This algorithm is a bit rough because of some weird interactions with unity's animation system. When transitioning between states
    // the state is undetectable, so it don't know when the animation is over. So I had to use a coroutine to try again and again to detect
    IEnumerator stateSpeed(string state, string nextState)
    {
        agentTalking = true;
        count++;

        AudioClip soundToPlay;
        yield return new WaitForSeconds(.1f);
        if (anim.GetCurrentAnimatorStateInfo(5).IsName(state) && !anim.IsInTransition(5))
        {
            if (ChosenSpeech == -1)
                soundToPlay = emotionSound;
            else
            {   
                soundToPlay = speeches[ChosenSpeech];
            }
            source.PlayOneShot(soundToPlay);
            int randomInt = Random.Range(1, 7);
            anim.CrossFade("ArmsTalk"+randomInt, 0.1f,3, 0.1f);
            StartCoroutine(endSpeech(soundToPlay.length,"HandsLegs",3));
            StartCoroutine(endSpeech(soundToPlay.length,nextState,5));
            isFirstkey = true;
            
        }    
        else
        {   
            if(count < 50)
                StartCoroutine(stateSpeed(state,nextState)); 
            else 
                count = 0;
        }
    }
    IEnumerator endSpeech(float duration, string state, int stateLayer)
    {
        yield return new WaitForSeconds(duration);
        anim.CrossFade(state, 0.1f,stateLayer, 0.1f);

        agentTalking = false;
    }

    // pre-defined sets of animations to play when agents' chat.cs algorithm finds it suitable
    // also used for the WizardOfOz approach in some cases
    public void playIntro()
    {
        random = Random.Range(1, 3);
        
        anim.CrossFade("intro"+random, 0.1f,2, 0.1f);
        anim.CrossFade("intro"+random, 0.1f,3, 0.1f);
        anim.CrossFade("intro"+random, 0.1f,4, 0.1f);
    }

    

    public void shortBreak()
    {
        lookAtTarget.GetComponent<Animator>().CrossFade("nod", 0.1f,0, 0.1f);
        anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
        anim.CrossFade("Straight", 0.1f,2, 0.1f);
        anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
        anim.CrossFade("Sitted", 0.1f,4, 0.1f);
        int speechIndex = Random.Range(0, 2);
        ChooseSpeech(7,"Blend_softsmile");
        anim.CrossFade("Blend_idle", 0.1f,6, 0.1f);
    }

    public void conclusion()
    {
        anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
        anim.CrossFade("Straight", 0.1f,2, 0.1f);
        
        anim.CrossFade("Sitted", 0.1f,4, 0.1f);
        ChooseSpeech(13,"Blend_softsmile");
        
        anim.CrossFade("Blend_smile", 0.1f,6, 0.1f);
        waitSecs(7f);
    }

    public void conclusionWarn()
    {

        anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
        anim.CrossFade("Straight", 0.1f,2, 0.1f);
        anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
        anim.CrossFade("Sitted", 0.1f,4, 0.1f);
        int speechIndex = Random.Range(0, 2);
        ChooseSpeech(11,"Blend_softsmile");
        anim.CrossFade("Blend_idle", 0.1f,6, 0.1f);

    }

    public void persuade()
    {
        int randomSpeech = UnityEngine.Random.Range(14, 22); // generate random integer between 14 and 22
        ChooseSpeech(randomSpeech,"Blend_softsmile");
        anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
        anim.CrossFade("Straight", 0.1f,2, 0.1f);
        anim.CrossFade("HandsLegs", 0.1f,3, 0.1f);
        anim.CrossFade("Sitted", 0.1f,4, 0.1f);
        anim.CrossFade("Blend_softsmile", 0.1f,6, 0.1f);
    }

    public void greet()
    {
        ChooseSpeech(0,"Blend_softsmile");
        anim.CrossFade("Head_Idle", 0.1f,1, 0.1f); 
        anim.CrossFade("Straight", 0.1f,2, 0.1f);
        anim.CrossFade("Wave", .1f,3, 0.1f);
        anim.CrossFade("Sitted", 0.1f,4, 0.1f);
        anim.CrossFade("Blend_softsmile", 0.1f,6, 0.1f);
    }

    // used by the agent's chat.cs 
    // depending on the emotion level, it chooses a random phrase from the list of phrases for that emotion level and plays it using the same mechanism as wizardofoz
    public void speakEmotion(int emotionLevel)
    {
        if (emotionLevel < 1 || emotionLevel > 10)
        {
            Debug.LogError("No emotional levels, default 1");
            emotionLevel = 1;
        }

        List<AudioClip> phrases = null;

        // Find the matching emotionID
        foreach (EmotionPhrase emotionPhrase in GetComponent<Emotions>().emotionPhrases)
        {
            if (emotionPhrase.emotionID == emotionLevel)
            {
                phrases = emotionPhrase.phrases;
                break;
            }
        }

        if (phrases == null || phrases.Count == 0)
        {
            Debug.LogError("No phrases found for the given emotion level");
            return;
        }

        int randomIndex = Random.Range(0, phrases.Count);
        AudioClip chosenPhrase = phrases[randomIndex];

        emotionSound = chosenPhrase;
        ChooseSpeech(-1, "Blend_softsmile");

        Debug.Log(chosenPhrase);
    }

    // wait some seconds before leaving the scene
    public void waitSecs(float secs)
    {
        StartCoroutine(waitSecsIE(secs));
    }
    IEnumerator waitSecsIE(float secs)
    {

        yield return new WaitForSeconds(secs);

        anim.CrossFade("Wave", 0.1f,3, 0.1f);
        GameObject.Find("StateController").GetComponent<StateController>().currScene="Gallery";
        GameObject.Find("StateController").GetComponent<StateController>().SpawnPos= new Vector3(-84.8499985f,12.4399996f,25.3199997f);
        JSONUtils.UpdateJson();
        GameObject.Find("StateController").GetComponent<StateController>().currStage=0;
        
        SceneManager.LoadScene("Gallery");
        

    }
}

