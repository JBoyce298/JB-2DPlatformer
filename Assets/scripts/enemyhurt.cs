using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhurt : MonoBehaviour
{
    public bool hurt = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hurt)
        {
            if(gameObject.GetComponentInParent<skeletoncontroller>() != null)
            {
                gameObject.GetComponentInParent<skeletoncontroller>().hurt = true;
            }
        }
        else
        {
            if (gameObject.GetComponentInParent<skeletoncontroller>() != null)
            {
                gameObject.GetComponentInParent<skeletoncontroller>().hurt = false;
            }
        }
    }
}
