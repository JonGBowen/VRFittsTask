using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionHandlerLeft : MonoBehaviour
{

    public static double increment_amplitude(double level)
    {
        return level * 0.04 + 0.08;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            transform.Translate(new Vector3(0.04f, 0, 0));
            GetComponent<Renderer>().material.color = Color.red;
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            transform.Translate(new Vector3(-0.04f, 0, 0));
            GetComponent<Renderer>().material.color = Color.red;
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {

            transform.localScale += new Vector3(-0.004F, 0, -0.004F);
            GetComponent<Renderer>().material.color = Color.red;
        }
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            transform.localScale += new Vector3(0.004F, 0, 0.004F);
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
}
