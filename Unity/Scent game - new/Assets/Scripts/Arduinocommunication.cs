//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Net.Sockets;
//using System.IO.Ports;
//using System.Text;

//public class Arduinocommunication : MonoBehaviour
//{
//    private TcpClient client;
//    private NetworkStream stream;

//    private string arduinoIPAddress = "172.20.10.2"; // Replace with your Arduino's IP address
//    private int arduinoPort = 80; // Replace with the port number you are using
//    private bool connected = false;

//    private void Start()
//    {
//        client = new TcpClient(arduinoIPAddress, arduinoPort);
//        stream = client.GetStream();
//        connected = true;

//    }

//    void Update()
//    {
//        if (connected)
//        {
//            // Check for incoming data from the Arduino
//            if (stream.DataAvailable)
//            {
//                byte[] data = new byte[1024];
//                int bytesRead = stream.Read(data, 0, data.Length);
//                string message = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);
//                Debug.Log("Received message from Arduino: " + message);
//            }
//        }
//    }

//    public void SendMessageToArduino(string message)
//    {
//        if (connected)
//        {

//            byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
//            Debug.Log(message);
//            stream.Write(data, 0, data.Length);
//        }
//    }

//    private void OnApplicationQuit()
//    {
//        SendMessageToArduino("x"); // send message 'x' to the Arduino before the game stops running
//        stream.Close(); // close the stream
//        client.Close(); // close the client connection
//    }
//}


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

    private string arduinoIPAddress = "172.20.10.2"; // Replace with your Arduino's IP address
    private int arduinoPort = 80; // Replace with the port number you are using
    private bool connected = false;
    //private bool binary = false;
    //public int atomNo;

    public List<string> binaryCodes = new List<string>(); // List to store the binary codes

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
            // Check for incoming data from the Arduino
            if (stream.DataAvailable)
            {
                byte[] data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string message = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);
                Debug.Log("Received message from Arduino: " + message);
                if (message == "0010" || message == "0001") // check if message is a binary code
                {
                    binaryCodes.Add(message);


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
        SendMessageToArduino("x"); // send message 'x' to the Arduino before the game stops running
        stream.Close(); // close the stream
        client.Close(); // close the client connection
    }
}
