using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    Animator myAnim;
    SpriteRenderer sr;
    Rigidbody2D myBod;

    public float speed;

    private bool isGrounded = false;

    private float timer = 0;
    private bool stopped = false;
    private float atkAir;
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
        float h = Input.GetAxis("Horizontal");

        if (h > 0 && !stopped)
        {
            //run right
            sr.flipX = false;
            myAnim.SetBool("RUN", true);
        }
        else if (h < 0 && !stopped)
        {
            //run left
            sr.flipX = true;
            myAnim.SetBool("RUN", true);
        }
        else
        {
            myAnim.SetBool("RUN", false);
        }

        myAnim.SetBool("FALL", !isGrounded);

        float x = h * speed;
        float y = myBod.velocity.y;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            y = 7;
            myAnim.SetBool("JUMP", true);
        }
        else
        {
            myAnim.SetBool("JUMP", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            stopped = true;
            myAnim.SetBool("ATK2", true);
            atkAir = myBod.velocity.x;
        }
        else
        {
            myAnim.SetBool("ATK2", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            stopped = true;
            myAnim.SetBool("ATK1", true);
            atkAir = myBod.velocity.x;
        }
        else
        {
            myAnim.SetBool("ATK1", false);
        }

        if (myAnim.GetBool("HURT"))
        {
            if (sr.flipX)
            {
                Vector2 force = -4 * myBod.transform.position + 3 * myBod.transform.position;
                myBod.AddForce(force, ForceMode2D.Impulse);
            }
            else
            {
                Vector2 force = 4 * myBod.transform.position + 3 * myBod.transform.position;
                myBod.AddForce(force, ForceMode2D.Impulse);
            }
            myAnim.SetBool("HURT", false);
        }

        if (!stopped)
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
            
            timer += Time.deltaTime;

            if(timer >= 0.583)
            {
                stopped = false;
                timer = 0;
            }
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
