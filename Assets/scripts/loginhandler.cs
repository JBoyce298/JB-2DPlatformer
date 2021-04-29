using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loginhandler : MonoBehaviour
{
    public string saveduser;
    public string savedpass;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login(string user, string pass)
    {
        saveduser = user;
        savedpass = pass;
    }

    public void Logout()
    {
        saveduser = null;
        savedpass = null;
    }

    public string getUserPass()
    {
        return saveduser + savedpass;
    }
}
