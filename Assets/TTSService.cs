using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TTSService : MonoBehaviour
{
    private const string ApiEndpoint = "https://westeurope.tts.speech.microsoft.com/cognitiveservices/v1";
    private const string VoicesEndpoint = "https://westeurope.tts.speech.microsoft.com/cognitiveservices/voices/list";

    // Subscription key for accessing the API
    private const string SubscriptionKey = "ea7dd199531644d0814e9c666eca524a";

    // The audio format (e.g., audio-24khz-96kbitrate-mono-mp3)
    private const string AudioFormat = "audio-16khz-32kbitrate-mono-mp3";

    // The language and voice name (e.g., en-US, en-US-AriaNeural)
    private const string Language = "en-US";
    public string VoiceName = "en-US-AriaNeural";

    public string accessToken;

    public void ConvertToSpeech(string text)
    {
        StartCoroutine(PostTextToSpeechRequest(text));
    }

    private IEnumerator PostTextToSpeechRequest(string text)
    {
        // Create the SSML request body
        string body = $"<speak version='1.0' xml:lang='{Language}'><voice xml:lang='{Language}' xml:gender='Female' name='{VoiceName}'>{text}</voice></speak>";

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
    }
}
