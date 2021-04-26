using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinkpotcontroller : MonoBehaviour
{
    bool given = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!given)
            {
                collision.gameObject.GetComponent<playercontroller>().maxHealth++;
                Destroy(gameObject);
                given = true;
            }
        }
    }
}
