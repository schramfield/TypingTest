using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// PURPOSE:
// This code differentiates between left and right touches
// it could easily be expanded to work with more contact points
// for a more complex haptic device
//
// INSTRUCTIONS:
// EVERY haptic interactable object needs this script attached
// drag it onto each object as a component
// it will also be used on event triggers in the Unity Event Wrapper component (or equivolent)
// to add it as an event trigger, drag the game object from the heirarchy onto its own event trigger
// use the drop down menu to select "LVersusRDetect.engage" or "LVersusRDetect.disengage"
// this event has an engage and a disengage trigger YOU MUST USE BOTH
// i.e.  When Select () put engage, When Unselect () put disengage
//
// IMPORTANT ADDITIONAL INSTRUCTIONS:
// This function requires that UNIQUE TAGS are applied to the parts of the hand engaging haptic feedback
// Find their game objects by going in the heirarchy to...
// Player > InputOVR > Hands > LeftHand (or RightHand) > HandInteractorsLeft (or Right) > Interactors > HandPikeInteractor > HandIndexFingertip
// In the Inspector there will be a drop-down labeled "Tag"
// Drop down to "Add Tag", and make a unique tag (IndexLeft / IndexRight in my code here)
// You then have to drop down again to apply it
// Repeat this for the other hand / all haptic touch points
// use that tag in the script below
//

public class LVersusRDetect : MonoBehaviour
{
    // These need to be private because more than one instance of this script will be running at once
    //
    private GameObject LeftHand;
    private GameObject RightHand;
    private bool isRight = false;

    // Start is called before the first frame update
    void Start()
    {
        // Assign the relevent game objects to your variables using the unique tags you gave them
        // Uncomment the debugger if you need to, but it runs for every single key so it can be cluttering
        // Because these variables are private the debug logger is only in the console, not in the data collection txt file
        //
        LeftHand = GameObject.FindGameObjectWithTag("IndexLeft");
        //Debug.Log("Left hand identified");
        RightHand = GameObject.FindGameObjectWithTag("IndexRight");
        //Debug.Log("Right hand identified");
    }

    public void engage()
    {
        // This is a very simple principle: compare the locations of all possible triggers and assume its the closest
        // findDistance is defined within this script
        //
        float distance1 = findDistance(gameObject, LeftHand);
        float distance2 = findDistance(gameObject, RightHand);
        
        // If left is closer, it's not the right hand
        // adjust the static variables from TouchIsActive script
        //
        if (distance1 < distance2)
        {
            TouchIsActive.LeftTouch = true;
            isRight = false;
        }

        // if right is closer it is the right hand
        // adjust the static variables from TouchIsActive script
        //
        else
        {
            TouchIsActive.RightTouch = true;
            isRight = true;
        }
    }

    public void disengage()
    {
        // it's important here to only turn off the one you just turned on!
        // more than one touch could be happening at a time and you don't want to mess up the other hand
        //
        if (isRight == true)
        {
            TouchIsActive.RightTouch = false;
        }
        if(isRight == false)
        {
            TouchIsActive.LeftTouch = false;
        }
    }

    // Because videogames are all about stuff moving in space
    // there's a lot of built-in ways to get that data
    //
    private float findDistance(GameObject hapticObject, GameObject hapticUser)
    {
        return Vector3.Distance(hapticObject.transform.position, hapticUser.transform.position);
    }

    // no Update required
    //
}
// End of Script
//