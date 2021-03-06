using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    Animator myAnim;
    SpriteRenderer sr;
    Rigidbody2D myBod;
    public GameObject atk1;
    public GameObject atk2;

    public float speed;

    private bool isGrounded = false;

    private float atktimer = 0;
    public bool attacking = false;
    private float atkAir;

    public bool isHurt = false;
    private float hurtTime = 0;

    public int health = 5;
    private bool dead;
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
        //movement
        float h;
        if (!dead)
        {
            h = Input.GetAxis("Horizontal");
        }
        else
        {
            h = 0;
        }
        
        if (h > 0 && !attacking)
        {
            //run right
            sr.flipX = false;
            myAnim.SetBool("RUN", true);
        }
        else if (h < 0 && !attacking)
        {
            //run left
            sr.flipX = true;
            myAnim.SetBool("RUN", true);
        }
        else
        {
            myAnim.SetBool("RUN", false);
        }

        float x = h * speed;
        float y = myBod.velocity.y;

        //jumping
        myAnim.SetBool("FALL", !isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded && !dead)
        {
            y = 7;
            myAnim.SetBool("JUMP", true);
        }
        else
        {
            myAnim.SetBool("JUMP", false);
        }

        if(y == 0)
        {
            myAnim.SetBool("FALL", false);
            isGrounded = true;
        }

        //attack1
        if (Input.GetMouseButtonDown(0) &&  !dead)
        {
            attacking = true;
            myAnim.SetBool("ATK2", true);
            atkAir = myBod.velocity.x;
            Instantiate(atk2);
        }
        else
        {
            myAnim.SetBool("ATK2", false);
        }

        //attack2
        if (Input.GetMouseButtonDown(1) && !dead)
        {
            attacking = true;
            myAnim.SetBool("ATK1", true);
            atkAir = myBod.velocity.x;
            Instantiate(atk1);
        }
        else
        {
            myAnim.SetBool("ATK1", false);
            
        }

        //keeps momentum if attacking in the air, also prevents attacking too many times in a row
        if (!attacking)
        {
            myBod.velocity = new Vector2(x, y);
        }
        else
        {
            if(!isGrounded)
            {
                myBod.velocity = new Vector2(atkAir, y);
            }
            else
            {
                myBod.velocity = new Vector3(0, y);
            }
            
            atktimer += Time.deltaTime;

            if(atktimer >= 0.6)
            {
                attacking = false;
                atktimer = 0;
            }
        }

        //getting hit
        if (isHurt && hurtTime > 1.5)
        {
            health--;
            myAnim.SetBool("HURT", true);
            hurtTime = 0;
        }
        else
        {
            myAnim.SetBool("HURT", false);
            hurtTime += Time.deltaTime;
        }

        //dying
        if(health <= 0)
        {
            myAnim.SetBool("DIE", true);
            dead = true;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" )
        {
            isGrounded = false;
        }
    }
}
