using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinkpotcontroller : MonoBehaviour
{
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
        GameObject g = Instantiate(collision.gameObject);
        g.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(0, 5), 0);
    }
}
