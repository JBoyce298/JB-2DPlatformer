using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = GameObject.Find("Player").transform;
        gameObject.transform.position = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.55)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if(g.tag == "Enemy")
        {
            g.GetComponent<enemyhurt>().hurt = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (g.tag == "Enemy")
        {
            g.GetComponent<enemyhurt>().hurt = false;
        }
    }
}
