using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;
using Newtonsoft.Json;

[System.Serializable]
public class Message
{
    public string role;
    public string content;
}


public class OpenAIChat : MonoBehaviour
{
    [SerializeField] public List<Message> messages;
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
        messages = new List<Message>();
        messages.Add(new Message
        {
            role = "system",
            content = " ALWAYS RESPOND IN ONLY ONE FULL SENTENCE and ALWAYS RESPOND ACCORDING TO YOUR PERSONALITY \\n YOUR Knowledge: " + agentBehaviour.knowledge +" \\n YOUR PERSONALITY: " + agentBehaviour.personality + "\\n REMEMBER TO RESPOND ACCORDING TO YOUR PERSONALITY"
        });
    }


    // The main function
    public void RunConversation(string localPrompt, bool actionsOnly = false)
    {
        // Add the user's prompt to the messages list
        messages.Add(new Message
        {
            role = "user",
            content = localPrompt
        });

        // Remove the oldest message if the number of stored messages exceed the limit.
        while (messages.Count > agentBehaviour.memory) // We add 3 to keep the first and last two elements
        {
            messages.RemoveAt(1); // Remove the second element, as the first one is the system message
        }

        prompt = localPrompt;
        message = "";
        actions.Clear();
        string jsonRequest = CreateJsonRequest();
 
            StartCoroutine(SendWebRequest(jsonRequest, actionsOnly));

    }

    // Creates the json request
    string CreateJsonRequest()
    {
        // Convert the list of available actions to a JSON array.
        var actionsJsonArray = "[";
        foreach (var action in agentBehaviour.availableActions)
        {
            actionsJsonArray += $"\"{action}\",";
        }
        actionsJsonArray = actionsJsonArray.TrimEnd(',') + "]";
        string[] styleEnumArray = Enum.GetNames(typeof(TTSService.StyleEnum));

        // Convert string array to JSON array
        string styleEnumJsonArray = JsonConvert.SerializeObject(styleEnumArray);
        print(styleEnumJsonArray);
        // Hardcoded function template. The action enum list is now replaced with the JSON array.
       var functions = @"
        [
            {
                ""name"": ""generate_npc_response"",
                ""description"": ""Generates a response of a full message and an array of possible actions for a NPC"",
                ""parameters"": 
                {
                    ""type"": ""object"",
                    ""properties"": 
                    {
                        ""actions"": 
                        {
                            ""type"": ""array"",
                            ""items"": 
                            {
                                ""type"": ""string"",
                                ""enum"": " + actionsJsonArray + @",
                                ""description"": ""The action that the NPC will perform""
                            },
                            ""description"": ""List of possible actions for the NPC, should be used one of each category""
                        },
                        ""message"": 
                        {
                            ""type"": ""string"",
                            ""description"": ""The response of the full message from the NPC, it should be the full message including whats after the : ""
                        },
                        ""style"": 
                        {
                            ""type"": ""array"",
                            ""items"": 
                            {
                                ""type"": ""string"",
                                ""enum"": " + styleEnumJsonArray + @",
                                ""description"": ""style of the message""
                            },
                            ""description"": ""List of possible styles for the message of the NPC, choose only one""
                        }
                    },
                    ""required"": [""message"", ""actions"", ""style""]
                }
            }
        ]";

        string jsonRequest =
            "{" +
            "\"model\": \"gpt-3.5-turbo-0613\", " +
            "\"temperature\": 1.0, " +
            "\"max_tokens\": 2048, " +
            "\"messages\": [";

        // Add each message in the list to the request
        foreach (var message in messages)
        {
            jsonRequest +=
                "{" +
                "\"role\": \"" + message.role + "\", " +
                "\"content\": \"" + message.content + "\"" +
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
        print("jsonrequest" + jsonRequest + "\n");
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
            messages.RemoveAt(messages.Count - 1);
            messages.RemoveAt(messages.Count - 1);
            ttsService.ConvertToSpeech("there was a problem with the connection, try again");
        }
        else
        {
            print("json response1" + request + "\n");
            print("json response2" + request.downloadHandler.text + "\n");
            // Here you can process the response
            var jsonResponse = JSON.Parse(request.downloadHandler.text);
            
            // Extract the assistant message and actions
            // Extract the assistant message and add it to the messages list
            string functionCallResponseStr = jsonResponse["choices"][0]["message"]["function_call"]["arguments"].Value;
            var functionCallResponse = JSON.Parse(functionCallResponseStr);

            if (!actionsOnly)
            {
                message = functionCallResponse["message"].Value.Replace("\"", " ");
                // in message, replace " with \"
                message = message.Replace("\"", "\\\"");
                message = message.Replace("\n", "     ");
                print("message" + message + "\n");
                print("messageS" + functionCallResponse["style"].Value + "\n");
                ttsService.styleEnumed = (TTSService.StyleEnum)Enum.Parse(typeof(TTSService.StyleEnum), functionCallResponse["style"].Value);
                messages.Add(new Message
                {
                    role = "assistant",
                    //replace the \n with a space
                    content = message

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
