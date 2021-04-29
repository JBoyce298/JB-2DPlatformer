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

            if (gameObject.GetComponentInParent<MushroomController>() != null)
            {
                gameObject.GetComponentInParent<MushroomController>().hurt = true;
            }

            if (gameObject.GetComponentInParent<goblincontroller>() != null)
            {
                gameObject.GetComponentInParent<goblincontroller>().hurt = true;
            }

            if (gameObject.GetComponentInParent<swordguycontroller>() != null)
            {
                gameObject.GetComponentInParent<swordguycontroller>().hurt = true;
            }

            if (gameObject.GetComponentInParent<wizardcontroller>() != null)
            {
                gameObject.GetComponentInParent<wizardcontroller>().hurt = true;
            }
        }
        else
        {
            if (gameObject.GetComponentInParent<skeletoncontroller>() != null)
            {
                gameObject.GetComponentInParent<skeletoncontroller>().hurt = false;
            }

            if (gameObject.GetComponentInParent<MushroomController>() != null)
            {
                gameObject.GetComponentInParent<MushroomController>().hurt = false;
            }

            if (gameObject.GetComponentInParent<goblincontroller>() != null)
            {
                gameObject.GetComponentInParent<goblincontroller>().hurt = false;
            }

            if (gameObject.GetComponentInParent<swordguycontroller>() != null)
            {
                gameObject.GetComponentInParent<swordguycontroller>().hurt = false;
            }

            if (gameObject.GetComponentInParent<wizardcontroller>() != null)
            {
                gameObject.GetComponentInParent<wizardcontroller>().hurt = false;
            }
        }
    }
}
