using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathtracker : MonoBehaviour
{
    playercontroller player;
    int health;

    public GameObject deathscreen;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<playercontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        health = player.health;

        if(health == 0)
        {
            deathscreen.SetActive(true);
        }
    }
}
