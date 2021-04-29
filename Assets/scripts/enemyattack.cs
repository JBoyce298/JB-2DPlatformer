using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyattack : MonoBehaviour
{
    private float timer;
    private float delayTimer;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInParent<skeletoncontroller>() != null)
        {
            if (delayTimer < 0.4)
            {
                delayTimer += Time.deltaTime;
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }

            timer += Time.deltaTime;
            if (timer >= 1)
            {
                Destroy(gameObject);
            }
        }

        if (gameObject.GetComponentInParent<MushroomController>() != null)
        {
            if (delayTimer < 0.4)
            {
                delayTimer += Time.deltaTime;
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }

            timer += Time.deltaTime;
            if (timer >= 1)
            {
                Destroy(gameObject);
            }
        }

        if (gameObject.GetComponentInParent<goblincontroller>() != null)
        {
            if (delayTimer < 0.4)
            {
                delayTimer += Time.deltaTime;
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }

            timer += Time.deltaTime;
            if (timer >= 1)
            {
                Destroy(gameObject);
            }
        }

        if (gameObject.GetComponentInParent<swordguycontroller>() != null)
        {
            if (delayTimer < 0.2)
            {
                delayTimer += Time.deltaTime;
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }

            timer += Time.deltaTime;
            if (timer >= 1)
            {
                Destroy(gameObject);
            }
        }

        if (gameObject.GetComponentInParent<wizardcontroller>() != null)
        {
            if (delayTimer < 0.2)
            {
                delayTimer += Time.deltaTime;
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }

            timer += Time.deltaTime;
            if (timer >= 4)
            {
                Destroy(gameObject);
            }
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (g.tag == "Player")
        {
            g.GetComponent<playercontroller>().isHurt = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (g.tag == "Player")
        {
            g.GetComponent<playercontroller>().isHurt = false;
        }
    }
}
