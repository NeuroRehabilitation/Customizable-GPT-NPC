using System.Collections;
using UnityEngine;
using OpenAI_API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using System.Threading.Tasks;
using System.Threading;

using Microsoft.CognitiveServices.Speech;

public class Chat : MonoBehaviour
{
    private SpeechConfig speechConfig;
    private SpeechRecognizer recognizer;
    private SpeechSynthesizer synthesizer;

    public WizardOfOz WO;
    public GameObject hourglass;
    public float rms = 0f;
    private OpenAIAPI chatGPTApi;
    public OpenAI_API.Chat.Conversation chat;
    public float silenceThreshold = 0.01f;
    private AudioClip microphoneInput;
    private string microphoneDevice;
    private Coroutine silenceCoroutine;

    [Header("Inputs")]

    public TMP_InputField firstStageSilenceThresholdInput;
    public TMP_InputField agentResponseThresholdInput;
    public TMP_InputField shortSilenceThresholdInput;

    public TMP_InputField userPersuadedLimitInput;
    public TMP_InputField firstStageLimitInput;
    public TMP_InputField sessionLimitInput;

    public TMP_InputField silenceDurationInput;
    public TMP_InputField talkingDurationInput;
    public TMP_InputField totalDurationInput;

    public TMP_InputField firstStageInput;
    public TMP_InputField agentTalkingInput;
    public TMP_InputField userPersuadedInput;
    public TMP_InputField userTalkedInput;
    public TMP_InputField userRespondedEmotionInput;
    public TMP_InputField finishedInput;
    public TMP_InputField agentRespondedEmotionInput;
    public TMP_InputField agentRespondedSilenceInput;
    public TMP_InputField therapistOverrideInput;

    [Header("Api Config")]
    [SerializeField] private string azureApiKey = "ea7dd199531644d0814e9c666eca524a";
    [SerializeField] private string azureApiRegion = "westeurope";
    [SerializeField] private string languageCode = "pt-PT";
    [SerializeField] private string chatGPTApiKey = "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";

    [Header("Time Limits")]
    public float silenceDuration;
    public float talkingDuration;
    public float firstStageSilenceThreshold = 4f;
    public float agentResponseThreshold = 4f;
    public float shortSilenceThreshold = 6f;
    public float totalDuration;
    public float sessionLimit=900f;
    public float firstStageLimit=60f;

    public float userPersuadedLimit=5f;

    public float silenceStartTime = 0;
    public float talkingStartTime = 0;


    [Header("Flags")]
    public bool finishingStage=false;
    public bool firstStage=true;
    public bool agentTalking=false;
    public bool userPersuaded=false;
    public bool userTalked=false;
    public bool userRespondedEmotion=false;
    public bool finished=false;
    public bool agentRespondedEmotion;
    public bool agentRespondedSilence;
    public bool canTalk = true ;
    public  bool isRecording = false;

    public  bool userTalking = false;

    

    public Coroutine myCoroutine;

    //structure to store the history of emotions detected through the conversation
    public List<HistoryEmotion> historyEmotions;
    [System.Serializable]
    public class HistoryEmotion
    {
        public string stringValue;
        public int intValue;

        public HistoryEmotion(string stringValue, int intValue)
        {
            this.stringValue = stringValue;
            this.intValue = intValue;
        }
    }

    private void Start()
    {
        // start greeting
        WO.greet();
        totalDuration = 0f;
        initializeUI();

        // Get the default microphone
        microphoneDevice = Microphone.devices[0];

        // Start recording from the microphone
        microphoneInput = Microphone.Start(microphoneDevice, true, 1, 44100);

        // Start checking for silence
        silenceCoroutine = StartCoroutine(SilenceDuration());

        // Initialize the microsoft STT api
        speechConfig = SpeechConfig.FromSubscription(azureApiKey, azureApiRegion);
        speechConfig.SpeechRecognitionLanguage = languageCode;
        speechConfig.SetProperty(PropertyId.SpeechServiceConnection_InitialSilenceTimeoutMs, "15000"); // 2 seconds
        speechConfig.SetProperty(PropertyId.Speech_SegmentationSilenceTimeoutMs , "1000"); // 2 seconds
        speechConfig.SpeechSynthesisLanguage = languageCode;
        speechConfig.SpeechSynthesisVoiceName = "pt-PT-RaquelNeural";
        recognizer = new SpeechRecognizer(speechConfig);
        synthesizer = new SpeechSynthesizer(speechConfig);
        Task.Run(() => RecordAndTranscribe());
    }

    public void initializeUI()
    {

        firstStageSilenceThresholdInput.text = firstStageSilenceThreshold+"";
        agentResponseThresholdInput.text =  agentResponseThreshold+"";
        shortSilenceThresholdInput.text = shortSilenceThreshold+"";

        userPersuadedLimitInput.text = userPersuadedLimit+"";
        firstStageLimitInput.text = firstStageLimit+"";
        sessionLimitInput.text = sessionLimit+"";
    }

    // detects silence and talking duration
    private IEnumerator SilenceDuration()
    {

        int sampleWindow = 1024; 
        float[] samples = new float[sampleWindow];

        while (true)
        {
        
            if (microphoneInput == null)
            {
                yield return null;
                continue;
            }
    
            int microphonePosition = Microphone.GetPosition(microphoneDevice);
            if (microphonePosition >= sampleWindow)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    
                    
                    if (microphoneInput != null)
                    {
                        microphoneInput.GetData(samples, microphonePosition - sampleWindow);
                    }
                });

                rms = 0f;
                for (int i = 0; i < sampleWindow; i++)
                {
                    rms += Mathf.Pow(samples[i], 2);
                }
                rms /= sampleWindow;
                rms = Mathf.Sqrt(rms);

        
                if (rms < silenceThreshold && !WO.agentTalking)
                {
                    silenceDuration = Time.time - silenceStartTime;

                    if (silenceDuration > 1f)
                    {
                        talkingStartTime = Time.time;
                        talkingDuration= 0; 
                    }
                    
                }

                else if (rms > silenceThreshold)
                {
                    if (agentRespondedEmotion)
                    {
                        userRespondedEmotion = true;
                    }
                        
                    userTalked=true;
                    userTalking = true;

                    talkingDuration = Time.time - talkingStartTime;
                    
                    silenceStartTime = Time.time; 
                    silenceDuration = 0; 
                
                }
                else if (WO.agentTalking)
                {
                    silenceStartTime = Time.time; 
                    silenceDuration = 0; 
                    talkingStartTime = Time.time;
                    talkingDuration= 0; 
                }
            }
            yield return null;
        }
    }

    // microsoft STT api callback
    private async Task RecordAndTranscribe()
    {
        Debug.Log("Starting voice recording...");
        recognizer.Recognizing += Recognizer_Recognizing;
        recognizer.Recognized += Recognizer_Recognized;

        await recognizer.StartContinuousRecognitionAsync();

        // Add a cancellation token and a delay here, so you can stop the continuous recognition after a specified amount of time.
        using (var cts = new CancellationTokenSource())
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(10), cts.Token);
            }
            catch (TaskCanceledException)
            {
                Debug.Log("Continuous recognition canceled.");
            }
        }

        await recognizer.StopContinuousRecognitionAsync();
        recognizer.Recognizing -= Recognizer_Recognizing;
        recognizer.Recognized -= Recognizer_Recognized;

        // Start the method again.
        await RecordAndTranscribe();
    }

    // microsoft STT api callback
    private void Recognizer_Recognizing(object sender, SpeechRecognitionEventArgs e){}

    // microsoft STT api method to get the STTed string, sends it to chatgpt and add the detected emotion 
    private async void Recognizer_Recognized(object sender, SpeechRecognitionEventArgs e)
    {
        canTalk = false;
        if (e.Result.Reason == ResultReason.RecognizedSpeech)
        {
            if (e.Result.Text == "")
            {
                Debug.Log("SILENCE");
            }
            else
            {
                // set up chatgpt api
                chatGPTApi = new OpenAIAPI(new APIAuthentication(chatGPTApiKey));
                chatGPTApi.Chat.DefaultChatRequestArgs.Temperature=0.1;
                chat = chatGPTApi.Chat.CreateConversation();
                chat.AppendSystemMessage("*O utilizador envia respostas transcritas atraves de um software imperfeito. Se a frase nao fizer sentido, atribui o nivel emocional que te parece mais adequado aconteça o que acontecer, a tua resposta terá sempre um valor do nivel emocional do utilizador* *AVALIA O NIVEL EMOCIONAL DA RESPOSTA DO UTILIZADOR* *A TUA RESPOSTA SERÁ SEMPRE APENAS O NIVEL EMOCIONAL DA FRASE DO UTILIZADOR, DE 1 A 10* RESPONDE SEMPRE, SEMPRE, COM UM NUMERO, *SEMPRE*");
                chat.AppendUserInput(e.Result.Text);
                var chatResult = await chat.GetResponseFromChatbot();
                var chatResponse = chatResult.ToString().Trim();
                try
                {
                    historyEmotions.Add(new HistoryEmotion(e.Result.Text,ExtractNumberFromString(chatResponse)) );
                    GameObject.Find("StateController").GetComponent<Logger>().logEvent("Share","user emotion level: "+historyEmotions[historyEmotions.Count].intValue + " to sentence: "+ historyEmotions[historyEmotions.Count].stringValue);
                }
                catch (ArgumentException ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
            isRecording = false;
        }
        else
        {
            Debug.LogWarning("No speech recognized");
        }
    }

    // algorithm better illustrated in the documentation
    private void FixedUpdate()
    {
        updateUI();
        agentTalking = WO.agentTalking;
        totalDuration += Time.fixedDeltaTime;
        //intro and persuade user to speak
        if (!WO.therapistOverride)
        {
            if (firstStage)
            {
                //Too long in the first stage, or used persuaded
                if (((totalDuration > firstStageLimit) || (userPersuaded)) && !WO.agentTalking)
                {
                    firstStage=false;
                    silenceStartTime = Time.time;
                    silenceDuration = 0; 
                }
                else
                // user silence
                if (silenceDuration>firstStageSilenceThreshold)
                {
                    WO.persuade();
                }
                else 
                // user talking, if so for userPersuadedLimit time then is persuaded
                if (userTalking)
                {
                    talkingDuration = Time.time - talkingStartTime;
                    if (talkingDuration > userPersuadedLimit)
                        userPersuaded = true;
                }
            }
            else
            if (!finished)
            {
                // has user begun?
                if (userTalked && silenceDuration>agentResponseThreshold)
                {
                    if(!finishingStage && (totalDuration+120f >= sessionLimit  && !WO.agentTalking))
                    {

                            finishingStage = true;
                            WO.conclusionWarn();
                            hourglass.SetActive(true);

                    }
                    else
                    //time limit?
                    if (totalDuration > sessionLimit && !WO.agentTalking)
                    {
                        WO.conclusion();
                        finished = true;
                    }
                    //Has user received a response from the agent before?
                    else
                    if(agentRespondedEmotion  && silenceDuration>shortSilenceThreshold)
                    {
                        // has the user received a response from the agent from the short silence?
                        if (agentRespondedSilence)
                        {
                            WO.conclusion();
                            finished = true;
                        }
                        else
                        {
                            WO.shortBreak();
                            agentRespondedSilence = true;
                        }

                    }
                  
                    else
                    if (!agentRespondedEmotion)
                    {
                   
                        if (historyEmotions.Count == 0)
                            WO.speakEmotion(1);
                        else
                            WO.speakEmotion(historyEmotions[historyEmotions.Count-1].intValue);
                        agentRespondedEmotion=true;
                        agentRespondedSilence= false;
                    }
                }
           
                else if (userRespondedEmotion)
                {
                    userRespondedEmotion=false;
                    agentRespondedSilence=false;
                    agentRespondedEmotion=false;
                }
            }
        }
    }

    public void updateUI()
    {
        silenceDurationInput.text = silenceDuration+"";
        talkingDurationInput.text =  talkingDuration+"";
        totalDurationInput.text = Mathf.RoundToInt(totalDuration).ToString()+"s";

        firstStageInput.text = firstStage+"";
        agentTalkingInput.text = agentTalking+"";
        userPersuadedInput.text = userPersuaded+"";
        userTalkedInput.text = userTalked+"";
        userRespondedEmotionInput.text = userRespondedEmotion+"";
        finishedInput.text = finished+"";
        agentRespondedEmotionInput.text = agentRespondedEmotion+"";
        agentRespondedSilenceInput.text = agentRespondedSilence+"";
        therapistOverrideInput.text = WO.therapistOverride+"";
        sessionLimitInput.text = sessionLimit+"";
    }

    private void OnDestroy()
    {
        recognizer.Dispose();
        synthesizer.Dispose();
    }

    // when window is not active, the microphone stops recording, so we need to restart it when is active/focused again
    void OnApplicationFocus(bool hasFocus)
    {
        microphoneDevice = Microphone.devices[0];
        microphoneInput = Microphone.Start(microphoneDevice, true, 1, 44100);
    }

    public int ExtractNumberFromString(string input)
    {
        Regex regex = new Regex(@"\d+"); // Matches one or more digits
        Match match = regex.Match(input);

        if (match.Success)
        {
            return Int32.Parse(match.Value);
        }
        else
        {
            throw new ArgumentException("No number found in the input string:_"+ input);
        }
    }

    // Therapist can click the buttons in the screen to add or decrease time to the session
    public void addTimetoSession(float timeExtra)
    {
        if(sessionLimit>0)
            sessionLimit+=timeExtra;
    }
}
