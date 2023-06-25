using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{
    [HideInInspector]public Animator anim;
    
    [HideInInspector]public AudioSource audioSource;
    
    [HideInInspector]public AudioClip voiceResponse;
    public List<string> actions;
    public bool agentTalking = false;

    private bool wasPlaying = false; // Add this line
    public string personality;

    public void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Act(AudioClip localVoiceResponse)
    {
        print("Acting");
        voiceResponse = localVoiceResponse;
        actions = GetComponent<OpenAIChat>().actions;
        Speak();
        Actions();
    }

    public void Speak()
    {
        audioSource.clip = voiceResponse;
        audioSource.Play();
    }

    public void Update()
    {
        bool isPlaying = audioSource.isPlaying;
        if (isPlaying != wasPlaying)  // Only change the animation when the audio's playing state changes
        {
            if (isPlaying)
            {
                // random int 1-5 inclusive
                int randomInt = Random.Range(1, 5);
                anim.CrossFade("ArmsMoving"+randomInt, 0.1f, 3, 0.1f); // Play the "ArmsTalk" animation while the AudioClip is playing
                anim.CrossFade("MouthTalk", 0.1f, 5, 0.01f);
            }
            else
            {
                anim.CrossFade("HeadIdle", 1f, 1, 0.5f); // Set the animation state to idle when the AudioClip is not playing
                anim.CrossFade("TorsoStraight", 0.4f, 2, 0.5f); // Set the animation state to idle when the AudioClip is not playing
                anim.CrossFade("ArmsInLegs", 0.1f, 3, 0.1f); // Set the animation state to idle when the AudioClip is not playing
                anim.CrossFade("LegsIdle", 0.4f, 4, 0.5f); // Set the animation state to idle when the AudioClip is not playing
                anim.CrossFade("MouthIdle", 0.1f, 5, 0.1f);
                agentTalking=false;
            }
            wasPlaying = isPlaying;  // Update the wasPlaying state
        }
    }

    public void Thinking()
    {
        anim.CrossFade("HeadTilt", 0.2f, 1, 0.1f);
        anim.CrossFade("ArmsThinking", .2f, 3, 0.1f);
    }

    public void Actions()
    {
        foreach (var action in actions)
        {
            if (action.Contains("Head"))
            {
                anim.CrossFade(action, 1f,1, 0.1f); 
            }

            if (action.Contains("Torso"))
            {
                anim.CrossFade(action, 0.2f,2, 0.1f); 
            }

            // Do not check for "Arms" here anymore since "ArmsTalk" and "ArmsIdle" are being controlled by the AudioClip status in Update()

            if (action.Contains("Legs"))
            {
                anim.CrossFade(action, 0.2f,4, 0.1f); 
            }
            if (action.Contains("Mouth"))
            {
                anim.CrossFade(action, 0.1f,5, 0.1f); 
            }
            if (action.Contains("EyeBrow"))
            {
                anim.CrossFade(action, 0.1f,6, 0.1f); 
            }
            
        }
    }


}
