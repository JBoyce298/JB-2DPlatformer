using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletoncontroller : MonoBehaviour
{
    Animator myAnim;
    SpriteRenderer sr;
    Rigidbody2D myBod;

    public bool hurt;
    public int health;
    public float speed;
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
        if(!hurt)
        {
           /* float x = speed;
            if(x > 0)
            {
                sr.flipX = false;
                myAnim.SetBool("WALK", true);
            }
            else if (x < 0)
            {
                sr.flipX = true;
                myAnim.SetBool("WALk", true);
            }
            else
            {
                myAnim.SetBool("WALk", false);
            }*/
        }

        if(hurt)
        {
            myAnim.SetBool("HURT", true);
            health--;
        }
        else 
        {
            myAnim.SetBool("HURT", false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if(collision.tag == "Player")
        {
            g.GetComponent<playercontroller>().isHurt = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (collision.tag == "Player")
        {
            g.GetComponent<playercontroller>().isHurt = false;
        }
    }
}
