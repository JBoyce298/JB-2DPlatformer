using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
