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

    public void levelSelect()
    {
        levels.SetActive(true);
        menu.SetActive(false);
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
    }

    public void quit()
    {
        //implement quit later
        EditorApplication.isPlaying = false;
    }

    public void level1()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1;
    }

    public void level2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void level3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void level4()
    {
        SceneManager.LoadScene("Level 4");
    }

    public void level5()
    {
        SceneManager.LoadScene("Level 5");
    }

    public void level6()
    {
        SceneManager.LoadScene("Level 6");
    }

    public void level7()
    {
        SceneManager.LoadScene("Level 7");
    }

    public void level8()
    {
        SceneManager.LoadScene("Level 8");
    }

    public void level9()
    {
        SceneManager.LoadScene("Level 9");
    }

    public void level10()
    {
        SceneManager.LoadScene("Level 10");
    }

    public void stats(int level)
    {

    }
}
