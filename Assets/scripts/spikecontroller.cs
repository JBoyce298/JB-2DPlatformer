using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikecontroller : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (g.tag == "Player")
        {
            g.GetComponent<playercontroller>().kill();
        }
    }
}
