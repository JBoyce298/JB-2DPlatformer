using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject levels;
    public GameObject stats;
    public InputField user;
    public InputField pass;
    public GameObject handler;

    string tempuser;
    string temppass;

    void Start()
    {
        handler = GameObject.Find("FirebaseHandler");     
    }

    public void login()
    {
        tempuser = user.text;
        temppass = pass.text;

        string path = tempuser + temppass;

        handler.GetComponent<test>().log(path);
        handler.GetComponent<loginhandler>().Login(tempuser, temppass);

        SceneManager.LoadScene("Main");
    }

    public void levelSelect()
    {
        levels.SetActive(true);       
        menu.SetActive(false);
        stats.SetActive(false);
    }

    public void backToMenu()
    {
        menu.SetActive(true);
        levels.SetActive(false);
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("Main");
    }

    public void resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void quit()
    {
        //implement quit later
        //EditorApplication.isPlaying = false;
        //tempuser = null;
        //temppass = null;
        //handler.GetComponent<loginhandler>().Logout();
        SceneManager.LoadScene("Login");
        
    }

    public void level1()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1;
    }

    public void level2()
    {
        SceneManager.LoadScene("Level 2");
        Time.timeScale = 1;
    }

    public void level3()
    {
        SceneManager.LoadScene("Level 3");
        Time.timeScale = 1;
    }

    public void level4()
    {
        SceneManager.LoadScene("Level 4");
        Time.timeScale = 1;
    }

    public void level5()
    {
        SceneManager.LoadScene("Level 5");
        Time.timeScale = 1;
    }

    public void level6()
    {
        SceneManager.LoadScene("Level 6");
        Time.timeScale = 1;
    }

    public void level7()
    {
        SceneManager.LoadScene("Level 7");
        Time.timeScale = 1;
    }

    public void level8()
    {
        SceneManager.LoadScene("Level 8");
        Time.timeScale = 1;
    }

    public void level9()
    {
        SceneManager.LoadScene("Level 9");
        Time.timeScale = 1;
    }

    public void level10()
    {
        SceneManager.LoadScene("Level 10");
        Time.timeScale = 1;
    }

    public void levelStats(int level)
    {
        stats.SetActive(true);
        stats.GetComponent<stats>().load(level);
        levels.SetActive(false);
    }
}
