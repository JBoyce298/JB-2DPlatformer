using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletoncontroller : MonoBehaviour
{
    Animator myAnim;
    SpriteRenderer sr;
    Rigidbody2D myBod;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        myBod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (g.tag == "Player")
        {
            Animator myAnim = g.GetComponent<Animator>();
            myAnim.SetBool("HURT", true);
        }
        
    }
}
