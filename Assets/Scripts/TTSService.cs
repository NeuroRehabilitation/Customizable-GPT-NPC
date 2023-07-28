using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TTSService : MonoBehaviour
{
    private const string ApiEndpoint = "https://westeurope.tts.speech.microsoft.com/cognitiveservices/v1";
    private const string VoicesEndpoint = "https://westeurope.tts.speech.microsoft.com/cognitiveservices/voices/list";

    // Subscription key for accessing the API
    public string SubscriptionKey = "ea7dd199531644d0814e9c666eca524a";

    // The audio format (e.g., audio-24khz-96kbitrate-mono-mp3)
    private const string AudioFormat = "audio-16khz-32kbitrate-mono-mp3";

    // The language and voice name (e.g., en-US, en-US-AriaNeural)
    public enum LanguageEnum
    {
        en_US,
        en_GB,
        es_ES,
        de_DE,
    }

    [SerializeField] private LanguageEnum languageEnumed;

    public string Language
    {
        get { return languageEnumed.ToString(); }
    }


    public enum VoiceNameEnum
    {
        en_US_AriaNeural,
        en_US_DavisNeural,
        en_US_AshleyNeural,
        en_US_BrandonNeura,
        // Add more as needed
    }

    [SerializeField] private VoiceNameEnum voiceNameEnumed;

    public string VoiceName
    {
        get { return voiceNameEnumed.ToString().Replace('_', '-'); }
    }


    public enum GenderEnum
    {
        Male,
        Female,
    }

    [SerializeField] private GenderEnum genderEnumed;

    public string Gender
    {
        get { return genderEnumed.ToString(); }
    }


    public enum StyleEnum
    {
        None,
        Angry,
        Chat,
        Cheerful,
        CustomerService,
        Empathetic,
        Excited,
        Friendly,
        Hopeful,
        NarrationProfessional,
        NewscastCasual,
        NewscastFormal,
        Sad,
        Shouting,
        Terrified,
        Unfriendly,
        Whispering
        // add more as needed
    }

    [SerializeField] public StyleEnum styleEnumed;

    public string Style
    {
        get { return styleEnumed.ToString().Replace('_', '-').ToLower(); } // This converts 'CustomerService' to 'customerservice' format
    }

    [Range(0.1f, 2f)]
    public float StyleStrength;

    [HideInInspector]
    public string accessToken;

    public void ConvertToSpeech(string text)
    {
        StartCoroutine(PostTextToSpeechRequest(text));
    }

    private IEnumerator PostTextToSpeechRequest(string text)
    {
        string body = "";
        if (Style.ToLower() == "none")
        {
            body = $"<speak version='1.0' xml:lang='{Language}'><voice xml:lang='{Language}' xml:gender='{Gender}' name='{VoiceName}'>{text}</voice></speak>";
        }
        else
        {
            body = $"<speak version='1.0' xmlns:mstts='http://www.w3.org/2001/mstts' xml:lang='{Language}'><voice xml:lang='{Language}' xml:gender='{Gender}' name='{VoiceName}'> <mstts:express-as style='{Style}' styledegree='{StyleStrength}'>{text}</mstts:express-as></voice></speak>";
        }
        print(body);
        // Create the HTTP request
        UnityWebRequest request = UnityWebRequest.Post(ApiEndpoint, "");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));
        request.downloadHandler = new DownloadHandlerAudioClip("", AudioType.MPEG);

        request.SetRequestHeader("Content-Type", "application/ssml+xml");
        request.SetRequestHeader("Authorization", $"Bearer {accessToken}");
        request.SetRequestHeader("X-Microsoft-OutputFormat", AudioFormat);

        // Send the request
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Text-to-Speech request failed: " + request.error);
            StartCoroutine(GetAccessToken(text));
            yield break;
        }
        print("TTS response sent to AgentBehaviour");
        GetComponent<AgentBehaviour>().Act(DownloadHandlerAudioClip.GetContent(request));
    }

    public IEnumerator GetAccessToken(string message)
    {
        UnityWebRequest tokenRequest = UnityWebRequest.Post("https://westeurope.api.cognitive.microsoft.com/sts/v1.0/issueToken", "");
        tokenRequest.SetRequestHeader("Ocp-Apim-Subscription-Key", SubscriptionKey);

        yield return tokenRequest.SendWebRequest();

        if (tokenRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Access token request failed: " + tokenRequest.error);
            yield break;
        }

        accessToken = tokenRequest.downloadHandler.text.Trim();
        ConvertToSpeech(message);
        yield break;
    }
}
