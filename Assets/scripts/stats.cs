using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stats : MonoBehaviour
{
    public Text levelNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void load(int level)
    {
        levelNum.text = level + "";
        string path1 = GameObject.Find("FirebaseHandler").GetComponent<loginhandler>().getUserPass();
        string path = path1 + "/level" + level;
        GameObject.Find("FirebaseHandler").GetComponent<test>().level(path);
    }
}
