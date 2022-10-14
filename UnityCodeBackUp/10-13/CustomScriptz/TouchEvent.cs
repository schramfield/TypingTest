using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;

public class TouchEvent : MonoBehaviour
{
    public UnityEvent proximityEvent;
    public UnityEvent contactEvent;
    public UnityEvent actionEvent;
    public UnityEvent defaultEvent;
    public GameObject Lhandy;
    public GameObject Rhandy;

    private string finger;
    private bool right;


    //Start is called before the first frame update
    void Start()
    {
        Debug.Log("porcupine");
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
        Debug.Log("cabbage TouchEvent started");
        //  stim = GameObject.FindGameObjectWithTag("stim");
        // stimulator = stim.GetComponent<Stimulation>();
    }

    void InitiateEvent(InteractableStateArgs state)
    {
        Debug.Log("Initate Event is rolling, beetroot");
        finger = state.Tool.tag;
        right = state.Tool.IsRightHandedTool;
        if (state.NewInteractableState == InteractableState.ProximityState)
        {
            proximityEvent.Invoke();
            Debug.Log("Proximity state invoked");
            //stimulator.StopStimulation(finger, right);
        }
        else if (state.NewInteractableState == InteractableState.ContactState)
        {
            contactEvent.Invoke();
            if (right == true)
            {
                TouchIsActive.RightTouch = true;
                Debug.Log("Right Touch logged");
            }
            else
            {
                TouchIsActive.LeftTouch = true;
                Debug.Log("Left Touch logged");
            }
            //stimulator.StartStimulation100(finger, right);
        }
        else if (state.NewInteractableState == InteractableState.ActionState)
        {
            actionEvent.Invoke();
            Debug.Log("Action event invoked");
        }
        else
        {
            defaultEvent.Invoke();
            TouchIsActive.RightTouch = false;
            TouchIsActive.LeftTouch = false;
            Debug.Log("Touch Disengaged/ default invoked");
            // stimulator.StopStimulation(finger, right);
        }
    }
}
