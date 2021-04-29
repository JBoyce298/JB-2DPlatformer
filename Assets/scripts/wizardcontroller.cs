using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wizardcontroller : MonoBehaviour
{
    Animator myAnim;
    SpriteRenderer sr;
    Rigidbody2D myBod;
    GameObject playTrack;

    public GameObject atk1;
    public GameObject atk1Left;

    public GameObject skele;
    public GameObject mush;
    public GameObject gob;
    public GameObject swordBro;

    public float health = 20;
    public RectTransform healthbar;
    public Text healthText;
    public bool hurt;
    public float speed = 3;
    public GameObject winZone;

    private float teleTimer = 0;
    private bool teleported;
    private bool summon = false;
    private int summonCount = 0;
    private float summonDelay = 0;
    private int summontotal = 0;
    float delaytotal = 0;

    private bool atkdelay = false;
    private float stopTimer = 0;
    private bool hitDelay = false;
    private float tempx = 0f;
    private float tempy = 0f;

    private float playDistanceX;

    private float atkTimer = 0;
    private bool dead = false;
    private float winTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        myBod = GetComponent<Rigidbody2D>();
        playTrack = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playDistanceX = playTrack.transform.position.x - transform.position.x;
        if (!atkdelay)
        {
            if (playDistanceX < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }

        if (Mathf.Abs(playDistanceX) > 2.5 && !hitDelay && !teleported && !atkdelay && !summon)
        {
            myAnim.SetBool("WALK", true);
            myAnim.SetBool("FIRE", false);
            if (playDistanceX < 0)
            {
                myBod.velocity = new Vector2(-speed, myBod.velocity.y);
            }
            else
            {
                myBod.velocity = new Vector2(speed, myBod.velocity.y);
            }
        }
        else if(playDistanceX <= 2.5 && !hitDelay && !atkdelay && !teleported && !summon)
        {
            myAnim.SetBool("FIRE", false);
            myAnim.SetBool("WALK", false);
            myBod.velocity = new Vector2(0, 0);
            atkdelay = true;
            tempx = transform.position.x;
            tempy = transform.position.y;
            attack();
        }

        if(atkdelay && !hitDelay)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer >= 6)
            {
                atkdelay = false;
                atkTimer = 0;
            }
            if(atkTimer >= 4)
            {
                myAnim.SetBool("FIRE", false);
            }
            transform.position = new Vector2(tempx, tempy);
        }

        if (hitDelay)
        {
            atkdelay = false;
            stopTimer += Time.deltaTime;
            myAnim.SetBool("FIRE", false);
            myAnim.SetBool("TELE", true);
            if(stopTimer >= 1)
            {
                transform.position = new Vector2(18f, 3f);
                stopTimer = 0;
                teleported = true;
                hitDelay = false;
                myBod.gravityScale = 0;
                summonCount++;
                speed++;
            }
        }

        if(teleported)
        {
            teleTimer += Time.deltaTime;
            if(teleTimer >= 1)
            {
                myAnim.SetBool("TELE", false);
            }
            if(teleTimer >= 2)
            {
                myAnim.SetBool("SUMMON", true);
                teleTimer = 0;
                teleported = false;
                summon = true;
            }
        }

        if(summon)
        {
            summonDelay += Time.deltaTime;
            if(summonCount == 1)
            {           
                delaytotal = 4;
                if(summonDelay >= delaytotal)
                {
                    summonDelay = 0;
                    summontotal++;
                    int num = Random.Range(0, 2);
                    int monster = Random.Range(0, 3);
                    float x = playTrack.transform.position.x;
                    if(num == 1)
                    {
                        x -= 3;
                    }
                    else
                    {
                        x += 3;
                    }

                    if(monster == 1)
                    {
                        Instantiate(skele, new Vector2(x, playTrack.transform.position.y + 4), Quaternion.identity);
                    }
                    else if(monster == 2)
                    {
                        Instantiate(gob, new Vector2(x, playTrack.transform.position.y + 4), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(mush, new Vector2(x, playTrack.transform.position.y + 4), Quaternion.identity);
                    }
                }
                if(summontotal >= 5)
                {
                    summon = false;
                    summonDelay = 0;
                    myAnim.SetBool("SUMMON", false);
                    myBod.gravityScale = 1;
                    summontotal = 0;
                }
            }
            else
            {
                delaytotal = 2;
                if (summonDelay >= delaytotal)
                {
                    summonDelay = 0;
                    summontotal++;
                    int num = Random.Range(0, 2);
                    int monster = Random.Range(0, 4);
                    float x = playTrack.transform.position.x;
                    if (num == 1)
                    {
                        x -= 3;
                    }
                    else
                    {
                        x += 3;
                    }

                    if (monster == 1)
                    {
                        Instantiate(skele, new Vector2(x, playTrack.transform.position.y + 4), Quaternion.identity);
                    }
                    else if (monster == 2)
                    {
                        Instantiate(gob, new Vector2(x, playTrack.transform.position.y + 4), Quaternion.identity);
                    }
                    else if (monster == 3)
                    {
                        Instantiate(mush, new Vector2(x, playTrack.transform.position.y + 4), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(swordBro, new Vector2(x, playTrack.transform.position.y + 4), Quaternion.identity);
                    }
                }
                if (summontotal >= 7)
                {
                    summon = false;
                    summonDelay = 0;
                    myAnim.SetBool("SUMMON", false);
                    myBod.gravityScale = 1;
                }
            }
        }

        if (hurt && !dead && !hitDelay && !teleported && !summon)
        {
            if (myAnim.GetBool("HURT") == false)
            {
                health--;
                if (health == 14 || health == 7)
                {
                    hitDelay = true;
                }
            }

            myAnim.SetBool("HURT", true);
        }
        else
        {
            myAnim.SetBool("HURT", false);
        }

        if(!dead)
        {
            healthbar.localScale = new Vector2(health / 20, 1);
            healthText.text = health + "/20";
        }
        
        if(health <= 0)
        {
            dead = true;
            myAnim.SetBool("DIE", true);

            winTimer += Time.deltaTime;
            if(winTimer >= 3)
            {
                winZone.SetActive(true);
            } 
        }
    }

    public void attack()
    {
        myAnim.SetBool("FIRE", true);

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
}
