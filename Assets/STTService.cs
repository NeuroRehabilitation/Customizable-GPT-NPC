using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
public class STTService : MonoBehaviour
{

    [SerializeField] private string subscriptionKey = "ea7dd199531644d0814e9c666eca524a";
    [SerializeField] private string region = "westeurope";
    [SerializeField] private string languageCode = "en-US";
    public AgentBehaviour agentBehaviour;

    [SerializeField] private OpenAIChat ChatGPTAPI;

    public List<string> prompt;

    void Start()
    {
        agentBehaviour = GetComponent<AgentBehaviour>();
    }

    public IEnumerator Transcribe(AudioClip clipRecorded, bool isSilenceDetected)
    {
        byte[] wavData = AudioClipToWav(clipRecorded);

        using (UnityWebRequest www = new UnityWebRequest())
        {
            int currIndex= prompt.Count;
            prompt.Add("");
            www.url = $"https://{region}.stt.speech.microsoft.com/speech/recognition/conversation/cognitiveservices/v1?language={languageCode}" + "&format=detailed";
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = new UploadHandlerRaw(wavData);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "audio/wav");
            www.SetRequestHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
            www.SetRequestHeader("X-Region", region);
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                string result = www.downloadHandler.text;

                // Parse JSON response to get transcribed text.
                string displayText = ParseDisplayText(result);
                prompt[currIndex] = displayText;
                if (isSilenceDetected)
                {
                    string promptSend = "";
                    foreach (string s in prompt)
                    {
                        promptSend += s + " ";
                    }
                    print("Prompt List Sent to ChatGPtAPI");
                    ChatGPTAPI.RunConversation(promptSend);
                    
                    prompt.Clear();
                }
                else
                {
                    ChatGPTAPI.RunConversation(prompt[currIndex],true);
                }
            }
            else
            {
                Debug.Log(www.error);
                string result = www.downloadHandler.text;
                Debug.Log(result);
            }
        }
    }

    private string ParseDisplayText(string json)
    {
        try
        {
            var jsonNode = JSON.Parse(json);
            if (jsonNode["DisplayText"] != null)
            {
                return jsonNode["DisplayText"].Value;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error parsing JSON: " + e.Message);
        }

        return string.Empty;
    }

    public byte[] AudioClipToWav(AudioClip clipRecorded)
    {
        int sampleRate = 16000;
        int channelCount = clipRecorded.channels;
        int sampleCount = clipRecorded.samples;
        int frequency = clipRecorded.frequency;

        float[] audioData = new float[sampleCount];
        clipRecorded.GetData(audioData, 0);

        short[] audioSamples = new short[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            audioSamples[i] = (short)(audioData[i] * short.MaxValue);
        }

        byte[] wavData = new byte[sampleCount * 2]; // 2 bytes per sample for 16-bit audio

        int dataIndex = 0;
        for (int i = 0; i < sampleCount; i++)
        {
            short sample = audioSamples[i];
            wavData[dataIndex++] = (byte)(sample & 0xff);
            wavData[dataIndex++] = (byte)((sample >> 8) & 0xff);
        }
        return wavData;
    }
}
