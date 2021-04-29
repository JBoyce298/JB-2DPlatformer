using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordguycontroller : MonoBehaviour
{
    Animator myAnim;
    SpriteRenderer sr;
    Rigidbody2D myBod;
    BoxCollider2D myBox;
    GameObject playTrack;

    public GameObject atk1;
    public GameObject atk2;
    public GameObject atk1Left;
    public GameObject atk2Left;

    public bool hurt;
    public int health;
    public float speed;
    public float distance;
    private bool isGrounded;
    private bool jumped;

    //private float walkDelay = 0;
    //private bool isRight = true;
    //private bool delay = false;
    private float stopCheck;
    private float stopTimer = 0;
    private float jumpTimer = 0;
    private Vector2 tether;

    private float playDistanceX;
    private float playDistanceY;
    private bool playerTracking = false;
    private float atkTimer = 0;

    private float alph = 1.0f;
    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        myBod = GetComponent<Rigidbody2D>();
        myBox = GetComponent<BoxCollider2D>();

        playTrack = GameObject.Find("Player");

        tether = transform.position;
        stopCheck = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float y = myBod.velocity.y;
        if(y == 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //tracking fall
        if (isGrounded)
        {
            myAnim.SetBool("FALL", false);
        }
        else
        {
            myAnim.SetBool("FALL", true);
        }

        //how long jump for
        if (jumped)
        {
            myAnim.SetBool("JUMP", true);
            jumpTimer += Time.deltaTime;
            if(jumpTimer >= 1)
            {
                jumped = false;
                jumpTimer = 0;
                myAnim.SetBool("JUMP", false);
                myAnim.SetBool("FALL", true);
            }
        }

        if (!dead)
        {
            if (!hurt)
            {
                //movement
                if (!playerTracking)
                {

                }
                else//track player
                {
                    if (playDistanceX < 0)
                    {
                        sr.flipX = true;
                    }
                    else
                    {
                        sr.flipX = false;
                    }
                    switchBox();

                    stopTimer += Time.deltaTime;
                    if (stopTimer > 0.1)
                    {
                        stopTimer = 0;
                        if (stopCheck == transform.position.x && isGrounded && !jumped && Mathf.Abs(playDistanceX) > 2.5)
                        {
                            y = 9; 
                            jumped = true;
                        }
                        stopCheck = transform.position.x;
                    }

                    if (Mathf.Abs(playDistanceY) > 2 && !jumped && isGrounded)
                    {
                        y = 9;
                        jumped = true;
                    }

                    if (Mathf.Abs(playDistanceX) > 2.5)
                    {
                        myAnim.SetBool("WALK", true);
                        if (playDistanceX < 0)
                        {
                            myBod.velocity = new Vector2(-speed, y);
                        }
                        else
                        {
                            myBod.velocity = new Vector2(speed, y);
                        }
                        atkTimer = 0;
                    }
                    else//call attack
                    {
                        if (Mathf.Abs(playDistanceY) > 2 && !jumped && isGrounded)
                        {
                            y = 9;
                            jumped = true;
                        }
                        myAnim.SetBool("WALK", false);
                        atkTimer += Time.deltaTime;
                        myBod.velocity = new Vector2(0, y);
                        myAnim.SetBool("ATK1", false);
                        myAnim.SetBool("ATK2", false);
                        if (atkTimer >= 0.75)
                        {
                            attack();
                            atkTimer = -1;
                        }
                    }
                }

                //check to track player
                playDistanceX = playTrack.transform.position.x - transform.position.x;
                playDistanceY = playTrack.transform.position.y - transform.position.y;
                //print(playDistanceX);
                if ((Mathf.Abs(playDistanceX) < 5 && Mathf.Abs(playDistanceY) < 5) && !playTrack.GetComponent<playercontroller>().getDead())
                {
                    playerTracking = true;
                }
                else if ((Mathf.Abs(playDistanceX) < 15 && Mathf.Abs(playDistanceY) < 15) && !playTrack.GetComponent<playercontroller>().getDead() && playerTracking)
                {
                    playerTracking = true;
                }
                else
                {
                    playerTracking = false;
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
                if (myAnim.GetBool("HURT") == false)
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
            myBox.size = new Vector2(0.57f, 0.83f);
            myBox.offset = new Vector2(myBox.offset.x, -0.45f);
            myBod.constraints = RigidbodyConstraints2D.FreezePosition;
            sr.material.color = new Color(1.0f, 1.0f, 1.0f, alph);
            alph -= 0.01f;
            if (alph <= 0)
            {
                playTrack.GetComponent<playercontroller>().monstersKilled++;
                Destroy(gameObject);
            }
        }
    }

    public void attack()
    {
        int num = Random.Range(0, 2);
        if (num == 1)
        {
            myAnim.SetBool("ATK1", true);

            if (sr.flipX)
            {
                GameObject g = Instantiate(atk1Left);
                g.transform.parent = transform;
            }
            else
            {
                GameObject g = Instantiate(atk1);
                g.transform.parent = transform;
            }
        }
        else
        {
            myAnim.SetBool("ATK2", true);

            if (sr.flipX)
            {
                GameObject g = Instantiate(atk2Left);
                g.transform.parent = transform;
            }
            else
            {
                GameObject g = Instantiate(atk2);
                g.transform.parent = transform;
            }
        }
    }

    public void switchBox()
    {
        if (sr.flipX)
        {
            Collider2D[] colliders = transform.GetComponents<Collider2D>();
            foreach (Collider2D c in colliders)
            {
                float cly = c.offset.y;
                c.offset = new Vector2(0f, cly);
            }
        }
        else
        {
            Collider2D[] colliders = transform.GetComponents<Collider2D>();
            foreach (Collider2D c in colliders)
            {
                float cly = c.offset.y;
                c.offset = new Vector2(0f, cly);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
