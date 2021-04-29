using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("FirebaseHandler");
        g.GetComponent<test>().increaseCount();
        SceneManager.LoadScene("Login");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
