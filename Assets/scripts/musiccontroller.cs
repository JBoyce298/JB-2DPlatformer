using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musiccontroller : MonoBehaviour
{
    public GameObject musicOn;
    public GameObject musicOff;

    AudioSource music;

    bool state = true;

    string sceneName = "";
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!sceneName.Equals(SceneManager.GetActiveScene().name))
        {
            music = GameObject.Find("Main Camera").GetComponent<AudioSource>();
            sceneName = SceneManager.GetActiveScene().name;
        }

        if (state)
        {
            music.enabled = true;
        }
        else
        {
            music.enabled = false;
        }
    }

    public void toggleMusic()
    {
        if(state)
        {
            musicOn.SetActive(false);
            musicOff.SetActive(true);
            state = false;
        }
        else
        {
            musicOn.SetActive(true);
            musicOff.SetActive(false);
            state = true;
        }
    }
}
