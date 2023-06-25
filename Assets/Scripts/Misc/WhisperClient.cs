using System.IO;
using System.Net.Sockets;
using System.Text;
using System;
using UnityEngine;

public class WhisperClient : MonoBehaviour
{
    private void Start()
    {
        SendAudioToWhisperServer("C:/Users/netco/Documents/Sound Recordings/Recording (9).mp3");
    }

    private void SendAudioToWhisperServer(string audioFilePath)
{
    TcpClient client = new TcpClient("127.0.0.1", 10000);
    NetworkStream stream = client.GetStream();

    byte[] audioData = File.ReadAllBytes(audioFilePath);
    byte[] fileSizeBytes = BitConverter.GetBytes(audioData.Length);

    // Send the file size
    stream.Write(fileSizeBytes, 0, fileSizeBytes.Length);

    // Send the audio data
    stream.Write(audioData, 0, audioData.Length);

    // Receive the transcribed text
    byte[] buffer = new byte[4096];
    int bytesRead = stream.Read(buffer, 0, buffer.Length);
    string text = Encoding.UTF8.GetString(buffer, 0, bytesRead);

    Debug.Log("Transcribed text: " + text);
}

}
