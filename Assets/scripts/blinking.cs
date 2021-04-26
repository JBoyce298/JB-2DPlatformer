using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class blinking : MonoBehaviour
{
    float timer = 0;
    TilemapCollider2D col;
    TilemapRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<TilemapCollider2D>();
        rend = gameObject.GetComponent<TilemapRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 3)
        {
            col.enabled = !col.enabled;
            rend.enabled = !rend.enabled;
            timer = 0;
        }
    }
}
