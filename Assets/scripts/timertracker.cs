using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timertracker : MonoBehaviour
{
    public Text timer;
    public Text best;
    float gameTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //implement scores storing system with database
        //best = ;
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime * 100;

        int centiseconds = (int)(gameTimer % 100);
        int seconds = (int)(gameTimer/ 100) % 60;
        int minutes = (int)((gameTimer / 100) / 60) % 60;

        //var span = TimeSpan.FromSeconds(gameTimer);

        string newtime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
        timer.text = newtime;
    }
}
