using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class BluetoothLEController : MonoBehaviour
{
    // The name of the Bluetooth device we want to connect to
    public string deviceName = "MyBluetoothDevice";

    // Start is called before the first frame update
    void Start()
    {
        // Create an instance of the BluetoothService class
        BluetoothService.CreateBluetoothObject();

        var devices = BluetoothService.ScanForDevices();
        Debug.Log($"Found {devices.Count} nearby devices:");
        foreach (var device in devices)
        {
            Debug.Log(device);
        }

        // Attempt to connect to the device with the given name
        bool isConnected = BluetoothService.StartBluetoothConnection(deviceName);

        // If the connection was successful, print "Connected"
        if (isConnected)
        {
            Debug.Log("Connected");
        }
    }

    // Called when the script is being destroyed
    void OnDestroy()
    {
        // Stop the Bluetooth connection when the script is destroyed
        BluetoothService.StopBluetoothConnection();
    }
}
