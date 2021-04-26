using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class platformcontroller : MonoBehaviour
{
    GameObject player;
    TilemapCollider2D col;

    public bool playerThrough = true;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        col = GetComponent<TilemapCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerThrough)
        {
            if (player.transform.position.y > transform.position.y + 0.5)
            {
                col.enabled = true;
            }
            else
            {
                col.enabled = false;
            }
        }
        else
        {
            col.enabled = false;

            timer += Time.deltaTime;
            if(timer >= 0.6)
            {
                playerThrough = false;
                timer = 0;
                col.enabled = true;
            }
        }
    }
}
