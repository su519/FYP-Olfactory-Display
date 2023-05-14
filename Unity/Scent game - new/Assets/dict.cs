using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dict : MonoBehaviour
{

    public Dictionary<string, string> ballCodeDict = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        ballCodeDict.Add("Lemon", "0001");
        ballCodeDict.Add("Lavender", "0010");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
