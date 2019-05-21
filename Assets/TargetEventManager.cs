using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEventManager : MonoBehaviour
{
    public string targetID;
    public AudioClip tapSound;
    public OVRHapticsClip clip;
    public string lastTappedTarget;
    // Start is called before the first frame update
    void Start()
    {
        clip = new OVRHapticsClip(tapSound);
    }
    
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("enter trigger " + col.gameObject.name);
        if (col.gameObject.name == "hands:b_r_index3" && lastTappedTarget != "right") 
        {
            GetComponent<Renderer>().material.color = Color.red;
            GetComponent<AudioSource>().PlayOneShot(tapSound);
            //OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            OVRHaptics.RightChannel.Preempt(clip);
            //lastTappedTarget = "right";
        }
        if (col.gameObject.name == "hands:b_l_index3" && lastTappedTarget != "left")
        {
            GetComponent<Renderer>().material.color = Color.red;
            GetComponent<AudioSource>().PlayOneShot(tapSound);
            //OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
            OVRHaptics.LeftChannel.Preempt(clip);
            //lastTappedTarget = "left";

            }
    }

    private void OnTriggerExit(Collider col)
    {
        Debug.Log("exit trigger " + col.gameObject.name);
        if (col.gameObject.name == "hands:b_l_index3" || col.gameObject.name == "hands:b_r_index3")
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
