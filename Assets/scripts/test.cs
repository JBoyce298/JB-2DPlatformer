using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Text testText;

    [DllImport("__Internal")]
    public static extern void GetJSON(string path, string objectName, string callback, string fallback);
    // Start is called before the first frame update
    void Start()
    {
        GetJSON("example", gameObject.name, "OnRequestSuccess", "OnRequestFailed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRequestSuccess(string data)
    {
        testText.color = Color.green;
        testText.text = data;
    }

    private void OnRequestFailed(string error)
    {
        testText.color = Color.red;
        testText.text = error;
    }
}
