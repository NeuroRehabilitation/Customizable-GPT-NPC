using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class OpenAIChat : MonoBehaviour
{
    [SerializeField] public List<Dictionary<string, string>> messages;
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
        messages = new List<Dictionary<string, string>>();
        messages.Add(new Dictionary<string, string>
        {
            { "role", "system" },
            { "content", agentBehaviour.personality + " keep your responses short"}
        });
    }

    // The main function
    public void RunConversation(string localPrompt, bool actionsOnly = false)
    {
        // Add the user's prompt to the messages list
        messages.Add(new Dictionary<string, string>
        {
            { "role", "user" },
            { "content", localPrompt }
        });

        prompt = localPrompt;
        message = "";
        actions.Clear();
        string jsonRequest = CreateJsonRequest();
        if (localPrompt.Length > 2)
            StartCoroutine(SendWebRequest(jsonRequest, actionsOnly));
        else
            agentBehaviour.agentTalking = false;
    }

    // Creates the json request
    string CreateJsonRequest()
    {
        var functions = "[{\"name\": \"generate_npc_response\", \"description\": \"Generates a response message and an array of possible actions for a NPC\", \"parameters\": {\"type\": \"object\", \"properties\": {\"message\": {\"type\": \"string\", \"description\": \"The response message from the NPC\"}, \"actions\": {\"type\": \"array\", \"items\": {\"type\": \"string\", \"enum\": [\"HeadIdle\", \"HeadTilt\", \"HeadNod\", \"TorsoStraight\", \"TorsoLeaning\", \"LegsSitted\", \"MouthIdle\", \"MouthSmile\", \"MouthSoftSmile\", \"EyebrowsIdle\", \"EyebrowsSoftSmile\", \"EyebrowsSmile\"], \"description\": \"The action that the NPC will perform\"}, \"description\": \"List of possible actions for the NPC, should be used one of each category\"}}, \"required\": [\"message\", \"actions\"]}}]";
        string jsonRequest =
            "{" +
            "\"model\": \"gpt-3.5-turbo-0613\", " +
            "\"temperature\": 1.2, " +
            "\"messages\": [";

        // Add each message in the list to the request
        foreach (var message in messages)
        {
            jsonRequest +=
                "{" +
                "\"role\": \"" + message["role"] + "\", " +
                "\"content\": \"" + message["content"] + "\"" +
                "},";
        }

        jsonRequest = jsonRequest.TrimEnd(',') + "]," +
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
            print("json response" + jsonResponse.ToString() + "\n");
            // Extract the assistant message and actions
            // Extract the assistant message and add it to the messages list
            var functionCallResponse = JSON.Parse(jsonResponse["choices"][0]["message"]["function_call"]["arguments"].Value);
            if (!actionsOnly)
            {
                message = functionCallResponse["message"].Value;
                messages.Add(new Dictionary<string, string>
                {
                    { "role", "assistant" },
                    { "content", message }
                });
            }
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
