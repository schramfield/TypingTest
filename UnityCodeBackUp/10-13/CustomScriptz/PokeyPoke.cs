using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeyPoke : MonoBehaviour
{
    private GameObject LeftHandy;
    private GameObject RightHandy;
    private bool isRight = false;

    // Start is called before the first frame update
    void Start()
    {
        LeftHandy = GameObject.FindGameObjectWithTag("IndexLeft");
        Debug.Log("Left hand identified");
        RightHandy = GameObject.FindGameObjectWithTag("IndexRight");
        Debug.Log("Right hand identified");
    }

    public void poke()
    {
        float distance1 = findDistance(gameObject, LeftHandy);
        float distance2 = findDistance(gameObject, RightHandy);
        if (distance1 < distance2)
        {
            TouchIsActive.LeftTouch = true;
            Debug.Log("Left Touch pokey poke");
            isRight = false;
        }
        else
        {
            TouchIsActive.RightTouch = true;
            Debug.Log("Right Touch pokey poke");
            isRight = true;
        }
    }

    public void unpoke()
    {
        if (isRight == true)
        {
            TouchIsActive.RightTouch = false;
        }
        if(isRight == false)
        {
            TouchIsActive.LeftTouch = false;
        }
    }

    private float findDistance(GameObject poked, GameObject poker)
    {
        return Vector3.Distance(poked.transform.position, poker.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
     
        
    }
}
