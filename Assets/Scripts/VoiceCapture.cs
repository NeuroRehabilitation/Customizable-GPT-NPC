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
