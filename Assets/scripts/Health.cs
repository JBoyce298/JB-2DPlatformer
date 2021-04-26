using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    playercontroller player;
    //public GameObject heart;
    int maxHearts;
    int health;
    public GameObject[] hearts;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<playercontroller>();
        maxHearts = player.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = player.health;
        maxHearts = player.maxHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].transform.Find("Heart").gameObject.SetActive(true);
            }
            else
            {
                hearts[i].transform.Find("Heart").gameObject.SetActive(false);
            }

            if(i < maxHearts)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}
