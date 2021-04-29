using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    int count;
    public Text testText;
    public int maxHealth = 5;
    int monst;
    string temppath;
    string temptime;
    string tempdata;
    bool starttimer = false;
    float delaytimer = 0;

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
        //getMaxHealth();
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
        count = int.Parse(data.Substring(1, data.Length - 2));
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

        timer.text = "00:00:00";
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

    private void ShowBestTime(string data)
    {
        Text timer = GameObject.Find("EventSystem").GetComponent<timertracker>().best;
        string cut = data.Substring(1, data.Length - 2);
        string[] info = cut.Split(',');
        timer.text = info[0];
    }

    private void playerUpdateDeath(string data)
    {
        PostJSON(temppath, null, gameObject.name, "OnRequestSuccess", "OnRequestFailed");
        
        string cut = data.Substring(1, data.Length - 2);
        string[] info = cut.Split(',');
        if (cut == "" || data == "" || cut == null || data == null)
        {
            string[] i = { "99:99:99", "0", "0", "0" };
            info = i;
        }


        string timer = info[0];
        int deaths = int.Parse(info[1]);
        int enemy = int.Parse(info[2]);
        int plays = int.Parse(info[3]);

        deaths++;
        enemy += monst;
        monst = 0;
        plays++;

        string input = timer + "," + deaths + "," + enemy + "," + plays;

        PostJSON(temppath, input, gameObject.name, "OnRequestSuccess", "OnRequestFailed");
        temppath = null;
    }

    private void playerUpdateNothing(string data)
    {
        PostJSON(temppath, null, gameObject.name, "OnRequestSuccess", "OnRequestFailed");

        string cut = data.Substring(1, data.Length - 2);
        string[] info = cut.Split(',');
        if (cut == "" || data == "" || cut == null || data == null)
        {
            string[] i = { "99:99:99", "0", "0", "0" };
            info = i;
        }


        string timer = info[0];
        int deaths = int.Parse(info[1]);
        int enemy = int.Parse(info[2]);
        int plays = int.Parse(info[3]);

        string input = timer + "," + deaths + "," + enemy + "," + plays;

        PostJSON(temppath, input, gameObject.name, "OnRequestSuccess", "OnRequestFailed");
        temppath = null;
    }

    private void playerUpdateWin(string data)
    {
        PostJSON(temppath, null, gameObject.name, "OnRequestSuccess", "OnRequestFailed");

        string cut = data.Substring(1, data.Length - 2);
        string[] info = cut.Split(',');
        if (cut == "" || data == "" || cut == null || data == null)
        {
            string[] i = { temptime, "0", "0", "0" };
            info = i;
        }

        string timer = info[0];
        int deaths = int.Parse(info[1]);
        int enemy = int.Parse(info[2]);
        int plays = int.Parse(info[3]);

        enemy += monst;
        monst = 0;
        plays++;

        string[] timercut = timer.Split(':');
        string[] newtimercut = temptime.Split(':');
        float count = 0;
        float newcount = 0;

        count += int.Parse(timercut[2]) + (int.Parse(timercut[1]) * 100) + (int.Parse(timercut[2]) * 100 * 60);
        newcount += int.Parse(newtimercut[2]) + (int.Parse(newtimercut[1]) * 100) + (int.Parse(newtimercut[2]) * 100 * 60);

        if (newcount > count)
        {
            timer = temptime;
        }

        string input = timer + "," + deaths + "," + enemy + "," + plays;

        PostJSON(temppath, input, gameObject.name, "OnRequestSuccess", "OnRequestFailed");
        temppath = null;
        temptime = null;
    }

    private void Health(string data)
    {
        string path = transform.GetComponent<loginhandler>().getUserPass() + "/health";
        PostJSON(path, null, gameObject.name, "OnRequestSuccess", "OnRequestFailed");
        if (data == null || data == "")
        {
            PostJSON(path, "" + maxHealth, gameObject.name, "OnRequestSuccess", "OnRequestFailed");
            temppath = path;
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
        GetJSON(path, gameObject.name, "LevelStats", "OnRequestFailed");
    }

    public void increaseCount()
    {
        GetJSON("count", gameObject.name, "SetCount", "OnRequestFailed");
    }


    public void getMaxHealth()
    {
        string path = transform.GetComponent<loginhandler>().getUserPass() + "/health";
        GetJSON(path, gameObject.name, "Health", "OnRequestFailed");
    }

    public void reportDeathFromPlayer(string levelname, int monsters)
    {
        monst = monsters;
        string path = transform.GetComponent<loginhandler>().getUserPass() + "/" + levelname;
        temppath = path;
        GetJSON(temppath, gameObject.name, "playerUpdateDeath", "OnRequestFailed");
    }

    public void reportWinFromPlayer(string levelname, int monsters, string time)
    {
        monst = monsters;
        temptime = time;
        string path = transform.GetComponent<loginhandler>().getUserPass() + "/" + levelname;
        temppath = path;
        GetJSON(temppath, gameObject.name, "playerUpdateWin", "OnRequestFailed");
        
    }

    public void reportNothingFromPlayer(string levelname)
    {
        string path = transform.GetComponent<loginhandler>().getUserPass() + "/" + levelname;
        temppath = path;
        GetJSON(temppath, gameObject.name, "playerUpdateNothing", "OnRequestFailed");
    }

    public void bestTime(string level)
    {
        string path = transform.GetComponent<loginhandler>().getUserPass() + "/" + level;
        temppath = path;
        GetJSON(temppath, gameObject.name, "ShowBestTime", "OnRequestFailed");
    }
}
