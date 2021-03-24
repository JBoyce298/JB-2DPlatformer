using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timertracker : MonoBehaviour
{
    public Text currenttimer;
    public Text deathTimer;
    public Text winTimer;
    public Text best;
    float gameTimer = 0f;

    playercontroller player;
    int health;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<playercontroller>();
        
        //implement scores storing system with database
        //best = ;
    }

    // Update is called once per frame
    void Update()
    {
        health = player.health;
        //print(health);
        if (health != 0)
        {
            print(health);
            gameTimer += Time.deltaTime * 100;
        }

        int centiseconds = (int)(gameTimer % 100);
        int seconds = (int)(gameTimer/ 100) % 60;
        int minutes = (int)((gameTimer / 100) / 60) % 60;

        //var span = TimeSpan.FromSeconds(gameTimer);

        string newtime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
        currenttimer.text = newtime;
        winTimer.text = newtime;
        deathTimer.text = newtime;
    }
}
