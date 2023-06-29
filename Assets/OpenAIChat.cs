using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class OpenAIChat : MonoBehaviour
{
    string apiKey = "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";
    string url = "https://api.openai.com/v1/chat/completions";

    // Declare public variables
    public string prompt;
    public string message;
    public List<string> actions;

    
    [HideInInspector]public TTSService ttsService;
    
    [HideInInspector]public AgentBehaviour agentBehaviour;

    public void Start()
    {
        // Get the TTSService component
        ttsService = GetComponent<TTSService>();
        agentBehaviour = GetComponent<AgentBehaviour>();
    }

    // The main function
    public void RunConversation(string localPrompt, bool actionsOnly = false)
    {
        prompt = localPrompt;
        message = "";
        actions.Clear();
        // Run the conversation
        string jsonRequest = CreateJsonRequest();
        if (localPrompt.Length > 2)
            StartCoroutine(SendWebRequest(jsonRequest, actionsOnly));
        else
            agentBehaviour.agentTalking = false;
    }

    // Creates the json request
    string CreateJsonRequest()
    {
        var functions = "[{\"name\": \"generate_npc_response\", \"description\": \"Generates a response message and an array of possible actions for a NPC\", \"parameters\": {\"type\": \"object\", \"properties\": {\"message\": {\"type\": \"string\", \"description\": \"The response message from the NPC\"}, \"actions\": {\"type\": \"array\", \"items\": {\"type\": \"string\", \"enum\": [\"HeadIdle\", \"HeadTilt\", \"HeadNod\", \"TorsoStraight\", \"TorsoLeaning\", \"LegsSitted\", \"LegsCrossed\", \"MouthIdle\", \"MouthSmile\", \"MouthSoftSmile\", \"EyebrowsIdle\", \"EyebrowsSoftSmile\", \"EyebrowsSmile\"], \"description\": \"The action that the NPC will perform\"}, \"description\": \"List of possible actions for the NPC, should be used one of each category\"}}, \"required\": [\"message\", \"actions\"]}}]";
        string jsonRequest =
            "{" +
            "\"model\": \"gpt-3.5-turbo-0613\", " +
            "\"temperature\": 1.2, " +
            "\"messages\": [" +
            "{" +
            "\"role\": \"system\", " +
            "\"content\": \"" + agentBehaviour.personality + ".You will receive data from STT that may be inaccurate and some words missing, do your best.\"" +
            "}," +
            "{" +
            "\"role\": \"user\", " +
            "\"content\": \"" + prompt + "\"" +
            "}" +
            "]," +
            "\"functions\": " + functions + "," +
            "\"function_call\": " + "{\"name\": \"generate_npc_response\"}" +
            "}";

        return jsonRequest;
    }

    

    // Sends the web request to the OpenAI API
    IEnumerator SendWebRequest(string jsonRequest, bool actionsOnly = false)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            
            Debug.Log(request.error);
            
        }
        else
        {
            // Here you can process the response
            var jsonResponse = JSON.Parse(request.downloadHandler.text);
            
            // Extract the assistant message and actions
            var functionCallResponse = JSON.Parse(jsonResponse["choices"][0]["message"]["function_call"]["arguments"].Value);
            if (!actionsOnly)
                message = functionCallResponse["message"].Value;
            actions = new List<string>();
            foreach (JSONNode action in functionCallResponse["actions"].AsArray)
            {
                actions.Add(action.ToString().Replace("\"", ""));
            }
            print("ChatGPT response sent to TTS \n" + functionCallResponse);
            if (!actionsOnly)
                ttsService.ConvertToSpeech(message);//StartCoroutine(ttsService.GetAccessToken(message));
            else
                GetComponent<AgentBehaviour>().Act();
            //prompt = "";
        }
    }
}
