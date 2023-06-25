using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceCapture : MonoBehaviour
{
    public string device;
    public AudioClip clipRecorded;
    public float minSilenceDuration; // Minimum duration of silence in seconds.

    public STTService sttService;
    private bool isRecording;
    private bool isSilenceDetected;

    public bool agentTalking;

    public float silenceTimer;
    public float speakingTimer;

    public float levelMax;
    public AgentBehaviour agentBehaviour;

    public float rmsValue;

    [SerializeField] private int frequency = 16000;
    private const int WindowSize = 500;  // Size of the window to check for silence
    public float SilenceThreshold;  // Threshold below which the signal is considered silent
    public float debugint;
    void Start()
    {
        agentBehaviour = GetComponent<AgentBehaviour>();
        agentTalking = false;
        // Get default microphone.
        device = Microphone.devices[0];
        StartRecording();
    }

    void StartRecording()
    {
        // Start recording indefinitely until silence is detected.
        clipRecorded = Microphone.Start(device, true, 5, frequency);
    }

    void StopRecording()
    {
        Microphone.End(device);
        StartCoroutine(sttService.Transcribe(clipRecorded,isSilenceDetected));
        StartRecording();
    }

    void FixedUpdate()
{
    if (!agentBehaviour.agentTalking && Microphone.GetPosition(device) > 0) // Added the check for microphone position
    {
        if (speakingTimer > 4f)
        {
            speakingTimer = 0f;
            StopRecording();
            return;
        }
        speakingTimer += Time.deltaTime;

        // Check audio signal levels to detect silence.
        float[] audioData = new float[clipRecorded.samples];
        clipRecorded.GetData(audioData, 0);
        try
        {
            rmsValue = audioData[Mathf.RoundToInt(speakingTimer*16000)-10]*1000;
        }
        catch (System.Exception)
        {
            
        }
        // If the RMS value is below the silence threshold, start counting silence time.
        if (rmsValue < SilenceThreshold)
        {
            silenceTimer += Time.deltaTime;

            // If the silence duration exceeds the minimum required, stop recording.
            if (silenceTimer >= minSilenceDuration && !isSilenceDetected)
            {
                print("Silence Detected");
                agentBehaviour.Thinking();
                isRecording = true;
                silenceTimer = 0f;
                agentBehaviour.agentTalking = true;
                isSilenceDetected = true;
                speakingTimer=0f;
                StopRecording();
                isSilenceDetected = false;
            }
        }
        else
        {
            silenceTimer = 0f;
            isSilenceDetected = false;
        }
    }
    
}

}
