using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletoncontroller : MonoBehaviour
{
    Animator myAnim;
    SpriteRenderer sr;
    Rigidbody2D myBod;
    BoxCollider2D myBox;

    public bool hurt;
    public int health;
    public float speed;
    public float distance;

    private float walkDelay = 0;
    private bool isRight = true;
    private bool delay = false;
    private float stopCheck;
    private float stopTimer = 0;
    private Vector2 tether;

    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        myBod = GetComponent<Rigidbody2D>();
        myBox = GetComponent<BoxCollider2D>();

        tether = transform.position;
        stopCheck = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (!hurt)
            {
                //movement
                if (!delay)
                {
                    if (isRight)//walking right
                    {
                        sr.flipX = false;
                        myAnim.SetBool("WALK", true);
                        if (transform.position.x - tether.x > distance)
                        {
                            delay = true;
                            myBod.velocity = new Vector2(0, myBod.velocity.y);
                        }
                        else
                        {
                            myBod.velocity = new Vector2(speed, myBod.velocity.y);
                        }
                    }
                    else//walking left
                    {
                        sr.flipX = true;
                        myAnim.SetBool("WALK", true);

                        if (transform.position.x - tether.x < (distance * -1))
                        {
                            delay = true;
                            myBod.velocity = new Vector2(0, myBod.velocity.y);
                        }
                        else
                        {
                            myBod.velocity = new Vector2((speed * -1), myBod.velocity.y);
                        }
                    }

                    stopTimer += Time.deltaTime;
                    if (stopTimer > 0.2)
                    {
                        stopTimer = 0;
                        if (stopCheck == transform.position.x)
                        {
                            delay = true;
                        }
                        stopCheck = transform.position.x;
                    }
                }
                else
                {
                    myAnim.SetBool("WALK", false);
                    walkDelay += Time.deltaTime;

                    if(walkDelay >= 2.5)
                    {
                        isRight = !isRight;
                        delay = false;
                        walkDelay = 0;
                    }
                }

                //dying
                if (health <= 0)
                {
                    myAnim.SetBool("DIE", true);
                    dead = true;
                }
            }

            //getting hit
            if (hurt)
            {
                if(myAnim.GetBool("HURT") == false)
                {
                    health--;
                }

                myAnim.SetBool("HURT", true);
            }
            else
            {
                myAnim.SetBool("HURT", false);
            }
        }
        else
        {
            myBox.size = new Vector2(0.721f,0.310f);
            myBox.offset = new Vector2(0.161f,-0.7f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if(g.tag == "Player")
        {
            if (!dead)
            {
                g.GetComponent<playercontroller>().isHurt = true;
            }  
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (g.tag == "Player")
        {
            if (!dead)
            {
                g.GetComponent<playercontroller>().isHurt = false;
            }
        }
    }
}
