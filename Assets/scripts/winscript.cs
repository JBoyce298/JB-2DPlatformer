using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class winscript : MonoBehaviour
{
    bool win = false;
    public GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(win && Time.timeScale != 0)
        {
            Time.timeScale -= 0.05f;
            if(Time.timeScale < 0.1f)
            {
                Time.timeScale = 0;
            }
        }

        if(Time.timeScale == 0 && win)
        {
            winScreen.SetActive(true);
            win = false;

            string levelname = SceneManager.GetActiveScene().name;
            string level = "";
            if (levelname.Substring(levelname.Length - 1) == "0")
            {
                level = "level10";
            }
            else
            {
                level = "level" + levelname.Substring(levelname.Length - 1);
            }

            int monst = GameObject.Find("Player").GetComponent<playercontroller>().monstersKilled;
            string time = GameObject.Find("WinTimer").GetComponent<Text>().text;
            GameObject.Find("FirebaseHandler").GetComponent<test>().reportWinFromPlayer(level, monst, time);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            win = true;
        }
    }
}
