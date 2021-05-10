using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    Animator myAnim;
    SpriteRenderer sr;
    Rigidbody2D myBod;
    public GameObject atk1;
    public GameObject atk2;
    public GameObject atk1Left;
    public GameObject atk2Left;
    Collider2D col;

    bool onPlatform = false;

    public float speed;

    private bool isGrounded = false;

    private float atktimer = 0;
    public bool attacking = false;
    private float atkAir;
    private float atkDelay = 0.6f;

    public bool isHurt = false;
    private float hurtTime = 0;

    public int health = 0;
    public int maxHealth;
    private bool dead;

    private string h;

    public int monstersKilled = 0;
    private bool deathreport;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        myBod = GetComponent<Rigidbody2D>();
        string levelname = SceneManager.GetActiveScene().name;

        /*GameObject.Find("FirebaseHandler").GetComponent<test>().getMaxHealth();
        maxHealth = GameObject.Find("FirebaseHandler").GetComponent<test>().maxHealth;*/
        health = maxHealth;

        levelname = SceneManager.GetActiveScene().name;
        string level;
        if (levelname.Substring(levelname.Length - 1) == "0")
        {
            level = "level10";
        }
        else
        {
            level = "level" + levelname.Substring(levelname.Length - 1);
        }
        GameObject.Find("FirebaseHandler").GetComponent<test>().reportNothingFromPlayer(level);
    }

    // Update is called once per frame
    void Update()
    {
        if (onPlatform)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                col.GetComponent<platformcontroller>().playerThrough = true;
            }
        }

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
        
        if (h > 0 && !attacking && Time.timeScale != 0)
        {
            //run right
            sr.flipX = false;
            myAnim.SetBool("RUN", true);
            Collider2D[] colliders = transform.GetComponents<Collider2D>();
            foreach (Collider2D c in colliders)
            {
                float cly = c.offset.y;
                c.offset = new Vector2(0.17f, cly);
            }
        }
        else if (h < 0 && !attacking && Time.timeScale != 0)
        {
            //run left
            sr.flipX = true;
            myAnim.SetBool("RUN", true);
            Collider2D[] colliders = transform.GetComponents<Collider2D>();
            foreach(Collider2D c in colliders)
            {
                float cly = c.offset.y;
                c.offset = new Vector2(-0.17f, cly);
            }
        }
        else
        {
            myAnim.SetBool("RUN", false);

        }

        float x = h * speed;
        float y = myBod.velocity.y;

        //jumping
        myAnim.SetBool("FALL", !isGrounded);

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded && !dead && Time.timeScale != 0)
        {
            y = 9;
            myAnim.SetBool("JUMP", true);
        }
        else
        {
            myAnim.SetBool("JUMP", false);
        }

        myAnim.SetBool("FALL", !isGrounded);

        if (y == 0 || isGrounded)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //attack1
        if ((Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)) &&  !dead && Time.timeScale != 0 && atkDelay >= 0.7)
        {
            atkDelay = 0;
            attacking = true;
            myAnim.SetBool("ATK2", true);
            atkAir = myBod.velocity.x;
            if(sr.flipX)
            {
                Instantiate(atk2Left);
            }
            else
            {
                Instantiate(atk2);
            }
        }
        else
        {
            myAnim.SetBool("ATK2", false);
        }

        //attack2
        if ((Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(1)) && !dead && Time.timeScale != 0 && atkDelay >= 0.6)
        {
            atkDelay = 0;
            attacking = true;
            myAnim.SetBool("ATK1", true);
            atkAir = myBod.velocity.x;
            if (sr.flipX)
            {
                Instantiate(atk1Left);
            }
            else
            {
                Instantiate(atk1);
            }
        }
        else
        {
            myAnim.SetBool("ATK1", false);
            
        }
        atkDelay += Time.deltaTime;
        //keeps momentum if attacking in the air, also prevents attacking too many times in a row
        if (!attacking)
        {
            if(h != 0)
            {
                myBod.velocity = new Vector2(x, y);
            }
            else
            {
                myBod.velocity = new Vector2(0, y);
            }
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
        if (isHurt && hurtTime > 1.5 && Time.timeScale != 0 && !dead)
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

        if(dead)
        {
            string levelname = SceneManager.GetActiveScene().name;
            string level;
            if (levelname.Substring(levelname.Length - 1) == "0")
            {
                level = "level10";
            }
            else
            {
                level = "level" + levelname.Substring(levelname.Length - 1);
            }

            if (!deathreport)
            {
                GameObject.Find("FirebaseHandler").GetComponent<test>().reportDeathFromPlayer(level, monstersKilled);
                deathreport = true;
            }
        }
    }

    public void kill()
    {
        health = 0;
        dead = true;
    }

    public bool getDead()
    {
        if(dead)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        if (collision.gameObject.tag == "Platform")
        {
            col = collision;
            onPlatform = true;
            if(Vector2.down.y > 0)
            {
                myBod.velocity = new Vector2(myBod.velocity.x, 0);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" )
        {
            isGrounded = false;
        }

        if (collision.gameObject.tag == "Platform")
        {
            col = null;
            onPlatform = false;
            isGrounded = false;
        }
    }
}
