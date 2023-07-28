/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceCapture : MonoBehaviour
{
    public bool agentTurn = false;
    public string device;
    public AudioClip clipRecorded;
    public float minSilenceDuration; // Minimum duration of silence in seconds.

    public STTService sttService;
    private bool isSilenceDetected;

    public float silenceTimer;
    public float speakingTimer;
    public AgentBehaviour agentBehaviour;
    public float rmsValue;

    [SerializeField] private int frequency = 16000;
    private const int WindowSize = 1600;  // Size of the window to check for silence
    public float SilenceThreshold;  // Threshold below which the signal is considered silent
    public float debugint;
    
    void Start()
    {
        agentBehaviour = GetComponent<AgentBehaviour>();
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
        if (!agentBehaviour.agentTalking && Microphone.GetPosition(device) > 0)
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
            int currentSample = Mathf.RoundToInt(speakingTimer * frequency) - 10;

            // Sliding window implementation
            float sum = 0f;
            int samplesCount = Mathf.Min(WindowSize, currentSample);  // Actual number of samples
            for (int i = currentSample - samplesCount; i < currentSample; i++)
            {
                sum += audioData[i] * audioData[i];  // Square and sum up
            }
            rmsValue = Mathf.Sqrt(sum / samplesCount);  // Calculate RMS

            // If the RMS value is below the silence threshold, start counting silence time.
            if (rmsValue < SilenceThreshold)
            {
                silenceTimer += Time.deltaTime;

                // If the silence duration exceeds the minimum required, stop recording.
                if (silenceTimer >= minSilenceDuration && !isSilenceDetected && agentTurn)
                {
                    print("Silence Detected");
                    agentBehaviour.Thinking();
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
                agentTurn=true;
            }
        }
    }   
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceCapture : MonoBehaviour
{
    public bool voiceResponse = false;
    public string device;
    public AudioClip clipRecorded;

    public STTService sttService;

    public float speakingTimer;
    public AgentBehaviour agentBehaviour;

    [SerializeField] private int frequency = 16000;
    
    void Start()
    {
        agentBehaviour = GetComponent<AgentBehaviour>();
        device = Microphone.devices[0]; // Get default microphone.
        StartRecording();
    }

    void StartRecording()
    {
        // Start recording indefinitely until 'K' key is pressed.
        clipRecorded = Microphone.Start(device, true, 5, frequency);
    }

    void StopRecording(bool voiceResponse=true)
    {
        Microphone.End(device);
        StartCoroutine(sttService.Transcribe(clipRecorded, voiceResponse));
        StartRecording();
    }

    void FixedUpdate()
    {
        if (!agentBehaviour.agentTalking)
        {
            if (speakingTimer > 4f)
            {
                speakingTimer = 0f;
                StopRecording(false);
                return;
            }

            speakingTimer += Time.deltaTime;

            // If 'K' key is pressed, stop recording.
            if (Input.GetKey(KeyCode.K))
            {
                agentBehaviour.Thinking();
                agentBehaviour.agentTalking = true;
                speakingTimer = 0f;
                StopRecording(true);
            }
        }
    }   
}
