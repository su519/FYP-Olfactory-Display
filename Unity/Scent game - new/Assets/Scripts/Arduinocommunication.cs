using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO.Ports;
using System.Text;

public class Arduinocommunication : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    private string arduinoIPAddress = "172.20.10.2";
    private int arduinoPort = 80;
    private bool connected = false;

    public static List<string> binaryCodes = new List<string>();

    private void Start()
    {
        client = new TcpClient(arduinoIPAddress, arduinoPort);
        stream = client.GetStream();
        connected = true;
    }

    void Update()
    {
        if (connected)
        {
            if (stream.DataAvailable)
            {
                byte[] data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string message = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);
                Debug.Log("Received message from Arduino: " + message);
                Debug.Log(message.Length);
                for(int i = 0; i < 18 ; i = i + 3) {
                    string scent = message.Substring(i, 3); ;
                    binaryCodes.Add(scent);
                }
                
            }
        }
    }


    public void SendMessageToArduino(string message)
    {
        if (connected)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            Debug.Log(message);
            stream.Write(data, 0, data.Length);
        }
    }


    private void OnApplicationQuit()
    {
        SendMessageToArduino("x");
        stream.Close();
        client.Close(); 
    }
}
