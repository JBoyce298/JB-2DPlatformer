using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttontracker : MonoBehaviour
{
    public GameObject pause;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause.activeSelf)
            {
                pause.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pause.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
