using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    int count;
    public Text testText;
    int maxHealth;

    [DllImport("__Internal")]
    public static extern void GetJSON(string path, string objectName, string callback, string fallback);

    [DllImport("__Internal")]
    public static extern void PushJSON(string path, string value, string objectName, string callback, string fallback);

    [DllImport("__Internal")]
    public static extern void PostJSON(string path, string value, string objectName, string callback, string fallback);

    [DllImport("__Internal")]
    public static extern void UpdateJSON(string path, string value, string objectName, string callback, string fallback);

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        //GetJSON("example", gameObject.name, "OnRequestSuccess", "OnRequestFailed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRequestSuccess(string data)
    {
        testText.color = Color.green;
        testText.text = data;
        
        //count++;
        //testText.text = count + "";
    }

    private void OnRequestFailed(string error)
    {
        testText.color = Color.red;
        testText.text = error;
    }

    private void SetCount(string data)
    {
        count = int.Parse(data.Substring(1, 1));
        count++;
        PostJSON("count", count + "", gameObject.name, "OnRequestSuccess", "OnRequestFailed");
    }

    private void LevelStats(string data)
    {
        testText.text = data;
        testText.color = Color.red;
        string cut = data.Substring(1, data.Length - 2);
        string[] info = cut.Split(',');

        Text timer = GameObject.Find("Timer").GetComponent<Text>();
        Text deaths = GameObject.Find("DeathCount").GetComponent<Text>();
        Text enemy = GameObject.Find("KillCount").GetComponent<Text>();
        Text plays = GameObject.Find("PlayCount").GetComponent<Text>();

        timer.text = "00:00.00";
        deaths.text = "0";
        enemy.text = "0";
        plays.text = "0";

        if (int.Parse(info[3]) > 0)
        {
            timer.text = info[0];
            deaths.text = info[1];
            enemy.text = info[2];
            plays.text = info[3];
        }
    }

    private void Health(string data)
    {
        if(data == null)
        {
            maxHealth = 05;

            string path = transform.GetComponent<loginhandler>().getUserPass() + "/health";
            PushJSON(path, "05", gameObject.name, "OnRequestSuccess", "OnRequestFailed");
        }
        else
        {
            maxHealth = int.Parse(data.Substring(1, 2));
        }
    }

    public void log(string path)
    {
        PushJSON(path, null, gameObject.name, "OnRequestSuccess", "OnRequestFailed");
    }

    public void levelData(string path, string data)
    {
        PushJSON(path, data, gameObject.name, "OnRequestSuccess", "OnRequestFailed");
    }

    public void level(string path)
    {
        //testText.text = path;
        //testText.color = Color.red;
        GetJSON(path, gameObject.name, "LevelStats", "OnRequestFailed");
    }

    public void increaseCount()
    {
        GetJSON("count", gameObject.name, "SetCount", "OnRequestFailed");
    }


    public int getMaxHealth()
    {
        string path = transform.GetComponent<loginhandler>().getUserPass() + "/health";
        GetJSON(path, gameObject.name, "Health", "OnRequestFailed");
        return maxHealth;
    }
}
